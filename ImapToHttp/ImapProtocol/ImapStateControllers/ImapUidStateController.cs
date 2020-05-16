using ImapProtocol.Contracts;

namespace ImapProtocol.ImapStateControllers
{
    public class ImapUidStateController : ImapStateController
    {
        public override ImapState State => ImapState.Uid;
        protected override bool RunInternal(ImapCommand cmd)
        {
            cmd.Args = cmd.Args.Substring(cmd.Args.IndexOf(' ') + 1);

            LoggerFactory.GetLogger().Print(cmd.Args);
            new ImapFetchStateController().Run(Context, cmd);
            return true;
            //Context.CommandProvider.Write($"{imapCommand.Tag} OK\r\n");
        }
    }
}