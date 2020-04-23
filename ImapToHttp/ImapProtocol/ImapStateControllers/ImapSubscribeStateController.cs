using ImapProtocol.Contracts;

namespace ImapProtocol.ImapStateControllers
{
    public class ImapSubscribeStateController : ImapStateController
    {
        public override ImapState State { get; } = ImapState.Subscribe;
        protected override bool RunInternal(ImapCommand cmd)
        {
            var mailbox = cmd.Args;
            Context.CommandProvider.Write($"{cmd.Tag} OK SUBSCRIBE completed\r\n");
            return true;
        }
    }
}