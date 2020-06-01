using System.Runtime.Serialization;
using ImapToHttpCore.Contracts;

namespace HttpProtocol.Requests
{
    [DataContract]
    public class MessageStoreFlagsRequest : AuthorizedBase
    {
        [DataMember(Name = "flags")]
        public string[] Flags { get; set; }
        [DataMember(Name = "directory_id")]
        public int DirectoryId { get; set; }
        [DataMember(Name = "uids")]
        public int[] Uids { get; set; }
        
        [DataMember(Name = "flag_op")]
        public StoreFlagOperation Operation { get; set; }
    }
}