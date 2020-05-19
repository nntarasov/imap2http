namespace ImapToHttpCore.Entities
{
    public class DirectoryDetails
    {
        public string[] Flags { get; set; }
        public int ExistCount { get; set; }
        public int RecentCount { get; set; }
        public int UnseenCount { get; set; }
        public int UidValidity { get; set; }
        public int UidNext { get; set; }
    }
}