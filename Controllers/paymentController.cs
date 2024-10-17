using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stripe;
using LTCSDLMayBay.SendEmail;

namespace LTCSDLMayBay.Controllers
{
    public class paymentController : Controller
    {
        ThongTinHanhKhachController TTKH = new ThongTinHanhKhachController();
        SendEmail.Email e = new SendEmail.Email();
        Dao.Dao dao = new Dao.Dao();
        thanhtoanController TH = new thanhtoanController();
        // GET: payment
        
        [AcceptVerbs(HttpVerbs.Post | HttpVerbs.Get)]
        public ActionResult Index()
        {
            var thongtintimkiem = Session["ThongTinTimKiem"] as Dictionary<string, dynamic>;                                                
                    var soGhe = Session["Ghe"] as List<int>;
                    string ngaydat = DateTime.Now.ToString("dd/MM/yy");
                   int madatcho = LuuThongTinChung();
                    Session["madatcho"] = madatcho;
                    var lienhe = Session["lienHe"] as Dictionary<string, dynamic>;
                    //e.SendMail(lienhe["Email"].ToString(),  madatcho.ToString(),  Session["nguoiLon"],  Session["treEm"],  Session["flight"], soGhe, venl: "", vete: "");
                    ViewBag.madatcho = madatcho;
                    var nguoiLon = Session["nguoiLon"];
                                   
                     ViewBag.nguoiLon=nguoiLon;
            if (Session["treEm"] != null)
            {
                var treEm = Session["treEm"];
                ViewBag.treEm = treEm;
            }
           
                     ViewBag.ngaydat= DateTime.Now;
                     ViewBag.lienHe=lienhe;
                    return View();                                      
        }
        public void LuuThongTinNguoiLon()
        {
            var nguoiLon = Session["nguoiLon"] as List<Dictionary<string, dynamic>>;
            var lienHe = Session["lienHe"] as Dictionary<string, dynamic>;
           
            var ds_idNguoiLon = new List<int>();
            
            for (int i = 0; i < nguoiLon.Count; i++)
            {
                if (i == 0)
                {
                    
                    if (nguoiLon[i]["gioitinh"] == "1")
                    {
                       
                        int id_Ngl = dao.LuuThongTinNguoiLon( nguoiLon[i]["hoten"], "Nam",DateTime.Parse( nguoiLon[i]["ngaySinh"]),  nguoiLon[i]["CCCD"]);
                        ds_idNguoiLon.Add(id_Ngl);
                        dao.LuuThongTinSDT(lienHe["SĐT"], id_Ngl.ToString());
                        dao.LuuThongTinEmail( lienHe["Email"], id_Ngl.ToString());
                    }
                    else if (nguoiLon[i]["gioitinh"] == "0")
                    {
                        int id_Ngl = dao.LuuThongTinNguoiLon( nguoiLon[i]["hoten"], "Nữ", DateTime.Parse( nguoiLon[i]["ngaySinh"]), nguoiLon[i]["CCCD"]);
                        ds_idNguoiLon.Add(id_Ngl);
                        dao.LuuThongTinSDT( lienHe["SĐT"], id_Ngl.ToString());
                        dao.LuuThongTinEmail( lienHe["Email"], id_Ngl.ToString());
                    }
                }
                else
                {

                    if (nguoiLon[i]["gioitinh"] == "1")
                    {
                        int id_Ngl = dao.LuuThongTinNguoiLon(nguoiLon[i]["hoten"],  "Nam",DateTime.Parse(  nguoiLon[i]["ngaySinh"]), nguoiLon[i]["CCCD"]);
                        ds_idNguoiLon.Add(id_Ngl);
                    }
                    else if (nguoiLon[i]["gioitinh"] == "0")
                    {
                        int id_Ngl = dao.LuuThongTinNguoiLon(nguoiLon[i]["hoten"], "Nữ", DateTime.Parse( nguoiLon[i]["ngaySinh"]),nguoiLon[i]["CCCD"]);
                        ds_idNguoiLon.Add(id_Ngl);
                    }
                }
            }

            Session["id_NguoiLon"] = ds_idNguoiLon;
        }

        public void LuuThongTinTreEm()
        {
            var treEm = Session["treEm"] as List<Dictionary<string, string>>;
            var ds_idTreEm = new List<int>();
            if(treEm != null)
            {
                foreach (var item in treEm)
                {
                    if (item["gioitinhTreEm"] == "1")
                    {
                        int id_treEm = dao.LuuThongTinTreEm(item["hotenTreEm"], "Nam", DateTime.Parse(item["ngaySinhTreEm"]), item["CCCD_NglondiCung"]);
                        ds_idTreEm.Add(id_treEm);
                    }
                    else if (item["gioitinhTreEm"] == "0")
                    {
                        int id_treEm = dao.LuuThongTinTreEm(item["hotenTreEm"], "Nữ", DateTime.Parse(item["ngaySinhTreEm"]), item["CCCD_NglondiCung"]);
                        ds_idTreEm.Add(id_treEm);
                    }
                }
            }
           

            Session["id_treEm"] = ds_idTreEm;
        }

