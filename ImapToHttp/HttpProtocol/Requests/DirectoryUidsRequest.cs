using System.Runtime.Serialization;

namespace HttpProtocol.Requests
{
    [DataContract]
    public class DirectoryUidsRequest : AuthorizedBase
    {
        [DataMember(Name = "directory_id")]
        public int DirectoryId { get; set; }
        [DataMember(Name = "relative_ids")]
        public int[] RelativeIds { get; set; }
    }
}