using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LTCSDLMayBay.Models;

namespace LTCSDLMayBay.Dao
{
    public class Dao
    {
        public ApplicationDBcontext db = new ApplicationDBcontext();

        public QuyDinh get_rule()
        {
            var s = db.QuyDinhs.First();
            return s;
        }
        public List<SanBay> get_destination()
        {
            var s = db.SanBays.ToList();

            return s;
        }
       
        public List<MayBay> get_MayBayLL()
        {
            var s = db.MayBays.ToList();

            return s;
        }
        public List<HangVe> get_ticketLevel()
        {
            var s = db.HangVes.ToList();

            return s;
        }
        public List<LichBay> get_LichBay()
        {
            var s = db.LichBays.ToList();

            return s;
        }
        public NhanVien GetHotenByAccount(string username, string password)
        {
            var query = from nv in db.NhanViens
                        join tk in db.TaiKhoans on nv.taiKhoanId equals tk.Id
                        where tk.Username == username && tk.Password == password
                        select new { nv.HoVaTen, nv.Id};

            var result = query.FirstOrDefault();

            if (result != null)
            {
                // Tạo một đối tượng NhanVien từ kết quả truy vấn
                return new NhanVien { HoVaTen = result.HoVaTen, Id = result.Id };
            }

            return null;
        }
        public List<LichBay> filter_LichBay_With_Valid_Time(DateTime startDate, DateTime dateNow, int? idNv)
        {
            List<LichBay> listLichBay = new List<LichBay>();
            
            foreach (var lb in get_LichBay())
            {
                dateNow = dateNow.ToLocalTime(); // Assuming dateNow is in UTC, you might need to adjust this according to your timezone
                TimeSpan dateDelta = lb.NgayBay - dateNow;

                if (lb.NgayBay.Date == startDate.Date)
                {
                    double further = Math.Abs(dateDelta.TotalHours);
                    
                        listLichBay.Add(lb);
                    
                }
            }

            return listLichBay;
        }

        public dynamic CountSoGheDaDat(int hangVe, int year, int month, int day)
        {
            var query = from vd in db.VeDats
                        join lb in db.LichBays on vd.LichBay.MaLB equals lb.MaLB
                        join lgv in db.LichBay_GiaVes on new { lbId = lb.MaLB, hangVeId = vd.HangVe.Id } equals new { lbId = lgv.LichBay.MaLB, hangVeId = lgv.HangVe.Id }
                        where vd.HangVe.Id == hangVe &&
                              lb.NgayBay.Year == year &&
                              lb.NgayBay.Month == month &&
                              lb.NgayBay.Day == day
                        group vd by new { vd.LichBay.MaLB, vd.HangVe.Id, lb.ChuyenBay.MaCB, lgv.SoLuongGhe } into g
                        select new
                        {
                            LichBayId = g.Key.MaLB,
                            MaHangVe = g.Key.Id,
                            IdChuyenBay = g.Key.MaCB,
                            SoLuongGhe = g.Key.SoLuongGhe,
                            SoLuongVeDaDat = g.Count()
                        };

            return query;
        }

        public List<LichBay> GetLichBayWithChuyenBays(List<ChuyenBay> listChuyenBay, List<LichBay> listLichBayFilterByTime)
        {
            List<LichBay> listLB = new List<LichBay>();

            foreach (var chuyenBay in listChuyenBay)
            {
                foreach (var lb in listLichBayFilterByTime)
                {
                    if (chuyenBay.MaCB == lb.chuyenBayId)
                    {
                        listLB.Add(lb);
                    }
                }
            }

            return listLB;
        }
        public float get_GiaVe(int HangVe)
        {


            var result  = db.GiaVes.FirstOrDefault(s=>s.Id==HangVe);
            return result.Gia;
        }

