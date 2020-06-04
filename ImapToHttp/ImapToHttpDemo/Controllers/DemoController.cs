using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceStack;

namespace ImapToHttpDemo.Controllers
{
    [ApiController]
    [Route("imap")]
    public class DemoController : ControllerBase
    {
        private readonly ILogger<DemoController> _logger;

        private object StorageLock = new object();

        private OperationResult Success = new OperationResult
        {
            Success = true
        };

        private OperationResult Fail = new OperationResult
        {
            Success = false
        };

        public DemoController(ILogger<DemoController> logger)
        {
            _logger = logger;
        }

        [Route("authorize")]
        public OperationResult Authorize([FromBody] AuthorizeRequest req)
        {
            if (req.Login == "ntarasov" && "123456".GetHashString() == req.Password)
            {
                return Success;
            }

            return Fail;
        }

        private IList<DirectoryData> GetFlatten(DirectoryData data)
        {
            lock (StorageLock)
            {
                var list = new List<DirectoryData>();
                list.Add(data);

                if (data.Children?.Any() ?? false)
                {
                    foreach (var child in data.Children)
                    {
                        list.AddRange(GetFlatten(child));
                    }
                }
                return list;
            }
        }

        [Route("directories/get")]
        public AllDirectoriesResponse GetDirectories()
        {
            lock (StorageLock)
            {
                var flatten = new List<DirectoryData>();
                foreach (var directory in Storage.Directories.Values)
                {
                    flatten.AddRange(GetFlatten(directory));
                }

                var directories = new AllDirectoriesResponse
                {
                    Directories = flatten.Select(f => new DirectoryResponse
                    {
                        Id = f.Id,
                        Name = f.Name
                    }).ToArray()
                };

                Console.WriteLine("\ndirectories: \n");
                Console.WriteLine(directories.ToJson());

                return directories;
            }
        }

        [Route("directory/details")]
        public object GetDirectoryDetails(DirectoryIdRequest request)
        {
            lock (StorageLock)
            {
                if (request?.Id == null || !Storage.Directories.ContainsKey(request.Id))
                {
                    return Fail;
                }

                var dir = Storage.Directories[request.Id];
                return new
                {
                    exist_count = dir.Messages.Count,
                    flags = dir.AvailableFlags,
                    recent_count = dir.Messages.Count(m => m.Flags.Contains("\\Recent")),
                    uidnext = Storage.Directories.Values.SelectMany(d => d.Messages)
                                  .Max(m => m.UId) + 1,
                    uidvalidity = 1337,
                    unseen_count = dir.Messages.Count(m => m.Flags.Contains("\\Unseen")),
                };
            }
        }

        [Route("message/exists")]
        public OperationResult HasMessageExists(MessageRequest request)
        {
            lock (StorageLock)
            {
                if (request.directory_id == 1 && (request.uid == 1 || request.uid == 2))
                {
                    return Success;
                }

                if (!Storage.Directories.ContainsKey(request.directory_id)
                    || Storage.Directories[request.directory_id].Messages
                        .All(m => m.UId != request.uid))
                {
                    return Fail;
                }

                return Success;
            }
        }
        
        [Route("directory/uids")]
        public object GetUids(DirectoryUidsRequest request)
        {
            lock (StorageLock)
            {
                if (!Storage.Directories.ContainsKey(request.directory_id))
                {
                    return new OperationResult
                    {
                        Success = false
                    };
                }

                var ordered = Storage.Directories[request.directory_id].Messages
                    .OrderByDescending(m => m.Date)
                    .ToList();

                var response = new
                {
                    uids = request.relative_ids
                        .Where(r => r - 1 >= 0 && r - 1 < ordered.Count)
                        .Select(r => ordered[r - 1].UId)
                        .ToArray()
                };

                return response;
            }
        }

        [Route("message/get")]
        public object GetMessage(MessageRequest request)
        {
            lock (StorageLock)
            {
                if (!Storage.Directories.ContainsKey(request.directory_id) ||
                    Storage.Directories[request.directory_id].Messages.All(m => m.UId != request.uid))
                {
                    return Fail;
                }

                var message = Storage.Directories[request.directory_id]
                    .Messages
                    .First(m => m.UId == request.uid);

                message.Flags.RemoveAll(r => r == "\\Recent");

                Console.WriteLine(message.ToJson());
                return message;
            }
        }
        
