using System.Linq;
using System.Text.RegularExpressions;
using ImapProtocol.Contracts;

namespace ImapProtocol.ImapStateControllers
{
    public class ImapCopyStateController : ImapStateController
    {
        public override ImapState State { get; } = ImapState.Copy;
        protected override bool RunInternal(ImapCommand cmd)
        {
            // <sequnce set> mbox
            // NO - Wrong name
            var match = Regex.Match(cmd.Args, "^(?<sq>[^ ]+) (?<mbox>[^ ]+)^");

            if (!match.Success || !match.Groups["mbox"].Success || !match.Groups["sq"].Success
                || string.IsNullOrWhiteSpace(match.Groups["mbox"].Value))
            {
                Context.CommandProvider.Write($"{cmd.Tag} BAD\r\n");
                return true;
            }

            var mailbox = match.Groups["mbox"].Value;
            var sqString = match.Value;
            var sequenceSet = ImapCommon.GetMessageSequenceSet(sqString);
            var uids = ImapCommon.ExtractRealMessageIds(Context, sequenceSet, MessageIdType.Id);

            if (!Context.EntityProvider.DirectoryProvider.Copy(mailbox, uids.ToArray()))
            {
                Context.CommandProvider.Write($"{cmd.Tag} NO\r\n");
                return true;
            }

            Context.CommandProvider.Write($"{cmd.Tag} OK COPY\r\n");
            return true;
        } 
    }
}