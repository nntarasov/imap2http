using System.Linq;
using System.Text.RegularExpressions;
using ImapProtocol.Contracts;

namespace ImapProtocol.ImapStateControllers
{
    public class ImapListStateController : ImapStateController
    {
        public override ImapState State { get; }

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

            var path = refname.Split('/');
            var mailboxes = Context.EntityProvider.GetAllDirectories()
                .Values
                .Select(p => p.Split('/'))
                .ToList();

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
                Context.CommandProvider.Write($"* LIST (\\Noinferiors) \"/\" {mailbox}\r\n");
            }

            Context.CommandProvider.Write($"{cmd.Tag} OK LIST\r\n");

            return true;
        }
    }
}