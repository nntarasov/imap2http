using System.Runtime.Serialization;

namespace HttpProtocol.Responses
{
    [DataContract]
    public class OperationResponse
    {
        [DataMember(Name = "success")]
        public bool Success { get; set; }
    }
}