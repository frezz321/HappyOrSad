namespace HappyOrSad.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateHappyOrSadContext : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.QuestionResponses", "VillageID", "dbo.Villages");
            DropIndex("dbo.QuestionResponses", new[] { "VillageID" });
            AddColumn("dbo.QuestionResponses", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.AspNetUsers", "VillageID", c => c.Int(nullable: false));
            CreateIndex("dbo.QuestionResponses", "UserId");
            CreateIndex("dbo.AspNetUsers", "VillageID");
            AddForeignKey("dbo.QuestionResponses", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUsers", "VillageID", "dbo.Villages", "VillageID", cascadeDelete: true);
            DropColumn("dbo.QuestionResponses", "VillageID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.QuestionResponses", "VillageID", c => c.Int(nullable: false));
            DropForeignKey("dbo.AspNetUsers", "VillageID", "dbo.Villages");
            DropForeignKey("dbo.QuestionResponses", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "VillageID" });
            DropIndex("dbo.QuestionResponses", new[] { "UserId" });
            AlterColumn("dbo.AspNetUsers", "VillageID", c => c.String());
            DropColumn("dbo.QuestionResponses", "UserId");
            CreateIndex("dbo.QuestionResponses", "VillageID");
            AddForeignKey("dbo.QuestionResponses", "VillageID", "dbo.Villages", "VillageID", cascadeDelete: true);
        }
    }
}
