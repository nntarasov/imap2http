using System.Runtime.Serialization;

namespace HttpProtocol.Responses
{
    [DataContract]
    public class DirectoryUidsResponse
    {
        [DataMember(Name = "uids")]
        public int[] Uids { get; set; }
    }
}