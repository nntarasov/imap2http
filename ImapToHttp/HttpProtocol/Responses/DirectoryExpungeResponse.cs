using System.Runtime.Serialization;

namespace HttpProtocol.Responses
{
    [DataContract]
    public class DirectoryExpungeResponse
    {
        [DataMember(Name = "relative_ids")]
        public int[] RelativeIds { get; set; }
    }
}