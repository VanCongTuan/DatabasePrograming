namespace LTCSDLMayBay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveIdentityFromMaPhieu : DbMigration
    {
        public override void Up()
        {// Drop foreign key constraints
            DropForeignKey("dbo.VeDat", "phieuDatChoId", "dbo.PhieuDatCho");

            // Drop primary key constraint
            DropPrimaryKey("dbo.PhieuDatCho");

            // Alter the column to remove the identity specification
            AlterColumn("dbo.PhieuDatCho", "MaPhieu", c => c.Int(nullable: false));

            // Re-add the primary key constraint
            AddPrimaryKey("dbo.PhieuDatCho", "MaPhieu");

            // Re-add foreign key constraints
            AddForeignKey("dbo.VeDat", "phieuDatChoId", "dbo.PhieuDatCho", "MaPhieu");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VeDat", "phieuDatChoId", "dbo.PhieuDatCho");

            // Drop primary key constraint
            DropPrimaryKey("dbo.PhieuDatCho");

            // Revert the column to include the identity specification
            AlterColumn("dbo.PhieuDatCho", "MaPhieu", c => c.Int(nullable: false, identity: true));

            // Re-add the primary key constraint
            AddPrimaryKey("dbo.PhieuDatCho", "MaPhieu");

            // Re-add foreign key constraints
            AddForeignKey("dbo.VeDat", "phieuDatChoId", "dbo.PhieuDatCho", "MaPhieu");
        }
    }
}
