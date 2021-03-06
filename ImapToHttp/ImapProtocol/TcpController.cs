using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using ImapProtocol.Contracts;
using ImapToHttpCore;
using ImapToHttpCore.Contracts;

namespace ImapProtocol
{
    public class TcpController : ITcpController
    {
        private readonly ILogger _logger = LoggerFactory.GetLogger();
        
        private const int BufferLength = 1024;
        private readonly TcpListener _tcpListener;
        private readonly IEntityProviderFactory _entityProviderFactory;
        
        public class TcpSessionContext : ITcpSessionContext
        {
            public int ThreadId { get; set; }
            public TcpClient TcpClient { get; set; }
            public NetworkStream NetworkStream { get; set; }
            public bool IsSessionAlive { get; set; }
        }

        public TcpController(string addressString, int port, IEntityProviderFactory entityProviderFactory)
        {
            _entityProviderFactory = entityProviderFactory;
            var address = IPAddress.Parse(addressString);
            _tcpListener = new TcpListener(address, port);
        }

        public bool ReadNext(ITcpSessionContext tcpContext, ITcpCommandController sender)
        {
            if (!(tcpContext.NetworkStream?.CanRead ?? false))
            {
                tcpContext.IsSessionAlive = false;
                tcpContext.NetworkStream?.Dispose();
                return false;
            }

            var recvBuffer = new byte[BufferLength];
            while (!tcpContext.NetworkStream.DataAvailable)
            {
                Thread.Yield();
            }
            var count = tcpContext.NetworkStream.Read(recvBuffer, 0, recvBuffer.Length);

            //string rr = recvBuffer.Take(count).Select(c => c.ToString()).Aggregate((a, b) => a + " " + b);
            //LoggerFactory.GetLogger().Print(rr);
            
            sender.OnTcpReceive( recvBuffer, 0, count);
            return true;
        }

        public bool Write(ITcpSessionContext tcpContext, byte[] data)
        {
            if (!(tcpContext.NetworkStream?.CanWrite ?? false))
            {
                tcpContext.IsSessionAlive = false;
                tcpContext.NetworkStream?.Dispose();
                return false;
            }
            tcpContext.NetworkStream.Write(data);
            return true;
        }

        public void Listen()
        {
            _tcpListener.Start();
            var recvBuffer = new byte[BufferLength];
            
            while (true)
            {
                _logger.Print("Waiting for a connection...");
                // Perform a blocking call to accept requests.
                // You could also use server.AcceptSocket() here.
                var client = _tcpListener.AcceptTcpClient();

                var clientThread = new Thread(StartClient);
                clientThread.Start(client);
            }
        }

        private void StartClient(object clientObject)
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            
            _logger.Print($"[{threadId}] Connection establised");
            var client = (TcpClient) clientObject;
            // Get a stream object for reading and writing
            NetworkStream stream = client.GetStream();
            
            var tcpContext = new TcpSessionContext
            {
                ThreadId = threadId,
                NetworkStream = stream,
                TcpClient = client,
                IsSessionAlive = true
            };
            var commandController = new TcpCommandController(this, tcpContext, _entityProviderFactory);
            
            commandController.OnTcpConnect();
            // Shutdown and end connection
            Console.WriteLine($"[{threadId}] Connection closed");
            client.Close();
        }

        public void Dispose()
        {
            _tcpListener.Stop();
        }
    }
}