using System.Runtime.Serialization;

namespace HttpProtocol.Requests
{
    [DataContract]
    public class CopyRequest
    {
        [DataMember(Name = "directory_from_id")]
        public int DirectoryFromId { get; set; }
        [DataMember(Name = "directory_to_id")]
        public int DirectoryToId { get; set; }
        [DataMember(Name = "uids")]
        public int[] UIds { get; set; }
    }
}