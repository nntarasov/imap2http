using System.Runtime.Serialization;

namespace HttpProtocol.Requests
{
    [DataContract]
    public class AuthorizeRequest
    {
        [DataMember(Name = "login")]
        public string Login { get; set; }
        [DataMember(Name = "password")]
        public string PasswordHash { get; set; }
    }
}