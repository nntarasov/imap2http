using ImapProtocol.Contracts;

namespace ImapProtocol.ImapStateControllers
{
    public class ImapRenameStateController : ImapStateController
    {
        public override ImapState State { get; } = ImapState.Rename;
        protected override bool RunInternal(ImapCommand cmd)
        {
            // if (NOT exits)
            // Write NO
            Context.CommandProvider.Write($"{cmd.Tag} OK RENAME\r\n");
            return true;
        }
    }
}