        public int GetIdHangVe(string hangVe)
        {
            var ticketLevels = get_ticketLevel();

            foreach (var hang in ticketLevels)
            {
                if (hang.LoaiHang == hangVe)
                {
                    return hang.Id;

                }
            }

            return -1;
        }
        public string GetHangVeByID(int mave)
        {
            var ticketLevels = get_ticketLevel();

            foreach (var ma in ticketLevels)
            {
                if (ma.Id == mave)
                {
                    return ma.LoaiHang;

                }
            }

            return "-1";
        }
        public dynamic GetSanBayTrungGian(int id_chuyenBay)
        {
            var result = (from sb in db.SanBays
                          join sbtg in db.SanBayTrungGians on sb.MaSB equals sbtg.SanBay.MaSB
                          where sbtg.ChuyenBay.MaCB == id_chuyenBay
                          select new { sb.TenSB, sb.DiaChi, sbtg.ThoiGianDung, sbtg.GhiChu, sbtg.ChuyenBay.MaCB }).ToList();

            return result;

        }

        public string GetMayBay(int id_mb)
        {

            var mayBay = db.MayBays.FirstOrDefault(mb => mb.MaMb == id_mb);

            if (mayBay != null)
            {
                return mayBay.TenMB;
            }

            return null; // hoặc một giá trị mặc định nếu không tìm thấy máy bay với id_mb đã cho

        }

        public dynamic GetValidLichBay(string start, string end, DateTime timeNow, int tongnguoi, int tiklevel, string startDateStr, int idnv)
        {
            DateTime startDate;
            DateTime.TryParseExact(startDateStr, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out startDate);

            

            var soHieuChuyenBay = GetChuyenBayByTuyenBay(start, end); 

            var listLBValidTime = filter_LichBay_With_Valid_Time(startDate, timeNow, idnv); 

            


            // So luong lich bay theo soHieu cua tuyen dc chon
            var listLB = GetLichBayWithChuyenBays(soHieuChuyenBay, listLBValidTime);
            List<LichBay> lbcg = new List<LichBay>();
            var listSoLuongVeDaDat = CountSoGheDaDat(tiklevel, startDate.Year, startDate.Month, startDate.Day);
            foreach (var dict2 in listSoLuongVeDaDat)
            {
                foreach (var dict1 in listLB)
                {
                    if (dict1.MaLB == dict2.LichBayId && dict2.SoLuongGhe - dict2.SoLuongVeDaDat >= tongnguoi)
                    {
                        lbcg.Add(dict1);
                    }
                }
            }



            //So lich bay chua co ve


            var lbdt = new List<int>();

            foreach (var s in listSoLuongVeDaDat)
            {
                lbdt.Add(s.LichBayId);
            }

            var lichBayValidGheTrong = listLB.Where(lb => !lbdt.Contains(lb.MaLB)).ToList();


            var result = lbcg;

            var finalRes = new List<Dictionary<string,dynamic>>();

           

            return finalRes;
        }

      
        public List<ChuyenBay> GetChuyenBayByTuyenBay(string start, string end)
        {
            var tuyenBayId = GetTuyenBayBySanBay(start, end);


            return db.ChuyenBays.Where(cb => cb.TuyenBay.MaTuyenBay == tuyenBayId).ToList();

        }
        public List<ChuyenBay> GetChuyenBayByTuyenBayID(int ID)
        {

            return db.ChuyenBays.Where(cb => cb.TuyenBay.MaTuyenBay == ID).ToList();

        }

        public int GetTuyenBayBySanBay(string start, string end)
        {
            int sbdi = GetIdByDiaDiem(start);
            int sbden = GetIdByDiaDiem(end);


            var tuyenBay = db.TuyenBays.FirstOrDefault(tb => tb.SanBayDi.MaSB == sbdi && tb.SanBayDen.MaSB == sbden);
            
            return tuyenBay.MaTuyenBay; 

        }
        public int GetIdByDiaDiem(string des)
        {
            
            var sanBay = db.SanBays.FirstOrDefault(sb => sb.DiaChi == des);
           
            return sanBay.MaSB;


        }

