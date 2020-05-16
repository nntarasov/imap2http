using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ImapProtocol.Contracts;
using ImapProtocol.Tools;

namespace ImapProtocol.ImapStateControllers
{
    public class ImapFetchStateController : ImapStateController
    {
        public override ImapState State { get; } = ImapState.Fetch;

        public enum MessageSequenceType
        {
            None = 0,
            Exact = 1,
            Range = 2
        }
        
        public readonly static (MessageSequenceType, long[]) BadSequence = (MessageSequenceType.None, new long[0]);
        public static (MessageSequenceType, long[]) GetMessageSequenceSet(string arg)
        {
            
            if (string.IsNullOrWhiteSpace(arg))
            {
                return BadSequence;
            }

            if (arg.Contains(':'))
            {
                if (arg.Contains('*'))
                {
                    var match = Regex.Match(arg, @"(\d+):\*");
                    long from;
                    if (!match.Success || !long.TryParse(match.Groups[1].Value, out from))
                    {
                        return BadSequence;
                    }

                    return (MessageSequenceType.Range, new[] {from, long.MaxValue});
                }
                else
                {
                    var match = Regex.Match(arg, @"(\d+):(\d+)");
                    long from, to;
                    if (!match.Success || 
                        !long.TryParse(match.Groups[1].Value, out from) ||
                        !long.TryParse(match.Groups[2].Value, out to))
                    {
                        return BadSequence;
                    }
                    return (MessageSequenceType.Range, new[] {from, to});
                }
            }

            var values = arg.Split(',').Select(s => s.Trim());
            var parsedValues = values
                .Select(v => (long.TryParse(v, out var result), result))
                .ToList();
            if (parsedValues.Any(p => !p.Item1))
            {
                return BadSequence;
            }

            return (MessageSequenceType.Exact, parsedValues.Select(p => p.result).ToArray());
        }

        private IEnumerable<long> ExtractRealMessageIds((MessageSequenceType, long[]) range)
        {
            var existMessageIds = Messages; // TODO
            
            switch (range.Item1)
            {
                case MessageSequenceType.None:
                    return new long[0];
                case MessageSequenceType.Exact:
                    return range.Item2.Where(i => existMessageIds.ContainsKey(i));
                case MessageSequenceType.Range:
                    if (Context.States.Any(s => s.State == ImapState.Uid))
                    {
                        return existMessageIds.Keys.Where(k => k >= range.Item2[0] && k <= range.Item2[1]);
                    }
                    return existMessageIds.Keys
                        .OrderByDescending(k => k)
                        .Skip((int) range.Item2[0] - 1)
                        .Take((int) range.Item2[1] - (int) range.Item2[0] + 1)
                        .ToList();
                default:
                        throw new ArgumentException(nameof(range));
            }
        }

        public class FetchData
        {
            public List<string> HeaderFields { get; set; }
            public List<string> HeaderFieldsNot { get; set; }
            public bool IncludeMime { get; set; }
            public bool IncludeText { get; set; }
            public bool IncludeHeaders { get; set; }
            public long? SizeFrom { get; set; }
            public long? Size { get; set; }
            public bool Peek { get; set; }
            public bool Rfc2822 { get; set; }
            public bool IncludeBodystructure { get; set; }
            public bool IncludeEnvelope { get; set; }
            public bool IncludeFlags { get; set; }
            public bool IncludeInternaldate { get; set; }
            public bool IncludeRfc822Header { get; set; }
            public bool IncludeRfc822Size { get; set; }
            public bool IncludeRfc822Text { get; set; }
            public bool IncludeUid { get; set; }
        }

