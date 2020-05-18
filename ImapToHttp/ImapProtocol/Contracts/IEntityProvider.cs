using ImapProtocol.Contracts.EntityProviders;

namespace ImapProtocol.Contracts
{
    public interface IEntityProvider
    {
        IMailProvider MailProvider { get; }
        IDirectoryProvider DirectoryProvider { get; }
        IUserProvider UserProvider { get; set; }

        bool CreateDirectory(string name);
        bool DeleteDirectory(string name);
        bool RenameDirectory(string name, string newName);
        int[] Expunge();

        string[] GetAllDirectories();
        DirectoryDetails GetDirectoryDetails(string directory);
    }
}