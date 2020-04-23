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
        LSub = 6,
        Subscribe = 7,
        Unsubscribe = 8,
        Create = 9,
        Delete = 10,
        Rename = 11,
        Close = 12,
        Check = 13,
        Status = 14,
        Expunge = 15,
        Copy = 16
    }
}