using System.Collections.Generic;
using ImapToHttpCore.Entities;

namespace ImapToHttpCore.EntityProviders
{
    public interface IEntityProvider
    {
        IMailProvider MailProvider { get; }
        IDirectoryProvider DirectoryProvider { get; }
        IUserProvider UserProvider { get; }

        bool SwitchDirectory(string name);

        bool CreateDirectory(string name);
        bool DeleteDirectory(string name);
        bool RenameDirectory(string name, string newName);
        int[] Expunge();

        IDictionary<int, string> GetAllDirectories();
        DirectoryDetails GetDirectoryDetails(string directory);
    }
}