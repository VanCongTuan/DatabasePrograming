namespace LTCSDLMayBay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LichBayView : DbMigration
    {
        public override void Up()
        {
           
            CreateTable(
            name: "dbo.LichBayView",
           c => new
           {
                MaLB = c.Int(nullable: false, identity: true),
                MaCB = c.Int(nullable: false),
                MayBay = c.String(nullable: false),
                NgayKhoiHanh = c.DateTime(nullable: false),
                ThoiGianDi = c.Int(nullable: false),
                HangVe = c.String(nullable: true),
                SanBayTrungGian = c.String(nullable: true),
                GiaVe = c.Single(nullable: false)
           }).PrimaryKey(t => t.MaLB);


        }
        
        public override void Down()
        {
            DropTable("dbo.LichBayView");
        }
    }
}
