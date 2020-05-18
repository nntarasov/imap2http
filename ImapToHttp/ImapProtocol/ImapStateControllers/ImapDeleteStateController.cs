using ImapProtocol.Contracts;

namespace ImapProtocol.ImapStateControllers
{
    public class ImapDeleteStateController : ImapStateController
    {
        public override ImapState State { get; } = ImapState.Delete;
        protected override bool RunInternal(ImapCommand cmd)
        {
            var mailbox = cmd.Args.Trim();

            if (!Context.EntityProvider.DeleteDirectory(mailbox))
            {
                Context.CommandProvider.Write($"{cmd.Tag} BAD\r\n");
                return true;
            }
            
            // \noselect delele - BAD
            // nonexist delete - BAD
            Context.CommandProvider.Write($"{cmd.Tag} OK DELETE\r\n");
            return true;
        }
    }
}