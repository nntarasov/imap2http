using HttpProtocol.Contracts;
using ImapProtocol.Contracts;
using ImapToHttpCore.EntityProviders;

namespace HttpProtocol
{
    public class HttpEntityProviderFactory : IEntityProviderFactory
    {
        private IHttpClient _httpClient;
        
        public HttpEntityProviderFactory(string serviceUrl)
        {
            _httpClient = new HttpClient(serviceUrl);
        }
        
        public IEntityProvider Initiate()
        {
            return new HttpEntityProvider(_httpClient);
        }
    }
}