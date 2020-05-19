using System.Runtime.Serialization;

namespace HttpProtocol.Requests
{
    [DataContract]
    public class DirectoryIdRequest
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
    }
}