        public void LuuHoaDon()
        {
            DateTime today = DateTime.Now;
            var tttk = Session["thongtintimkiem"] as Dictionary<string, dynamic>;
            var loaiVe =tttk["cateFlight"].ToString();
            var maHoaDon = Session["maHoaDon"] as List<int>;
            var threeDaysLater = today.AddDays(3).ToString("yyyy-MM-dd");
            var filght = Session["flight"] as List<Dictionary<string, dynamic>>;
            int maHoaDonValue = maHoaDon[0];

            // Chuyển đổi giá trị "Tongtien" từ filght[0] sang int
            int tongTien;
            if (!int.TryParse(filght[0]["Tongtien"].ToString(), out tongTien))
            {
                Console.WriteLine("Error: 'Tongtien' is not in a correct format.");
                return;
            }

            // Chuyển đổi giá trị threeDaysLater sang DateTime
            DateTime parsedDate;
            if (!DateTime.TryParse(threeDaysLater, out parsedDate))
            {
                Console.WriteLine("Error: 'threeDaysLater' is not in a correct format.");
                return;
            }

            // Chuyển đổi giá trị "MaLB" từ filght[0] sang int
            int maLB;
            if (!int.TryParse(filght[0]["MaLB"].ToString(), out maLB))
            {
                Console.WriteLine("Error: 'MaLB' is not in a correct format.");
                return;
            }

            // Gọi hàm dao.LuuThanhToan với các giá trị đã được chuyển đổi
            dao.LuuThanhToan(maHoaDonValue, tongTien, parsedDate, maLB);

            if (loaiVe == "round-trip")
            {
                int maHoaDonValue1 = maHoaDon[1];

                // Chuyển đổi giá trị "Tongtien" từ filght[0] sang int
                int tongTien1;
                if (!int.TryParse(filght[0]["Tongtien"].ToString(), out tongTien1))
                {
                    Console.WriteLine("Error: 'Tongtien' is not in a correct format.");
                    return;
                }

                // Chuyển đổi giá trị threeDaysLater sang DateTime
                DateTime parsedDate1;
                if (!DateTime.TryParse(threeDaysLater, out parsedDate1))
                {
                    Console.WriteLine("Error: 'threeDaysLater' is not in a correct format.");
                    return;
                }

                // Chuyển đổi giá trị "MaLB" từ filght[0] sang int
                int maLB1;
                if (!int.TryParse(filght[0]["MaLB"].ToString(), out maLB1))
                {
                    Console.WriteLine("Error: 'MaLB' is not in a correct format.");
                    return;
                }

                // Gọi hàm dao.LuuThanhToan với các giá trị đã được chuyển đổi
                dao.LuuThanhToan(maHoaDonValue1, tongTien1, parsedDate1, maLB1);
               
            }
        }

        public void LuuThongTinMaDC(int ma_dc)
        {
            dao.luuPhieuDatCho(ma_dc, "đang xử lý");
            
        }

