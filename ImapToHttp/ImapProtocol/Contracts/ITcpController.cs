using System;
using System.Threading.Tasks;

namespace ImapProtocol.Contracts
{
    public interface ITcpController : IDisposable
    {
        void Listen();
        bool Write(ITcpSessionContext tcpContext, byte[] data);
        bool ReadNext(ITcpSessionContext tcpContext, ITcpCommandController sender);
    }
}