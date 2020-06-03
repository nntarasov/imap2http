using System;
using System.Runtime.Serialization;

namespace HttpProtocol.Responses
{
    [DataContract]
    public class MessageHeaderResponse
    {
        [DataMember(Name = "key")]
        public string Key { get; set; }
        [DataMember(Name = "value")]
        public string Value { get; set; }
    }
    
    [DataContract]
    public class MessageResponse
    {
        [DataMember(Name = "headers")]
        public MessageHeaderResponse[] Headers { get; set; }
        [DataMember(Name = "body")]
        public string Body { get; set; }
        [DataMember(Name = "date")]
        public DateTime Date { get; set; }
        
        [DataMember(Name = "flags")]
        public string[] Flags { get; set; }
    }
}