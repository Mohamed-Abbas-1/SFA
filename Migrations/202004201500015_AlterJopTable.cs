namespace SFA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterJopTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jops", "LocationId", c => c.Short(nullable: false));
            CreateIndex("dbo.Jops", "LocationId");
            AddForeignKey("dbo.Jops", "LocationId", "dbo.Locations", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jops", "LocationId", "dbo.Locations");
            DropIndex("dbo.Jops", new[] { "LocationId" });
            DropColumn("dbo.Jops", "LocationId");
        }
    }
}
