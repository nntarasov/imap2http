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
            [DataMember(Name = "directories")]
            public IDictionary Directories { get; set; }
        }

        [Route("directories/get")]
        public IActionResult GetDirectories()
        {

            return Content(@"{""directories"": {1: ""INBOX""} }", "application/json");
        }
    }
}