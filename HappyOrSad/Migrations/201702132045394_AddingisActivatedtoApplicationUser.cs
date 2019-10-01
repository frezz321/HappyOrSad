namespace HappyOrSad.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingisActivatedtoApplicationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "isActivated", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "isActivated");
        }
    }
}
