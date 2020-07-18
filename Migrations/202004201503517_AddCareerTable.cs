namespace SFA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCareerTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Careers",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Careers");
        }
    }
}