        public dynamic GetHanhKhachNguoiLonByMaPhieu(int maphieu)
        {
            {
                var query = (from vd in db.VeDats
                             join hk in db.HanhKhachs on vd.HanhKhach.Id equals hk.Id
                             join nl in db.NguoiLons on hk.Id equals nl.HanhKhach.Id
                             join sdt in db.SoDienThoais on hk.Id equals sdt.Id
                             where vd.PhieuDatCho.MaPhieu == maphieu
                             select new
                             {
                                 HangVeId = vd.HangVe.Id,
                                 PhieuDatChoMaPhieu = vd.PhieuDatCho.MaPhieu,
                                 HanhKhachId = hk.Id,
                                 HoTen = hk.HoTen,
                                 NgSinh = hk.NgSinh,
                                 GioiTinh = hk.GioiTinh,
                                 CCCD = nl.CCCD,
                                 Sdt = sdt.Sdt
                             });

                return query;

            }
        }

        public dynamic getHanhKhachTreEmByMaPhieu(int maphieu)
        {
            {
                var query = (from vd in db.VeDats
                             join hk in db.HanhKhachs on vd.HanhKhach.Id equals hk.Id
                             join nl in db.NguoiLons on hk.Id equals nl.HanhKhach.Id
                             join te in db.TreEms on hk.Id equals te.NguoiLon_id
                             where vd.PhieuDatCho.MaPhieu == maphieu
                             select new
                             {
                                 HangVeId = vd.HangVe.Id,
                                 PhieuDatChoMaPhieu = vd.PhieuDatCho.MaPhieu,
                                 HanhKhachId = hk.Id,
                                 HoTen = hk.HoTen,
                                 NgSinh = hk.NgSinh,
                                 GioiTinh = hk.GioiTinh,
                                 NguoiLon = nl.HanhKhach.Id
                             });

                return query;

            }


        }
        public string GetNguoiBaoHoById(int id)
        {
            var hoTen = db.HanhKhachs
                                .Where(hk => hk.Id == id)
                                .Select(hk => hk.HoTen)
                                .FirstOrDefault();

            return hoTen;
        }

        public dynamic GetLichBayByMaPhieu(int maPhieu)
        {
            var result = (from vd in db.VeDats
                          join lb in db.LichBays on vd.LichBay.MaLB equals lb.MaLB
                          join lbGiaVe in db.LichBay_GiaVes on lb.MaLB equals lbGiaVe.LichBay.MaLB
                          where vd.PhieuDatCho.MaPhieu == maPhieu
                          select new
                          {
                              LichBayId = vd.LichBay.MaLB,
                              NgayBay = lb.NgayBay,
                              ThoiGianBay = lb.ThoiGianBay,
                              ChuyenBayId = lb.ChuyenBay.MaCB,
                              MayBayId = lb.MayBay.MaMb
                          }).Distinct().ToList();

            return result;
        }

        public dynamic GetTuyenBayByIDChuyenBay(int ID)
        {

            var query = (from cb in db.ChuyenBays
                         join tb in db.TuyenBays on cb.TuyenBay.MaTuyenBay equals tb.MaTuyenBay
                         where cb.MaCB == ID
                         select new
                         {
                             MaCB = cb.MaCB,
                             id_SbDi = tb.SanBayDi.MaSB,
                             id_SbDen = tb.SanBayDen.MaSB
                         }).FirstOrDefault();

            return query;

        }
        public string getSanBayByID(int id)
        {
            var diaChi = (from sb in db.SanBays
                          where sb.MaSB == id
                          select sb.DiaChi).FirstOrDefault();
            return diaChi;
        }
        public int CountSoGhe(int id_mb)
        {

            var subQuery = db.MayBay_Ghes
                                  .Where(mb => mb.MayBay.MaMb == id_mb)
                                  .GroupBy(mb => mb.DayGhe)
                                  .Select(g => new
                                  {
                                      maxghe = g.Count(),
                                      dayGhe = g.Key
                                  });
            var result =0 ;
            if (subQuery != null)
            {
                 result = subQuery.Max(sq => sq.maxghe);
            }           

            return result;

        }

