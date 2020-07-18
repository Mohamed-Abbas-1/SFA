namespace SFA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLocationTable1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Locations");
            AlterColumn("dbo.Locations", "Id", c => c.Short(nullable: false, identity: true));
            AddPrimaryKey("dbo.Locations", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Locations");
            AlterColumn("dbo.Locations", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Locations", "Id");
        }
    }
}
