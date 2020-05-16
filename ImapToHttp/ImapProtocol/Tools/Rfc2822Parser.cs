using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImapProtocol.Contracts;

namespace ImapProtocol.Tools
{
    public static class Rfc2822Parser
    {
            public static Rfc2822Message Parse(string message)
            {
                var messageLines = message
                    .Split('\n')
                    .Select(ln => ln.TrimEnd('\r'))
                    .ToList();

                var headerLines = messageLines
                    .TakeWhile(ln => !string.IsNullOrEmpty(ln))
                    .ToList();
                var headersString = string.Empty;
                if (headerLines.Any())
                {
                    headersString = string.Join(' ', headerLines);
                }

                var bodyLines = messageLines
                    .TakeLast(messageLines.Count - headerLines.Count - 1)
                    .ToList();
                var bodyString = string.Empty;
                if (bodyLines.Any())
                {
                    bodyString = string.Join(' ', bodyLines);
                }
                
                return new Rfc2822Message
                {
                    Headers = ParseHeaders(message),
                    HeadersString = headersString,
                    BodyString = bodyString.TrimEnd('\n').TrimEnd('\r'),
                    MessageString = message
                };
            }

            private static IDictionary<string, string> ParseHeaders(string message)
            {
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

                return messageHeaders;
            }
    }
}