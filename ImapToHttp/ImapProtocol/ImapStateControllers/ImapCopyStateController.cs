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

            Context.CommandProvider.Write($"{cmd.Tag} OK COPY\r\n");
            return true;
        } 
    }
}