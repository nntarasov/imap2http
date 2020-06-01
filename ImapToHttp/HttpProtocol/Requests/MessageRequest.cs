using System.Runtime.Serialization;

namespace HttpProtocol.Requests
{
    [DataContract]
    public class MessageRequest : AuthorizedBase
    {
        [DataMember(Name = "uid")]
        public int Uid { get; set; }
        [DataMember(Name = "directory_id")]
        public int DirectoryId { get; set; }
    }
}