using ImapProtocol.Contracts;

namespace ImapProtocol.ImapStateControllers
{
    public class ImapSubscribeStateController : ImapStateController
    {
        public override ImapState State { get; } = ImapState.Subscribe;
        protected override bool RunInternal(ImapCommand cmd)
        {
            var mailbox = cmd.Args.Trim();

            if (Context.SubscribedMailboxes.Contains(mailbox))
            {
                Context.CommandProvider.Write($"{cmd.Tag} NO\r\n");
            }
            Context.SubscribedMailboxes.Add(mailbox);
            Context.CommandProvider.Write($"{cmd.Tag} OK SUBSCRIBE completed\r\n");
            return true;
        }
    }
}