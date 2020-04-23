using System.Threading.Tasks;

namespace ImapProtocol.Contracts
{
    public interface ITcpCommandProvider
    {
        void Write(string command);
        string Read();
    }
}