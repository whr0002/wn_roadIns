namespace wn_RoadInspection.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RoadInspection", "INSP_DATE", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RoadInspection", "INSP_DATE", c => c.String());
        }
    }
}
