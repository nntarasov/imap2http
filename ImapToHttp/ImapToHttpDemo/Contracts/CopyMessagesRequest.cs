namespace ImapToHttpDemo.Controllers
{
    public class CopyMessagesRequest
    {
        public string who { get; set; }
        public int directory_from_id { get; set; }
        public int directory_to_id { get; set; }
        public int[] uids { get; set; }
    }
}