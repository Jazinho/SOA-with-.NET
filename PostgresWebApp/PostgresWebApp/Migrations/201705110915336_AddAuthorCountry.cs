namespace PostgresWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuthorCountry : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.Authors", "Country", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("public.Authors", "Country");
        }
    }
}