        [Route("message/set_flags")]
        public object SetFlags(SetFlagsRequest request)
        {
            lock (StorageLock)
            {
                if (!Storage.SetOnce)
                {
                    Storage.SetOnce = true;
                    
                    Storage.Directories[1].Messages.Add(new MessageData
                     {
                         UId = 14,
                         Body = Storage.BodyHse1,
                         Flags = new[] { "\\Recent" }.ToList(),
                         Date = DateTime.Parse("Thu, 4 Jun 2020 16:10:18 +0000"),
                         Headers = new Dictionary<string, string>
                         {
                             {"From", "=?UTF-8?B?0JLRi9GI0LrQsCDQntC90LvQsNC50L0=?= <elearn@hse.ru>"},
                             {"To", "User <mail@ntarasov.ru>"},
                             {"Subject", @"=?UTF-8?B?0J/RgNC+0LrRgtC+0YDQuNC90LM6INCy0L4=?=
          =?UTF-8?B?0L/RgNC+0YHRiyDQuCDQvtGC0LLQtdGC0Ys=?="},
                             {"Date", "Thu, 4 Jun 2020 16:10:18 +0000"},
                             {"Message-Id", "<DQntC90xxLv@mail.yandex.ru>"},
                             {"Content-Transfer-Encoding", "quoted-printable"},
                             {"Content-Type", "text/html; charset=utf-8"},
                             {"MIME-Version", "1.0"}
                         }.Select(h => new MessageHeaderResponse
                         {
                             Key = h.Key,
                             Value = h.Value
                         }).ToArray()
                     });
                    
                    Storage.Directories[1].Messages.Add(new MessageData
                    {
                        UId = 16,
                        Body = Storage.BodyHse3,
                        Flags = new[] { "\\Recent" }.ToList(),
                        Date = DateTime.Parse("Thu, 4 Jun 2020 16:40:35 +0000"),
                        Headers = new Dictionary<string, string>
                        {
                            {"From", "=?UTF-8?B?0KbQtdC90YLRgCDRgNCw0LfQstC40YLQuA==?= =?UTF-8?B?0Y8g0LrQsNGA0YzQtdGA0Ysg0JLQqNCt?= <career@hse.ru>"},
                            {"To", "User <mail@ntarasov.ru>"},
                            {"Subject", @"=?UTF-8?B?0JzQvdC+0LPQviDQu9C40LTQtdGA0YHQutC40YUg0L8=?=
 =?UTF-8?B?0YDQvtCz0YDQsNC80Lwg0L3QsCDQu9C10YLQviArIA==?=
 =?UTF-8?B?0L7QvdC70LDQudC9LdGN0LrRgdC60YPRgNGB0LjRjyA=?=
 =?UTF-8?B?0L3QsCDQt9Cw0LLQvtC0IENvY2EtQ29sYQ==?="},
                            {"Date", "Thu, 4 Jun 2020 16:40:35 +0000"},
                            {"Message-Id", "<DQxeeeeetC90Lqeqweqv@mail.yandex.ru>"},
                            {"Content-Transfer-Encoding", "quoted-printable"},
                            {"Content-Type", "text/html; charset=utf-8"},
                            {"MIME-Version", "1.0"}
                        }.Select(h => new MessageHeaderResponse
                        {
                            Key = h.Key,
                            Value = h.Value
                        }).ToArray()
                    });
                }
                if (!Storage.Directories.ContainsKey(request.directory_id))
                {
                    return Fail;
                }

                var directory = Storage.Directories[request.directory_id];
                var messages = directory.Messages
                    .Where(m => request.uids.Contains(m.UId))
                    .ToList();

                Console.WriteLine(request.flag_op);
                switch (request.flag_op)
                {
                    case "Add":
                        Console.WriteLine(request.ToJson());
                        Console.WriteLine(messages.ToJson());
                        foreach (var message in messages)
                        {
                            var flags = request.flags
                                .Where(f => !message.Flags.Contains(f))
                                .ToList();
                            message.Flags.AddRange(flags);
                        }

                        Console.WriteLine(messages.ToJson());
                        return Success;
                    case "Delete":
                        foreach (var message in messages)
                        {
                            message.Flags.RemoveAll(f => request.flags.Contains(f));
                        }

                        return Success;
                    case "Replace":
                        foreach (var message in messages)
                        {
                            message.Flags = request.flags.ToList();
                        }

                        return Success;
                    default:
                        return Fail;
                }
            }
        }

        [Route("directory/copy_messages")]
        public object CopyMessages(CopyMessagesRequest request)
        {
            lock (StorageLock)
            {
                if (!Storage.Directories.ContainsKey(request.directory_from_id) ||
                    !Storage.Directories.ContainsKey(request.directory_to_id))
                {
                    return Fail;
                }

                var from = Storage.Directories[request.directory_from_id];
                var to = Storage.Directories[request.directory_to_id];

                var messages = from.Messages.Where(m => request.uids.Contains(m.UId))
                    .ToList();

                to.Messages.AddRange(messages.Select(m => new MessageData
                {
                    Body = m.Body,
                    Date = m.Date,
                    Flags = m.Flags.ToList(),
                    Headers = m.Headers,
                    UId = Storage.GetNextUid()
                }));
                return Success;
            }
        }

        [Route("directory/create")]
        public object CreateDirectory(DirectoryNameRequest request)
        {
            lock (StorageLock)
            {
                if (Storage.Directories
                    .Any(d => d.Value.Name.ToUpper().StartsWith(request.name.ToUpper())))
                {
                    return Fail;
                }

                if (request.name == "&BBIEMAQ2BD0EPgQ1-")
                {
                    Console.Write("KO");
                    Storage.Directories[1].Messages.Add(new MessageData
                    {
                        UId = 20,
                        Body = "Happy thesis!",
                        Flags = new[] { "\\Recent" }.ToList(),
                        Date = DateTime.Now,
                        Headers = new Dictionary<string, string>
                        {
                            {"From", "Nikita Tarasov <mail@ntarasov.ru>"},
                            {"To", "User <mail@ntarasov.ru>"},
                            {"Subject", "Letter to you"},
                            {"Date", "Sun, 05 May 2020 18:00:00 +0300"},
                            {"Message-Id", "<12920843@mail.yandex.ru>"},
                            {"Content-Transfer-Encoding", "8bit"},
                            {"Content-Type", "text/html; charset=utf-8"},
                            {"MIME-Version", "1.0"}
                        }.Select(h => new MessageHeaderResponse
                        {
                            Key = h.Key,
                            Value = h.Value
                        }).ToArray()
                    });
                }

                var id = Storage.Directories.Keys.Max() + 1;
                Storage.Directories.Add(id, new DirectoryData
                {
                    Id = id,
                    Name = request.name,
                    AvailableFlags = new[] {"\\Deleted", "\\Unseen", "\\Important"}.ToList(),
                });
                return Success;
            }
        }

        [Route("directory/delete")]
        public object DeleteDirectory(DirectoryIdRequest request)
        {
            lock (StorageLock)
            {
                if (!Storage.Directories.ContainsKey(request.Id))
                {
                    return Success;
                }

                Storage.Directories.Remove(request.Id);
                return Success;
            }
        }

        [Route("directory/rename")]
        public object RenameDirectory(DirectoryRenameRequest request)
        {
            lock (StorageLock)
            {
                if (!Storage.Directories.ContainsKey(request.id) ||
                    Storage.Directories.Any(d => d.Value.Name.ToUpper().StartsWith(request.new_name.ToUpper())))
                {
                    return Fail;
                }

                Storage.Directories[request.id].Name = request.new_name;
                return Success;
            }
        }


        [Route("directory/expunge")]
        public object Expunge(DirectoryIdRequest request)
        {
            lock (StorageLock)
            {
                if (!Storage.Directories.ContainsKey(request.Id))
                {
                    return Fail;
                }

                Storage.Directories[request.Id].Messages
                    .RemoveAll(m => m.Flags.Contains("\\Deleted"));
                return Success;
            }
        }
    }
}