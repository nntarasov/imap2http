using System.Runtime.Serialization;

namespace HttpProtocol.Requests
{
    [DataContract]
    public class MessageRequest
    {
        [DataMember(Name = "uid")]
        public int Uid { get; set; }
    }
}