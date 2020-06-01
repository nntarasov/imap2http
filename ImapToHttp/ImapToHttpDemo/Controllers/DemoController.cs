using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ImapToHttpDemo.Controllers
{
    [ApiController]
    [Route("imap")]
    public class DemoController : ControllerBase
    {
        private readonly ILogger<DemoController> _logger;

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

        [Route("directories/get")]
        public AllDirectoriesResponse GetDirectories()
        {
            var flatten = new List<DirectoryData>();
            foreach (var directory in Storage.Directories.Values)
            {
                flatten.AddRange(GetFlatten(directory));
            }
            
            return new AllDirectoriesResponse
            {
                Directories = flatten.Select(f => new DirectoryResponse
                {
                    Id = f.Id,
                    Name = f.Name
                }).ToArray()
            };
        }

        [Route("directory/details")]
        public object GetDirectoryDetails(DirectoryIdRequest request)
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

        [Route("message/exists")]
        public OperationResult HasMessageExists(MessageRequest request)
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
        
        [Route("directory/uids")]
        public object GetUids(DirectoryUidsRequest request)
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
                    .Where(r => r-1 >= 0 && r-1 < ordered.Count)
                    .Select(r => ordered[r - 1].UId)
                    .ToArray()
            };

            return response;
        }

        [Route("message/get")]
        public object GetMessage(MessageRequest request)
        {
            if (!Storage.Directories.ContainsKey(request.directory_id) ||
                Storage.Directories[request.directory_id].Messages.All(m => m.UId != request.uid))
            {
                return Fail;
            }

            return Storage.Directories[request.directory_id]
                .Messages
                .First(m => m.UId == request.uid);
        }
    }
}