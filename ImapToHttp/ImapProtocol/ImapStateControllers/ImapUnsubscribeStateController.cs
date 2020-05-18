using ImapProtocol.Contracts;

namespace ImapProtocol.ImapStateControllers
{
    public class ImapUnsubscribeStateController : ImapStateController
    {
        public override ImapState State { get; } = ImapState.Unsubscribe;
        protected override bool RunInternal(ImapCommand cmd)
        {
            var mailbox = cmd.Args.Trim();
            if (Context.SubscribedMailboxes.Contains(mailbox))
            {
                Context.CommandProvider.Write($"{cmd.Tag} NO\r\n");
                return true;
            }
            
            Context.SubscribedMailboxes.Remove(mailbox);
            Context.CommandProvider.Write($"{cmd.Tag} OK UNSUBSCRIBE completed\r\n");
            return true;
        }
    }
}