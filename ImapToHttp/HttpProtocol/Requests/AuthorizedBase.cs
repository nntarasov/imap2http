using System.Runtime.Serialization;

namespace HttpProtocol.Requests
{
    [DataContract]
    public abstract class AuthorizedBase
    {
        [DataMember(Name = "who")]
        public string Who { get; set; }
    }
}