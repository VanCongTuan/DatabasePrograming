namespace LTCSDLMayBay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TaoMoiView : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LichBayViewModels",
                c => new
                    {
                        MaLB = c.Int(nullable: false, identity: true),
                        MaCB = c.Int(nullable: false),
                        MayBay = c.String(),
                        NgayKhoiHanh = c.DateTime(nullable: false),
                        ThoiGianDi = c.Int(nullable: false),
                        HangVe = c.String(),
                        SanBayTrungGian = c.String(),
                        GiaVe = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.MaLB);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LichBayViewModels");
        }
    }
}
