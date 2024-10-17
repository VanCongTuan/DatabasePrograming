using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using LTCSDLMayBay.Models;

namespace LTCSDLMayBay.Controllers
{
    public class APIController : Controller
    {
        Dao.Dao dao = new Dao.Dao();
        // GET: API
        public void DeleteSession()
        {
            Session["URL_CI"] = "";
            Session["URL"] = "";
            Session["LenSS"] = 0;
            Session["arr"] = new List<dynamic>();

            if (Session["CheckInFlight"] != null)
            {
                Session.Remove("CheckInFlight");
                Session.Remove("CheckInHKNL");
                Session.Remove("CheckInHKTE");
                Session.Remove("GHE");
                Session.Remove("id_mb");
                Session.Remove("Ghe_HK");
                Session.Remove("hk_ghe");
            }
            else
            {
                Session.Remove("thongtintimkiem");
                Session.Remove("flight");
                Session.Remove("soluong");
                Session.Remove("email");
                Session.Remove("sodienthoai");
                Session.Remove("nguoiLon");
                Session.Remove("treEm");
                Session.Remove("maHoaDon");
                Session.Remove("id_NguoiLon");
                Session.Remove("id_treEm");
                Session.Remove("ve_banNgl");
                Session.Remove("madatcho");
                Session.Remove("ve_banTreEm");
                if (Session["GHE"] != null)
                {
                    Session.Remove("GHE");
                }
            }
        }
        public ActionResult Index()
        {
            return View();
        }
      
       
        public static List<Dictionary<string, dynamic>> flightList = new List<Dictionary<string, dynamic>>();
        public static List<Dictionary<string, dynamic>> Airportlist = new List<Dictionary<string, dynamic>>();
        public static int LenSS = 0;

        // POST: /api/flight        
        [HttpPost]
        public ActionResult flight()
        {
            var jsonString = new System.IO.StreamReader(Request.InputStream).ReadToEnd();
            var serializer = new JavaScriptSerializer();
            var data = serializer.Deserialize<Dictionary<string, dynamic>>(jsonString);

            var flight = new Dictionary<string, dynamic>
            {
                { "MaLB", data["MaLB"] },
                { "MaCB", data["MaCB"] },
                { "noidi", data["noidi"] },
                { "noiden", data["noiden"] },
                { "idmb", dao.GetID_MayBay(data["tenMB"].ToString()) },
                { "NgayKhoiHanh", data["NgayKhoiHanh"] },                
                { "Tongtien", data["Tongtien"] }
            };

            flightList.Add(flight);           
            Session["flight"] = flightList;           
            LenSS = flightList.Count;           
            return Json(flight);
        }
        //[HttpPost]
        //public ActionResult airport()
        //{
        //    var jsonString = new System.IO.StreamReader(Request.InputStream).ReadToEnd();
        //    var serializer = new JavaScriptSerializer();
        //    var data = serializer.Deserialize<Dictionary<string, dynamic>>(jsonString);

        //    var flight = new Dictionary<string, dynamic>
        //    {
        //        { "MaLB", data["MaLB"] },
        //        { "MaCB", data["MaCB"] },
        //        { "noidi", data["noidi"] },
        //        { "noiden", data["noiden"] },
        //        { "idmb", dao.GetID_MayBay(data["tenMB"].ToString()) },
        //        { "NgayKhoiHanh", data["NgayKhoiHanh"] },
        //        { "ThoiGianBay", data["ThoiGianBay"] }
        //    };

        //    Airportlist.Add(flight);
           
        //    Session["id_mb"] = Airportlist;
           
        //    return Json(flight);
        //}
        [HttpGet]
        public ActionResult ghe()
        {
            string idLichBay = null;

             
                var flight = flightList.Count > 0 ? flightList[0] : null;
                if (flight != null)
                {
                    idLichBay = flight["MaLB"].ToString();
                }
            
            if (Session["flight"] != null)
            {
                var idMb = Session["flight"] as Dictionary<string, dynamic>;
                if (idMb != null)
                {
                    idLichBay = idMb["MaLB"].ToString();
                }
            }

            if (idLichBay == null)
            {
                return Json(new { error = "No flight found" }, JsonRequestBehavior.AllowGet);
            }

            var listGhe = dao.GetGheDaDat(int.Parse(idLichBay));
            var gheList = new List<Dictionary<string, string>>();

            foreach (var ghe in listGhe)
            {
                gheList.Add(new Dictionary<string, string>
                {
                    { "soghe", ghe.GheDaDat }
                });
            }

            return Json(gheList, JsonRequestBehavior.AllowGet);
        }
      


        [HttpPost]
        public ActionResult thuonggia()
        {
            string idMb;
            List<dynamic> HangGhe = new List<dynamic>();

            if (Session["idnv"] != null)
            {
                var flight = flightList.Count > 0 ? flightList[0] : null;
                if (flight != null)
                {
                    idMb = flight["idmb"].ToString();

                }
                
               
            }
            else
            {
                idMb = (string)((Dictionary<string, dynamic>)Session["flight"])["id"];
            }

            
            HangGhe.Add(new
            {
                Day = "giá trị của i.dayGhe",
                Ghe = "giá trị của i.ghe_id",
                Hang = "giá trị của i.hangVe_id"
            });

            return Json(HangGhe);
        }
        
        [HttpGet]
        public ActionResult diadiem()
        {          
            var s = dao.db.SanBays.ToList();
            return Json(s, JsonRequestBehavior.AllowGet); 
        }
     

