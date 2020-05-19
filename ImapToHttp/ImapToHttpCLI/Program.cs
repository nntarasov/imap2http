using System;
using System.IO;
using HttpProtocol;
using ImapProtocol;
using ImapToHttpCore;
using ImapToHttpCore.Contracts;
using ServiceStack;

namespace ImapToHttpCLI
{
    public class Program
    {
        static void Main(string[] args)
        {
            LoggerFactory.SetLogger(new ConsoleLogger());
            var logger = LoggerFactory.GetLogger();

            if (args.Length == 0)
            {
                logger.Print(0, MessageType.Error, 
                    "No configuration file");
                return;
            }

            Configuration configuration = null;
            try
            {
                var contents = File.ReadAllText(args[0]);
                configuration = contents.FromJson<Configuration>();
            }
            catch (Exception ex)
            {
                logger.Print(0, MessageType.Error, 
                    $"Error on reading config [{args[0]}] file");
                logger.Print(0, MessageType.Error, ex.Message);
                return;
            }

            if (string.IsNullOrWhiteSpace(configuration?.HttpBaseUrl))
            {
                logger.Print(0, MessageType.Error, 
                    $"http_base_url required");
                return;
            }
            
            logger.Print(0, MessageType.Info, "configuration successfully loaded");
            logger.Print(0, MessageType.Info, $"http_base_url loaded [{configuration.HttpBaseUrl}]");
            if (string.IsNullOrWhiteSpace(configuration.ImapAddress))
            {
                configuration.ImapAddress = "127.0.0.1";
                logger.Print(0, MessageType.Info, 
                    $"imap_address using default value [{configuration.ImapAddress}]");
            }
            else
            {
                logger.Print(0, MessageType.Info, 
                    $"imap_address loaded [{configuration.ImapAddress}]");
            }


            var entityFactory = new HttpEntityProviderFactory(configuration.HttpBaseUrl);
            var tcpController = new TcpController(configuration.ImapAddress, 143, entityFactory);
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