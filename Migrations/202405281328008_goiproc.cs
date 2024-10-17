namespace LTCSDLMayBay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class goiproc : DbMigration
    {
        public override void Up()
        {
           
        }
        
        public override void Down()
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
    }
}
