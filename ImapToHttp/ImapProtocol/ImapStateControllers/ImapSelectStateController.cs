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


            while (true)
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
                }
            }
            return true;
        }
    }
}