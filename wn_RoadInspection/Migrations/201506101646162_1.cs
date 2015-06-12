namespace wn_RoadInspection.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoadForm",
                c => new
                    {
                        RoadFormID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Group = c.String(),
                        Client = c.String(),
                        InspectorName = c.String(),
                        INSP_DATE = c.String(),
                        Licence = c.String(),
                        RoadName = c.String(),
                        DLO = c.String(),
                        KmFrom = c.String(),
                        KmTo = c.String(),
                        RoadStatus = c.String(),
                        StatusMatch = c.String(),
                        RS_Condition = c.String(),
                        RS_Notification = c.String(),
                        RS_RoadSurface = c.String(),
                        RS_GravelCondition = c.String(),
                        RS_VegetationCover = c.String(),
                        RS_CoverType = c.String(),
                        DI_Ditches = c.String(),
                        DI_VegetationCover = c.String(),
                        DI_CoverType = c.String(),
                        OT_Signage = c.String(),
                        OT_Crossings = c.String(),
                        OT_GroundAccess = c.String(),
                        OT_RoadMR = c.String(),
                        OT_RoadRIA = c.String(),
                        OT_Comments = c.String(),
                        Locations = c.String(),
                    })
                .PrimaryKey(t => t.RoadFormID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RoadForm");
        }
    }
}
