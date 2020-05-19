using System.Runtime.Serialization;

namespace HttpProtocol.Responses
{
    [DataContract]
    public class DirectoryExpungeResponse
    {
        [DataMember(Name = "message_ids")]
        public int[] MessageIds { get; set; }
    }
}