namespace SFA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterJopTable2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jops", "CareerId", c => c.Short(nullable: false));
            CreateIndex("dbo.Jops", "CareerId");
            AddForeignKey("dbo.Jops", "CareerId", "dbo.Careers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jops", "CareerId", "dbo.Careers");
            DropIndex("dbo.Jops", new[] { "CareerId" });
            DropColumn("dbo.Jops", "CareerId");
        }
    }
}
