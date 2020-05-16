using System;
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
        private readonly ITcpSessionContext _sessionContext;
        private readonly ImapStateController _connectedStateController = new ImapConnectedStateController();

        private const string CommandSeparator = "\r\n";

        public TcpCommandController(ITcpController tcpController, ITcpSessionContext sessionContext)
        {
            _tcpController = tcpController;
            _sessionContext = sessionContext;
        }

        public bool IsSessionAlive => _sessionContext.IsSessionAlive;

        public void Write(string command)
        {
            Logger.Print(_sessionContext.ThreadId, MessageType.Out,  command.TrimEnd('\n').TrimEnd('\r'));
            var responseBytes = Encoding.ASCII.GetBytes(command);
            try
            {
                if (!_tcpController.Write(_sessionContext, responseBytes))
                {
                    Logger.Print(_sessionContext.ThreadId, MessageType.Error, "Unable to write response");
                }
            }
            catch (Exception ex)
            {
                Logger.Print(_sessionContext.ThreadId, MessageType.Error, "Unable to write response, ex: {ex.Message}");
                _sessionContext.IsSessionAlive = false;
                _sessionContext.NetworkStream?.Dispose();
            }
        }
        
        public string Read()
        {
            string command;
            while (!_commands.TryDequeue(out command) && _sessionContext.IsSessionAlive)
            {
                try
                {
                    if (!_tcpController.ReadNext(_sessionContext, this))
                    {
                        Logger.Print(_sessionContext.ThreadId, MessageType.Error, "Unable to read command");
                    }
                }
                catch (Exception ex)
                {
                    Logger.Print(_sessionContext.ThreadId, MessageType.Error, "Unable to read command, ex: {ex.Message}");
                    _sessionContext.IsSessionAlive = false;
                    _sessionContext.NetworkStream?.Dispose();
                }
            }
            return command ?? string.Empty;
        }

        public void OnTcpReceive(byte[] bytes, int offset, int count)
        {
            var bufferString = Encoding.UTF7.GetString(bytes, offset, count);
            var commandParts = bufferString.Split(CommandSeparator);
            if (commandParts.Length > 1)
            {
                _commandBuilder.Append(commandParts[0]);
                _commands.Enqueue(_commandBuilder.ToString());
                Logger.Print(_sessionContext.ThreadId, MessageType.In, _commandBuilder.ToString());
                _commandBuilder.Clear();

                for (int i = 1; i < commandParts.Length - 1; i++)
                {
                    _commands.Enqueue(commandParts[i]);
                    Logger.Print(_sessionContext.ThreadId, MessageType.In, commandParts[i]);
                }
            }
            _commandBuilder.Append(commandParts[^1]);
        }

        public void OnTcpConnect()
        {
            _connectedStateController.Run(new ImapContext(this), new ImapCommand());
            Logger.Print(_sessionContext.ThreadId, MessageType.None, "Connection closed");
        }
    }
}