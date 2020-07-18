namespace SFA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExperienceTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Experiences",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Jops", "ExperienceId", c => c.Short());
            CreateIndex("dbo.Jops", "ExperienceId");
            AddForeignKey("dbo.Jops", "ExperienceId", "dbo.Experiences", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jops", "ExperienceId", "dbo.Experiences");
            DropIndex("dbo.Jops", new[] { "ExperienceId" });
            DropColumn("dbo.Jops", "ExperienceId");
            DropTable("dbo.Experiences");
        }
    }
}
