using System;
using System.Text;
using System.Text.RegularExpressions;
using ImapProtocol.Contracts;
using ImapToHttpCore;
using ImapToHttpCore.Contracts;

namespace ImapProtocol.ImapStateControllers
{
    public class ImapAuthenticateController : ImapStateController
    {
        private static ILogger Logger = LoggerFactory.GetLogger();
        
        public override ImapState State { get; } = ImapState.Authenticate;

        protected override bool RunInternal(ImapCommand cmd)
        {
            var mode = Regex.Match(cmd.Args, @"(?<m>\w+)").Groups["m"].Value;

            switch (mode)
            {
                case "PLAIN":
                    // RFC 4616.2. PLAIN SASL Mechanism. 
                    Context.CommandProvider.Write("+\r\n");
                    var credentialsEncoded = Context.CommandProvider.Read();
                    var credentialBytes = Convert.FromBase64String(credentialsEncoded);
                    var credentials = Encoding.UTF8.GetString(credentialBytes);

                    var identities = credentials.Split('\0');
                    if (identities.Length != 3)
                    {
                        Context.CommandProvider.Write($"{cmd.Tag} BAD AUTH=PLAIN\r\n");
                        return true;
                    }

                    var authzid = identities[0];
                    var authcid = identities[1];
                    var password = identities[2];

                    string login = authcid;
                    if (!Context.EntityProvider.UserProvider.Authorize(login, password))
                    {
                        Context.CommandProvider.Write($"{cmd.Tag} NO AUTH=PLAIN\r\n");
                        return true;
                    }
                    Logger.Print(Context.ThreadId, MessageType.Info, $"User authorized. login: {login}");
                    Context.CommandProvider.Write($"{cmd.Tag} OK AUTH=PLAIN\r\n");
                    break;
                default:
                    Context.CommandProvider.Write($"{cmd.Tag} BAD\r\n");
                    return true;
            }

            while (Context.CommandProvider.IsSessionAlive)
            {
                var command = Context.CommandProvider.Read();
                var imapCommand = new ImapCommand(command);
                switch (imapCommand.Command)
                {
                    case "LIST":
                        new ImapListStateController().Run(Context, imapCommand);
                        break;
                    case "SELECT":
                        new ImapSelectStateController().Run(Context, imapCommand);
                        break;
                    case "LSUB":
                        new ImapLSubStateController().Run(Context, imapCommand);
                        break;
                    case "SUBSCRIBE":
                        new ImapSubscribeStateController().Run(Context, imapCommand);
                        break;
                    case "UNSUBSCRIBE":
                        new ImapUnsubscribeStateController().Run(Context, imapCommand);
                        break;
                    case "CREATE":
                        new ImapCreateStateController().Run(Context, imapCommand);
                        break;
                    case "DELETE":
                        new ImapDeleteStateController().Run(Context, imapCommand);
                        break;
                    case "RENAME":
                        new ImapRenameStateController().Run(Context, imapCommand);
                        break;
                    case "EXAMINE":
                        new ImapSelectStateController().Run(Context, imapCommand);
                        break;
                    case "CAPABILITY":
                        new ImapCapabilityStateController().Run(Context, imapCommand);
                        break;
                }
            }
            return true;
        }
    }
}