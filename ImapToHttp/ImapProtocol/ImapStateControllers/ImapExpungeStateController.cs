using ImapProtocol.Contracts;

namespace ImapProtocol.ImapStateControllers
{
    public class ImapExpungeStateController : ImapStateController
    {
        public override ImapState State { get; } = ImapState.Expunge;
        protected override bool RunInternal(ImapCommand cmd)
        {
            /*
             *    Arguments:  none

   Responses:  untagged responses: EXPUNGE

   Result:     OK - expunge completed
               NO - expunge failure: can't expunge (e.g., permission
                    denied)
               BAD - command unknown or arguments invalid

      The EXPUNGE command permanently removes all messages that have the
      \Deleted flag set from the currently selected mailbox.  Before
      returning an OK to the client, an untagged EXPUNGE response is
      sent for each message that is removed.

   Example:    C: A202 EXPUNGE
               S: * 3 EXPUNGE
               S: * 3 EXPUNGE
               S: * 5 EXPUNGE
               S: * 8 EXPUNGE
               S: A202 OK EXPUNGE completed

        Note: In this example, messages 3, 4, 7, and 11 had the
        \Deleted flag set.  See the description of the EXPUNGE
        response for further explanation.
             */

            var expunged = Context.EntityProvider.Expunge();

            if (expunged == null)
            {
                Context.CommandProvider.Write($"{cmd.Tag} NO EXPUNGE\r\n");
                return true;
            }

            foreach (var id in expunged)
            {
                Context.CommandProvider.Write($"* {id} EXPUNGE\r\n");
            }
            Context.CommandProvider.Write($"{cmd.Tag} OK EXPUNGE completed\r\n");
            return true;
        }
    }
}