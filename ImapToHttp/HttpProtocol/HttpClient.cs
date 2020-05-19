using ServiceStack;

namespace HttpProtocol
{
    public class HttpClient
    {
        private string _host;
        
        public HttpClient(string host)
        {
            _host = host;
        }
        
        public string Request(string path, string jsonData)
        {
            string url = _host.TrimEnd('/') + '/' + path;
            return url.PostJsonToUrl(jsonData);
        }
    }
}