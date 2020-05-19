using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HttpProtocol.Responses
{
    [DataContract]
    public class AllDirectoriesResponse
    {
        [DataMember(Name = "directories")]
        public IDictionary<int, string> Directories { get; set; }
    }
}