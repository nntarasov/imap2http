using System;
using System.Text;
using System.Text.RegularExpressions;
using ImapProtocol.Contracts;

namespace ImapProtocol.ImapStateControllers
{
    public class ImapAuthenticateController : ImapStateController
    {
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

                    if (authcid != "ntarasov" && password != "123456")
                    {
                        Context.CommandProvider.Write($"{cmd.Tag} NO AUTH=PLAIN\r\n");
                        return true;
                    }

                    Context.CommandProvider.Write($"{cmd.Tag} OK AUTH=PLAIN\r\n");
                    break;
                default:
                    Context.CommandProvider.Write($"{cmd.Tag} BAD\r\n");
                    return true;
            }

            while (true)
            {
                var command = Context.CommandProvider.Read();
                var imapCommand = new ImapCommand(command);
                if (imapCommand.Command == "LIST")
                {
                    new ImapListStateController().Run(Context, imapCommand);
                }
                else if (imapCommand.Command == "SELECT")
                {
                    new ImapSelectStateController().Run(Context, imapCommand);
                }
                else if (imapCommand.Command == "LSUB")
                {
                    new ImapLSubStateController().Run(Context, imapCommand);
                }

                return true;
        }
    }
    }
}