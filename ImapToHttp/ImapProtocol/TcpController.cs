using System;
using System.Net;
using System.Net.Sockets;
using ImapProtocol.Contracts;

namespace ImapProtocol
{
    public class TcpController : ITcpController
    {
        private const int BufferLength = 1024;
        private readonly TcpListener _tcpListener;
        private TcpSessionContext _tcpContext;
        public class TcpSessionContext
        {
            public TcpClient TcpClient { get; set; }
            public NetworkStream NetworkStream { get; set; }
            public ITcpCommandController CommandController { get; set; }
        }

        public TcpController(string addressString, int port)
        {
            var address = IPAddress.Parse(addressString);
            _tcpListener = new TcpListener(address, port);
        }

        public bool ReadNext()
        {
            if (!(_tcpContext.NetworkStream?.CanRead ?? false))
            {
                return false;
            }

            var recvBuffer = new byte[BufferLength];
            var count = _tcpContext.NetworkStream.Read(recvBuffer, 0, recvBuffer.Length);
            _tcpContext.CommandController.OnTcpReceive( recvBuffer, 0, count);
            return true;
        }

        public bool Write(byte[] data)
        {
            if (!(_tcpContext.NetworkStream?.CanWrite ?? false))
            {
                return false;
            }
            _tcpContext.NetworkStream.Write(data);
            return true;
        }

        public void Listen()
        {
            _tcpListener.Start();
            var recvBuffer = new byte[BufferLength];
            while (true)
            {
                Console.WriteLine("Waiting for a connection...");
                // Perform a blocking call to accept requests.
                // You could also use server.AcceptSocket() here.
                var client = _tcpListener.AcceptTcpClient();
                Console.WriteLine($"Connection establised");

                // Get a stream object for reading and writing
                NetworkStream stream = client.GetStream();
                _tcpContext = new TcpSessionContext
                {
                    NetworkStream = stream,
                    TcpClient = client,
                    CommandController = new TcpCommandController(this)
                };

                _tcpContext.CommandController.OnTcpConnect();

                /*
                // Loop to receive all the data sent by the client.
                int count;
                while ((count = stream.Read(recvBuffer, 0, recvBuffer.Length)) != 0)
                {
                    _tcpContext.CommandController.OnTcpReceive(recvBuffer, 0, count);
                }*/

                // Shutdown and end connection
                Console.WriteLine("Connection closed");
                client.Close();
            }
        }

        public void Dispose()
        {
            _tcpListener.Stop();
        }
    }
}