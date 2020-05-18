namespace ImapProtocol.Contracts.EntityProviders
{
    public interface IDirectoryProvider
    {
        int[] GetUids(params int[] relativeIds);
        bool HasMessage(int uid);

        bool Copy(string destination, params int[] uids);
    }
}