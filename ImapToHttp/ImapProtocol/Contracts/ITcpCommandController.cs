namespace ImapProtocol.Contracts
{
    public interface ITcpCommandController
    {
        void OnTcpReceive(byte[] bytes, int offset, int count);
        void OnTcpConnect();
    }
}