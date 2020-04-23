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
            // STATUS MUST NOT cause messages to lose the \Recent
            // The STATUS command provides an alternative to opening a second
            // IMAP4rev1 connection and doing an EXAMINE command on a mailbox
            
            // copy of examine
            
            var mailboxes = new[]
            {
                "INBOX"
            }.ToList();

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
          
            /*
             *
                 S: * OK [UNSEEN 8] Message 8 is first unseen
                 S: * OK [UIDVALIDITY 3857529045] UIDs valid
                 S: * OK [UIDNEXT 4392] Predicted next UID
             * 
             */
          
            Context.CommandProvider.Write($"{cmd.Tag} OK STATUS completed\r\n");
          
            return true;
        }
    }
}