namespace ImapToHttpCore.Contracts
{
    public interface ILogger
    {
        void Print(string message);
        void Print(int threadId, MessageType type, string message);
    }
}