using System;
using System.Text.RegularExpressions;

namespace ImapProtocol.Contracts
{
    public class ImapCommand
    {
        public string Command { get; set; }
        public string Tag { get; set; }
        public string Args { get; set; }

        public ImapCommand(string command)
        {
            var match = Regex.Match(command, @"^(?<tag>[^ ]*) (?<cmd>[\w]+)[ ]*(?<args>.*)");
            if (!match.Success)
            {
                throw new ArgumentException(nameof(command));
            }
            
            Tag = match.Groups["tag"].Value;
            Command = match.Groups["cmd"].Value;
            Args = match.Groups["args"].Value;
        }

        public ImapCommand()
        {
        }
    }
}