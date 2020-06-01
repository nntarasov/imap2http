using System;
using System.Collections.Generic;
using System.Linq;

namespace ImapToHttpDemo.Controllers
{
    public class DirectoryData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DirectoryData> Children { get; set; } = new List<DirectoryData>(); 
        public List<MessageData> Messages { get; set; } = new List<MessageData>();
        public List<string> AvailableFlags { get; set; } = new List<string>();
    }

    public class MessageData : MessageResponse
    {
        public int UId { get; set; }
        public List<string> Flags { get; set; } = new List<string>();
    }

    public static class Storage
    {
        public static IDictionary<int, DirectoryData> Directories { get; } =
            new Dictionary<int, DirectoryData>
            {
                {
                    1, new DirectoryData
                    {
                        Id = 1,
                        Name = "INBOX",
                        AvailableFlags = new[] {"\\Deleted", "\\Unseen", "\\Important"}.ToList(),
                    }
                }
            };



        static Storage()
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

            Directories[1].Messages.Add(new MessageData
                {
                    UId = 1,
                    Body = "Hello, world",
                    Date = DateTime.Now,
                    Headers = headers1.Select(h => new MessageHeaderResponse
                    {
                        Key = h.Key,
                        Value = h.Value
                    }).ToArray()
                }
            );

            Directories[1].Messages.Add(new MessageData
            {
                UId = 2,
                Body = "Hello,<b> world</b>",
                Date = DateTime.Now,
                Headers = headers2.Select(h => new MessageHeaderResponse
                {
                    Key = h.Key,
                    Value = h.Value
                }).ToArray()
            });
        }
    }
}