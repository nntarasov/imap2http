using System.Runtime.Serialization;

namespace ImapToHttpCLI
{
    [DataContract]
    public class Configuration
    {
        [DataMember(Name = "imap_address")]
        public string ImapAddress { get; set; }
        [DataMember(Name = "http_base_url")]
        public string HttpBaseUrl { get; set; }
    }
}