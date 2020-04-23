using System.Linq;
using System.Text.RegularExpressions;
using ImapProtocol.Contracts;

namespace ImapProtocol.ImapStateControllers
{
    public class ImapExamineStateController : ImapStateController
    {
        public override ImapState State { get; }
        protected override bool RunInternal(ImapCommand cmd)
        {
            
          /*  Responses:  REQUIRED untagged responses: FLAGS, EXISTS, RECENT
                REQUIRED OK untagged responses:  UNSEEN,  PERMANENTFLAGS,
            UIDNEXT, UIDVALIDITY

            Result:     OK - examine completed, now in selected state
            NO - examine failure, now in authenticated state: no
            such mailbox, can't access mailbox
            BAD - command unknown or arguments invalid

            The EXAMINE command is identical to SELECT and returns the same
                output; however, the selected mailbox is identified as read-only.
                No changes to the permanent state of the mailbox, including
            per-user state, are permitted; in particular, EXAMINE MUST NOT
                cause messages to lose the \Recent flag.

                The text of the tagged OK response to the EXAMINE command MUST
            begin with the "[READ-ONLY]" response code.

                Example:    C: A932 EXAMINE blurdybloop
            S: * 17 EXISTS
            S: * 2 RECENT
            S: * OK [UNSEEN 8] Message 8 is first unseen
            S: * OK [UIDVALIDITY 3857529045] UIDs valid
            S: * OK [UIDNEXT 4392] Predicted next UID
            S: * FLAGS (\Answered \Flagged \Deleted \Seen \Draft)
            S: * OK [PERMANENTFLAGS ()] No permanent flags permitted
            S: A932 OK [READ-ONLY] EXAMINE completed*/

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
          
          Context.CommandProvider.Write($"{cmd.Tag} OK [READ-ONLY] EXAMINE completed\r\n");
          
          return true;
        }
    }
}