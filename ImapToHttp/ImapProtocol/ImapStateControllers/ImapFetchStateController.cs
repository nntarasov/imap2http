using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ImapProtocol.Contracts;

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
            public long? SizeTo { get; set; }
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
                SizeTo = null,
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
                @"BODY(?<peek>\.PEEK){0,1}(\[(?<section>[^]]*)\](?<partial>[\w.]*))*(<(?<szFrom>\d+)\.(?<szTo>\d+)>)*");
            if (bodyMatch.Success)
            {
                data.Peek = bodyMatch.Groups["peek"].Success;
                if (bodyMatch.Groups["szFrom"].Success)
                {
                    long.TryParse(bodyMatch.Groups["szFrom"].Value, out var sizeFrom);
                    data.SizeFrom = sizeFrom;
                }

                if (bodyMatch.Groups["szTo"].Success)
                {
                    long.TryParse(bodyMatch.Groups["szTo"].Value, out var sizeTo);
                    data.SizeTo = sizeTo;
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
                    data.IncludeHeaders = true;
                    data.IncludeText = true;
                    data.IncludeMime = true;
                }
                
                var logger = LoggerFactory.GetLogger();
                /*logger.Print($"includeBodystructure: {includeBodystructure}");
                logger.Print($"includeEnvelope: {includeEnvelope}");
                logger.Print($"includeFlags: {includeFlags}");
                logger.Print($"includeInternaldate: {includeInternaldate}");
                logger.Print($"includeRfc822Header: {includeRfc822Header}");
                logger.Print($"includeRfc822Size: {includeRfc822Size}");
                logger.Print($"includeRfc822Text: {includeRfc822Text}");
                logger.Print($"includeUid: {includeUid}");
                
                logger.Print($"includeMime: {includeMime}");
                logger.Print($"includeText: {includeText}");
                logger.Print($"includeHeaders: {includeHeaders}");
                logger.Print($"szFrom: {szFrom}");
                logger.Print($"szTo: {szTo}");
                logger.Print($"peek: {peek}");
                logger.Print($"rfc2822: {rfc2822}");
                
                logger.Print($"headerFields: {headerFields.Aggregate((a, b) => a + " " + b)}");
                logger.Print($"headerFieldsNot: {headerFieldsNot.Aggregate((a, b) => a + " " + b)}");*/
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
                FetchMessage(messageId, fetchData);
            }
            
            Context.CommandProvider.Write($"{cmd.Tag} OK FETCH\r\n");
            return true;
        }

        private void FetchMessage(long messageId, FetchData fetchData)
        {
            /* 3 FETCH (BODY[HEADER,FIELDS ("DATE" "FROM" "SUBJECT")] {112}
            18 Date: Tue, 14 Sep 1999 10:09:50 -0500
            19 From: alex@shadrach.smallorg.org
            20 Subject: This is the first test message
            21*/
            //ParseDataItems(cmd.Args);

            var message = Messages[messageId - 1];
            var messageDate = MessageDts[messageId -1];
            
            var lines = message.Split('\n');
            Dictionary<string, string> messageHeaders = new Dictionary<string, string>();
            var currentHeaderValue = new StringBuilder();
            string currentHeaderKey = null;
            for (int i = 0; i < lines.Length; ++i)
            {
                if (lines[i] == string.Empty)
                {
                    if (currentHeaderValue.Length != 0)
                    {
                        var upperKey = currentHeaderKey.ToUpper();
                        if (!messageHeaders.ContainsKey(upperKey))
                        {
                            messageHeaders.Add(upperKey, currentHeaderValue.ToString());
                        }
                    }
                    break;
                }
                if (lines[i].Contains(':'))
                {
                    if (currentHeaderKey != null && !messageHeaders.ContainsKey(currentHeaderKey.ToUpper()))
                    {
                        messageHeaders.Add(currentHeaderKey.ToUpper(), currentHeaderValue.ToString());
                    }

                    currentHeaderKey = lines[i].Split(':')[0];
                    currentHeaderValue.Clear();
                    currentHeaderValue.Append(lines[i].Split(':')[1]);
                }
                else
                {
                    currentHeaderValue.Append(lines[i]);
                }
            }
            
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
            
                        
            if (fetchData.IncludeUid)
            {
                messageBuilder.Append($"UID {messageId} ");
            }

            if (fetchData.IncludeBodystructure || fetchData.IncludeText)
            {
                //("TEXT" "PLAIN" ("CHARSET"
                // "US-ASCII") NIL NIL "7BIT" 2279 48)
                var octetCount = message.Length;
                var linesCount = message.Split('\n').Length;
                //messageBuilder.Append($"BODYSTRUCTURE (\"TEXT\" \"PLAIN\" (\"CHARSET\" \"US-ASCII\") NIL NIL \"7BIT\" {octetCount} {linesCount}) ");
                var mm =
                    "TEXT (<div>Test mail</div><div> </div><div>-- </div><div>С уважением,<br />Тарасов Никита</div><div> </div>) MESSAGE/RFC822";
                messageBuilder.Append($"BODY[TEXT] {{{mm.Length}}}\r\n");
                messageBuilder.Append(mm.TrimEnd('\n').TrimEnd('\r'));
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
                foreach (var header in  messageHeaders)
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

            messageBuilder.Append(")\r\n");
            Context.CommandProvider.Write(messageBuilder.ToString());

        }


        private DateTime[] MessageDts = new DateTime[] {DateTime.Now, DateTime.Today, DateTime.Now, DateTime.Now};
        private string[] Messages = new[] { @"Received: from mxback12q.mail.yandex.net (localhost [127.0.0.1])
	by mxback12q.mail.yandex.net with LMTP id BH3URSwjv9-av7e8ePR
	for <nektar97+nt@yandex.ru>; Sat, 25 Apr 2020 21:21:16 +0300
Received: from forward101q.mail.yandex.net (forward101q.mail.yandex.net [2a02:6b8:c0e:4b:0:640:4012:bb98])
	by mxback12q.mail.yandex.net (mxback/Yandex) with ESMTP id Vnjg9UR7qO-LFaCVXwC;
	Sat, 25 Apr 2020 21:21:15 +0300
X-Yandex-Front: mxback12q.mail.yandex.net
X-Yandex-TimeMark: 1587838875.957
X-Yandex-Suid-Status: 1 193281561
X-Yandex-Spam: 1
Received: from mxback12q.mail.yandex.net (mxback12q.mail.yandex.net [IPv6:2a02:6b8:c0e:1b3:0:640:3818:d096])
	by forward101q.mail.yandex.net (Yandex) with ESMTP id DF426CF40002
	for <nektar97+nt@yandex.ru>; Sat, 25 Apr 2020 21:21:15 +0300 (MSK)
Received: from mxback12q.mail.yandex.net (localhost [127.0.0.1])
	by mxback12q.mail.yandex.net with LMTP id 8BW8eLLDC4-1dXGvw7r;
	Sat, 25 Apr 2020 21:21:15 +0300
Received: from mxback12q.mail.yandex.net (localhost [127.0.0.1])
	by mxback12q.mail.yandex.net (Yandex) with ESMTP id B47BD6B00F9F;
	Sat, 25 Apr 2020 21:21:15 +0300 (MSK)
Received: from localhost (localhost [::1])
	by mxback12q.mail.yandex.net (mxback/Yandex) with ESMTP id cTvkDo9kaD-LFeaFkst;
	Sat, 25 Apr 2020 21:21:15 +0300
DKIM-Signature: v=1; a=rsa-sha256; c=relaxed/relaxed; d=yandex.ru; s=mail; t=1587838875;
	bh=0Z4/ohSw4WuJHscc1BSo+FGKH5WVuzLWcPjCaYu8keo=;
	h=Message-Id:Date:Subject:To:From;
	b=gkVLd4XD/xfvk/NLeDKAoGcmf6sFC57ffIyn4FD8lg3E4bCVygL0z7se4ZbaYpOP/
	 s6IXsvkgbgSgdH0Asbt9x8SoLE7q96KRJHkEdcRglcObfkiPn8WPzn9i0SRX5cDZXm
	 Q8PvGtGl6qzMf36Qim/n8WqEY4D4xrnZWpdaPLwI=
Authentication-Results: mxback12q.mail.yandex.net; dkim=pass header.i=@yandex.ru
X-Yandex-Suid-Status: 1 1130000035882273,1 0
X-Yandex-Sender-Uid: 70876967
Received: by vla4-4046ec513d04.qloud-c.yandex.net with HTTP;
	Sat, 25 Apr 2020 21:21:15 +0300
From: Nikita Tarasov <mail@ntarasov.ru>
Envelope-From: nektar97@yandex.ru
To: mail <mail@ntarasov.ru>
Subject: Hello world! 
MIME-Version: 1.0
X-Mailer: Yamail [ http://yandex.ru ] 5.0
Date: Sat, 25 Apr 2020 21:21:15 +0300
Message-Id: <578041587838860@mail.yandex.ru>
Content-Transfer-Encoding: 8bit
Content-Type: text/html; charset=utf-8
X-Yandex-Forward: 9d668b532af1baa9ecc9f739379b8bef
Return-Path: mail@ntarasov.ru
X-Yandex-Forward: 85d02b20992161de89aefe959a6f9d4d

<div>Test mail</div><div> </div><div>-- </div><div>С уважением,<br />Тарасов Никита</div><div> </div>",
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

<div> </div><div> </div><div>-- </div><div>С уважением,<br />Тарасов Никита</div><div> </div>",
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

<div> </div><div> </div><div>-- </div><div>С уважением,<br />Тарасов Никита</div><div> </div>",
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

<div> </div><div> </div><div>-- </div><div>С уважением,<br />Тарасов Никита</div><div> </div>"};
    }
}