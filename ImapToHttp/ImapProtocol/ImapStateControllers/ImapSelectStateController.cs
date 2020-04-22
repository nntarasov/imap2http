using System.Linq;
using System.Text.RegularExpressions;
using ImapProtocol.Contracts;

namespace ImapProtocol.ImapStateControllers
{
    public class ImapSelectStateController : ImapStateController
    {
        public override ImapState State { get; } = ImapState.Selected;
        protected override bool RunInternal(ImapCommand cmd)
        {
            var mailboxes = new[]
            {
                "INBOX"
            };

            var match = Regex.Match(cmd.Args, @"^(?<box>\w+)");

            if (!match.Success || string.IsNullOrWhiteSpace(match.Groups["box"].Value))
            {
                Context.CommandProvider.Write($"{cmd.Tag} BAD");
                return true;
            }

            var mailboxArg = match.Groups["box"].Value;
            if (!mailboxes.Contains(mailboxArg))
            {
                Context.CommandProvider.Write($"{cmd.Tag} NO\r\n");
                return true;
            }
            
            Context.CommandProvider.Write(@"* FLAGS (\\Answered \\Flagged \\Deleted \\Seen \\Draft)\r\n");
            Context.CommandProvider.Write("* 0 EXISTS\r\n");
            Context.CommandProvider.Write("* 0 RECENT\r\n");
            Context.CommandProvider.Write($"{cmd.Tag} OK SELECT\r\n");
            return true;
        }
    }
}