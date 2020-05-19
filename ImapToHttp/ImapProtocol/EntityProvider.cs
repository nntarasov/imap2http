using System;
using System.Collections.Generic;
using System.Linq;
using ImapProtocol.Contracts;
using ImapProtocol.Contracts.EntityProviders;
using ImapProtocol.Entities;
using ImapProtocol.ImapStateControllers;

namespace ImapProtocol
{
    public class EntityProvider : IEntityProvider, IMailProvider, IDirectoryProvider, IUserProvider
    {
        private int? _currentDirectoryId;
        
        public IMailProvider MailProvider => this;
        public IDirectoryProvider DirectoryProvider => this;
        public IUserProvider UserProvider => this;
        public bool SwitchDirectory(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                _currentDirectoryId = null;
                return true;
            }

            name = name.ToUpper();
            
            var directories = GetAllDirectories();
            if (!directories.Values.Contains(name))
            {
                return false;
            }

            _currentDirectoryId = directories
                .First(p => p.Value == name)
                .Key;
            return true;
        }

        public bool CreateDirectory(string name)
        {
            return true;
        }

        public bool DeleteDirectory(string name)
        {
            return true;
        }

        public bool RenameDirectory(string name, string newName)
        {
            return true;
        }

        public int[] Expunge()
        {
            return new int[0];
        }

        public IDictionary<int, string> GetAllDirectories()
        {
            return new Dictionary<int, string>
            {
                { 1, "INBOX" }
            };
        }

        public DirectoryDetails GetDirectoryDetails(string directory)
        {
            return new DirectoryDetails
            {
                ExistCount = 2,
                Flags = new[] {"\\Deleted", "\\Unseen", "\\Important"},
                RecentCount = 2,
                UidNext = 3,
                UidValidity = 3,
                UnseenCount = 2
            };
        }

        public Mail GetMessage(int uid)
        {
            LoggerFactory.GetLogger().Print(uid.ToString());
            if (uid != 1 && uid != 2)
            {
                return null;
            }

            if (uid == 1)
            {
                return new Mail
                {
                    Body = "Hello, world",
                    Date = DateTime.Now,
                    UId = 1,
                    Headers = new Dictionary<string, string>
                    {
                        {"From".ToUpper(), "Nikita Tarasov <mail@ntarasov.ru>"},
                        {"To".ToUpper(), "mail <mail@ntarasov.ru>"},
                        {"Subject".ToUpper(), "The test letter!"},
                        {"Date".ToUpper(), "Sun, 26 Apr 2020 20:07:35 +0300"},
                        {"Message-Id".ToUpper(), "<1255x51xxxx587920843@mail.yandex.ru>"},
                        {"Content-Transfer-Encoding".ToUpper(), "8bit"},
                        {"Content-Type".ToUpper(), "text/html; charset=utf-8"},
                        {"MIME-Version".ToUpper(), "1.0"}
                    }
                };
            }

            return new Mail
            {
                Body = "Hello,<b> world</b>",
                Date = DateTime.Now,
                UId = 2,
                Headers = new Dictionary<string, string>
                {
                    {"From", "Nikita Tarasov <mail@ntarasov.ru>"},
                    {"To", "Vasuas <mail@ntarasov.ru>"},
                    {"Subject", "TSecong letter"},
                    {"Date", "Sun, 26 Apr 2020 21:07:35 +0300"},
                    {"Message-Id", "<12920843@mail.yandex.ru>"},
                    {"Content-Transfer-Encoding", "8bit"},
                    {"Content-Type", "text/html; charset=utf-8"},
                    {"MIME-Version", "1.0"}
                }
            };
        }

        public bool StoreFlags(int[] uids, string[] flags, StoreFlagOperation operation)
        {
            return true;
        }

        public int[] GetUids(params int[] relativeIds)
        {
            return relativeIds.Where(i => i == 1 || i == 2)
                .Select(i => i == 1 ? 2 : 1)
                .ToArray();
        }

        public bool HasMessage(int uid)
        {
            return uid == 1 || uid == 2;
        }

        public bool Copy(string destination, params int[] uids)
        {
            return false;
        }

        public User Current { get; private set; }
        public bool Authorize(string login, string password)
        {
            Current = new User
            {
                Login = login
            };
            return true;
        }
    }
}