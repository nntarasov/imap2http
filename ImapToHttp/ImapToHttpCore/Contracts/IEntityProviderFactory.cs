using ImapToHttpCore.EntityProviders;

namespace ImapProtocol.Contracts
{
    public interface IEntityProviderFactory
    {
        IEntityProvider Initiate();
    }
}