using System.Linq;
using System.Text.RegularExpressions;
using ImapProtocol.Contracts;

namespace ImapProtocol.ImapStateControllers
{
    public class ImapStatusStateController : ImapStateController
    {
        public override ImapState State { get; } = ImapState.Status;
        protected override bool RunInternal(ImapCommand cmd)
        {
            var match = Regex.Match(cmd.Args, @"^(?<box>\w+)");

            if (!match.Success || string.IsNullOrWhiteSpace(match.Groups["box"].Value))
            {
                Context.CommandProvider.Write($"{cmd.Tag} BAD\r\n");
                return true;
            }

            var mailboxes = Context.EntityProvider.GetAllDirectories();

            var mailboxArg = match.Groups["box"].Value.Trim();
            if (!mailboxes.Contains(mailboxArg))
            {
                Context.CommandProvider.Write($"{cmd.Tag} NO\r\n");
                return true;
            }

            var details = Context.EntityProvider.GetDirectoryDetails(mailboxArg);
            if (details == null)
            {
                Context.CommandProvider.Write($"{cmd.Tag} NO\r\n");
                return true;
            }

            var flagsString = (details.Flags?.Any() ?? false) ? 
                details.Flags.Aggregate((a, b) => a + " " + b) : 
                string.Empty;
            Context.CommandProvider.Write($"* FLAGS ({flagsString})\r\n");
            Context.CommandProvider.Write($"* {details.ExistCount} EXISTS\r\n");
            Context.CommandProvider.Write($"* {details.RecentCount} RECENT\r\n");
            Context.CommandProvider.Write($"* OK [UIDVALIDITY {details.UidValidity}] UIDVALIDITY\r\n");
            Context.CommandProvider.Write($"* OK [UIDNEXT {details.UidNext}] UIDNEXT\r\n");
            Context.CommandProvider.Write($"* OK [UNSEEN {details.UnseenCount}] UNSEEN\r\n");
            Context.CommandProvider.Write($"{cmd.Tag} OK STATUS\r\n");
          
            return true;
        }
    }
}