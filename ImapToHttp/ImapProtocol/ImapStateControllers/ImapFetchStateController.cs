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
            var range = ImapCommon.GetMessageSequenceSet(args[0]);

            if (range.Item1 == ImapCommon.MessageSequenceType.None)
            {
                Context.CommandProvider.Write($"{cmd.Tag} BAD\r\n");
            }

            var dataItemsString = string.Join(' ', args.Skip(1));

            var idType = Context.States.Any(s => s.State == ImapState.Uid) ? MessageIdType.Uid : MessageIdType.Id;
            var messageIds = ImapCommon.ExtractRealMessageIds(range, idType).ToList();
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

            // Если команда была вызвана из SELECT ящика
            // не из UID и не из STORE
            if (Context.PrePeekState.State == ImapState.Selected)
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

            var message = ImapCommon.Messages[messageId];
            var messageDate = ImapCommon.MessageDts.First();
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
    }
}