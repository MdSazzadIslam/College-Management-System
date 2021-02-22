namespace CMS.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        CourseCode = c.String(nullable: false),
                        CourseName = c.String(nullable: false),
                        CourseCredit = c.Double(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        DeleteDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.CourseId);
            
            CreateTable(
                "dbo.StudentEnrollment",
                c => new
                    {
                        StudentEnrollmentId = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                        GradeId = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        DeleteDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.StudentEnrollmentId)
                .ForeignKey("dbo.Course", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Grade", t => t.GradeId, cascadeDelete: true)
                .ForeignKey("dbo.Student", t => t.StudentId, cascadeDelete: true)
                .ForeignKey("dbo.Subject", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.CourseId)
                .Index(t => t.SubjectId)
                .Index(t => t.GradeId);
            
            CreateTable(
                "dbo.Grade",
                c => new
                    {
                        GradeId = c.Int(nullable: false, identity: true),
                        GradeName = c.String(nullable: false),
                        GradePoint = c.Double(nullable: false),
                        ScoreFrom = c.Int(nullable: false),
                        ScoreTo = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        DeleteDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.GradeId);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        StudentName = c.String(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        RegistrationNo = c.String(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        DeleteDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.StudentId);
            
            CreateTable(
                "dbo.Subject",
                c => new
                    {
                        SubjectId = c.Int(nullable: false, identity: true),
                        SubjectCode = c.String(nullable: false),
                        SubjectName = c.String(nullable: false),
                        SubjectCredit = c.Double(nullable: false),
                        CourseId = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        DeleteDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.SubjectId)
                .ForeignKey("dbo.Course", t => t.CourseId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.TeacherEnrollment",
                c => new
                    {
                        TeacherEnrollmentId = c.Int(nullable: false, identity: true),
                        TeacherId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        DeleteDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.TeacherEnrollmentId)
                .ForeignKey("dbo.Course", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Subject", t => t.SubjectId, cascadeDelete: true)
                .ForeignKey("dbo.Teacher", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.TeacherId)
                .Index(t => t.CourseId)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.Teacher",
                c => new
                    {
                        TeacherId = c.Int(nullable: false, identity: true),
                        TeacherName = c.String(nullable: false),
                        BirthDate = c.String(nullable: false),
                        Salary = c.Double(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        DeleteDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.TeacherId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeacherEnrollment", "TeacherId", "dbo.Teacher");
            DropForeignKey("dbo.TeacherEnrollment", "SubjectId", "dbo.Subject");
            DropForeignKey("dbo.TeacherEnrollment", "CourseId", "dbo.Course");
            DropForeignKey("dbo.StudentEnrollment", "SubjectId", "dbo.Subject");
            DropForeignKey("dbo.Subject", "CourseId", "dbo.Course");
            DropForeignKey("dbo.StudentEnrollment", "StudentId", "dbo.Student");
            DropForeignKey("dbo.StudentEnrollment", "GradeId", "dbo.Grade");
            DropForeignKey("dbo.StudentEnrollment", "CourseId", "dbo.Course");
            DropIndex("dbo.TeacherEnrollment", new[] { "SubjectId" });
            DropIndex("dbo.TeacherEnrollment", new[] { "CourseId" });
            DropIndex("dbo.TeacherEnrollment", new[] { "TeacherId" });
            DropIndex("dbo.Subject", new[] { "CourseId" });
            DropIndex("dbo.StudentEnrollment", new[] { "GradeId" });
            DropIndex("dbo.StudentEnrollment", new[] { "SubjectId" });
            DropIndex("dbo.StudentEnrollment", new[] { "CourseId" });
            DropIndex("dbo.StudentEnrollment", new[] { "StudentId" });
            DropTable("dbo.Teacher");
            DropTable("dbo.TeacherEnrollment");
            DropTable("dbo.Subject");
            DropTable("dbo.Student");
            DropTable("dbo.Grade");
            DropTable("dbo.StudentEnrollment");
            DropTable("dbo.Course");
        }
    }
}
