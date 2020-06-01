using System.Runtime.Serialization;

namespace HttpProtocol.Requests
{
    [DataContract]
    public class MessageExistsRequest : AuthorizedBase
    {
        [DataMember(Name = "directory_id")]
        public int DirectoryId { get; set; }
        [DataMember(Name = "uid")]
        public int Uid { get; set; }
    }
}