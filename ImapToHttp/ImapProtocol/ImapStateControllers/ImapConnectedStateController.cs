using ImapProtocol.Contracts;
using ImapToHttpCore;

namespace ImapProtocol.ImapStateControllers
{
    public class ImapConnectedStateController : ImapStateController
    {
        public override ImapState State
        {
            get => ImapState.Connected;
        }
        
        protected override bool RunInternal(ImapCommand cmd)
        {
            LoggerFactory.GetLogger().Print("Greeted");
            var greetingString = "* OK IMAP4rev1 Service Ready\r\n";
            Context.CommandProvider.Write(greetingString);

            while (Context.CommandProvider.IsSessionAlive)
            {
                string commandText = Context.CommandProvider.Read();
                var imapCommand = new ImapCommand(commandText);

                if (imapCommand.Command == "CAPABILITY")
                {
                    new ImapCapabilityStateController().Run(Context, imapCommand);
                }

                else if (imapCommand.Command == "NOOP")
                {
                    new ImapNoopStateController().Run(Context, imapCommand);
                }

                else if (imapCommand.Command == "AUTHENTICATE")
                {
                    new ImapAuthenticateController().Run(Context, imapCommand);
                }

                else if (imapCommand.Command == "LOGOUT")
                {
                    Context.CommandProvider.Write("* BYE\r\n");
                    Context.CommandProvider.Write($"{imapCommand.Tag} OK\r\n");
                    break;
                }
            }
            return false;
        }
    }
}