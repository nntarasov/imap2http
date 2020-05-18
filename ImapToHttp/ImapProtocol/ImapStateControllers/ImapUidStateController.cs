using ImapProtocol.Contracts;

namespace ImapProtocol.ImapStateControllers
{
    public class ImapUidStateController : ImapStateController
    {
        public override ImapState State => ImapState.Uid;
        protected override bool RunInternal(ImapCommand cmd)
        {
            var command = cmd.Args.Substring(0, cmd.Args.IndexOf(' '));
            cmd.Args = cmd.Args.Substring(cmd.Args.IndexOf(' ') + 1);

            switch (command)
            {
                case "FETCH":
                    var fetchResult = new ImapFetchStateController().Run(Context, cmd);
                    // command successful and request is not generated
                    if (fetchResult && Context.PrePeekState.State == ImapState.Selected)
                    {
                        Context.CommandProvider.Write($"{cmd.Tag} OK UID FETCH\r\n");
                    }
                    return fetchResult;
                case "STORE":
                    var storeResult = new ImapStoreStateController().Run(Context, cmd);
                    // command successful and request is not generated
                    if (storeResult && Context.PrePeekState.State == ImapState.Selected)
                    {
                        Context.CommandProvider.Write($"{cmd.Tag} OK UID STORE\r\n");
                    }
                    return storeResult;
                default:
                    Context.CommandProvider.Write($"{cmd.Tag} BAD\r\n");
                    return true;
            }
            //Context.CommandProvider.Write($"{imapCommand.Tag} OK\r\n");
        }
    }
}