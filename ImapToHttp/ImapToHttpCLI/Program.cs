using HttpProtocol;
using ImapProtocol;
using ImapToHttpCore;

namespace ImapToHttpCLI
{
    public class Program
    {
        static void Main(string[] args)
        {
            LoggerFactory.SetLogger(new ConsoleLogger());
            var entityFactory = new HttpEntityProviderFactory("http://127.0.0.1:16085");
            var tcpController = new TcpController("127.0.0.1", 143, entityFactory);
            try
            {
                tcpController.Listen();
            }
            finally
            {
                tcpController.Dispose();
            }
        }
    }
}