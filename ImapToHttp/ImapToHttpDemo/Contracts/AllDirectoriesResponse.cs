using System.Runtime.Serialization;

namespace ImapToHttpDemo.Controllers
{
    [DataContract]
    public class AllDirectoriesResponse
    {
        [DataMember(Name = "directories")] public DirectoryResponse[] Directories { get; set; }
    }
}