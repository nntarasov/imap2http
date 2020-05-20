using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ImapToHttpDemo.Controllers
{
    [DataContract]
    public class OperationResult
    {
        [DataMember(Name = "success")]
        public bool Success { get; set; }
    }
    
    [DataContract]
    public class AuthorizeRequest
    {
        [DataMember(Name = "login")]
        public string Login { get; set; }
        [DataMember(Name = "password")]
        public string Password { get; set; }
    }

    [ApiController]
    [Route("imap")]
    public class DemoController : ControllerBase
    {
        private readonly ILogger<DemoController> _logger;

        public DemoController(ILogger<DemoController> logger)
        {
            _logger = logger;
        }

        [Route("authorize")]
        public OperationResult Authorize([FromBody] AuthorizeRequest req)
        {
            if (req.Login == "ntarasov" && "123456".GetHashString() == req.Password)
            {
                return new OperationResult
                {
                    Success = true
                };
            }

            return new OperationResult
            {
                Success = false
            };
        }

        [DataContract]
        public class AllDirectoriesResponse
        {
            [DataMember(Name = "directories")] public DirectoryResponse[] Directories { get; set; }
        }

        [DataContract]
        public class DirectoryResponse
        {
            [DataMember(Name = "id")] public int Id { get; set; }
            [DataMember(Name = "name")] public string Name { get; set; }
        }

        [Route("directories/get")]
        public AllDirectoriesResponse GetDirectories()
        {
            return new AllDirectoriesResponse
            {
                Directories = new[]
                {
                    new DirectoryResponse
                    {
                        Id = 1,
                        Name = "INBOX"
                    }
                }
            };
        }

        [DataContract]
        public class DirectoryIdRequest
        {
            [DataMember(Name = "id")] public int Id { get; set; }
        }

        [Route("directory/details")]
        public object GetDirectoryDetails(DirectoryIdRequest request)
        {
            if (request?.Id == 1)
            {
                return new
                {
                    exist_count = 2,
                    flags = new[] {"\\Deleted", "\\Unseen", "\\Important"},
                    recent_count = 2,
                    uidnext = 3,
                    uidvalidity = 3,
                    unseen_count = 2
                };
            }

            return new object();
        }

        public class MessageRequest
        {
            public int directory_id { get; set; }
            public int uid { get; set; }
        }

        [Route("message/exists")]
        public OperationResult HasMessageExists(MessageRequest request)
        {
            if (request.directory_id == 1 && (request.uid == 1 || request.uid == 2))
            {
                return new OperationResult
                {
                    Success = true
                };
            }

            return new OperationResult
            {
                Success = false
            };
        }
        
        
        [DataContract]
        public class DirectoryUidsRequest
        {
            [DataMember(Name = "directory_id")]
            public int directory_id { get; set; }
            [DataMember(Name = "relative_ids")]
            public int[] relative_ids { get; set; }
        }
        
        [Route("directory/uids")]
        public object GetUids(DirectoryUidsRequest request)
        {
            if (request.directory_id != 1)
            {
                return new OperationResult
                {
                    Success = false
                };
            }
            return new {
                uids = request.relative_ids?.Where(i => i == 1 || i == 2)
                .Select(i => i == 1 ? 2 : 1)
                .ToArray()
            };
        }
        
        
        [DataContract]
        public class MessageHeaderResponse
        {
            [DataMember(Name = "key")]
            public string Key { get; set; }
            [DataMember(Name = "value")]
            public string Value { get; set; }
        }
    
        [DataContract]
        public class MessageResponse
        {
            [DataMember(Name = "headers")]
            public MessageHeaderResponse[] Headers { get; set; }
            [DataMember(Name = "body")]
            public string Body { get; set; }
            [DataMember(Name = "date")]
            public DateTime Date { get; set; }
        }


        [Route("message/get")]
        public object GetMessage(MessageRequest request)
        {
            var headers1 = new Dictionary<string, string>
            {
                {"From", "Nikita Tarasov <mail@ntarasov.ru>"},
                {"To", "mail <mail@ntarasov.ru>"},
                {"Subject", "The test letter!"},
                {"Date", "Sun, 26 Apr 2020 20:07:35 +0300"},
                {"Message-Id", "<1255x51xxxx587920843@mail.yandex.ru>"},
                {"Content-Transfer-Encoding", "8bit"},
                {"Content-Type", "text/html; charset=utf-8"},
                {"MIME-Version", "1.0"}
            };

            var headers2 = new Dictionary<string, string>
            {
                {"From", "Nikita Tarasov <mail@ntarasov.ru>"},
                {"To", "Vasuas <mail@ntarasov.ru>"},
                {"Subject", "TSecong letter"},
                {"Date", "Sun, 26 Apr 2020 21:07:35 +0300"},
                {"Message-Id", "<12920843@mail.yandex.ru>"},
                {"Content-Transfer-Encoding", "8bit"},
                {"Content-Type", "text/html; charset=utf-8"},
                {"MIME-Version", "1.0"}
            };
            
            if (request.uid == 1)
            {
                return new MessageResponse
                {
                    Body = "Hello, world",
                    Date = DateTime.Now,
                    Headers = headers1.Select(h => new MessageHeaderResponse
                    {
                        Key = h.Key,
                        Value = h.Value
                    }).ToArray()
                };
            }

            return new MessageResponse
            {
                Body = "Hello,<b> world</b>",
                Date = DateTime.Now,
                Headers = headers2.Select(h => new MessageHeaderResponse
                {
                    Key = h.Key,
                    Value = h.Value
                }).ToArray()
            };
        }
    }
}