        public dynamic CountSoGheAll(int id_mb, int hang)
        {

            var query = db.MayBay_Ghes
                               .Where(mb => mb.MayBay.MaMb == id_mb && mb.HangVe.Id == hang)
                               .GroupBy(mb => mb.HangVe.Id)
                               .Select(g => new
                               {
                                   hangVe_id = g.Key,
                                   soghe = g.Count()
                               })
                               .FirstOrDefault();

            return query;

        }
        public dynamic GetDayGheInMayBay(int id_mb)
        {

            var query = db.MayBay_Ghes
                               .Where(mb => mb.MayBay.MaMb == id_mb)
                               .Select(mb => mb.DayGhe)
                               .Distinct()
                               .OrderBy(dayGhe => dayGhe)
                               .ToList();

            return query;

        }
        public dynamic GetGheInMayBay(int id_mb)
        {

            var query = db.MayBay_Ghes
                               .Where(mb => mb.MayBay.MaMb == id_mb)
                               .OrderBy(mb => mb.Ghe.Id)
                               .ThenBy(mb => mb.DayGhe)
                               .Select(mb => new
                               {
                                   DayGhe = mb.DayGhe,
                                   GheId = mb.Ghe.Id,
                                   HangVeId = mb.HangVe.Id
                               })
                               .ToList();

            return query;

        }

        public dynamic GetHangGhe(int id_mb)
        {

            var query = db.MayBay_Ghes
                               .Where(mb => mb.MayBay.MaMb == id_mb && mb.HangVe.Id == 1)
                               .Select(mb => new
                               {
                                   DayGhe = mb.DayGhe,
                                   GheId = mb.Ghe.Id,
                                   HangVeId = mb.HangVe.Id
                               })
                               .Distinct()
                               .ToList();

            return query;

        }

        public dynamic GetHoTenByAccount(string username, string password)
        {

            var query = (from nv in db.NhanViens
                         join tk in db.TaiKhoans on nv.TaiKhoan.Id equals tk.Id
                         where tk.Username == username && tk.Password == password
                         select new
                         {
                             HoVaTen = nv.HoVaTen,
                             Id = nv.Id
                         }).FirstOrDefault();

            return query;

        }
        public int GetID_MayBay(string tenmb)
        {

            var query = db.MayBays
                               .Where(mb => mb.TenMB == tenmb)
                               .Select(mb => mb.MaMb)
                               .FirstOrDefault();

            return query;

        }
        public List<VeDat> GetVeDat(int Lichbay_id,int maPhieu)
        {

            var query = db.VeDats
                               .Where(ve => ve.PhieuDatCho.MaPhieu == maPhieu && ve.LichBay.MaLB == Lichbay_id);

            return query.ToList();

        }
        public string GetTinhTrangVe(int Lichbay_id, int maPhieu)
        {

            var tinhTrangVe = db.VeDats
                                     .Where(ve => ve.PhieuDatCho.MaPhieu == maPhieu && ve.LichBay.MaLB == Lichbay_id)
                                     .Select(ve => ve.TinhTrangVe)
                                     .FirstOrDefault();

            return tinhTrangVe;

        }

        public LichBay GetLichBayByID(int ID)
        {

            var lichBay = db.LichBays
                                 .FirstOrDefault(lb => lb.MaLB == ID);

            return lichBay;

        }
        public dynamic GetGheDaDat(int LichBay_id)
        {

            var gheDaDatList = db.VeDats
                                       .Where(vd => vd.LichBay.MaLB == LichBay_id && vd.TinhTrangVe == "da xu ly")
                                       .Select(vd => new
                                       {
                                           GheDaDat = vd.GheDaDat,
                                           TinhTrangVe = vd.TinhTrangVe
                                       })
                                       .ToList();

            return gheDaDatList;

        }

