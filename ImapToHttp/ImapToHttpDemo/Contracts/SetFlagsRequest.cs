namespace ImapToHttpDemo.Controllers
{
    public class SetFlagsRequest
    {
        public string who { get; set; }
        public string[] flags { get; set; }
        public int directory_id { get; set; }
        public int[] uids { get; set; }
        public string flag_op { get; set; }
    }
}