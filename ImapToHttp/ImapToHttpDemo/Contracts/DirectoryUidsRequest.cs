using System.Runtime.Serialization;

namespace ImapToHttpDemo.Controllers
{
    [DataContract]
    public class DirectoryUidsRequest
    {
        [DataMember(Name = "directory_id")]
        public int directory_id { get; set; }
        [DataMember(Name = "relative_ids")]
        public int[] relative_ids { get; set; }
    }
}