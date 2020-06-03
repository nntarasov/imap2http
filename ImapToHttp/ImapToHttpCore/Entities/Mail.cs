using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImapToHttpCore.Entities
{
    public class Mail
    {
        public IDictionary<string, string> Headers { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
        public int UId { get; set; }
        public string[] Flags { get; set; }

        private string _rfc2822 = null;
        public string Rfc2822
        {
            get
            {
                if (_rfc2822 != null)
                {
                    return _rfc2822;
                }

                var builder = new StringBuilder();
                var hLines = Headers.Select(t => t.Key + ": " + t.Value + "\r\n")
                    .ToList();
                foreach (var line in hLines)
                {
                    builder.Append(line);
                }

                builder.Append("\r\n");
                builder.Append(Body);
                _rfc2822 = builder.ToString();
                return _rfc2822;
            }
        }
    }
}