        private FetchData ParseDataItems(string dataItemsString)
        {
            if (dataItemsString == "ALL")
            {
                dataItemsString = "FLAGS INTERNALDATE RFC822.SIZE ENVELOPE";
            } 
            else if (dataItemsString == "FULL")
            {
                dataItemsString = "FLAGS INTERNALDATE RFC822.SIZE ENVELOPE BODY";
            } 
            else if (dataItemsString == "FAST")
            {
                dataItemsString = "FLAGS INTERNALDATE RFC822.SIZE";
            }

            var data = new FetchData
            {
                HeaderFields = new List<string>(),
                HeaderFieldsNot = new List<string>(),
                IncludeMime = false,
                IncludeText = false,
                IncludeHeaders = false,
                SizeFrom = null,
                Size = null,
                Peek = false,
                Rfc2822 = false,
                IncludeBodystructure = dataItemsString.Contains("BODYSTRUCTURE"),
                IncludeEnvelope = dataItemsString.Contains("ENVELOPE"),
                IncludeFlags = dataItemsString.Contains("FLAGS"),
                IncludeInternaldate = dataItemsString.Contains("INTERNALDATE"),
                IncludeRfc822Header = dataItemsString.Contains("RFC822.HEADER"),
                IncludeRfc822Size = dataItemsString.Contains("RFC822.SIZE"),
                IncludeRfc822Text = dataItemsString.Contains("RFC822.TEXT"),
                IncludeUid = dataItemsString.Contains("UID")
            };


            if (Regex.IsMatch(dataItemsString, @"RFC-2822[^.]"))
            {
                data.Rfc2822 = true;
                if (!dataItemsString.Contains("BODY"))
                {
                    Regex.Replace(dataItemsString, "RFC-2822[^.]", "BODY[]");
                }
            }

            // BODY
            var bodyMatch = Regex.Match(dataItemsString, 
                @"BODY(?<peek>\.PEEK){0,1}(\[(?<section>[^]]*)\](?<partial>[\w.]*))*(<(?<szFrom>\d+)\.(?<sz>\d+)>)*");
            if (bodyMatch.Success)
            {
                data.Peek = bodyMatch.Groups["peek"].Success;
                if (bodyMatch.Groups["szFrom"].Success)
                {
                    long.TryParse(bodyMatch.Groups["szFrom"].Value, out var sizeFrom);
                    data.SizeFrom = sizeFrom;
                }

                if (bodyMatch.Groups["sz"].Success)
                {
                    long.TryParse(bodyMatch.Groups["sz"].Value, out var size);
                    data.Size = size;
                }

                var partsString = bodyMatch.Groups["section"]?.Value;
                string[] parts = new string[0];
                if (!string.IsNullOrEmpty(partsString))
                {
                    parts = partsString.Split(',').Select(s => s.Trim()).ToArray();

                    foreach (var part in parts)
                    {
                        if (part.StartsWith("HEADER.FIELDS"))
                        {
                            var headerFieldsMatch = Regex.Match(part,
                                @"HEADER\.FIELDS[ ]*\((?<fields>[^)]*)\)");
                            if (!headerFieldsMatch.Success)
                            {
                                continue;
                            }

                            string fields = headerFieldsMatch.Groups["fields"].Value;
                            data.HeaderFields.AddRange(fields
                                .Split(' ')
                                .Where(f => !string.IsNullOrWhiteSpace(f))
                                .Select(f => f.ToUpper().Trim('"')));
                        }
                        else if (part.StartsWith("HEADER.FIELDS.NOT"))
                        {
                            var headerFieldsMatch = Regex.Match(part,
                                @"HEADER\.FIELDS.NOT[ ]*\((?<fields>[^)]*)\)");
                            if (!headerFieldsMatch.Success)
                            {
                                continue;
                            }

                            string fields = headerFieldsMatch.Groups["fields"].Value;
                            data.HeaderFieldsNot.AddRange(fields
                                .Split(' ')
                                .Where(f => !string.IsNullOrWhiteSpace(f))
                                .Select(f => f.ToUpper().Trim('"')));
                        }
                        else if (part == "MIME")
                        {
                            data.IncludeMime = true;
                        }
                        else if (part == "TEXT")
                        {
                            data.IncludeText = true;
                        }
                        else if (part == "HEADER")
                        {
                            data.IncludeHeaders = true;
                        }
                    }
                }
                else
                {
                    data.Rfc2822 = true;
                }
                
                var logger = LoggerFactory.GetLogger();
                logger.Print($"includeBodystructure: {data.IncludeBodystructure}");
                logger.Print($"IncludeEnvelope: {data.IncludeEnvelope}");
                logger.Print($"IncludeFlags: {data.IncludeFlags}");
                logger.Print($"IncludeInternaldate: {data.IncludeInternaldate}");
                logger.Print($"IncludeRfc822Header: {data.IncludeRfc822Header}");
                logger.Print($"IncludeRfc822Size: {data.IncludeRfc822Size}");
                logger.Print($"IncludeRfc822Text: {data.IncludeRfc822Text}");
                logger.Print($"IncludeUid: {data.IncludeUid}");
                
                logger.Print($"IncludeMime: {data.IncludeMime}");
                logger.Print($"IncludeText: {data.IncludeText}");
                logger.Print($"IncludeHeaders: {data.IncludeHeaders}");
                logger.Print($"szFrom: {data.SizeFrom}");
                logger.Print($"sz: {data.Size}");
                logger.Print($"peek: {data.Peek}");
                logger.Print($"rfc2822: {data.Rfc2822}");
                
                if (data.HeaderFields?.Any() ?? false)
                    logger.Print($"headerFields: {data.HeaderFields.Aggregate((a, b) => a + " " + b)}");
                if (data.HeaderFieldsNot?.Any() ?? false)
                    logger.Print($"headerFieldsNot: {data.HeaderFieldsNot.Aggregate((a, b) => a + " " + b)}");
            }
            return data;
        }
        
