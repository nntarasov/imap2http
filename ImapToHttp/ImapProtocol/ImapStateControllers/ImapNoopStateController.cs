using ImapProtocol.Contracts;

namespace ImapProtocol.ImapStateControllers
{
    public class ImapNoopStateController : ImapStateController
    {
        public override ImapState State { get; } = ImapState.Noop;
        protected override bool RunInternal(ImapCommand cmd)
        {
            Context.CommandProvider.Write($"{cmd.Tag} OK NOOP completed\r\n");
            return true;
        }
    }
}