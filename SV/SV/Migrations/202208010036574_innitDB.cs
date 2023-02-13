namespace SV.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class innitDB : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Class", newName: "Course");
            RenameTable(name: "dbo.ScheduleEntities", newName: "SlotStudent");
            RenameTable(name: "dbo.Users", newName: "Employee");
            CreateTable(
                "dbo.Lecturer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        Address = c.String(),
                        Gender = c.Int(nullable: false),
                        Phone = c.String(),
                        DateOfBirth = c.String(),
                        Role = c.Int(nullable: false),
                        Email = c.String(),
                        Password = c.String(),
                        Username = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Slot",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        SubjectId = c.Int(nullable: false),
                        LectureId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudentMark",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                        PointStudy = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PointGK = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PointCK = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        Address = c.String(),
                        Gender = c.Int(nullable: false),
                        Phone = c.String(),
                        Role = c.Int(nullable: false),
                        DateOfBirth = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Username = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.SlotStudent", "SlotId", c => c.Int(nullable: false));
            DropColumn("dbo.Course", "SubjectId");
            DropColumn("dbo.SlotStudent", "LopId");
            DropColumn("dbo.SlotStudent", "Rank");
            DropColumn("dbo.SlotStudent", "Session");
            DropTable("dbo.ClassSchedule");
            DropTable("dbo.SubjectLop");
            DropTable("dbo.UserLop");
            DropTable("dbo.UserPoint");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserPoint",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Period = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        PointStudy = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PointGK = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PointCK = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserLop",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        LopId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubjectLop",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        LopId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ClassSchedule",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LopId = c.Int(nullable: false),
                        Rank = c.Int(nullable: false),
                        Session = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.SlotStudent", "Session", c => c.Int(nullable: false));
            AddColumn("dbo.SlotStudent", "Rank", c => c.Int(nullable: false));
            AddColumn("dbo.SlotStudent", "LopId", c => c.Int(nullable: false));
            AddColumn("dbo.Course", "SubjectId", c => c.Int(nullable: false));
            DropColumn("dbo.SlotStudent", "SlotId");
            DropTable("dbo.Student");
            DropTable("dbo.StudentMark");
            DropTable("dbo.Slot");
            DropTable("dbo.Lecturer");
            RenameTable(name: "dbo.Employee", newName: "Users");
            RenameTable(name: "dbo.SlotStudent", newName: "ScheduleEntities");
            RenameTable(name: "dbo.Course", newName: "Class");
        }
    }
}
