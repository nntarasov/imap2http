using System.Runtime.Serialization;

namespace HttpProtocol.Requests
{
    [DataContract]
    public class DirectoryRenameRequest : AuthorizedBase
    {
        [DataMember(Name = "new_name")]
        public string NewName { get; set; }
        [DataMember(Name = "id")]
        public int Id { get; set; }
    }
}