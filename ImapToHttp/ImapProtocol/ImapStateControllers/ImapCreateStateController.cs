using ImapProtocol.Contracts;

namespace ImapProtocol.ImapStateControllers
{
    public class ImapCreateStateController : ImapStateController
    {
        public override ImapState State { get; } = ImapState.Create;
        protected override bool RunInternal(ImapCommand cmd)
        {
            var mailbox = cmd.Args.Trim();
            if (!Context.EntityProvider.CreateDirectory(mailbox))
            {
                Context.CommandProvider.Write($"{cmd.Tag} BAD CREATE\r\n");
            }
            
            Context.CommandProvider.Write($"{cmd.Tag} OK CREATE\r\n");
            return true;
        }
    }
}