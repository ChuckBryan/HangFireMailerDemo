namespace HangFireMailerDemo.Models
{
    using Postal;

    public class NewQuoteEmail : Email
    {
        public string To { get; set; }
        public string CharacterName { get; set; }
        public string Quote { get; set; }
        public string Movie { get; set; }
    }
}