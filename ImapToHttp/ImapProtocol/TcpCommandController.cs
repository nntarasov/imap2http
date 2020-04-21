using System.Collections.Generic;
using System.Text;
using ImapProtocol.Contracts;
using ImapProtocol.ImapStateControllers;

namespace ImapProtocol
{
    public class TcpCommandController : ITcpCommandProvider, ITcpCommandController
    {
        private static ILogger Logger = LoggerFactory.GetLogger();
        
        private readonly StringBuilder _commandBuilder = new StringBuilder();
        private readonly Queue<string> _commands = new Queue<string>();

        private readonly ITcpController _tcpController;
        private readonly ImapStateController _connectedStateController = new ImapConnectedStateController();

        private const string CommandSeparator = "\r\n";

        public TcpCommandController(ITcpController tcpController)
        {
            _tcpController = tcpController;
        }
        
        public void Write(string command)
        {
            Logger.Print("<< " + command.TrimEnd('\n').TrimEnd('\r'));
            var responseBytes = Encoding.ASCII.GetBytes(command);
            _tcpController.Write(responseBytes);
            while (true)
            {
                Read();
            }
        }
        
        public string Read()
        {
            string command;
            while (!_commands.TryDequeue(out command))
            {
                _tcpController.ReadNext();
            }
            return command;
        }

        public void OnTcpReceive(byte[] bytes, int offset, int count)
        {
            var bufferString = Encoding.UTF7.GetString(bytes, offset, count);
            var commandParts = bufferString.Split(CommandSeparator);
            if (commandParts.Length > 1)
            {
                _commandBuilder.Append(commandParts[0]);
                _commands.Enqueue(_commandBuilder.ToString());
                Logger.Print(">> " + _commandBuilder.ToString());
                _commandBuilder.Clear();

                for (int i = 1; i < commandParts.Length - 1; i++)
                {
                    _commands.Enqueue(commandParts[i]);
                    Logger.Print(">> " + commandParts[i]);
                }
            }
            _commandBuilder.Append(commandParts[^1]);
        }

        public void OnTcpConnect()
        {
            _connectedStateController.Run(new ImapContext(this), new ImapCommand());
        }
    }
}