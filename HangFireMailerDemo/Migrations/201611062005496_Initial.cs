namespace HangFireMailerDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MovieQuotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Movie = c.String(nullable: false),
                        CharacterName = c.String(nullable: false),
                        Quote = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MovieQuotes");
        }
    }
}
