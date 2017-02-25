namespace DeejayApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Deejays", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Deejays", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Deejays", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Deejays", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Deejays", "Password", c => c.String());
            AlterColumn("dbo.Deejays", "Email", c => c.String());
            AlterColumn("dbo.Deejays", "LastName", c => c.String());
            AlterColumn("dbo.Deejays", "FirstName", c => c.String());
        }
    }
}
