using ImapProtocol.Contracts;

namespace ImapProtocol.ImapStateControllers
{
    public class ImapCloseStateController : ImapStateController
    {
        public override ImapState State { get; } = ImapState.Close;
        protected override bool RunInternal(ImapCommand cmd)
        {
            Context.EntityProvider.SwitchDirectory(null);
            Context.CommandProvider.Write($"{cmd.Tag} OK CLOSE\r\n");
            return true;
        }
    }
}