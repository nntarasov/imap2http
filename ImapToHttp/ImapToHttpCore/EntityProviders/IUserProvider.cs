using ImapToHttpCore.Entities;

namespace ImapToHttpCore.EntityProviders
{
    public interface IUserProvider
    {
        User Current { get; }
        bool Authorize(string login, string password);
    }
}