using System.Collections.Generic;

namespace ImapProtocol.Contracts
{
    public interface IImapContext
    {
        IEnumerable<ImapStateItem> States { get; }
        ImapStateItem PeekState { get; }
        bool TryPopState(out ImapStateItem currentState);
        void AddState(ImapStateItem state);
        ITcpCommandProvider CommandProvider { get; }

    }
}