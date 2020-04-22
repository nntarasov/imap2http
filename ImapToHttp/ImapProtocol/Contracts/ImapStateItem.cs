namespace ImapProtocol.Contracts
{
    public class ImapStateItem
    {
        public ImapState State { get; set; }
        public string Tag { get; set; }
        public string Command { get; set; }
        public string Args { get; set; }
    }
}