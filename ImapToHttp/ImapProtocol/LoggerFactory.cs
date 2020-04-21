using ImapProtocol.Contracts;

namespace ImapProtocol
{
    public static class LoggerFactory
    {
        private static ILogger _logger = new DevNullLogger();

        private class DevNullLogger : ILogger
        {
            public void Print(string message)
            {
                return;
            }
        }
        
        public static void SetLogger(ILogger logger)
        {
            _logger = logger;
        }
        
        public static ILogger GetLogger()
        {
            return _logger;
        }
    }
}