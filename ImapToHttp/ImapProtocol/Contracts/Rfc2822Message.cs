using System.Collections.Generic;

namespace ImapProtocol.Contracts
{
    public class Rfc2822Message
    {
        public string MessageString { get; set; }
        public string BodyString { get; set; }
        public string HeadersString { get; set; }
        
        public IDictionary<string, string> Headers { get; set; }
    }
}