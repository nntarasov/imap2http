using System;
using System.Runtime.Serialization;

namespace ImapToHttpDemo.Controllers
{
    [DataContract]
    public class MessageResponse
    {
        [DataMember(Name = "headers")]
        public MessageHeaderResponse[] Headers { get; set; }
        [DataMember(Name = "body")]
        public string Body { get; set; }
        [DataMember(Name = "date")]
        public DateTime Date { get; set; }
    }
}