        public void Save_Ghe(int maPhieu, List<string> CacGhe, int id_lb)
        {

            var listVeMB = db.VeDats
                                  .Where(vd => vd.LichBay.MaLB == id_lb && vd.PhieuDatCho.MaPhieu == maPhieu)
                                  .ToList();

            for (int i = 0; i < CacGhe.Count && i < listVeMB.Count; i++)
            {
                listVeMB[i].GheDaDat = CacGhe[i];
                listVeMB[i].TinhTrangVe = "da xu ly";
            }

            db.SaveChanges();

        }

        public dynamic GetMaVeForHanhKhach(int Lichbay_id,int maphieu)
        {

            var maVeList = (from vd in db.VeDats
                            join hk in db.HanhKhachs on vd.HanhKhach.Id equals hk.Id
                            join hv in db.HangVes on vd.HangVe.Id equals hv.Id
                            where vd.LichBay.MaLB == Lichbay_id && vd.PhieuDatCho.MaPhieu == maphieu
                            select new
                            {
                                vd.MaVe,
                                hk.HoTen,
                                vd.GheDaDat,
                                hv.LoaiHang
                            }).ToList();

            return maVeList;

        }
        public dynamic ThongKeBaoCao(int m)
        {

            var totalRevenue = db.HoaDons.Sum(hd => hd.TongTien);
            var k = 100 / totalRevenue;

            var subquery1 = (from hd in db.HoaDons
                             where hd.NgayLap.Month == m
                             group hd by hd.LichBay into g
                             select new
                             {
                                 tongTienTheoLich = g.Sum(x => x.TongTien),
                                 lichBay = g.Key
                             }).ToList();

            var subquery2 = (from sq1 in subquery1
                             join lb in db.LichBays on sq1.lichBay.MaLB equals lb.MaLB
                             group sq1 by lb.ChuyenBay.MaCB into g
                             select new
                             {
                                 soLichMotChuyen = g.Count(),
                                 tongTienTheoChuyen = g.Sum(x => x.tongTienTheoLich),
                                 id_ChuyenBay = g.Key
                             }).ToList();

            var result = (from sq2 in subquery2
                          join cb in db.ChuyenBays on sq2.id_ChuyenBay equals cb.MaCB
                          group sq2 by cb.TuyenBay.MaTuyenBay into g
                          select new
                          {
                              id_TuyenBay = g.Key,
                              doanhThu = g.Sum(x => x.tongTienTheoChuyen),
                              soChuyenMotTuyen = g.Sum(x => x.soLichMotChuyen),
                              tongTienTheoTuyen = g.Sum(x => x.tongTienTheoChuyen) * k
                          }).ToList();

            return result;

        }
        public decimal TongDoanhThu(int m)
        {
            decimal tong = 0;
            var thongKe = ThongKeBaoCao(m);

            foreach (var item in thongKe)
            {
                tong += item.Item2;
            }

            return tong;
        }
        public int LuuThongTinNguoiLon(string name, string gioitinh, DateTime ngaySinh, string CCCD)
        {
            // Kiểm tra xem CCCD đã tồn tại trong CSDL chưa
            var can_Cuoc = db.NguoiLons.FirstOrDefault(n => n.CCCD == CCCD);

            if (can_Cuoc == null)
            {
                // Nếu CCCD chưa tồn tại, thêm thông tin khách hàng và người lớn mới
                var khachhangs = new HanhKhach { HoTen = name, GioiTinh = gioitinh, NgSinh = ngaySinh };
                db.HanhKhachs.Add(khachhangs);
                db.SaveChanges();


                var nguoilon = new NguoiLon { idNguoiLon = khachhangs.Id, CCCD = CCCD };
                db.NguoiLons.Add(nguoilon);
                db.SaveChanges();

                return khachhangs.Id;
            }
            else
            {
                // Nếu CCCD đã tồn tại, trả về id của người lớn tương ứng
                return can_Cuoc.idNguoiLon;
            }
        }
        public int LuuThongTinTreEm(string name, string gioiTinh, DateTime ngaySinh, string CCCD)
        {
            var hanhKhach = new HanhKhach();

            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(gioiTinh) && !string.IsNullOrEmpty(CCCD))
            {
                var nguoiLon = db.NguoiLons.FirstOrDefault(n => n.CCCD == CCCD);
                if (nguoiLon != null)
                {
                    hanhKhach = new HanhKhach
                    {
                        HoTen = name,
                        GioiTinh = gioiTinh,
                        NgSinh = ngaySinh
                    };
                    db.HanhKhachs.Add(hanhKhach);
                    db.SaveChanges();

                    var treEm = new TreEm
                    {
                        Id_TreEm = hanhKhach.Id,
                        NguoiLon_id = nguoiLon.idNguoiLon
                    };
                    db.TreEms.Add(treEm);
                    db.SaveChanges();


                }

            }

