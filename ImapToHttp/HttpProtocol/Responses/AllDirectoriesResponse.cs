using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HttpProtocol.Responses
{
    [DataContract]
    public class AllDirectoriesResponse
    {
        [DataMember(Name = "directories")]
        public DirectoryResponse[] Directories { get; set; }
    }
}