        public void LuuThongTinVeNguoiLon(int maPhieu)
        {
            var tttk = Session["thongtintimkiem"] as Dictionary<string, dynamic>;
            var loaiVe = tttk["cateFlight"].ToString();
            var nguoiLon = Session["nguoiLon"] as List<Dictionary<string, dynamic>>;
            var idnv = Session["idnv"] as Dictionary<int, dynamic>;
            var thongtin_VeBanNgl = new List<dynamic>();
            var filght = Session["flight"] as List<Dictionary<string, dynamic>>;
            var id_NguoiLon = Session["id_NguoiLon"] as List<int>;
            List<string> seats = new List<string>();
            char[] rows = { 'A', 'B', 'C', 'D' };
            for (int i = 0; i < rows.Length; i++)
            {
                for (int num = 1; num <= 5; num++)
                {
                    seats.Add(rows[i] + num.ToString());
                }
            }

            // Khởi tạo đối tượng Random
            Random random = new Random();

            // Tạo danh sách GHE và thêm một số ghế ngẫu nhiên vào đó
            List<string> GHE = new List<string>();
            for (int i = 0; i < nguoiLon.Count; i++) // Số lượng ghế ngẫu nhiên bạn muốn tạo, ở đây là 5
            {
                int randomIndex = random.Next(seats.Count);
                GHE.Add(seats[randomIndex]);
            }
            var tel =tttk["ticketLevel"].ToString();
            for (int i = 0; i < nguoiLon.Count; i++)
            {
                if (idnv != null)
                {
                    int maxId = dao.db.VeDats.Max(b => (int)b.MaVe);
                    int mave = maxId + 1;
                    dao.luu_ThongTinVeBan(  Convert.ToInt32(filght[0]["MaLB"]), Convert.ToInt32(id_NguoiLon[i]),  maPhieu, int.Parse(tel),  Convert.ToInt32(idnv),GHE[i].ToString());
                  
                    thongtin_VeBanNgl.Add(new { mave = mave });
                }
                else
                {
                    dao.luuThongTinVeDat(Convert.ToInt32(filght[0]["MaLB"]), GHE[i].ToString(),  Convert.ToInt32(id_NguoiLon[i]), maPhieu, int.Parse(tel));
                }

                if (loaiVe.Equals("round-trip"))
                {
                    if (idnv != null)
                    {
                        int maxId = dao.db.VeDats.Max(b => (int)b.MaVe);
                        int mave = maxId + 1;
                        dao.luu_ThongTinVeBan( Convert.ToInt32(filght[0]["MaLB"]), Convert.ToInt32(id_NguoiLon[i]), maPhieu, int.Parse(tel), Convert.ToInt32(idnv), GHE[i].ToString());
                        thongtin_VeBanNgl.Add(new { mave = mave, so_Ghe = 123 }); 
                    }
                    else
                    {
                        dao.luuThongTinVeDat(Convert.ToInt32(filght[0]["MaLB"]), GHE[i].ToString(), Convert.ToInt32(id_NguoiLon[i]), maPhieu, int.Parse(tel));
                    }
                }
            }

            if (thongtin_VeBanNgl.Count > 0)
            {
                Session["ve_banNgl"] = thongtin_VeBanNgl;
            }
        }
        public void LuuThongTinVeTreEm(int maPhieu)
        {
            var tttk = Session["ThongTinTimKiem"] as Dictionary<string, dynamic>;
            var loaiVe = tttk["cateFlight"].ToString();
            var nguoiLon = Session["nguoiLon"] as List<Dictionary<string, dynamic>>;
            var idnv = Session["idnv"] as Dictionary<string, dynamic>;        
            var filght = Session["flight"] as List<Dictionary<string, dynamic>>;
            var id_NguoiLon = Session["id_NguoiLon"] as List<int>;
            List<string> seats = new List<string>();
            char[] rows = { 'A', 'B', 'C', 'D' };
            for (int i = 0; i < rows.Length; i++)
            {
                for (int num = 1; num <= 5; num++)
                {
                    seats.Add(rows[i] + num.ToString());
                }
            }

            // Khởi tạo đối tượng Random
            Random random = new Random();

            // Tạo danh sách GHE và thêm một số ghế ngẫu nhiên vào đó
            List<string> GHE = new List<string>();
            for (int i = 0; i < nguoiLon.Count; i++) // Số lượng ghế ngẫu nhiên bạn muốn tạo, ở đây là 5
            {
                int randomIndex = random.Next(seats.Count);
                GHE.Add(seats[randomIndex]);
            }
            var thongtin_VeBanTreEm = new List<dynamic>();
            var id_treEm=Session["id_treEm"]as List<int>;
            var tel = tttk["ticketLevel"].ToString();
            if (thongtin_VeBanTreEm != null)
            {
                for (int i = 0; i < id_treEm.Count; i++)
                {
                    if (idnv != null)
                    {
                        int maxId = dao.db.VeDats.Max(b => (int)b.MaVe);
                        int mave = maxId + 1;
                        dao.luu_ThongTinVeBan( Convert.ToInt32(filght[0]["MaLB"]), Convert.ToInt32(id_treEm[i]), maPhieu, int.Parse(tel), Convert.ToInt32(idnv), GHE[id_NguoiLon.Count + i].ToString());
                        thongtin_VeBanTreEm.Add(new { mave = mave });
                    }
                    else
                    {
                        dao.luuThongTinVeDat( Convert.ToInt32(filght[0]["MaLB"]), GHE[id_NguoiLon.Count + i].ToString(), Convert.ToInt32(id_treEm[i]), maPhieu, int.Parse(tel));
                    }

                    if (loaiVe.Equals("round-trip"))
                    {
                        if (idnv != null)
                        {
                            int maxId = dao.db.VeDats.Max(b => (int)b.MaVe);
                            int mave = maxId + 1;
                            dao.luu_ThongTinVeBan( Convert.ToInt32(filght[1]["MaLB"]), Convert.ToInt32(id_treEm[i]), maPhieu, int.Parse(tel), Convert.ToInt32(idnv), GHE[id_NguoiLon.Count + i].ToString());
                            thongtin_VeBanTreEm.Add(new { mave = mave, soghe = 123 }); // This part will get each seat element saved in the seat diagram later
                        }
                        else
                        {
                            
                            dao.luuThongTinVeDat( Convert.ToInt32(filght[1]["MaLB"]), GHE[id_NguoiLon.Count + i].ToString(), Convert.ToInt32(id_treEm[i]), maPhieu, int.Parse(tel));
                        }
                    }
                }
            }
          

            if (thongtin_VeBanTreEm.Count > 0)
            {
                Session["ve_banTreEm"] = thongtin_VeBanTreEm;
            }
        }

        public int LuuThongTinChung()
        {
            LuuThongTinNguoiLon();
            
           
            int madatcho = TH.CheckMaDatCho();
            LuuThongTinMaDC( madatcho);
            LuuThongTinVeNguoiLon( madatcho);
            if (Session["treEm"] != null)
            {
                LuuThongTinTreEm();
                LuuThongTinVeTreEm(madatcho);
            }
            LuuHoaDon();
            
            return madatcho;
        }

       
        
    }
}