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
            var existMessageIds = new long[] {1, 2, 3, 4}.ToDictionary(i => i, i => i); // TODO
            
            switch (range.Item1)
            {
                case MessageSequenceType.None:
                    return new long[0];
                case MessageSequenceType.Exact:
                    return range.Item2.Where(i => existMessageIds.ContainsKey(i));
                case MessageSequenceType.Range:
                    return existMessageIds.Keys.Where(k => k >= range.Item2[0] && k <= range.Item2[1]);
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

            var message = Messages[messageId - 1];
            var messageDate = MessageDts[messageId - 1];
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
                messageBuilder.Append($"BODYSTRUCTURE (\"TEXT\" \"PLAIN\" (\"CHARSET\" \"UTF8\") NIL NIL \"7BIT\" {octetCount} {linesCount}) ");
                //var mm =
                //    "TEXT (<div>Test mail</div><div> </div><div>-- </div><div>С уважением,<br />Тарасов Никита</div><div> </div>) MESSAGE/RFC822";

            }*/
            
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
        private string[] Messages = new[] {
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
Message-ID: <-qPL00HjYphEnrISrf5v4g.0@notifications.google.com>
Subject: =?UTF-8?B?0J7Qv9C+0LLQtdGJ0LXQvdC40LUg0L4g0LHQtdC30L7Qv9Cw0YHQvdC+0YHRgtC4INGB?=
	=?UTF-8?B?0LLRj9C30LDQvdC90L7Qs9C+INCw0LrQutCw0YPQvdGC0LAgR29vZ2xl?=
From: Google <no-reply@accounts.google.com>
To: nektar97@yandex.ru
Content-Type: multipart/alternative; boundary=""000000000000b1715b05a5665777""
X-Yandex-Forward: 85d02b20992161de89aefe959a6f9d4d

Content-Type: text/html; charset=""UTF-8""
Content-Transfer-Encoding: quoted-printable

<!DOCTYPE html><html lang=3D""en""><head><meta name=3D""format-detection"" cont=
ent=3D""email=3Dno""/><meta name=3D""format-detection"" content=3D""date=3Dno""/>=
<style nonce=3D""XUU54E2GbusQNZpRig13tA"">.awl a {color: #FFFFFF; text-decora=
tion: none;} .abml a {color: #000000; font-family: Roboto-Medium,Helvetica,=
Arial,sans-serif; font-weight: bold; text-decoration: none;} .adgl a {color=
: rgba(0, 0, 0, 0.87); text-decoration: none;} .afal a {color: #b0b0b0; tex=
t-decoration: none;} @media screen and (min-width: 600px) {.v2sp {padding: =
6px 30px 0px;} .v2rsp {padding: 0px 10px;}} @media screen and (min-width: 6=
00px) {.mdv2rw {padding: 40px 40px;}} </style><link href=3D""//fonts.googlea=
pis.com/css?family=3DGoogle+Sans"" rel=3D""stylesheet"" type=3D""text/css""/></h=
ead><body style=3D""margin: 0; padding: 0;"" bgcolor=3D""#FFFFFF""><table width=
=3D""100%"" height=3D""100%"" style=3D""min-width: 348px;"" border=3D""0"" cellspac=
ing=3D""0"" cellpadding=3D""0"" lang=3D""en""><tr height=3D""32"" style=3D""height: =
32px;""><td></td></tr><tr align=3D""center""><td><div itemscope itemtype=3D""//=
schema.org/EmailMessage""><div itemprop=3D""action"" itemscope itemtype=3D""//s=
chema.org/ViewAction""><link itemprop=3D""url"" href=3D""https://accounts.googl=
e.com/AccountChooser?Email=3Dnikita.tarasov1997@gmail.com&amp;continue=3Dht=
tps://myaccount.google.com/alert/nt/1589233859000?rfn%3D31%26rfnc%3D1%26eid=
%3D-1949072752098066202%26et%3D1%26anexp%3Dhsc-control_b""/><meta itemprop=
=3D""name"" content=3D""=D0=9F=D1=80=D0=BE=D1=81=D0=BC=D0=BE=D1=82=D1=80=D0=B5=
=D1=82=D1=8C =D0=B4=D0=B5=D0=B9=D1=81=D1=82=D0=B2=D0=B8=D1=8F""/></div></div=
><table border=3D""0"" cellspacing=3D""0"" cellpadding=3D""0"" style=3D""padding-b=
ottom: 20px; max-width: 516px; min-width: 220px;""><tr><td width=3D""8"" style=
=3D""width: 8px;""></td><td><div style=3D""background-color: #F5F5F5; directio=
n: ltr; padding: 16px;margin-bottom: 6px;""><table width=3D""100%"" border=3D""=
0"" cellspacing=3D""0"" cellpadding=3D""0""><tbody><tr><td style=3D""vertical-ali=
gn: top;""><img height=3D""20"" src=3D""https://www.gstatic.com/accountalerts/e=
mail/Icon_recovery_x2_20_20.png""></td><td width=3D""13"" style=3D""width: 13px=
;""></td><td style=3D""direction: ltr;""><span style=3D""font-family: Roboto-Re=
gular,Helvetica,Arial,sans-serif; font-size: 13px; color: rgba(0,0,0,0.87);=
 line-height: 1.6; color: rgba(0,0,0,0.54);"">=D0=90=D0=B4=D1=80=D0=B5=D1=81=
 <a style=3D""text-decoration: none; color: rgba(0,0,0,0.87);"">nektar97@yand=
ex.ru</a> =D1=83=D0=BA=D0=B0=D0=B7=D0=B0=D0=BD =D0=B2 =D0=BA=D0=B0=D1=87=D0=
=B5=D1=81=D1=82=D0=B2=D0=B5 =D1=80=D0=B5=D0=B7=D0=B5=D1=80=D0=B2=D0=BD=D0=
=BE=D0=B3=D0=BE =D0=B4=D0=BB=D1=8F =D0=B2=D0=BE=D1=81=D1=81=D1=82=D0=B0=D0=
=BD=D0=BE=D0=B2=D0=BB=D0=B5=D0=BD=D0=B8=D1=8F =D0=B4=D0=BE=D1=81=D1=82=D1=
=83=D0=BF=D0=B0 =D0=BA =D0=B0=D0=BA=D0=BA=D0=B0=D1=83=D0=BD=D1=82=D1=83 <a =
style=3D""text-decoration: none; color: rgba(0,0,0,0.87);"">nikita.tarasov199=
7@gmail.com</a>.</span> <span><span style=3D""font-family: Roboto-Regular,He=
lvetica,Arial,sans-serif; font-size: 13px; color: rgba(0,0,0,0.87); line-he=
ight: 1.6; color: rgba(0,0,0,0.54);"">=D0=AD=D1=82=D0=BE =D0=BD=D0=B5 =D0=92=
=D0=B0=D1=88 =D0=B0=D0=BA=D0=BA=D0=B0=D1=83=D0=BD=D1=82? =D0=9D=D0=B0=D0=B6=
=D0=BC=D0=B8=D1=82=D0=B5 <a href=3D""https://accounts.google.com/AccountDisa=
vow?adt=3DAOX8kip424YnwreejXc5XLfo4IyprZe0sqGhcXmwR2FBU21o5K_B8oPJdpAbJLc&a=
mp;rfn=3D31&amp;anexp=3Dhsc-control_b"" data-meta-key=3D""disavow"" style=3D""t=
ext-decoration: none; color: #4285F4;"" target=3D""_blank"">=D0=B7=D0=B4=D0=B5=
=D1=81=D1=8C</a>.</span></span></td></tr></tbody></table></div><div style=
=3D""border-style: solid; border-width: thin; border-color:#dadce0; border-r=
adius: 8px; padding: 40px 20px;"" align=3D""center"" class=3D""mdv2rw""><img src=
=3D""https://www.gstatic.com/images/branding/googlelogo/2x/googlelogo_color_=
74x24dp.png"" width=3D""74"" height=3D""24"" aria-hidden=3D""true"" style=3D""margi=
n-bottom: 16px;"" alt=3D""Google""><div style=3D""font-family: &#39;Google Sans=
&#39;,Roboto,RobotoDraft,Helvetica,Arial,sans-serif;border-bottom: thin sol=
id #dadce0; color: rgba(0,0,0,0.87); line-height: 32px; padding-bottom: 24p=
x;text-align: center; word-break: break-word;""><div style=3D""font-size: 24p=
x;"">=D0=92=D1=8B=D0=BF=D0=BE=D0=BB=D0=BD=D0=B5=D0=BD =D0=B2=D1=85=D0=BE=D0=
=B4 =D0=B2 =D0=92=D0=B0=D1=88 =D1=81=D0=B2=D1=8F=D0=B7=D0=B0=D0=BD=D0=BD=D1=
=8B=D0=B9 =D0=B0=D0=BA=D0=BA=D0=B0=D1=83=D0=BD=D1=82</div><table align=3D""c=
enter"" style=3D""margin-top:8px;""><tr style=3D""line-height: normal;""><td ali=
gn=3D""right"" style=3D""padding-right:8px;""><img width=3D""20"" height=3D""20"" s=
tyle=3D""width: 20px; height: 20px; vertical-align: sub; border-radius: 50%;=
;"" src=3D""https://lh3.googleusercontent.com/a-/AOh14GjCucR-rYFWBkt5LpWP2nLC=
-9CrrQ4WXB3iqGtP7w=3Ds96"" alt=3D""""></td><td><a style=3D""font-family: &#39;G=
oogle Sans&#39;,Roboto,RobotoDraft,Helvetica,Arial,sans-serif;color: rgba(0=
,0,0,0.87); font-size: 14px; line-height: 20px;"">nikita.tarasov1997@gmail.c=
om</a></td></tr></table></div><div style=3D""font-family: Roboto-Regular,Hel=
vetica,Arial,sans-serif; font-size: 14px; color: rgba(0,0,0,0.87); line-hei=
ght: 20px;padding-top: 20px; text-align: center;"">=D0=92 =D0=92=D0=B0=D1=88=
 =D0=B0=D0=BA=D0=BA=D0=B0=D1=83=D0=BD=D1=82 Google =D1=82=D0=BE=D0=BB=D1=8C=
=D0=BA=D0=BE =D1=87=D1=82=D0=BE =D0=B2=D1=8B=D0=BF=D0=BE=D0=BB=D0=BD=D0=B5=
=D0=BD =D0=B2=D1=85=D0=BE=D0=B4 =D0=BD=D0=B0 =D0=BD=D0=BE=D0=B2=D0=BE=D0=BC=
 =D1=83=D1=81=D1=82=D1=80=D0=BE=D0=B9=D1=81=D1=82=D0=B2=D0=B5 Apple iPhone.=
 =D0=9C=D1=8B =D1=85=D0=BE=D1=82=D0=B8=D0=BC =D1=83=D0=B1=D0=B5=D0=B4=D0=B8=
=D1=82=D1=8C=D1=81=D1=8F, =D1=87=D1=82=D0=BE =D1=8D=D1=82=D0=BE =D0=B1=D1=
=8B=D0=BB=D0=B8 =D0=92=D1=8B.<div style=3D""padding-top: 32px; text-align: c=
enter;""><a href=3D""https://accounts.google.com/AccountChooser?Email=3Dnikit=
a.tarasov1997@gmail.com&amp;continue=3Dhttps://myaccount.google.com/alert/n=
t/1589233859000?rfn%3D31%26rfnc%3D1%26eid%3D-1949072752098066202%26et%3D1%2=
6anexp%3Dhsc-control_b"" target=3D""_blank"" link-id=3D""main-button-link"" styl=
e=3D""font-family: &#39;Google Sans&#39;,Roboto,RobotoDraft,Helvetica,Arial,=
sans-serif; line-height: 16px; color: #ffffff; font-weight: 400; text-decor=
ation: none;font-size: 14px;display:inline-block;padding: 10px 24px;backgro=
und-color: #4184F3; border-radius: 5px; min-width: 90px;"">=D0=9F=D0=BE=D1=
=81=D0=BC=D0=BE=D1=82=D1=80=D0=B5=D1=82=D1=8C =D0=B4=D0=B5=D0=B9=D1=81=D1=
=82=D0=B2=D0=B8=D1=8F</a></div></div></div><div style=3D""text-align: left;""=
><div style=3D""font-family: Roboto-Regular,Helvetica,Arial,sans-serif;color=
: rgba(0,0,0,0.54); font-size: 11px; line-height: 18px; padding-top: 12px; =
text-align: center;""><div>=D0=AD=D1=82=D0=BE =D1=81=D0=BE=D0=BE=D0=B1=D1=89=
=D0=B5=D0=BD=D0=B8=D0=B5 =D0=BE =D0=B2=D0=B0=D0=B6=D0=BD=D1=8B=D1=85 =D0=B8=
=D0=B7=D0=BC=D0=B5=D0=BD=D0=B5=D0=BD=D0=B8=D1=8F=D1=85 =D0=B2 =D0=92=D0=B0=
=D1=88=D0=B5=D0=BC =D0=B0=D0=BA=D0=BA=D0=B0=D1=83=D0=BD=D1=82=D0=B5 =D0=B8 =
=D1=81=D0=B5=D1=80=D0=B2=D0=B8=D1=81=D0=B0=D1=85 Google.</div><div style=3D=
""direction: ltr;"">&copy; 2020 Google LLC, <a class=3D""afal"" style=3D""font-f=
amily: Roboto-Regular,Helvetica,Arial,sans-serif;color: rgba(0,0,0,0.54); f=
ont-size: 11px; line-height: 18px; padding-top: 12px; text-align: center;"">=
1600 Amphitheatre Parkway, Mountain View, CA 94043, USA</a></div></div></di=
v></td><td width=3D""8"" style=3D""width: 8px;""></td></tr></table></td></tr><t=
r height=3D""32"" style=3D""height: 32px;""><td></td></tr></table></body></html=
>
--000000000000b1715b05a5665777--",
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
Message-Id: <125551587920843@mail.yandex.ru>
Content-Transfer-Encoding: 8bit
Content-Type: text/html; charset=utf-8
X-Yandex-Forward: 9d668b532af1baa9ecc9f739379b8bef
Return-Path: mail@ntarasov.ru
X-Yandex-Forward: 85d02b20992161de89aefe959a6f9d4d

<div> </div><div> </div><div>-- </div><div>124456</div><div> </div>",
            @"Received: from mxback29j.mail.yandex.net (localhost [127.0.0.1])
	by mxback29j.mail.yandex.net with LMTP id lQMUGk8aTm-ke9BygF1
	for <nektar97+nt@yandex.ru>; Sun, 26 Apr 2020 20:08:50 +0300
Received: from forward103o.mail.yandex.net (forward103o.mail.yandex.net [37.140.190.177])
	by mxback29j.mail.yandex.net (mxback/Yandex) with ESMTP id E7bXlDtnC3-8okKDCG6;
	Sun, 26 Apr 2020 20:08:50 +0300
X-Yandex-Front: mxback29j.mail.yandex.net
X-Yandex-TimeMark: 1587920930.415
X-Yandex-Suid-Status: 1 193281561
X-Yandex-Spam: 1
Received: from mxback9o.mail.yandex.net (mxback9o.mail.yandex.net [IPv6:2a02:6b8:0:1a2d::23])
	by forward103o.mail.yandex.net (Yandex) with ESMTP id 5C88E5F8088D
	for <nektar97+nt@yandex.ru>; Sun, 26 Apr 2020 20:08:50 +0300 (MSK)
Received: from mxback9o.mail.yandex.net (localhost [127.0.0.1])
	by mxback9o.mail.yandex.net with LMTP id t61lZhiBsI-e2Cg4Lm1;
	Sun, 26 Apr 2020 20:08:50 +0300
Received: from mxback9o.mail.yandex.net (localhost.localdomain [127.0.0.1])
	by mxback9o.mail.yandex.net (Yandex) with ESMTP id 26F76144CCED;
	Sun, 26 Apr 2020 20:08:50 +0300 (MSK)
Received: from localhost (localhost [::1])
	by mxback9o.mail.yandex.net (mxback/Yandex) with ESMTP id VPyNJaDaak-8nbOqqsa;
	Sun, 26 Apr 2020 20:08:49 +0300
DKIM-Signature: v=1; a=rsa-sha256; c=relaxed/relaxed; d=yandex.ru; s=mail; t=1587920929;
	bh=0ssghA7nBK+MzvCyBqnnyxm6w7KXH0nd85KoqvbrUOI=;
	h=Message-Id:Date:Subject:To:From;
	b=Dqk0M1SIdWep/JodHhgNCXRSBcS4HYUl+I7JHx/xH2aeh8/8InSyM7JN7TfnJhWP6
	 2XJ7GCxHdN6GW8H4TTGc8obWgGlFkxPl8elIQkV8bEq+9wX2wkCHWy0uTCqmLBxjD+
	 tGpJ0Ppo5P5SkwZQI6kDRIXBom1GJH6+iy5o5vfo=
Authentication-Results: mxback9o.mail.yandex.net; dkim=pass header.i=@yandex.ru
X-Yandex-Suid-Status: 1 1130000035882273,1 0
X-Yandex-Sender-Uid: 70876967
Received: by myt5-b646bde4b8f3.qloud-c.yandex.net with HTTP;
	Sun, 26 Apr 2020 20:08:49 +0300
From: Nikita Tarasov <mail@ntarasov.ru>
Envelope-From: nektar97@yandex.ru
To: mail <mail@ntarasov.ru>
Subject: Another letter
MIME-Version: 1.0
X-Mailer: Yamail [ http://yandex.ru ] 5.0
Date: Sun, 26 Apr 2020 20:08:49 +0300
Message-Id: <613041587920919@mail.yandex.ru>
Content-Transfer-Encoding: 8bit
Content-Type: text/html; charset=utf-8
X-Yandex-Forward: 9d668b532af1baa9ecc9f739379b8bef
Return-Path: mail@ntarasov.ru
X-Yandex-Forward: 85d02b20992161de89aefe959a6f9d4d

<div> </div><div> </div><div>-- </div><div>12345</div><div> </div>",
            @"Received: from mxback22g.mail.yandex.net (localhost [127.0.0.1])
	by mxback22g.mail.yandex.net with LMTP id NiTul4T3ij-Eujq6W50
	for <nektar97+nt@yandex.ru>; Sun, 26 Apr 2020 20:10:21 +0300
Received: from forward100p.mail.yandex.net (forward100p.mail.yandex.net [2a02:6b8:0:1472:2741:0:8b7:100])
	by mxback22g.mail.yandex.net (mxback/Yandex) with ESMTP id A4n3NxsjnP-AL9CFTbG;
	Sun, 26 Apr 2020 20:10:21 +0300
X-Yandex-Front: mxback22g.mail.yandex.net
X-Yandex-TimeMark: 1587921021.354
X-Yandex-Suid-Status: 1 193281561
X-Yandex-Spam: 1
Received: from mxback30g.mail.yandex.net (mxback30g.mail.yandex.net [IPv6:2a02:6b8:0:1472:2741:0:8b7:330])
	by forward100p.mail.yandex.net (Yandex) with ESMTP id 53E305980750
	for <nektar97+nt@yandex.ru>; Sun, 26 Apr 2020 20:10:21 +0300 (MSK)
Received: from mxback30g.mail.yandex.net (localhost [127.0.0.1])
	by mxback30g.mail.yandex.net with LMTP id swHnzfktoI-hvmJ5a9d;
	Sun, 26 Apr 2020 20:10:21 +0300
Received: from mxback30g.mail.yandex.net (localhost [127.0.0.1])
	by mxback30g.mail.yandex.net (Yandex) with ESMTP id 2BFBB21A3A;
	Sun, 26 Apr 2020 20:10:21 +0300 (MSK)
Received: from localhost (localhost [::1])
	by mxback30g.mail.yandex.net (mxback/Yandex) with ESMTP id dScKzewRpc-AKrmlJhk;
	Sun, 26 Apr 2020 20:10:20 +0300
DKIM-Signature: v=1; a=rsa-sha256; c=relaxed/relaxed; d=yandex.ru; s=mail; t=1587921020;
	bh=0ssghA7nBK+MzvCyBqnnyxm6w7KXH0nd85KoqvbrUOI=;
	h=Message-Id:Date:Subject:To:From;
	b=nZyqAMxd/21BTl+ZKKC+lf8/9VY0hL/enP6ntn8ad3RjWzv7BXwXI33RFu/PnZ7RA
	 +cnJIsLzKBLhcEgWEpwXYfKL1mm8H2b6/xznyVa0a09f4PDuLASZ1t4/gp2/OYhdn7
	 ex9Zq0b9qL7lqXj2JLLclJhb02KptGo6sBldD21E=
Authentication-Results: mxback30g.mail.yandex.net; dkim=pass header.i=@yandex.ru
X-Yandex-Suid-Status: 1 1130000035882273,1 0
X-Yandex-Sender-Uid: 70876967
Received: by iva7-8a22bc446c12.qloud-c.yandex.net with HTTP;
	Sun, 26 Apr 2020 20:10:20 +0300
From: Nikita Tarasov <mail@ntarasov.ru>
Envelope-From: nektar97@yandex.ru
To: mail <mail@ntarasov.ru>
Subject: hello 12345
MIME-Version: 1.0
X-Mailer: Yamail [ http://yandex.ru ] 5.0
Date: Sun, 26 Apr 2020 20:10:20 +0300
Message-Id: <695211587921002@mail.yandex.ru>
Content-Transfer-Encoding: 8bit
Content-Type: text/html; charset=utf-8
X-Yandex-Forward: 9d668b532af1baa9ecc9f739379b8bef
Return-Path: mail@ntarasov.ru
X-Yandex-Forward: 85d02b20992161de89aefe959a6f9d4d

<div> </div><div> </div><div>-- </div><div>12415161</div><div> </div>"};
    }
}