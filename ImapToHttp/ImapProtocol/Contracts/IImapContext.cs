using System.Collections.Generic;
using ImapToHttpCore.EntityProviders;

namespace ImapProtocol.Contracts
{
    public interface IImapContext
    {
        IEnumerable<ImapStateItem> States { get; }
        ImapStateItem PrePeekState { get; }
        bool TryPopState(out ImapStateItem currentState);
        void AddState(ImapStateItem state);
        ITcpCommandProvider CommandProvider { get; }
        IEntityProvider EntityProvider { get; }
        int ThreadId { get; }
        IList<string> SubscribedMailboxes { get; }
    }
}