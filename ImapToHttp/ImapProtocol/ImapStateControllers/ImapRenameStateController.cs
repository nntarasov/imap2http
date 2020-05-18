using ImapProtocol.Contracts;

namespace ImapProtocol.ImapStateControllers
{
    public class ImapRenameStateController : ImapStateController
    {
        public override ImapState State { get; } = ImapState.Rename;
        protected override bool RunInternal(ImapCommand cmd)
        {
            var names = cmd.Args.Split(' ');
            
            var oldName = names.Length < 2 ? string.Empty : names[0];
            var newName = names.Length < 2 ? string.Empty : names[1];
            
            if (names.Length < 2 || string.IsNullOrWhiteSpace(oldName) || 
                string.IsNullOrWhiteSpace(newName))
            {
                Context.CommandProvider.Write($"{cmd.Tag} BAD\r\n");
                return true;
            }

            if (!Context.EntityProvider.RenameDirectory(oldName, newName))
            {
                Context.CommandProvider.Write($"{cmd.Tag} NO\r\n");
                return true;
            }
            
            Context.CommandProvider.Write($"{cmd.Tag} OK RENAME\r\n");
            return true;
        }
    }
}