        protected override bool RunInternal(ImapCommand cmd)
        {
            var args = cmd.Args.Split(' ');
            if (args.Length < 1)
            {
                Context.CommandProvider.Write($"{cmd.Tag} BAD\r\n");
            }
            var range = GetMessageSequenceSet(args[0]);

            if (range.Item1 == MessageSequenceType.None)
            {
                Context.CommandProvider.Write($"{cmd.Tag} BAD\r\n");
            }

            var dataItemsString = string.Join(' ', args.Skip(1));
            var messageIds = ExtractRealMessageIds(range).ToList();
            if (!messageIds.Any())
            {
                Context.CommandProvider.Write($"{cmd.Tag} NO\r\n");
            }
            foreach (var messageId in messageIds)
            {
                var fetchData = ParseDataItems(dataItemsString);
                if (!FetchMessage(messageId, fetchData))
                {
                    Context.CommandProvider.Write($"{cmd.Tag} BAD");
                    return false;
                }
            }

            if (Context.States.Any(data => data.State == ImapState.Uid))
            {
                Context.CommandProvider.Write($"{cmd.Tag} OK UID FETCH\r\n");
            }
            else
            {
                Context.CommandProvider.Write($"{cmd.Tag} OK FETCH\r\n");
            }

            return true;
        }

