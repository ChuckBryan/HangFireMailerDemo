namespace HangFireMailerDemo.Models
{
    using Postal;

    public class NewCommentEmail : Email
    {
        public string To { get; set; }
        public string UserName { get; set; }
        public string Comment { get; set; }
    }
}