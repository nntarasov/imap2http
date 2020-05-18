using ImapProtocol.Entities;
using ImapProtocol.ImapStateControllers;

namespace ImapProtocol.Contracts.EntityProviders
{
    public interface IMailProvider
    {
        Mail GetMessage(int uid);
        bool StoreFlags(int[] uids, string[] flags, StoreFlagOperation operation);
    }
}