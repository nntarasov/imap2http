using System.Linq;
using System.Text.RegularExpressions;
using ImapProtocol.Contracts;

namespace ImapProtocol.ImapStateControllers
{
    public class ImapStoreStateController : ImapStateController
    {
        public override ImapState State => ImapState.Store;


        public class StoreFlagData
        {
            public enum StoreFlagOperation
            {
                None = 0,
                Add = 1,
                Replace = 2,
                Delete = 3
            }

            public StoreFlagOperation Operation { get; set; }
            public string[] Flags { get; set; }
            public bool IsSilent { get; set; }
        }

        private StoreFlagData ParseStoreFlagData(string args)
        {
            var match = Regex.Match(args, @"(?<op>[+-]){0,1}FLAGS(?<s>\.SILENT){0,1} \((?<f>[^)]*)\)");
            if (!match.Success || !match.Groups["f"].Success)
            {
                return null;
            }


            var operation = StoreFlagData.StoreFlagOperation.None;
            if (!match.Groups["op"].Success)
            {
                operation = StoreFlagData.StoreFlagOperation.Replace;
            }
            else
            {
                switch (match.Groups["op"].Value)
                {
                    case "+":
                        operation = StoreFlagData.StoreFlagOperation.Add;
                        break;
                    case "-":
                        operation = StoreFlagData.StoreFlagOperation.Delete;
                        break;
                }
            }

            var data = new StoreFlagData
            {
                Flags = match.Groups["f"].Value.Split(' '),
                IsSilent = match.Groups["s"].Success,
                Operation = operation
            };

            //DEBUG
            var logger = LoggerFactory.GetLogger();
            logger.Print($"flags: {data.Flags.Aggregate((a, b) => (a + " " + b))}");
            logger.Print($"silent: {data.IsSilent}");
            logger.Print($"op: {data.Operation}");
            
            return data;
        }
        
        protected override bool RunInternal(ImapCommand cmd)
        {
            var args = cmd.Args.Split(' ');
            var sequenceSet = ImapCommon.GetMessageSequenceSet(args[0]);
            var idType = Context.States.Any(s => s.State == ImapState.Uid) ? MessageIdType.Uid : MessageIdType.Id;

            var messageIds = ImapCommon.ExtractRealMessageIds(sequenceSet, idType);
            var flagsData = ParseStoreFlagData(cmd.Args);
            if (flagsData == null)
            {
                Context.CommandProvider.Write($"{cmd.Tag} BAD");
                return true;
            }

            foreach (var messageId in messageIds)
            {
                if (!ApplyStore(messageId, flagsData))
                {
                    return false;
                }
            }
            
            Context.CommandProvider.Write($"{cmd.Tag} OK STORE completed");
            return true;
        }

        private bool ApplyStore(long messageId, StoreFlagData flagData)
        {
            if (!flagData.IsSilent)
            {
                var cmd = new ImapCommand($"xx UID FETCH {messageId} (FLAGS)");
                var uidResult = new ImapUidStateController().Run(Context, cmd);
                if (!uidResult)
                {
                    return false;
                }
            }

            return true;
        }
    }
}