        private bool FetchMessage(long messageId, FetchData fetchData)
        {
            /* 3 FETCH (BODY[HEADER,FIELDS ("DATE" "FROM" "SUBJECT")] {112}
            18 Date: Tue, 14 Sep 1999 10:09:50 -0500
            19 From: alex@shadrach.smallorg.org
            20 Subject: This is the first test message
            21*/
            //ParseDataItems(cmd.Args);

            var message = Messages[messageId];
            var messageDate = MessageDts.First();
            var messageData = Rfc2822Parser.Parse(message);

            var messageBuilder = new StringBuilder($"* {messageId} FETCH (");

            if (fetchData.IncludeFlags)
            {
                messageBuilder.Append("FLAGS () ");
            }

            if (fetchData.IncludeInternaldate)
            {
                messageBuilder.Append($"INTERNALDATE \"{messageDate.ToImapString()}\" ");
            }

            if (fetchData.IncludeRfc822Size)
            {
                messageBuilder.Append($"RFC822.SIZE {message.Length} ");
            }
            
                        
            if (fetchData.IncludeUid || Context.States.Any(data => data.State == ImapState.Uid))
            {
                messageBuilder.Append($"UID {messageId} ");
            }

            if (fetchData.IncludeBodystructure)
            {
                //("TEXT" "PLAIN" ("CHARSET"
                //"UTF-8") NIL NIL "7BIT" 2279 48)
                var octetCount = messageData.BodyString.Length;
                var linesCount = messageData.BodyString.Split('\n').Length;
                messageBuilder.Append($"BODYSTRUCTURE (\"TEXT\" \"HTML\" (\"CHARSET\" \"UTF-8\") NIL NIL \"QUOTED-PRINTABLE\" {octetCount} {linesCount}) ");
                //var mm =
                //    "TEXT (<div>Test mail</div><div> </div><div>-- </div><div>С уважением,<br />Тарасов Никита</div><div> </div>) MESSAGE/RFC822";

            }
            
            if (fetchData.IncludeText)
            {
                var octetCount = messageData.BodyString.Length;

                bool isPart = false;
                bool toEnd = false;
                long szTo = 0;
                long szToResponse = 0;
                if (fetchData.SizeFrom.HasValue && fetchData.Size.HasValue)
                {
                    isPart = true;
                    szTo = fetchData.SizeFrom.Value + fetchData.Size.Value;
                    if (szTo >= octetCount)
                    {
                        toEnd = true;
                    }
                    szToResponse = toEnd ? octetCount - fetchData.SizeFrom.Value : fetchData.Size.Value;
                }
                string contents;
                if (isPart)
                {
                    if (fetchData.SizeFrom >= octetCount)
                    {
                        contents = string.Empty;
                    }
                    else if (toEnd)
                    {
                        contents = messageData.BodyString.Substring((int) fetchData.SizeFrom.Value);
                    }
                    else
                    {
                        contents = messageData.BodyString.Substring((int) fetchData.SizeFrom.Value, (int) fetchData.Size);
                    }
                }
                else
                {
                    contents = messageData.BodyString;
                } 
                
                string partSize = isPart ? $"{fetchData.SizeFrom}.{szToResponse}"  : "0";
                messageBuilder.Append($"BODY[TEXT]<{partSize}> {{{contents.Length}}}\r\n");
                messageBuilder.Append(contents);
                messageBuilder.Append("\r\n");
            }
            
            if (fetchData.IncludeHeaders || fetchData.IncludeRfc822Header || fetchData.HeaderFields.Any())
            {
                if (fetchData.HeaderFields.Any())
                {
                    messageBuilder.Append(
                        $"BODY[HEADER.FIELDS ({fetchData.HeaderFields.Select(f => '"' + f.ToLower() + '"').Aggregate((a, b) => a + " " + b)})] ");
                }
                else
                {
                    messageBuilder.Append($"BODY[HEADER] ");
                }
                var headerBuilder = new StringBuilder();
                foreach (var header in  messageData.Headers)
                {
                    if (!fetchData.HeaderFieldsNot.Contains(header.Key) && 
                        (!fetchData.HeaderFields.Any() || fetchData.HeaderFields.Contains(header.Key)))
                    {
                        headerBuilder.Append(header.Key.ToUpper()[0] +  header.Key.Substring(1).ToLower() + ": " + header.Value.Trim('\n').Trim('\r').Trim() + "\n");
                    }
                }
  
                //headerBuilder.Append("\n");
  
                messageBuilder.Append("{" + headerBuilder.Length + "}\r\n");
                messageBuilder.Append(headerBuilder);
            }

            if (fetchData.Rfc2822)
            {
                var octetCount = messageData.MessageString.Length;
                messageBuilder.Append($"BODY[] {{{octetCount}}}\r\n");
                messageBuilder.Append(messageData.MessageString.TrimEnd('\n').TrimEnd('\r'));
                messageBuilder.Append("\r\n");
            }

            messageBuilder.Append(")\r\n");
            Context.CommandProvider.Write(messageBuilder.ToString());
            return true;
        }


