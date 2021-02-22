namespace CMS.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nextmigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Student", "BirthDate", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Student", "BirthDate", c => c.DateTime(nullable: false));
        }
    }
}
