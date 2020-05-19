using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HttpProtocol.Responses
{
    [DataContract]
    public class MessageResponse
    {
        [DataMember(Name = "headers")]
        public IDictionary<string, string> Headers { get; set; }
        [DataMember(Name = "body")]
        public string Body { get; set; }
        [DataMember(Name = "date")]
        public DateTime Date { get; set; }
    }
}