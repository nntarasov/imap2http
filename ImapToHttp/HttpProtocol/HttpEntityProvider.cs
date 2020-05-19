using System;
using System.Collections.Generic;
using System.Linq;
using HttpProtocol.Contracts;
using HttpProtocol.Requests;
using HttpProtocol.Responses;
using ImapToHttpCore;
using ImapToHttpCore.Contracts;
using ImapToHttpCore.Entities;
using ImapToHttpCore.EntityProviders;
using ServiceStack;

namespace HttpProtocol
{
    public class HttpEntityProvider : IEntityProvider, IMailProvider, IDirectoryProvider, IUserProvider
    {
        private int? _currentDirectoryId;
        private IHttpClient _httpClient;
        
        public HttpEntityProvider(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

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
            name = name?.Trim();
            if (string.IsNullOrWhiteSpace(name) || name.ToUpper() == "INBOX"
                || name.Contains(' '))
            {
                return false;
            }
            var response = _httpClient.Request("/imap/directory/create", new DirectoryCreateRequest
            {
                Name = name
            });
            return response.IsOperationSuccessful();
        }

        public bool DeleteDirectory(string name)
        {
            name = name?.Trim();
            if (string.IsNullOrWhiteSpace(name) || name.ToUpper() == "INBOX")
            {
                return false;
            }

            var id = GetDirectoryIdByName(name);
            if (!id.HasValue)
            {
                return false;
            }
            
            var response = _httpClient.Request("/imap/directory/delete", new DirectoryDeleteRequest
            {
                Id = id.Value
            });
            return response.IsOperationSuccessful();
        }

        public bool RenameDirectory(string name, string newName)
        {
            name = name?.Trim();
            newName = newName?.Trim();
            
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(newName) ||
                name.ToUpper() == "INBOX" || newName.ToUpper() == "INBOX" ||
                name.Contains(' ') || newName.Contains(' '))
            {
                return false;
            }

            var id = GetDirectoryIdByName(name);
            if (!id.HasValue)
            {
                return false;
            }
            
            var response = _httpClient.Request("/imap/directory/rename", new DirectoryRenameRequest
            {
                Id = id.Value,
                NewName = newName
            });
            return response.IsOperationSuccessful();
        }

        public int[] Expunge()
        {
            if (!_currentDirectoryId.HasValue)
            {
                return null;
            }
            
            var response = _httpClient.Request("/imap/directory/expunge", new DirectoryExpungeRequest
            {
                Id = _currentDirectoryId.Value
            });

            return response?.FromJson<DirectoryExpungeResponse>()?.MessageIds;
        }

        public IDictionary<int, string> GetAllDirectories()
        {
            /*return new Dictionary<int, string>
            {
                { 1, "INBOX" }
            };*/

            var response = _httpClient.Request("/imap/directories/get", new object());
            if (response == null)
            {
                return null;
            }

            return response.FromJson<AllDirectoriesResponse>()?.Directories;
        }

        public DirectoryDetails GetDirectoryDetails(string directory)
        {
            directory = directory?.Trim();
            if (string.IsNullOrWhiteSpace(directory))
            {
                return null;
            }
            var id = GetDirectoryIdByName(directory);
            if (!id.HasValue)
            {
                return null;
            }

            var response = _httpClient.Request("/imap/directory/details", new DirectoryDetailsRequest
            {
                Id = id.Value
            });

            var dto = response?.FromJson<DirectoryDetailsResponse>();
            if (dto == null)
            {
                return null;
            }

            return new DirectoryDetails
            {
                ExistCount = dto.ExistCount,
                Flags = dto.Flags,
                RecentCount = dto.RecentCount,
                UidNext = dto.UidNext,
                UidValidity = dto.UidValidity
            };
            /*return new DirectoryDetails
            {
                ExistCount = 2,
                Flags = new[] {"\\Deleted", "\\Unseen", "\\Important"},
                RecentCount = 2,
                UidNext = 3,
                UidValidity = 3,
                UnseenCount = 2
            };*/
        }

