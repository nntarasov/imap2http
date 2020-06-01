using System.Runtime.Serialization;

namespace ImapToHttpDemo.Controllers
{
    [DataContract]
    public class OperationResult
    {
        [DataMember(Name = "success")]
        public bool Success { get; set; }
    }
}