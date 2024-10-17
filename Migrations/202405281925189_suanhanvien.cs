namespace LTCSDLMayBay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class suanhanvien : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.NhanVien", "NgaySinh", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.NhanVien", "NgaySinh", c => c.DateTime());
        }
    }
}