        [HttpPost]
        public ActionResult diadiem(SanBay sanBay)
        {        
            var s = new SanBay { TenSB = sanBay.TenSB , DiaChi = sanBay.DiaChi };
           dao.db.SanBays.Add(s);
            dao.db.SaveChanges();
            return Json(new { message = "Destination created successfully." });
        }
     
        [HttpDelete]
        public ActionResult diadiem(int id)
        {
            SanBay s = dao.db.SanBays.Find(id);
                  dao.db.SanBays.Remove(s);
            dao.db.SaveChanges();
            return Json(new { message = "Destination delete successfully." });
        }
        [HttpPut]
        public ActionResult diadiem(int id,SanBay sanBay)
        {
            SanBay s = dao.db.SanBays.Find(id);
            if(sanBay.TenSB != null)
            {
                s.TenSB = sanBay.TenSB;
            }
           
            if (sanBay.DiaChi != null)
            {
                s.DiaChi = sanBay.DiaChi;
            }
            dao.db.SaveChanges();
            return Json(new { message = "Destination Put successfully." });
        }
        [HttpPost]
        public ActionResult timcb()
        {
            var jsonString = new System.IO.StreamReader(Request.InputStream).ReadToEnd();
            var serializer = new JavaScriptSerializer();
            var data = serializer.Deserialize<Dictionary<string, dynamic>>(jsonString);
            var macbs = new List<dynamic>();

            foreach (var cb in dao.GetChuyenBayByTuyenBayID(int.Parse(data["tuyen"])))
            {
                macbs.Add(new
                {
                    macb = cb.MaCB
                });
            }

            return Json(macbs);
        }


       
        [HttpPost]
        public ActionResult countghe()
        {
            var jsonString = new System.IO.StreamReader(Request.InputStream).ReadToEnd();
            var serializer = new JavaScriptSerializer();
            var data = serializer.Deserialize<Dictionary<string, dynamic>>(jsonString);
         
            var soluongghe = new
            {
                hang1 = dao.CountSoGheAll(int.Parse(data["idmb"]), 1).soghe, // Thay đổi từ count_so_ghe_all() thành CountSoGheAll() nếu bạn đã đổi tên phương thức.
                hang2 = dao.CountSoGheAll(int.Parse(data["idmb"]), 2).soghe // Thay đổi từ count_so_ghe_all() thành CountSoGheAll() nếu bạn đã đổi tên phương thức.
            };

            return Json(soluongghe);
        }


        
        [HttpGet]
        public ActionResult sanbay()
        {
            var tensb = new List<dynamic>();

            foreach (var sb in dao.get_destination()) 
            {
                tensb.Add(new
                {
                    masb = sb.TenSB
                });
            }

            return Json(tensb, JsonRequestBehavior.AllowGet); 
        }

       
        [HttpGet]
        public ActionResult maybay()
        {
            var tenmb = new List<dynamic>();

            foreach (var mb in dao.get_MayBayLL()) 
            {
                tenmb.Add(new
                {
                    mamb = mb.MaMb,
                    tenmb = mb.TenMB
                });
            }

            return Json(tenmb, JsonRequestBehavior.AllowGet); 
        }


       
        
       
        [HttpPost]
        public ActionResult laplich()
        { var jsonString = new System.IO.StreamReader(Request.InputStream).ReadToEnd();
            var serializer = new JavaScriptSerializer();
            var data = serializer.Deserialize<List<Dictionary<string, dynamic>>>(jsonString);
            string id_cb = "";
            string tgb = "";
            string tgd = "";
            string ngd = "";
            foreach (var item in data)
            {
                if (item.ContainsKey("id_chuyenbay"))
                {
                    id_cb = item["id_chuyenbay"].ToString();
                    tgb = item["thoigianbay"].ToString();
                    tgd = item["thoigiandung"].ToString();
                    ngd = item["ngaygiodi"].ToString();
                    DateTime datetime_object = DateTime.ParseExact(ngd , "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture);

                    dao.luuThongTinLapLich(int.Parse(id_cb), item["maybay"].ToString(), 1, datetime_object, int.Parse(tgb));
                   
                   
                }
                
            }

            return Json(new { message = "successfully." });
        }

        



        [HttpGet]
        public ActionResult nhanvien()
        {
            List<NhanVien> nhanvien = dao.db.GetAllNhanVien();
            return Json(nhanvien, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult nhanvien(NhanVien nhanvien)
        {
            //DateTime ngaysinh = DateTime.Parse(nhanvien.NgaySinh.ToString());
            dao.db.ThemNhanVien(nhanvien.HoVaTen, nhanvien.NgaySinh, nhanvien.GioiTinh, nhanvien.Luong, nhanvien.taiKhoanId);
            return Json(new { message = "Destination created successfully." });
        }

        [HttpDelete]
        public ActionResult nhanvien(int id)
        {
            dao.db.XoaNhanVien(id);
            return Json(new { message = "Destination delete successfully." });
        }
        [HttpPut]
        public ActionResult nhanvien(int id, NhanVien nhanvien)
        {
           
            dao.db.SuaNhanVien(id, nhanvien.HoVaTen, nhanvien.NgaySinh,nhanvien.GioiTinh, nhanvien.Luong, nhanvien.taiKhoanId);
            return Json(new { message = "Destination Put successfully." });
        }
    }
}