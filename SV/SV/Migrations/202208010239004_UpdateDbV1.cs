namespace SV.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDbV1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Slot", "CourseId", c => c.Int(nullable: false));
            AddColumn("dbo.Slot", "LecturerId", c => c.Int(nullable: false));
            DropColumn("dbo.Slot", "LectureId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Slot", "LectureId", c => c.Int(nullable: false));
            DropColumn("dbo.Slot", "LecturerId");
            DropColumn("dbo.Slot", "CourseId");
        }
    }
}
