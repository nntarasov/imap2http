using ImapProtocol.Contracts;

namespace ImapProtocol.ImapStateControllers
{
    public class ImapCreateStateController : ImapStateController
    {
        public override ImapState State { get; } = ImapState.Create;
        protected override bool RunInternal(ImapCommand cmd)
        {
            // if exists, INBOX
            // BAD CREATE
            
            Context.CommandProvider.Write($"{cmd.Tag} OK CREATE\r\n");
            return true;
        }
    }
}