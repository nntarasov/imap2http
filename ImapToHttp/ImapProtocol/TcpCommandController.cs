using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
        private readonly ITcpSessionContext _sessionContext;
        private readonly ImapStateController _connectedStateController = new ImapConnectedStateController();

        private const string CommandSeparator = "\r\n";

        public TcpCommandController(ITcpController tcpController, ITcpSessionContext sessionContext)
        {
            _tcpController = tcpController;
            _sessionContext = sessionContext;
        }
        
        public void Write(string command)
        {
            Logger.Print("<< " + command.TrimEnd('\n').TrimEnd('\r'));
            var responseBytes = Encoding.ASCII.GetBytes(command);
            if (!_tcpController.Write(_sessionContext, responseBytes))
            {
                throw new Exception("Unable to write response");
            }
        }
        
        public string Read()
        {
            string command;
            while (!_commands.TryDequeue(out command))
            {
                if (!_tcpController.ReadNext(_sessionContext, this))
                {
                    throw new Exception("Unable to read command");
                }
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
                Logger.Print($"[{_sessionContext.ThreadId}]>> " + _commandBuilder.ToString());
                _commandBuilder.Clear();

                for (int i = 1; i < commandParts.Length - 1; i++)
                {
                    _commands.Enqueue(commandParts[i]);
                    Logger.Print($"[{_sessionContext.ThreadId}]>> " + commandParts[i]);
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