using System.Runtime.Serialization;

namespace ImapToHttpDemo.Controllers
{
    [DataContract]
    public class DirectoryIdRequest
    {
        [DataMember(Name = "id")] public int Id { get; set; }
    }
}