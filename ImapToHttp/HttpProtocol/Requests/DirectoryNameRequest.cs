using System.Runtime.Serialization;

namespace HttpProtocol.Requests
{
    [DataContract]
    public class DirectoryNameRequest
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}