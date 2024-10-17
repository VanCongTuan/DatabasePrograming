using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using LTCSDLMayBay.Models;


namespace LTCSDLMayBay.Models
{
    public class ApplicationDBcontext :DbContext
    {
        public ApplicationDBcontext(): base("name=StrConnect") {
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<TaiKhoan> TaiKhoans { set; get; }
         public DbSet<BaoCaoDoanhThu> BaoCaoDoanhThus { set; get; }
        public DbSet<ChiTietDoanhThu> ChiTietDoanhThus { set; get; }

        public DbSet<ChuyenBay> ChuyenBays
        {
            set; get;
        }
         public DbSet<Email> Emails { set; get; }
        public DbSet<Ghe> Ghes { set; get; }
        public DbSet<GiaVe> GiaVes
        {
            set; get;
        }
        public DbSet<HangVe> HangVes { set; get; }
        public DbSet<HanhKhach> HanhKhachs { set; get; }
        public DbSet<HoaDon> HoaDons
        {
            set; get;
        }
        public DbSet<LichBay> LichBays { set; get; }
        public DbSet<LichBay_GiaVe> LichBay_GiaVes { set; get; }
        public DbSet<MayBay> MayBays
        {            set; get;        }
        public DbSet<MayBay_Ghe> MayBay_Ghes { set; get; }
        public DbSet<NguoiLon> NguoiLons { set; get; }
        public DbSet<NhanVien> NhanViens
        {           set; get;
       }
        public DbSet<PhieuDatCho> PhieuDatChos { set; get; }
        public DbSet<QuyDinh> QuyDinhs { set; get; }
        public DbSet<SanBay> SanBays
        {
            set; get;
        }
        public DbSet<SanBayTrungGian> SanBayTrungGians { set; get; }
        public DbSet<SoDienThoai> SoDienThoais { set; get; }
        public DbSet<TuyenBay> TuyenBays
        {
            set; get;
        }
        public DbSet<TreEm> TreEms { set; get; }
        public DbSet<VeBan> VeBans { set; get; }
        public DbSet<VeDat> VeDats { set; get; }

        public List<NhanVien> GetAllNhanVien()
        {
            return this.Database.SqlQuery<NhanVien>("LayDanhSachNhanVien").ToList();
        }
        public void ThemNhanVien(string hoVaTen, DateTime ngaySinh, string gioiTinh, int luong, int taiKhoanId)
        {
            this.Database.ExecuteSqlCommand("taoNhanVien @HoVaTen, @NgaySinh, @GioiTinh, @Luong, @TaiKhoanId",
                                             new SqlParameter("HoVaTen", hoVaTen),
                                             new SqlParameter("NgaySinh", ngaySinh),
                                             new SqlParameter("GioiTinh", gioiTinh),
                                             new SqlParameter("Luong", luong),
                                             new SqlParameter("TaiKhoanId", taiKhoanId));
        }

        // Sửa thông tin nhân viên
        public void SuaNhanVien(int id, string hoVaTen, DateTime ngaySinh, string gioiTinh, int luong, int taiKhoanId)
        {
            this.Database.ExecuteSqlCommand("SuaNhanVien @Id, @HoVaTen, @NgaySinh, @GioiTinh, @Luong, @TaiKhoanId",
                                             new SqlParameter("Id", id),
                                             new SqlParameter("HoVaTen", hoVaTen),
                                             new SqlParameter("NgaySinh", ngaySinh),
                                             new SqlParameter("GioiTinh", gioiTinh),
                                             new SqlParameter("Luong", luong),
                                             new SqlParameter("TaiKhoanId", taiKhoanId));
        }

        // Xóa nhân viên
        public void XoaNhanVien(int id)
        {
            this.Database.ExecuteSqlCommand("XoaNhanVien @Id",
                                             new SqlParameter("Id", id));
        }
    }
}