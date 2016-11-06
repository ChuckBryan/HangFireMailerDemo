namespace HangFireMailerDemo.Models
{
    using System.CodeDom;
    using System.Data.Entity;

    public class MailerDbContext : DbContext
    {
        public MailerDbContext() : base("DefaultConnection")
        {
            
        }

        public DbSet<MovieQuote> MovieQuotes { get; set; }
    }
}