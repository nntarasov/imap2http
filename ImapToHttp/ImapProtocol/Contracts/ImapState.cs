namespace ImapProtocol.Contracts
{
    public enum ImapState
    {
        None = 0,
        Connected = 1,
        Capability = 2,
        Noop = 3,
        Authenticate = 4,
        Selected = 5,
        LSub = 6
    }
}