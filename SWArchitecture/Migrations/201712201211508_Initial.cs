namespace SWArchitecture.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Specializations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Institute = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudyGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Course = c.Int(nullable: false),
                        Specialization_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Specializations", t => t.Specialization_Id)
                .Index(t => t.Specialization_Id);
            
            CreateTable(
                "dbo.SystemUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                        UserType = c.Int(nullable: false),
                        Name = c.String(),
                        Surname = c.String(),
                        Patronymic = c.String(),
                        Birthday = c.DateTime(nullable: false),
                        Group_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StudyGroups", t => t.Group_Id)
                .Index(t => t.Group_Id);
            
            CreateTable(
                "dbo.Statistics",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Mark = c.String(),
                        Date = c.DateTime(nullable: false),
                        Task_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Tasks", t => t.Task_Id, cascadeDelete: true)
                .ForeignKey("dbo.SystemUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Task_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Name = c.String(),
                        Upcode = c.String(),
                        TaskCode = c.String(),
                        DownCode = c.String(),
                        Answer = c.String(),
                        Description = c.String(),
                        RuleForTask = c.String(),
                        Language_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TaskLanguages", t => t.Language_Id)
                .Index(t => t.Language_Id);
            
            CreateTable(
                "dbo.TaskLanguages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Statistics", "User_Id", "dbo.SystemUsers");
            DropForeignKey("dbo.Statistics", "Task_Id", "dbo.Tasks");
            DropForeignKey("dbo.Tasks", "Language_Id", "dbo.TaskLanguages");
            DropForeignKey("dbo.SystemUsers", "Group_Id", "dbo.StudyGroups");
            DropForeignKey("dbo.StudyGroups", "Specialization_Id", "dbo.Specializations");
            DropIndex("dbo.Tasks", new[] { "Language_Id" });
            DropIndex("dbo.Statistics", new[] { "User_Id" });
            DropIndex("dbo.Statistics", new[] { "Task_Id" });
            DropIndex("dbo.SystemUsers", new[] { "Group_Id" });
            DropIndex("dbo.StudyGroups", new[] { "Specialization_Id" });
            DropTable("dbo.TaskLanguages");
            DropTable("dbo.Tasks");
            DropTable("dbo.Statistics");
            DropTable("dbo.SystemUsers");
            DropTable("dbo.StudyGroups");
            DropTable("dbo.Specializations");
        }
    }
}
