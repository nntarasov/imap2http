using System.Runtime.Serialization;

namespace HttpProtocol.Responses
{
    [DataContract]
    public class DirectoryResponse
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}