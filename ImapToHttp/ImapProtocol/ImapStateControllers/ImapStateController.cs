using ImapProtocol.Contracts;

namespace ImapProtocol.ImapStateControllers
{
    public abstract class ImapStateController
    {
        public abstract ImapState State { get; }
        
        protected IImapContext Context;

        public bool Run(IImapContext context, ImapCommand cmd)
        {
            Context = context;
            context.AddState(new ImapStateItem
            {
                State = State,
                Args = cmd.Args,
                Tag = cmd.Tag
            });
            return RunInternal(cmd);
        }

        protected abstract bool RunInternal(ImapCommand cmd);
    }
}