        private DateTime[] MessageDts = new DateTime[] {DateTime.Now, DateTime.Today, DateTime.Now, DateTime.Now};
        private Dictionary<long, string> Messages = new Dictionary<long, string>() { {29,
            @"Received: from mxfront3q.mail.yandex.net (localhost [127.0.0.1])
	by mxfront3q.mail.yandex.net with LMTP id XHgJiklRSE-oiZJ5R89
	for <nektar97@yandex.ru>; Tue, 12 May 2020 00:51:02 +0300
Received: from mail-ua1-x94a.google.com (mail-ua1-x94a.google.com [2607:f8b0:4864:20::94a])
	by mxfront3q.mail.yandex.net (mxfront/Yandex) with ESMTPS id EGNfVZSP6P-p1uimQ6O;
	Tue, 12 May 2020 00:51:01 +0300
	(using TLSv1.3 with cipher TLS_AES_256_GCM_SHA384 (256/256 bits))
	(Client certificate not present)
Return-Path: 3xMi5XggTCDkij-mZkgtVXXjpion.bjjbgZ.XjhiZfoVm42tViYZs.mp@gaia.bounces.google.com
X-Yandex-Front: mxfront3q.mail.yandex.net
X-Yandex-TimeMark: 1589233861.788
Authentication-Results: mxfront3q.mail.yandex.net; spf=pass (mxfront3q.mail.yandex.net: domain of gaia.bounces.google.com designates 2607:f8b0:4864:20::94a as permitted sender, rule=[ip6:2607:f8b0:4000::/36]) smtp.mail=3xMi5XggTCDkij-mZkgtVXXjpion.bjjbgZ.XjhiZfoVm42tViYZs.mp@gaia.bounces.google.com; dkim=pass header.i=@accounts.google.com
X-Yandex-Suid-Status: 1 193281561
X-Yandex-Spam: 1
X-Yandex-Fwd: MTA1ODE4NTY2MzI0MjY4MjQ2NiwxMDIxOTA4NzA1NjUwNTA1ODc4NQ==
Received: by mail-ua1-x94a.google.com with SMTP id j2so5019010uak.3
        for <nektar97@yandex.ru>; Mon, 11 May 2020 14:51:01 -0700 (PDT)
DKIM-Signature: v=1; a=rsa-sha256; c=relaxed/relaxed;
        d=accounts.google.com; s=20161025;
        h=mime-version:date:feedback-id:message-id:subject:from:to;
        bh=LAPfTwj/m6QnMFxK+ShetUMWB5u5JMXABnGtBBn11ko=;
        b=LS3cPTc79Gu8oGmeCpVTL58g+aH6fC52lMjhhbkAZZb6JZZ0GK49ef8sD3lHHP9l3c
         vB57DunF5XC5uWpA5rnxq0uc8rtf9z68I9bgYsOPPAOyhqCA1vz4oJDZiBF2Wmq9pOgT
         wzqkGJtQvmjQBhfXGv0FzH+TKNWdPvc+J+1MGfZIHeU+1oXMnifAC3ux7HC9yjxrs5Si
         2h7uL8AZsbP0tlfbnIe9pulKbz9gE2KtZKaONuZbsep8z/n+Zijo7+JZ4+BIk5hxEwa3
         fXpVKYZOqi1e0LN9B6+kM/PxDIHf8j++oSiwUZOtwSp521vDUx/uBoYMe2kRyBMYSeTl
         GT9A==
X-Google-DKIM-Signature: v=1; a=rsa-sha256; c=relaxed/relaxed;
        d=1e100.net; s=20161025;
        h=x-gm-message-state:mime-version:date:feedback-id:message-id:subject
         :from:to;
        bh=LAPfTwj/m6QnMFxK+ShetUMWB5u5JMXABnGtBBn11ko=;
        b=LQCg89HJx16pPNrYLVOCFGk57GdKkJpqB9/JWwR4wmdUKru0x3LaZKbleqTyAVnwQL
         xpOkgoB+h4ig/mow479IEEhC8WTV2ov77tmJDHKZP1E+R/KDPeeNel+50sYujAeK9T2H
         e2nfNvq8sSQgoP5i9TY2mcwiRGkfrAF3SsURgbA32HRAvr4KnSK203a5iCh15RvsX+93
         v5JBu7JlHOB9aS1J6icI+FKYVz7u9Efyll58HCkfb4ZZjJsG/ZaSp9WjkmFKYFJ8gD0z
         aN2HqpZc0p+u6iTYMi0kzHReCSplXYr74WJyXzUhcTBn7G+GiG8S8UFqwkHSsOkaoquW
         ckqw==
X-Gm-Message-State: AOAM533grkdreBbsy8h5eNQdaWZq6iGtUomX30BG9C+UrYX13ZSAK9wg
	x0bAqVA/L56P4gtLC7avlCHgmbhmsXcr
X-Google-Smtp-Source: ABdhPJwMpLAO/+gk1aRsYoMeSiaSsq6TdWJBJHZRI/nNa/Xu1tvhl7WL5i8diXRx3O4ZKvW1hF63P16bj2Loq2YT0sRRug==
MIME-Version: 1.0
X-Received: by 2002:a1f:4d03:: with SMTP id a3mr4214163vkb.51.1589233860046;
 Mon, 11 May 2020 14:51:00 -0700 (PDT)
Date: Mon, 11 May 2020 21:50:59 +0000 (GMT)
X-Account-Notification-Type: 31-RECOVERY-anexp#hsc-control_b
Feedback-ID: 31-RECOVERY-anexp#hsc-control_b:account-notifier
X-Notifications: 945326ed94400000
Message-ID: <-qPLx0x0xxxjdxxssdslrISrf5xxvx4g.0@nxot1ifications.google.com>
Subject: =?UTF-8?B?0J7Qv9C+0LLQtdGJ0LXQvdC40LUg0L4g0LHQtdC30L7Qv9Cw0YHQvdC+0YHRgtC4INGB?=
	=?UTF-8?B?0LLRj9C30LDQvdC90L7Qs9C+INCw0LrQutCw0YPQvdGC0LAgR29vZ2xl?=
From: Google <no-reply@accounts.google.com>
To: nektar97@yandex.ru
Content-Transfer-Encoding: 8bit
Content-Type: text/html; charset=utf-8

<!DOCTYPE html><html lang=""en""><head><meta name=""format-detection"" content=""email=no""/><meta name=""format-detection"" content=""date=no""/><style nonce=""XUU54E2GbusQNZpRig13tA"">.awl a {color: #FFFFFF; text-decoration: none;} .abml a {color: #000000; font-family: Roboto-Medium,Helvetica,Arial,sans-serif; font-weight: bold; text-decoration: none;} .adgl a {color: rgba(0, 0, 0, 0.87); text-decoration: none;} .afal a {color: #b0b0b0; text-decoration: none;} @media screen and (min-width: 600px) {.v2sp {padding: 6px 30px 0px;} .v2rsp {padding: 0px 10px;}} @media screen and (min-width: 600px) {.mdv2rw {padding: 40px 40px;}} </style><link href=""//fonts.googleapis.com/css?family=Google+Sans"" rel=""stylesheet"" type=""text/css""/></head><body style=""margin: 0; padding: 0;"" bgcolor=""#FFFFFF""><table width=""100%"" height=""100%"" style=""min-width: 348px;"" border=""0"" cellspacing=""0"" cellpadding=""0"" lang=""en""><tr height=""32"" style=""height: 32px;""><td></td></tr><tr align=""center""><td><div itemscope itemtype=""//schema.org/EmailMessage""><div itemprop=""action"" itemscope itemtype=""//schema.org/ViewAction""><link itemprop=""url"" href=""https://accounts.google.com/AccountChooser?Email=nikita.tarasov1997@gmail.com&amp;continue=https://myaccount.google.com/alert/nt/1589233859000?rfn%3D31%26rfnc%3D1%26eid%3D-1949072752098066202%26et%3D1%26anexp%3Dhsc-control_b""/><meta itemprop=""name"" content=""Просмотреть действия""/></div></div><table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""padding-bottom: 20px; max-width: 516px; min-width: 220px;""><tr><td width=""8"" style=""width: 8px;""></td><td><div style=""background-color: #F5F5F5; direction: ltr; padding: 16px;margin-bottom: 6px;""><table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0""><tbody><tr><td style=""vertical-align: top;""><img height=""20"" src=""https://www.gstatic.com/accountalerts/email/Icon_recovery_x2_20_20.png""></td><td width=""13"" style=""width: 13px;""></td><td style=""direction: ltr;""><span style=""font-family: Roboto-Regular,Helvetica,Arial,sans-serif; font-size: 13px; color: rgba(0,0,0,0.87); line-height: 1.6; color: rgba(0,0,0,0.54);"">Адрес <a style=""text-decoration: none; color: rgba(0,0,0,0.87);"">nektar97@yandex.ru</a> &#x443;&#x43A;&#x430;&#x437;&#x430;&#x43D; &#x432; &#x43A;&#x430;&#x447;&#x435;&#x441;&#x442;&#x432;&#x435; &#x440;&#x435;&#x437;&#x435;&#x440;&#x432;&#x43D;&#x43E;&#x433;&#x43E; &#x434;&#x43B;&#x44F; &#x432;&#x43E;&#x441;&#x441;&#x442;&#x430;&#x43D;&#x43E;&#x432;&#x43B;&#x435;&#x43D;&#x438;&#x44F; &#x434;&#x43E;&#x441;&#x442;&#x443;&#x43F;&#x430; &#x43A; &#x430;&#x43A;&#x43A;&#x430;&#x443;&#x43D;&#x442;&#x443; <a style=""text-decoration: none; color: rgba(0,0,0,0.87);"">nikita.tarasov1997@gmail.com</a>.</span> <span><span style=""font-family: Roboto-Regular,Helvetica,Arial,sans-serif; font-size: 13px; color: rgba(0,0,0,0.87); line-height: 1.6; color: rgba(0,0,0,0.54);"">Это не Ваш аккаунт? Нажмите <a href=""https://accounts.google.com/AccountDisavow?adt=AOX8kip424YnwreejXc5XLfo4IyprZe0sqGhcXmwR2FBU21o5K_B8oPJdpAbJLc&amp;rfn=31&amp;anexp=hsc-control_b"" data-meta-key=""disavow"" style=""text-decoration: none; color: #4285F4;"" target=""_blank"">здесь</a>.</span></span></td></tr></tbody></table></div><div style=""border-style: solid; border-width: thin; border-color:#dadce0; border-radius: 8px; padding: 40px 20px;"" align=""center"" class=""mdv2rw""><img src=""https://www.gstatic.com/images/branding/googlelogo/2x/googlelogo_color_74x24dp.png"" width=""74"" height=""24"" aria-hidden=""true"" style=""margin-bottom: 16px;"" alt=""Google""><div style=""font-family: &#39;Google Sans&#39;,Roboto,RobotoDraft,Helvetica,Arial,sans-serif;border-bottom: thin solid #dadce0; color: rgba(0,0,0,0.87); line-height: 32px; padding-bottom: 24px;text-align: center; word-break: break-word;""><div style=""font-size: 24px;"">&#x412;&#x44B;&#x43F;&#x43E;&#x43B;&#x43D;&#x435;&#x43D; &#x432;&#x445;&#x43E;&#x434; &#x432; &#x412;&#x430;&#x448; &#x441;&#x432;&#x44F;&#x437;&#x430;&#x43D;&#x43D;&#x44B;&#x439; &#x430;&#x43A;&#x43A;&#x430;&#x443;&#x43D;&#x442;</div><table align=""center"" style=""margin-top:8px;""><tr style=""line-height: normal;""><td align=""right"" style=""padding-right:8px;""><img width=""20"" height=""20"" style=""width: 20px; height: 20px; vertical-align: sub; border-radius: 50%;;"" src=""https://lh3.googleusercontent.com/a-/AOh14GjCucR-rYFWBkt5LpWP2nLC-9CrrQ4WXB3iqGtP7w=s96"" alt=""""></td><td><a style=""font-family: &#39;Google Sans&#39;,Roboto,RobotoDraft,Helvetica,Arial,sans-serif;color: rgba(0,0,0,0.87); font-size: 14px; line-height: 20px;"">nikita.tarasov1997@gmail.com</a></td></tr></table></div><div style=""font-family: Roboto-Regular,Helvetica,Arial,sans-serif; font-size: 14px; color: rgba(0,0,0,0.87); line-height: 20px;padding-top: 20px; text-align: center;"">В Ваш аккаунт Google только что выполнен вход на новом устройстве Apple iPhone. Мы хотим убедиться, что это были Вы.<div style=""padding-top: 32px; text-align: center;""><a href=""https://accounts.google.com/AccountChooser?Email=nikita.tarasov1997@gmail.com&amp;continue=https://myaccount.google.com/alert/nt/1589233859000?rfn%3D31%26rfnc%3D1%26eid%3D-1949072752098066202%26et%3D1%26anexp%3Dhsc-control_b"" target=""_blank"" link-id=""main-button-link"" style=""font-family: &#39;Google Sans&#39;,Roboto,RobotoDraft,Helvetica,Arial,sans-serif; line-height: 16px; color: #ffffff; font-weight: 400; text-decoration: none;font-size: 14px;display:inline-block;padding: 10px 24px;background-color: #4184F3; border-radius: 5px; min-width: 90px;"">Посмотреть действия</a></div></div></div><div style=""text-align: left;""><div style=""font-family: Roboto-Regular,Helvetica,Arial,sans-serif;color: rgba(0,0,0,0.54); font-size: 11px; line-height: 18px; padding-top: 12px; text-align: center;""><div>Это сообщение о важных изменениях в Вашем аккаунте и сервисах Google.</div><div style=""direction: ltr;"">&copy; 2020 Google LLC, <a class=""afal"" style=""font-family: Roboto-Regular,Helvetica,Arial,sans-serif;color: rgba(0,0,0,0.54); font-size: 11px; line-height: 18px; padding-top: 12px; text-align: center;"">1600 Amphitheatre Parkway, Mountain View, CA 94043, USA</a></div></div></div></td><td width=""8"" style=""width: 8px;""></td></tr></table></td></tr><tr height=""32"" style=""height: 32px;""><td></td></tr></table></body></html>" },{16, 
            @"Received: from mxback23g.mail.yandex.net (localhost [127.0.0.1])
	by mxback23g.mail.yandex.net with LMTP id Flrtgaf4AP-i3WimzCS
	for <nektar97+nt@yandex.ru>; Sun, 26 Apr 2020 20:07:37 +0300
Received: from forward105o.mail.yandex.net (forward105o.mail.yandex.net [2a02:6b8:0:1a2d::608])
	by mxback23g.mail.yandex.net (mxback/Yandex) with ESMTP id l41jEovxrP-7aCCUAa5;
	Sun, 26 Apr 2020 20:07:36 +0300
X-Yandex-Front: mxback23g.mail.yandex.net
X-Yandex-TimeMark: 1587920856.654
X-Yandex-Suid-Status: 1 193281561
X-Yandex-Spam: 1
Received: from mxback24o.mail.yandex.net (mxback24o.mail.yandex.net [IPv6:2a02:6b8:0:1a2d::75])
	by forward105o.mail.yandex.net (Yandex) with ESMTP id 96EB642007F5
	for <nektar97+nt@yandex.ru>; Sun, 26 Apr 2020 20:07:36 +0300 (MSK)
Received: from mxback24o.mail.yandex.net (localhost [127.0.0.1])
	by mxback24o.mail.yandex.net with LMTP id yNdu4YYrip-lEUovJQb;
	Sun, 26 Apr 2020 20:07:36 +0300
Received: from mxback24o.mail.yandex.net (localhost [127.0.0.1])
	by mxback24o.mail.yandex.net (Yandex) with ESMTP id 63FB45421A29;
	Sun, 26 Apr 2020 20:07:36 +0300 (MSK)
Received: from localhost (localhost [::1])
	by mxback24o.mail.yandex.net (mxback/Yandex) with ESMTP id CJIF4Wo7r0-7aPixIx9;
	Sun, 26 Apr 2020 20:07:36 +0300
DKIM-Signature: v=1; a=rsa-sha256; c=relaxed/relaxed; d=yandex.ru; s=mail; t=1587920856;
	bh=0ssghA7nBK+MzvCyBqnnyxm6w7KXH0nd85KoqvbrUOI=;
	h=Message-Id:Date:Subject:To:From;
	b=TvmDnglhbHdGGjxQmqHgWs5NOUUYXjdVmN4IzHdBubQoWStf2OXdpq6yM0P2gGdkV
	 ZPjrhaCfoIzxC/XLrAVDKbKur5JGTnbScDNgXYT88E8ReuqOSBCm5oJwhfJ+wYdn0W
	 a29K8BWfqeYwWBPPi/c/4RjVwZRXPDYn+3iNFLRs=
Authentication-Results: mxback24o.mail.yandex.net; dkim=pass header.i=@yandex.ru
X-Yandex-Suid-Status: 1 1130000035882273,1 0
X-Yandex-Sender-Uid: 70876967
Received: by sas1-55829ddbd171.qloud-c.yandex.net with HTTP;
	Sun, 26 Apr 2020 20:07:35 +0300
From: Nikita Tarasov <mail@ntarasov.ru>
Envelope-From: nektar97@yandex.ru
To: mail <mail@ntarasov.ru>
Subject: The test letter!
MIME-Version: 1.0
X-Mailer: Yamail [ http://yandex.ru ] 5.0
Date: Sun, 26 Apr 2020 20:07:35 +0300
Message-Id: <1255x51xxxx587920843@mail.yandex.ru>
Content-Transfer-Encoding: 8bit
Content-Type: text/html; charset=utf-8
X-Yandex-Forward: 9d668b532af1baa9ecc9f739379b8bef
Return-Path: mail@ntarasov.ru
X-Yandex-Forward: 85d02b20992161de89aefe959a6f9d4d

<div> </div><div> </div><div>-- </div><div>1<b>2</b>4456<img src=""https://sun9-40.userapi.com/c857636/v857636350/1f2d7c/QtotoIZnME8.jpg""/></div><div> </div>" }};
    }
}