            return hanhKhach.Id;

        }

        public string LuuThongTinEmail(string ten, string CCCD)
        {
            var cancuoc = db.NguoiLons.FirstOrDefault(n => n.CCCD == CCCD);
            var eml = db.Emails.FirstOrDefault(e => e.Ten == ten);

            if (eml == null)
            {
                if (cancuoc != null)
                {
                    var email = new Email { Ten = ten, Id = cancuoc.idNguoiLon };
                    db.Emails.Add(email);
                    db.SaveChanges();
                    return "Đã lưu thông tin email.";
                }
                else
                {
                    return "Không tìm thấy người lớn có số CCCD này.";
                }
            }
            else
            {
                return "Email đã tồn tại.";
            }
        }
        public string LuuThongTinSDT(string soDienThoai, string CCCD)
        {
            var canCuoc = db.NguoiLons.FirstOrDefault(n => n.CCCD == CCCD);
            var sdt = db.SoDienThoais.FirstOrDefault(s => s.Sdt == soDienThoai);

            if (sdt == null)
            {
                if (canCuoc != null)
                {
                    var soDienThoaiEntity = new SoDienThoai { Sdt = soDienThoai, Id = canCuoc.idNguoiLon };
                   db.SoDienThoais.Add(soDienThoaiEntity);
                    db.SaveChanges();
                    return "Đã lưu thông tin số điện thoại.";
                }
                else
                {
                    return "Không tìm thấy người lớn có số CCCD này.";
                }
            }
            else
            {
                return "Số điện thoại đã tồn tại.";
            }
        }

        public int LuuThanhToan(int maHoaDon, float thanhTien, DateTime ngayHetHan, int LichBay)
        {
            var hoaDon = new HoaDon { Id = maHoaDon, TongTien = thanhTien, NgHetHanThanhToan = ngayHetHan, lichBayId = LichBay };
            db.HoaDons.Add(hoaDon);
            db.SaveChanges();
            return hoaDon.Id;
        }
        public NguoiLon truyvan_HanhKhach(string CCCD)
        {
            return db.NguoiLons.FirstOrDefault(n => n.CCCD == CCCD);
        }

        public int truyVanMaPDC(int maDaCho)
        {
            var phieuDatCho = db.PhieuDatChos.FirstOrDefault(pdc => pdc.MaPhieu == maDaCho);
            if(phieuDatCho == null)
            {
                return -1;
            }
            else
            {
                return phieuDatCho.MaPhieu;
            }
            
            
        }

        public int truyVanMaHangVe(string loai_Hang)
        {
            
                var hangVe = db.HangVes.FirstOrDefault(hv => hv.LoaiHang == loai_Hang);
                return hangVe.Id;
            
        }

