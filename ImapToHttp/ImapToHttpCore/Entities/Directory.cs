namespace ImapToHttpCore.Entities
{
    public class Directory
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public string[] Flags { get; set; }
        public User Owner { get; set; }
    }
}