using ImapProtocol.Contracts;

namespace ImapProtocol.ImapStateControllers
{
    public class ImapCheckStateController : ImapStateController
    {
        public override ImapState State { get; } = ImapState.Check;
        protected override bool RunInternal(ImapCommand cmd)
        {
            // housekeeping
            Context.CommandProvider.Write($"{cmd.Tag} OK CHECK completed\r\n");
            return true;
        }
    }
}