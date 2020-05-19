using System.Runtime.Serialization;

namespace HttpProtocol.Responses
{
    [DataContract]
    public class DirectoryDetailsResponse
    {
        [DataMember(Name = "flags")]
        public string[] Flags { get; set; }
        [DataMember(Name = "exist_count")]
        public int ExistCount { get; set; }
        [DataMember(Name = "recent_count")]
        public int RecentCount { get; set; }
        [DataMember(Name = "unseen_count")]
        public int UnseenCount { get; set; }
        [DataMember(Name = "uidvalidity")]
        public int UidValidity { get; set; }
        [DataMember(Name = "uidnext")]
        public int UidNext { get; set; }
    }
}