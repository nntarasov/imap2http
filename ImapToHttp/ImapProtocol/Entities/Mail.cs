using System;
using System.Collections.Generic;

namespace ImapProtocol.Entities
{
    public class Mail
    {
        public IDictionary<string, string> Headers { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
        public string[] Flags { get; set; }
        public int UId { get; set; }
        public Directory Location { get; set; }
        public string ContentType { get; set; }
        public string Rfc2822 { get; }
    }
}