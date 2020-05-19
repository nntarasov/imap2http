using ImapProtocol.Contracts;
using ImapToHttpCore.EntityProviders;

namespace HttpProtocol
{
    public class HttpEntityProviderFactory : IEntityProviderFactory
    {
        public IEntityProvider Initiate()
        {
            return new EntityProvider();
        }
    }
}