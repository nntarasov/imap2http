using ImapProtocol.Contracts;

namespace ImapProtocol.ImapStateControllers
{
    public class ImapUnsubscribeStateController : ImapStateController
    {
        public override ImapState State { get; } = ImapState.Unsubscribe;
        protected override bool RunInternal(ImapCommand cmd)
        {
            var mailbox = cmd.Args;
            Context.CommandProvider.Write($"{cmd.Tag} OK UNSUBSCRIBE completed");
            return true;
        }
    }
}