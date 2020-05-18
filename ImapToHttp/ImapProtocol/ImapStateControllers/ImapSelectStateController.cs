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
            Context.CommandProvider.Write($"{cmd.Tag} OK {cmd.Command}\r\n");

            while (Context.CommandProvider.IsSessionAlive)
            {
                var command = Context.CommandProvider.Read();
                var imapCommand = new ImapCommand(command);

                switch (imapCommand.Command)
                {
                    case "CLOSE":
                        return new ImapCloseStateController().Run(Context, imapCommand);
                    case "CHECK":
                        new ImapCheckStateController().Run(Context, imapCommand);
                        break;
                    case "STATUS":
                        new ImapStatusStateController().Run(Context, imapCommand);
                        break;
                    case "EXPUNGE":
                        new ImapExpungeStateController().Run(Context, imapCommand);
                        break;
                    case "COPY":
                        new ImapCopyStateController().Run(Context, imapCommand);
                        break;
                    case "FETCH":
                        new ImapFetchStateController().Run(Context, imapCommand);
                        break;
                    case "UID":
                        new ImapUidStateController().Run(Context, imapCommand);
                        break;
                    case "STORE":
                        new ImapStoreStateController().Run(Context, imapCommand);
                        break;
                }
            }
            return true;
        }
    }
}