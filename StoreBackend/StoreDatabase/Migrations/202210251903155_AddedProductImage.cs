namespace StoreDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProductImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Image", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Image");
        }
    }
}
