using System;
using HttpProtocol.Contracts;
using ImapToHttpCore;
using ImapToHttpCore.Contracts;
using ServiceStack;

namespace HttpProtocol
{
    public class HttpClient : IHttpClient
    {
        private ILogger Logger = LoggerFactory.GetLogger();
        
        private string _host;

        public HttpClient(string host)
        {
            _host = host;
        }
        
        public string Request(string path, string jsonData)
        {
            string url = _host.TrimEnd('/') + '/' + path.TrimStart('/');
            try
            {
                Logger.Print(0, MessageType.Debug, $"Request url: {url}");
                return url.PostJsonToUrl(jsonData);
            }
            catch (Exception ex)
            {
                Logger.Print(0, MessageType.Error, 
                    $"Server returned status: {ex.GetStatus()}, response:{ex.GetResponseBody()}");
                return null;
            }
        }

        public string Request(string path, object request)
        {
            return Request(path, request.ToJson());
        }
    }
}