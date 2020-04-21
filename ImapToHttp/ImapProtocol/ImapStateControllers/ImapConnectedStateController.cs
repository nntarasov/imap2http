using ImapProtocol.Contracts;

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
            var greetingString = "* OK IMAP4rev1 Service Ready\r\n";
            //Console.Write($"<< {greetingString}");
            Context.CommandProvider.Write(greetingString);
            return true;
        }
    }
}