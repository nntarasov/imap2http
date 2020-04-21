using ImapProtocol.Contracts;

namespace ImapProtocol.ImapStateControllers
{
    public class ImapCapabilityStateController : ImapStateController
    {
        public override ImapState State { get; } = ImapState.Capability;
        protected override bool RunInternal(ImapCommand cmd)
        {
            Context.CommandProvider.Write("* CAPABILITY IMAP4rev1 AUTH=PLAIN LOGINDISABLED\r\n");
            Context.CommandProvider.Write($"{cmd.Tag} OK CAPABILITY completed\r\n");
            return true;
        }
    }
}