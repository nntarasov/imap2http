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
                Command = cmd.Command,
                Tag = cmd.Tag,
                Args = cmd.Args
            });
            return RunInternal(cmd);
        }

        protected abstract bool RunInternal(ImapCommand cmd);
    }
}