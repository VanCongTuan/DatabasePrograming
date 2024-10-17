namespace LTCSDLMayBay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class taobang : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BaoCaoDoanhThu",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NgayXuat = c.DateTime(nullable: false),
                        ThangDoanhThu = c.DateTime(nullable: false),
                        TongDoanhThu = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ChiTietDoanhThu",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SoLuotBay = c.Int(nullable: false),
                        TyLe = c.Single(nullable: false),
                        DoanhThu = c.Single(nullable: false),
                        lichBayId = c.Int(nullable: false),
                        baoCaoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BaoCaoDoanhThu", t => t.baoCaoId, cascadeDelete: true)
                .ForeignKey("dbo.TuyenBay", t => t.lichBayId, cascadeDelete: true)
                .Index(t => t.lichBayId)
                .Index(t => t.baoCaoId);
            
            CreateTable(
                "dbo.TuyenBay",
                c => new
                    {
                        MaTuyenBay = c.Int(nullable: false, identity: true),
                        Id_SbDi = c.Int(nullable: false),
                        Id_SbDen = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaTuyenBay)
                .ForeignKey("dbo.SanBay", t => t.Id_SbDen, cascadeDelete: false)
                .ForeignKey("dbo.SanBay", t => t.Id_SbDi, cascadeDelete: false)
                .Index(t => t.Id_SbDi)
                .Index(t => t.Id_SbDen);
            
            CreateTable(
                "dbo.SanBay",
                c => new
                    {
                        MaSB = c.Int(nullable: false, identity: true),
                        TenSB = c.String(nullable: false, maxLength: 30),
                        DiaChi = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.MaSB);
            
            CreateTable(
                "dbo.ChuyenBay",
                c => new
                    {
                        MaCB = c.Int(nullable: false, identity: true),
                        TenCB = c.String(),
                        tuyenBayId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaCB)
                .ForeignKey("dbo.TuyenBay", t => t.tuyenBayId, cascadeDelete: true)
                .Index(t => t.tuyenBayId);
            
            CreateTable(
                "dbo.Email",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ten = c.String(nullable: false, maxLength: 50),
                        nguoiLonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NguoiLon", t => t.nguoiLonId, cascadeDelete: true)
                .Index(t => t.nguoiLonId);
            
            CreateTable(
                "dbo.NguoiLon",
                c => new
                    {
                        idNguoiLon = c.Int(nullable: false),
                        CCCD = c.String(nullable: false, maxLength: 13),
                    })
                .PrimaryKey(t => t.idNguoiLon)
                .ForeignKey("dbo.HanhKhach", t => t.idNguoiLon)
                .Index(t => t.idNguoiLon);
            
            CreateTable(
                "dbo.HanhKhach",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GioiTinh = c.String(nullable: false, maxLength: 3),
                        HoTen = c.String(nullable: false, maxLength: 30),
                        NgSinh = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ghe",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GiaVe",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Gia = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HangVe",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LoaiHang = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HoaDon",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NgayLap = c.DateTime(nullable: false),
                        TongTien = c.Single(nullable: false),
                        NgHetHanThanhToan = c.DateTime(nullable: false),
                        lichBayId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LichBay", t => t.lichBayId, cascadeDelete: true)
                .Index(t => t.lichBayId);
            
            CreateTable(
                "dbo.LichBay",
                c => new
                    {
                        MaLB = c.Int(nullable: false, identity: true),
                        NgayBay = c.DateTime(nullable: false),
                        ThoiGianBay = c.Int(nullable: false),
                        TrangThai = c.String(maxLength: 20),
                        mayBayId = c.Int(nullable: false),
                        chuyenBayId = c.Int(nullable: false),
                        nhanVienId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaLB)
                .ForeignKey("dbo.ChuyenBay", t => t.chuyenBayId, cascadeDelete: true)
                .ForeignKey("dbo.MayBay", t => t.mayBayId, cascadeDelete: true)
                .ForeignKey("dbo.NhanVien", t => t.nhanVienId, cascadeDelete: true)
                .Index(t => t.mayBayId)
                .Index(t => t.chuyenBayId)
                .Index(t => t.nhanVienId);
            
            CreateTable(
                "dbo.MayBay",
                c => new
                    {
                        MaMb = c.Int(nullable: false, identity: true),
                        TenMB = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.MaMb);
            
            CreateTable(
                "dbo.NhanVien",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HoVaTen = c.String(nullable: false, maxLength: 20),
                        NgaySinh = c.DateTime(),
                        GioiTinh = c.String(nullable: false, maxLength: 5),
                        Luong = c.Int(nullable: false),
                        taiKhoanId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TaiKhoan", t => t.taiKhoanId, cascadeDelete: true)
                .Index(t => t.taiKhoanId);
            
            CreateTable(
                "dbo.TaiKhoan",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 100),
                        Avatar = c.String(),
                        UserRole = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LichBay_GiaVe",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NgayApDung = c.DateTime(nullable: false),
                        NgayKetThuc = c.DateTime(nullable: false),
                        SoLuongGhe = c.Int(nullable: false),
                        hangVeId = c.Int(nullable: false),
                        lichBayId = c.Int(nullable: false),
                        giaVeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GiaVe", t => t.giaVeId, cascadeDelete: true)
                .ForeignKey("dbo.HangVe", t => t.hangVeId, cascadeDelete: true)
                .ForeignKey("dbo.LichBay", t => t.lichBayId, cascadeDelete: true)
                .Index(t => t.hangVeId)
                .Index(t => t.lichBayId)
                .Index(t => t.giaVeId);
            
            CreateTable(
                "dbo.MayBay_Ghe",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        gheId = c.Int(nullable: false),
                        hangVeId = c.Int(nullable: false),
                        mayBayId = c.Int(nullable: false),
                        DayGhe = c.String(nullable: false, maxLength: 3),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ghe", t => t.gheId, cascadeDelete: true)
                .ForeignKey("dbo.HangVe", t => t.hangVeId, cascadeDelete: true)
                .ForeignKey("dbo.MayBay", t => t.mayBayId, cascadeDelete: true)
                .Index(t => t.gheId)
                .Index(t => t.hangVeId)
                .Index(t => t.mayBayId);
            
            CreateTable(
                "dbo.PhieuDatCho",
                c => new
                    {
                        MaPhieu = c.Int(nullable: false, identity: true),
                        NgMua = c.DateTime(nullable: false),
                        TrangThai = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.MaPhieu);
            
            CreateTable(
                "dbo.QuyDinh",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ThoiGianChamNhatDatVe = c.Int(nullable: false),
                        ThoiGianChamNhatBanVe = c.Int(nullable: false),
                        ThoiGianBayToiThieu = c.Int(nullable: false),
                        SanBayTG_ToiDa = c.Int(nullable: false),
                        ThoiGianDungToiThieu = c.Int(nullable: false),
                        ThoiGianDungToiDa = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SanBayTrungGian",
                c => new
                    {
                        MaSBTG = c.Int(nullable: false, identity: true),
                        ThoiGianDung = c.Int(nullable: false),
                        SanBayId = c.Int(nullable: false),
                        ChuyenBayId = c.Int(nullable: false),
                        GhiChu = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.MaSBTG)
                .ForeignKey("dbo.ChuyenBay", t => t.ChuyenBayId, cascadeDelete: true)
                .ForeignKey("dbo.SanBay", t => t.SanBayId, cascadeDelete: true)
                .Index(t => t.SanBayId)
                .Index(t => t.ChuyenBayId);
            
            CreateTable(
                "dbo.SoDienThoai",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sdt = c.String(maxLength: 14),
                        nguoiLonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NguoiLon", t => t.nguoiLonId, cascadeDelete: true)
                .Index(t => t.nguoiLonId);
            
            CreateTable(
                "dbo.TreEm",
                c => new
                    {
                        Id_TreEm = c.Int(nullable: false),
                        NguoiLon_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_TreEm)
                .ForeignKey("dbo.HanhKhach", t => t.Id_TreEm)
                .ForeignKey("dbo.NguoiLon", t => t.NguoiLon_id, cascadeDelete: true)
                .Index(t => t.Id_TreEm)
                .Index(t => t.NguoiLon_id);
            
            CreateTable(
                "dbo.VeBan",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        veDatId = c.Int(nullable: false),
                        nguoiLon_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NhanVien", t => t.nguoiLon_id, cascadeDelete: true)
                .ForeignKey("dbo.VeDat", t => t.veDatId, cascadeDelete: true)
                .Index(t => t.veDatId)
                .Index(t => t.nguoiLon_id);
            
            CreateTable(
                "dbo.VeDat",
                c => new
                    {
                        MaVe = c.Int(nullable: false, identity: true),
                        NgDat = c.DateTime(nullable: false),
                        GheDaDat = c.String(maxLength: 4),
                        TinhTrangVe = c.String(maxLength: 20),
                        lichBayId = c.Int(nullable: false),
                        hanhKhachId = c.Int(nullable: false),
                        hangVeId = c.Int(nullable: false),
                        phieuDatChoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaVe)
                .ForeignKey("dbo.HangVe", t => t.hangVeId, cascadeDelete: true)
                .ForeignKey("dbo.HanhKhach", t => t.hanhKhachId, cascadeDelete: true)
                .ForeignKey("dbo.LichBay", t => t.lichBayId, cascadeDelete: false)
                .ForeignKey("dbo.PhieuDatCho", t => t.phieuDatChoId, cascadeDelete: true)
                .Index(t => t.lichBayId)
                .Index(t => t.hanhKhachId)
                .Index(t => t.hangVeId)
                .Index(t => t.phieuDatChoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VeBan", "veDatId", "dbo.VeDat");
            DropForeignKey("dbo.VeDat", "phieuDatChoId", "dbo.PhieuDatCho");
            DropForeignKey("dbo.VeDat", "lichBayId", "dbo.LichBay");
            DropForeignKey("dbo.VeDat", "hanhKhachId", "dbo.HanhKhach");
            DropForeignKey("dbo.VeDat", "hangVeId", "dbo.HangVe");
            DropForeignKey("dbo.VeBan", "nguoiLon_id", "dbo.NhanVien");
            DropForeignKey("dbo.TreEm", "NguoiLon_id", "dbo.NguoiLon");
            DropForeignKey("dbo.TreEm", "Id_TreEm", "dbo.HanhKhach");
            DropForeignKey("dbo.SoDienThoai", "nguoiLonId", "dbo.NguoiLon");
            DropForeignKey("dbo.SanBayTrungGian", "SanBayId", "dbo.SanBay");
            DropForeignKey("dbo.SanBayTrungGian", "ChuyenBayId", "dbo.ChuyenBay");
            DropForeignKey("dbo.MayBay_Ghe", "mayBayId", "dbo.MayBay");
            DropForeignKey("dbo.MayBay_Ghe", "hangVeId", "dbo.HangVe");
            DropForeignKey("dbo.MayBay_Ghe", "gheId", "dbo.Ghe");
            DropForeignKey("dbo.LichBay_GiaVe", "lichBayId", "dbo.LichBay");
            DropForeignKey("dbo.LichBay_GiaVe", "hangVeId", "dbo.HangVe");
            DropForeignKey("dbo.LichBay_GiaVe", "giaVeId", "dbo.GiaVe");
            DropForeignKey("dbo.HoaDon", "lichBayId", "dbo.LichBay");
            DropForeignKey("dbo.LichBay", "nhanVienId", "dbo.NhanVien");
            DropForeignKey("dbo.NhanVien", "taiKhoanId", "dbo.TaiKhoan");
            DropForeignKey("dbo.LichBay", "mayBayId", "dbo.MayBay");
            DropForeignKey("dbo.LichBay", "chuyenBayId", "dbo.ChuyenBay");
            DropForeignKey("dbo.Email", "nguoiLonId", "dbo.NguoiLon");
            DropForeignKey("dbo.NguoiLon", "idNguoiLon", "dbo.HanhKhach");
            DropForeignKey("dbo.ChuyenBay", "tuyenBayId", "dbo.TuyenBay");
            DropForeignKey("dbo.ChiTietDoanhThu", "lichBayId", "dbo.TuyenBay");
            DropForeignKey("dbo.TuyenBay", "Id_SbDi", "dbo.SanBay");
            DropForeignKey("dbo.TuyenBay", "Id_SbDen", "dbo.SanBay");
            DropForeignKey("dbo.ChiTietDoanhThu", "baoCaoId", "dbo.BaoCaoDoanhThu");
            DropIndex("dbo.VeDat", new[] { "phieuDatChoId" });
            DropIndex("dbo.VeDat", new[] { "hangVeId" });
            DropIndex("dbo.VeDat", new[] { "hanhKhachId" });
            DropIndex("dbo.VeDat", new[] { "lichBayId" });
            DropIndex("dbo.VeBan", new[] { "nguoiLon_id" });
            DropIndex("dbo.VeBan", new[] { "veDatId" });
            DropIndex("dbo.TreEm", new[] { "NguoiLon_id" });
            DropIndex("dbo.TreEm", new[] { "Id_TreEm" });
            DropIndex("dbo.SoDienThoai", new[] { "nguoiLonId" });
            DropIndex("dbo.SanBayTrungGian", new[] { "ChuyenBayId" });
            DropIndex("dbo.SanBayTrungGian", new[] { "SanBayId" });
            DropIndex("dbo.MayBay_Ghe", new[] { "mayBayId" });
            DropIndex("dbo.MayBay_Ghe", new[] { "hangVeId" });
            DropIndex("dbo.MayBay_Ghe", new[] { "gheId" });
            DropIndex("dbo.LichBay_GiaVe", new[] { "giaVeId" });
            DropIndex("dbo.LichBay_GiaVe", new[] { "lichBayId" });
            DropIndex("dbo.LichBay_GiaVe", new[] { "hangVeId" });
            DropIndex("dbo.NhanVien", new[] { "taiKhoanId" });
            DropIndex("dbo.LichBay", new[] { "nhanVienId" });
            DropIndex("dbo.LichBay", new[] { "chuyenBayId" });
            DropIndex("dbo.LichBay", new[] { "mayBayId" });
            DropIndex("dbo.HoaDon", new[] { "lichBayId" });
            DropIndex("dbo.NguoiLon", new[] { "idNguoiLon" });
            DropIndex("dbo.Email", new[] { "nguoiLonId" });
            DropIndex("dbo.ChuyenBay", new[] { "tuyenBayId" });
            DropIndex("dbo.TuyenBay", new[] { "Id_SbDen" });
            DropIndex("dbo.TuyenBay", new[] { "Id_SbDi" });
            DropIndex("dbo.ChiTietDoanhThu", new[] { "baoCaoId" });
            DropIndex("dbo.ChiTietDoanhThu", new[] { "lichBayId" });
            DropTable("dbo.VeDat");
            DropTable("dbo.VeBan");
            DropTable("dbo.TreEm");
            DropTable("dbo.SoDienThoai");
            DropTable("dbo.SanBayTrungGian");
            DropTable("dbo.QuyDinh");
            DropTable("dbo.PhieuDatCho");
            DropTable("dbo.MayBay_Ghe");
            DropTable("dbo.LichBay_GiaVe");
            DropTable("dbo.TaiKhoan");
            DropTable("dbo.NhanVien");
            DropTable("dbo.MayBay");
            DropTable("dbo.LichBay");
            DropTable("dbo.HoaDon");
            DropTable("dbo.HangVe");
            DropTable("dbo.GiaVe");
            DropTable("dbo.Ghe");
            DropTable("dbo.HanhKhach");
            DropTable("dbo.NguoiLon");
            DropTable("dbo.Email");
            DropTable("dbo.ChuyenBay");
            DropTable("dbo.SanBay");
            DropTable("dbo.TuyenBay");
            DropTable("dbo.ChiTietDoanhThu");
            DropTable("dbo.BaoCaoDoanhThu");
        }
    }
}
