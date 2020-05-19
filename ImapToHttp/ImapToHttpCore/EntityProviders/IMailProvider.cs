using ImapToHttpCore.Contracts;
using ImapToHttpCore.Entities;

namespace ImapToHttpCore.EntityProviders
{
    public interface IMailProvider
    {
        Mail GetMessage(int uid);
        bool StoreFlags(int[] uids, string[] flags, StoreFlagOperation operation);
    }
}