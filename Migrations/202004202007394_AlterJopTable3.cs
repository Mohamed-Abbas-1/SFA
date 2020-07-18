namespace SFA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterJopTable3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Jops", "CareerId", "dbo.Careers");
            DropForeignKey("dbo.Jops", "LocationId", "dbo.Locations");
            DropIndex("dbo.Jops", new[] { "LocationId" });
            DropIndex("dbo.Jops", new[] { "CareerId" });
            AlterColumn("dbo.Jops", "LocationId", c => c.Short());
            AlterColumn("dbo.Jops", "CareerId", c => c.Short());
            CreateIndex("dbo.Jops", "LocationId");
            CreateIndex("dbo.Jops", "CareerId");
            AddForeignKey("dbo.Jops", "CareerId", "dbo.Careers", "Id");
            AddForeignKey("dbo.Jops", "LocationId", "dbo.Locations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jops", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Jops", "CareerId", "dbo.Careers");
            DropIndex("dbo.Jops", new[] { "CareerId" });
            DropIndex("dbo.Jops", new[] { "LocationId" });
            AlterColumn("dbo.Jops", "CareerId", c => c.Short(nullable: false));
            AlterColumn("dbo.Jops", "LocationId", c => c.Short(nullable: false));
            CreateIndex("dbo.Jops", "CareerId");
            CreateIndex("dbo.Jops", "LocationId");
            AddForeignKey("dbo.Jops", "LocationId", "dbo.Locations", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Jops", "CareerId", "dbo.Careers", "Id", cascadeDelete: true);
        }
    }
}
