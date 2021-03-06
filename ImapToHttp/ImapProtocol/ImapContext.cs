using System.Collections.Generic;
using System.Linq;
using ImapProtocol.Contracts;
using ImapToHttpCore.EntityProviders;

namespace ImapProtocol
{
    public class ImapContext : IImapContext
    {
        private readonly Stack<ImapStateItem> _stateItems = new Stack<ImapStateItem>();

        public IEnumerable<ImapStateItem> States => _stateItems;
        public ImapStateItem PrePeekState => _stateItems.Skip(1).First();
        public bool HasState => _stateItems.Count != 0;
        
        public ITcpCommandProvider CommandProvider { get; }
        public IEntityProvider EntityProvider { get; set; }
        
        public IList<string> SubscribedMailboxes { get; } = new List<string>();

        public int ThreadId { get; set; }

        public ImapContext(ITcpCommandProvider commandProvider, int threadId)
        {
            CommandProvider = commandProvider;
        }
        
        public bool TryPopState(out ImapStateItem currentState)
        {
            return _stateItems.TryPop(out currentState);
        }

        public void AddState(ImapStateItem state)
        {
            _stateItems.Push(state);
        }
    }
}