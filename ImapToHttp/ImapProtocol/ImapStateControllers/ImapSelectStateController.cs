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
            
            Context.CommandProvider.Write("* FLAGS (\\Answered \\Flagged \\Deleted \\Seen \\Draft)\r\n");
            Context.CommandProvider.Write("* 2 EXISTS\r\n");
            Context.CommandProvider.Write("* 1 RECENT\r\n");
            Context.CommandProvider.Write("* OK [UIDVALIDITY 1345] UIDVALIDITY\r\n");
            Context.CommandProvider.Write("* OK [UIDNEXT 1345] UIDNEXT\r\n");
            Context.CommandProvider.Write("* OK [UNSEEN 2] UNSEEN\r\n");
            Context.CommandProvider.Write($"{cmd.Tag} OK SELECT\r\n");

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