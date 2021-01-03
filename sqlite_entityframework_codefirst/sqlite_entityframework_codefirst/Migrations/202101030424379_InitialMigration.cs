namespace sqlite_entityframework_codefirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Table1",
                c => new
                    {
                        idStr1 = c.String(nullable: false, maxLength: 260),
                        idStr2 = c.String(nullable: false, maxLength: 32767),
                        value1 = c.String(maxLength: 16),
                        data1 = c.Binary(),
                    })
                .PrimaryKey(t => new { t.idStr1, t.idStr2 });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Table1");
        }
    }
}
