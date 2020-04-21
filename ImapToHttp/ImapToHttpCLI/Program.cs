using System;
using ImapProtocol;
using ImapProtocol.Contracts;

namespace ImapToHttpCLI
{
    public class Program
    {
        public class ConsoleLogger : ILogger
        {
            public void Print(string message)
            {
                Console.WriteLine(message);
            }
        }
        
        static void Main(string[] args)
        {
            LoggerFactory.SetLogger(new ConsoleLogger());
            var tcpController = new TcpController("127.0.0.1", 143);
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