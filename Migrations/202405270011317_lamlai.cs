namespace LTCSDLMayBay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lamlai : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.VeDat", "phieuDatChoId", "dbo.PhieuDatCho");
            DropPrimaryKey("dbo.PhieuDatCho");
            AlterColumn("dbo.PhieuDatCho", "MaPhieu", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.PhieuDatCho", "MaPhieu");
            AddForeignKey("dbo.VeDat", "phieuDatChoId", "dbo.PhieuDatCho", "MaPhieu", cascadeDelete: true);
           
        }
        
        public override void Down()
        {
            AddColumn("dbo.PhieuDatCho", "id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.VeDat", "phieuDatChoId", "dbo.PhieuDatCho");
            DropPrimaryKey("dbo.PhieuDatCho");
            AlterColumn("dbo.PhieuDatCho", "MaPhieu", c => c.String(nullable: false));        
            AddForeignKey("dbo.VeDat", "phieuDatChoId", "dbo.PhieuDatCho", "id", cascadeDelete: true);
        }
    }
}
