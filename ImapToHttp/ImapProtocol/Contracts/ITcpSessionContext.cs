using System.Net.Sockets;

namespace ImapProtocol.Contracts
{
    public interface ITcpSessionContext
    {
        int ThreadId { get; set; }
        TcpClient TcpClient { get; set; }
        NetworkStream NetworkStream { get; set; }
        bool IsSessionAlive { get; set; }
    }
}