        public void luuPhieuDatCho(int maPhieu, string trangThai)
        {
            var phieuDatCho = new PhieuDatCho { MaPhieu = maPhieu, TrangThai = trangThai };

            db.PhieuDatChos.Add(phieuDatCho);
            db.SaveChanges();
        }

        public void luuThongTinVeDat( int lichBayId, string cho , int hanhKhachId, int maPhieu,int maHangVe)
        {
            var time = DateTime.Now;
            var ve = new VeDat { NgDat =time,GheDaDat = cho, lichBayId = lichBayId, hanhKhachId = hanhKhachId, phieuDatChoId = maPhieu, hangVeId = maHangVe, TinhTrangVe = "xu ly" };
            db.VeDats.Add(ve);
            db.SaveChanges();
        }


        public void luu_ThongTinVeBan(int lichBayId, int hanhKhachId, int maPhieu, int maHangVe, int idNhanVien, string soGhe)
        {
            var veDat = new VeDat {  lichBayId = lichBayId, hanhKhachId = hanhKhachId, phieuDatChoId = maPhieu, hangVeId = maHangVe, TinhTrangVe = "da xu ly", GheDaDat = soGhe };
            db.VeDats.Add(veDat);
            db.SaveChanges();

            var veBan = new VeBan { veDatId= veDat.MaVe, nguoiLon_id = idNhanVien };
            db.VeBans.Add(veBan);
            db.SaveChanges();
        }
        public bool truyvan_NgayGioBay(DateTime ngayGio)
        {
            return !db.LichBays.Any(lb => lb.NgayBay == ngayGio);
        }

        public MayBay Lay_idMayBay(string tenMB)
        {
            var mayBay = db.MayBays.FirstOrDefault(mb => mb.TenMB == tenMB);
            return mayBay;
        }

        public void luuThongTinLapLich(int idChuyenBay, string tenMB, int idNhanVien, DateTime ngayGio, int thoiGianBay)
        {
            if (truyvan_NgayGioBay(ngayGio))
            {
                MayBay idMayBay = Lay_idMayBay(tenMB);
                var lapLich = new LichBay { chuyenBayId = idChuyenBay, MayBay = idMayBay, NgayBay = ngayGio, ThoiGianBay = thoiGianBay, nhanVienId = idNhanVien };
                db.LichBays.Add(lapLich);
                db.SaveChanges();
            }
        }

        public SanBay Lay_idSanBay(string tenSanBay)
        {
            var sanBay =db.SanBays.FirstOrDefault(sb => sb.TenSB.Trim() == tenSanBay.Trim());
            return sanBay;
        }

        public void luu_ThongTinSanBayTrungGian(string tenSanBay, int idChuyenBay,int thoiGianDung)
        {
            SanBay maSanBay = Lay_idSanBay(tenSanBay);
            var sanBayTG = new SanBayTrungGian { ChuyenBayId = idChuyenBay, SanBay = maSanBay, ThoiGianDung = thoiGianDung };
            db.SanBayTrungGians.Add(sanBayTG);
            db.SaveChanges();
        }

        public TaiKhoan get_user_by_id(int user_id)
        {
            return db.TaiKhoans.Find(user_id);
        }
        public TaiKhoan check_login(string username, string password, UserRole role = UserRole.Admin)
        {
            var result = db.TaiKhoans
                .Join(db.NhanViens, tk => tk.Id, nv => nv.TaiKhoan.Id, (tk, nv) => new { TaiKhoan = tk, NhanVien = nv })
                .Where(data => data.TaiKhoan.Username.Trim() == username.Trim() &&
                               data.TaiKhoan.Password == password &&
                               data.TaiKhoan.UserRole == role)
                .FirstOrDefault();
            return result?.TaiKhoan;
        }

        public TaiKhoan auth_user(string username, string password)
        {
            return db.TaiKhoans.FirstOrDefault(tk => tk.Username == username && tk.Password == password);
        }

        
        
    }
    }