        public Mail GetMessage(int uid)
        {
            var response = _httpClient.Request("/imap/message/get", new MessageRequest
            {
                Uid = uid
            });

            var dto = response?.FromJson<MessageResponse>();
            if (dto == null)
            {
                return null;
            }

            var headers = dto.Headers ?? new Dictionary<string, string>();
            var headersCapitalized = headers.ToDictionary(h => h.Key.ToUpper(), h => h.Value);

            var defaultHeaders = new Dictionary<string, string>
            {
                {"Content-Transfer-Encoding".ToUpper(), "8bit"},
                {"Content-Type".ToUpper(), "text/html; charset=utf-8"}
            };

            foreach (var defaultHeader in defaultHeaders)
            {
                if (!headersCapitalized.ContainsKey(defaultHeader.Key))
                {
                    headersCapitalized.Add(defaultHeader.Key, defaultHeader.Value);
                }   
            }

            return new Mail
            {
                Body = dto.Body,
                Date = dto.Date,
                Headers = headersCapitalized,
                UId = uid
            };
            
            /*LoggerFactory.GetLogger().Print(uid.ToString());
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
            };*/
        }

        public bool StoreFlags(int[] uids, string[] flags, StoreFlagOperation operation)
        {
            if ((uids?.Length ?? 0) == 0 || (flags?.Length ?? 0) == 0 || 
                operation == StoreFlagOperation.None || !_currentDirectoryId.HasValue)
            {
                return false;
            }

            var response = _httpClient.Request("/imap/message/set_flags", new MessageStoreFlagsRequest
            {
                DirectoryId = _currentDirectoryId.Value,
                Uids = uids,
                Flags = flags,
                Operation = operation
            });

            return response.IsOperationSuccessful();
        }

        public int[] GetUids(params int[] relativeIds)
        {
            //return relativeIds.Where(i => i == 1 || i == 2)
            //    .Select(i => i == 1 ? 2 : 1)
            //    .ToArray();

            if ((relativeIds?.Length ?? 0) == 0 || !_currentDirectoryId.HasValue)
            {
                return null;
            }

            var response = _httpClient.Request("imap/directory/uids", new DirectoryUidsRequest
            {
                DirectoryId = _currentDirectoryId.Value,
                RelativeIds = relativeIds
            });
            return response?.FromJson<DirectoryUidsResponse>()?.Uids;
        }

        public bool HasMessage(int uid)
        {
            //return uid == 1 || uid == 2;
            if (!_currentDirectoryId.HasValue)
            {
                return false;
            }

            var response = _httpClient.Request("/imap/message/exists", new MessageExistsRequest
            {
                DirectoryId = _currentDirectoryId.Value,
                Uid = uid
            });

            return response.IsOperationSuccessful();
        }

        public bool Copy(string destination, params int[] uids)
        {
            destination = destination?.Trim();
            if (string.IsNullOrWhiteSpace(destination) || (uids?.Length ?? 0) == 0
                || !_currentDirectoryId.HasValue)
            {
                return false;
            }
            
            var destId = GetDirectoryIdByName(destination);
            if (!destId.HasValue)
            {
                return false;
            }

            var response = _httpClient.Request("/imap/directory/copy_messages/", new CopyRequest
            {
                DirectoryFromId = _currentDirectoryId.Value,
                DirectoryToId = destId.Value,
                UIds = uids
            });
            return response.IsOperationSuccessful();
        }

        public User Current { get; private set; }
        public bool Authorize(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }
            
            var response = _httpClient.Request("/imap/authorize", new AuthorizeRequest
            {
                Login = login.Trim(),
                PasswordHash = password.Trim().GetHashString()
            });

            if (!response.IsOperationSuccessful())
            {
                return false;
            }
            
            Current = new User
            {
                Login = login
            };
            return true;
        }

        private int? GetDirectoryIdByName(string name)
        {
            var directories = GetAllDirectories();
            if (directories == null)
            {
                return null;
            }
            
            var tuple = directories
                .Select(d => (d.Key, d.Value))
                .FirstOrDefault(n => n.Value == name);
            
            if (tuple == default)
            {
                return null;
            }

            return tuple.Key;
        }
    }
}