namespace HangFireMailerDemo.Models
{
    using System.CodeDom;
    using System.Data.Entity;

    public class MailerDbContext : DbContext
    {
        public MailerDbContext() : base("MailerDb")
        {
            
        }

        public DbSet<MovieQuote> Comments { get; set; }
    }
}