namespace PostgresWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookPageCount : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.Books", "PageCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("public.Books", "PageCount");
        }
    }
}
