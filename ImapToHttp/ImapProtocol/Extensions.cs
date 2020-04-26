using System;
using System.Globalization;

namespace ImapProtocol
{
    public static class Extensions
    {
        public static string ToImapString(this DateTime date)
        {
            return date.ToString("dd-MMM-yyyy HH:mm:ss zz00", new CultureInfo("en-US"));
        }
    }
}