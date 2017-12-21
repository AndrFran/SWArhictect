namespace SWArchitecture.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatedmarkinstatistic : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Statistics", "Mark", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Statistics", "Mark", c => c.String());
        }
    }
}
