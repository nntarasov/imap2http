using System.Runtime.Serialization;

namespace HttpProtocol.Requests
{
    [DataContract]
    public class DirectoryIdRequest : AuthorizedBase
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
    }
}