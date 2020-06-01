using System.Runtime.Serialization;

namespace ImapToHttpDemo.Controllers
{
    [DataContract]
    public class MessageHeaderResponse
    {
        [DataMember(Name = "key")]
        public string Key { get; set; }
        [DataMember(Name = "value")]
        public string Value { get; set; }
    }
}