using System.Runtime.Serialization;

namespace HttpProtocol.Requests
{
    [DataContract]
    public class DirectoryNameRequest : AuthorizedBase
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}