using System;
using ImapToHttpCore.Contracts;

namespace ImapToHttpCLI
{
    public class ConsoleLogger : ILogger
    {
        private static object Lock = new object();
            
        public void Print(string message)
        {
            lock (Lock)
            {
                Console.WriteLine(message);
            }
        }

        public void Print(int threadId, MessageType type, string message)
        {
            lock (Lock)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write(DateTime.Now.ToString("HH:mm:ss "));
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("[" + threadId + "] ");
                string prefix = string.Empty;
                switch (type)
                {
                    case MessageType.In:
                        Console.ForegroundColor = ConsoleColor.Green;
                        prefix = ">>";
                        break;
                    case MessageType.Error:
                        Console.ForegroundColor = ConsoleColor.Red;
                        prefix = "ERR";
                        break;
                    case MessageType.Out:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        prefix = "<<";
                        break;
                    case MessageType.Info:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        prefix = "INF";
                        break;
                }
                Console.Write(prefix + " ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(message);
            }
        }
    }
}