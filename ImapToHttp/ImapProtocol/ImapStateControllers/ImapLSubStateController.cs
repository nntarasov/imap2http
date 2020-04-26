using System.Linq;
using System.Text.RegularExpressions;
using ImapProtocol.Contracts;

namespace ImapProtocol.ImapStateControllers
{
    public class ImapLSubStateController : ImapStateController
    {
        public override ImapState State { get; } = ImapState.LSub;
        protected override bool RunInternal(ImapCommand cmd)
        {
            var match = Regex.Match(cmd.Args, 
                @"^[""]{0,1}(?<refname>[^""]*)[""]{0,1} [""]{0,1}(?<box>[^""]*)[""]{0,1}");
            if (!match.Success)
            {
                Context.CommandProvider.Write($"{cmd.Tag} BAD\r\n");
                return true;
            }

            var refname = match.Groups["refname"].Value;
            var boxWildcard = match.Groups["box"].Value;

            var mailboxes = new[]
            {
                new[] { "INBOX" },
                new[] { "Junk"},
                new[] { "Sent"},
                //new [] { "important", "1"},
                //new [] { "important" , "2"}
            };

            var path = refname.Split('/');
            
            var refResultBoxes = mailboxes
                .Where(m => m.Take(path.Length).SequenceEqual(path))
                .ToList();

            if (string.IsNullOrWhiteSpace(refname))
            {
                refResultBoxes = mailboxes.ToList();
            }

            var boxRegex = boxWildcard
                .Replace("*", @".*")
                .Replace("%", @"[^/]*")
                .Insert(0, "^");

            var resultBoxes = refResultBoxes
                .Select(path => string.Join("/", path))
                .Where(p => Regex.IsMatch(p, boxRegex))
                .ToList();

            foreach (var mailbox in resultBoxes)
            {
                Context.CommandProvider.Write($"* LSUB () \"/\" {mailbox}\r\n");
            }

            Context.CommandProvider.Write($"{cmd.Tag} OK LSUB\r\n");
            return true;
        }
    }
}