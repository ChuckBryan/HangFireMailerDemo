namespace HangFireMailerDemo.Models
{
    using System.ComponentModel.DataAnnotations;

    public class MovieQuote
    {
        public int Id { get; set; }

        [Required]
        public string Movie { get; set; }

        [Required, Display(Name = "Character")]
        public string CharacterName { get; set; }

        [Required]
        public string Quote { get; set; }
    }
}