using ImapProtocol.Contracts;
using ImapProtocol.Contracts.EntityProviders;
using ImapProtocol.Entities;
using ImapProtocol.ImapStateControllers;

namespace ImapProtocol
{
    public class EntityProvider : IEntityProvider, IMailProvider, IDirectoryProvider, IUserProvider
    {
        public IMailProvider MailProvider => this;
        public IDirectoryProvider DirectoryProvider => this;
        public IUserProvider UserProvider { get; set; }
        public bool CreateDirectory(string name)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteDirectory(string name)
        {
            throw new System.NotImplementedException();
        }

        public bool RenameDirectory(string name, string newName)
        {
            throw new System.NotImplementedException();
        }

        public int[] Expunge()
        {
            throw new System.NotImplementedException();
        }

        public string[] GetAllDirectories()
        {
            throw new System.NotImplementedException();
        }

        public DirectoryDetails GetDirectoryDetails(string directory)
        {
            throw new System.NotImplementedException();
        }

        public Mail GetMessage(int uid)
        {
            throw new System.NotImplementedException();
        }

        public bool StoreFlags(int[] uids, string[] flags, StoreFlagOperation operation)
        {
            throw new System.NotImplementedException();
        }

        public int[] GetUids(params int[] relativeIds)
        {
            throw new System.NotImplementedException();
        }

        public bool HasMessage(int uid)
        {
            throw new System.NotImplementedException();
        }

        public bool Copy(string destination, params int[] uids)
        {
            throw new System.NotImplementedException();
        }

        public string GetLogin()
        {
            throw new System.NotImplementedException();
        }

        public User Current { get; }
        public bool Authorize(string login, string password)
        {
            throw new System.NotImplementedException();
        }
    }
}