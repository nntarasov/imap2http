using System;

namespace ImapProtocol.Contracts
{
    public interface ITcpController : IDisposable
    {
        void Listen();
        bool Write(byte[] data);
        bool ReadNext();
    }
}