using ImapProtocol.Entities;

namespace ImapProtocol.Contracts.EntityProviders
{
    public interface IUserProvider
    {
        User Current { get; }
        bool Authorize(string login, string password);
    }
}