using System.Runtime.Serialization;

namespace ImapToHttpDemo.Controllers
{
    [DataContract]
    public class AuthorizeRequest
    {
        [DataMember(Name = "login")]
        public string Login { get; set; }
        [DataMember(Name = "password")]
        public string Password { get; set; }
    }
}