using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using LTCSDLMayBay.Models;

namespace LTCSDLMayBay.Controllers
{
    public class chuyenbayController : Controller
    {
        Dao.Dao dao = new Dao.Dao();
         public void StoreSearchInformation()
        {
            var searchInformation = new Dictionary<string, dynamic>
        {
            { "start", Request.Form["StartDes"] },
            { "end", Request.Form["EndDes"] },
            { "adultNum", int.Parse(Request.Form["adultNumber"]) },
            { "childrenNum", int.Parse(Request.Form["childrenNumber"]) },
            { "ticketLevel", Request.Form["level"] },
            { "startDate", Request.Form["StartDate"] },
            { "timeNow", DateTime.Now }
        };

            if (Request.Form["one-way"] != null)
            {
                searchInformation["cateFlight"] = Request.Form["one-way"];
            }
            else
            {
                searchInformation["cateFlight"] = Request.Form["round-trip"];
                searchInformation["endDate"] = Request.Form["EndDate"];
            }

            Session["thongtintimkiem"] = searchInformation;
        }


        public void StoreSearchInformationThongTin()
        {
            var searchInformation = new Dictionary<string, dynamic>
        {
            { "start", Request.Form["StartDes"] },
            { "end", Request.Form["EndDes"] },
            { "adultNum", int.Parse(Request.Form["adultNumber"]) },
            { "childrenNum", int.Parse(Request.Form["childrenNumber"]) },
            { "ticketLevel", Request.Form["level"] },
            { "startDate", Request.Form["StartDate"] },
            { "timeNow", DateTime.Now }
        };

            if (Request.Form["one-way"] != null)
            {
                searchInformation["cateFlight"] = Request.Form["one-way"];
            }
            else
            {
                searchInformation["cateFlight"] = Request.Form["round-trip"];
                searchInformation["endDate"] = Request.Form["EndDate"];
            }

            Session["ThongTinTimKiem"] = searchInformation;
        }
        public void StoreSearchInformationThongTinTimKiem()
        {
            var searchInformation = new Dictionary<string, dynamic>
        {
            { "start", Request.Form["StartDes"] },
            { "end", Request.Form["EndDes"] },
            { "adultNum", int.Parse(Request.Form["adultNumber"]) },
            { "childrenNum", int.Parse(Request.Form["childrenNumber"]) },
            { "ticketLevel", Request.Form["level"] },
            { "startDate", Request.Form["StartDate"] },
            { "timeNow", DateTime.Now }
        };

            if (Request.Form["one-way"] != null)
            {
                searchInformation["cateFlight"] = Request.Form["one-way"];
            }
            else
            {
                searchInformation["cateFlight"] = Request.Form["round-trip"];
                searchInformation["endDate"] = Request.Form["EndDate"];
            }

            Session["TTTK"] = searchInformation;
        }
        public string URL = "";
        public void Chung()
        {


        }
       
        // GET: chuyenbay
        public ActionResult Index(string time)
        {  

            var SSI = Session["thongtintimkiem"] as Dictionary<string, dynamic>;
            
            if (SSI == null)
            {

                return RedirectToAction("Index");
            }
           
            if (LenSS > 0 )
            {
                string temp = SSI["end"].ToString();
                SSI["end"] = SSI["start"];
                SSI["start"] = temp;
            }
            

            string days = SSI["timeNow"].ToString();
            DateTime day;
            if (!DateTime.TryParse(days, out day))
            {
                // Handle parse error
                Console.WriteLine("Invalid date format in SSI['timeNow']");
               
            }

            List<DateTime> dateRange = new List<DateTime>();
            DateTime startDate;
            if (DateTime.TryParse(SSI["startDate"].ToString(), out startDate))
            {
                for (int i = 0; i < 30; i++)
                {
                    dateRange.Add(startDate.AddDays(i));
                }
            }
           
            if (time != null && SSI["cateFlight"].ToString() == "round-trip")
            {
                URL = "/chuyenbay";
            }
            if (SSI != null && SSI.ContainsKey("cateFlight") && SSI["cateFlight"] != null && SSI["cateFlight"].ToString() == "one-way")
            {
                URL = "/ThongTinHanhKhach";
            }
            int tongNguoi = (int)SSI["adultNum"] + (int)SSI["childrenNum"];
           
            var start = SSI["start"];
            var end = SSI["end"];           
            var ticketLevel = SSI["ticketLevel"];
            var cateFlight = SSI["cateFlight"];
            var adultNum = SSI["adultNum"];
            var childrenNum = SSI["childrenNum"];
            var timeNow = SSI["timeNow"];
           
            if (URL == "/chuyenbay" && LenSS > 0)
            {
                var endDate = SSI["endDate"];
                URL = "/ThongTinHanhKhach";
               
                var soHieuChuyenBay = dao.GetChuyenBayByTuyenBay(dao.getSanBayByID(int.Parse(start.ToString())), dao.getSanBayByID(int.Parse(end.ToString())));

                var listLBValidTime= dao.filter_LichBay_With_Valid_Time(startDate, timeNow, 1);
                var LichBay = dao.GetLichBayWithChuyenBays(soHieuChuyenBay, listLBValidTime);
                List<LichBay> lbcg = new List<LichBay>();
                var listSoLuongVeDaDat = dao.CountSoGheDaDat(int.Parse(ticketLevel), startDate.Year, startDate.Month, startDate.Day);
                foreach (var dict2 in listSoLuongVeDaDat)
                {
                    foreach (var dict1 in LichBay)
                    {
                        if (dict1.MaLB == dict2.LichBayId && dict2.SoLuongGhe - dict2.SoLuongVeDaDat >= tongNguoi)
                        {
                            lbcg.Add(dict1);
                        }
                    }
                }

                

               

                ViewBag.soluong = tongNguoi;
                ViewBag.soluongngl = int.Parse(adultNum.ToString());
                ViewBag.soluongtre = int.Parse(childrenNum.ToString());
                ViewBag.daterange = dateRange;
                ViewBag.LichBayArr = LichBay;
                var giaVe = dao.get_GiaVe(int.Parse(ticketLevel));
                ViewBag.GiaVe = giaVe;
                ViewBag.current_day = timeNow;
                ViewBag.cateFlight = cateFlight;
                ViewBag.noidi = dao.getSanBayByID(int.Parse(end.ToString()));
                ViewBag.noiden = dao.getSanBayByID(int.Parse(start.ToString()));
                ViewBag.URL = URL;
                if (Request.Form["one-way"] == null)
                {
                    ViewBag.dayStart = SSI["endDate"];
                }
                               
               
                return View();
            }
           
            var soHieuChuyenBay2 = dao.GetChuyenBayByTuyenBay(dao.getSanBayByID(int.Parse(start.ToString())), dao.getSanBayByID(int.Parse(end.ToString())));

            var listLBValidTime2 = dao.filter_LichBay_With_Valid_Time(startDate, timeNow, 1);
           
            var LichBay2 = dao.GetLichBayWithChuyenBays(soHieuChuyenBay2, listLBValidTime2);
            List<LichBay> lbcg2 = new List<LichBay>();
            var listSoLuongVeDaDat2 = dao.CountSoGheDaDat(int.Parse(ticketLevel), startDate.Year, startDate.Month, startDate.Day);
            foreach (var dict2 in listSoLuongVeDaDat2)
            {
                foreach (var dict1 in LichBay2)
                {
                    if (dict1.MaLB == dict2.LichBayId && dict2.SoLuongGhe - dict2.SoLuongVeDaDat >= tongNguoi)
                    {
                        lbcg2.Add(dict1);
                    }
                }
            }
            


            var giaVe1 = dao.get_GiaVe(int.Parse(ticketLevel));
            ViewBag.GiaVe = giaVe1;
            ViewBag.soluong = tongNguoi;
            ViewBag.soluongngl = int.Parse(adultNum.ToString());
            ViewBag.soluongtre = int.Parse(childrenNum.ToString());
            ViewBag.daterange = dateRange;
            ViewBag.LichBayArr = LichBay2;
            ViewBag.current_day = timeNow;          
            ViewBag.cateFlight = cateFlight;
            ViewBag.noidi = dao.getSanBayByID(int.Parse(end.ToString()));
            ViewBag.noiden = dao.getSanBayByID(int.Parse(start.ToString()));
            ViewBag.URL = URL;


            return View();
        }
        public static List<Dictionary<string, dynamic>> flightList = new List<Dictionary<string, dynamic>>();
        public static int LenSS = 0;

        // POST: /api/flight
        [Route("api/flight")]
        [HttpPost]
        public JsonResult AddFlight()
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
        [HttpPost]
        public ActionResult Index()
        {
            
            var SSI = new Dictionary<string, dynamic>();
            
                 
         
            if (URL == "/chuyenbay")
            {
                SSI = Session["thongtintimkiem"] as Dictionary<string, dynamic>;
            }
            else
            {
                StoreSearchInformation();
                SSI = Session["thongtintimkiem"] as Dictionary<string, dynamic>;
            }
            string days = SSI["timeNow"].ToString();
            DateTime day;
            if (!DateTime.TryParse(days, out day))
            {
                // Handle parse error
                Console.WriteLine("Invalid date format in SSI['timeNow']");
               
            }
            List<DateTime> dateRange = new List<DateTime>();
            DateTime startDate;
            if (DateTime.TryParse(SSI["startDate"].ToString(), out startDate))
            {
                for (int i = 0; i < 30; i++)
                {
                    dateRange.Add(startDate.AddDays(i));
                }
            }
            else
            {
                // Handle parse error
                Console.WriteLine("Invalid date format in SSI['startDate']");
            }

            int tongNguoi = (int)SSI["adultNum"] + (int)SSI["childrenNum"];
           

            var start = SSI["start"];
            var end = SSI["end"];
            var ticketLevel = SSI["ticketLevel"];
            var cateFlight = SSI["cateFlight"];
            var adultNum = SSI["adultNum"];
            var childrenNum = SSI["childrenNum"];
            var timeNow = SSI["timeNow"];
            var idnv = Convert.ToInt32(Session["idnv"]);

         
           
            if (URL == "/chuyenbay" && LenSS > 0)
            {
                var endDate = SSI["endDate"];
                URL = "/ThongTinHanhKhach";
               

                var soHieuChuyenBay = dao.GetChuyenBayByTuyenBay(dao.getSanBayByID(int.Parse(start.ToString())), dao.getSanBayByID(int.Parse(end.ToString())));

                var listLBValidTime = dao.filter_LichBay_With_Valid_Time(startDate, timeNow, 1);
                List<LichBay> LichBay = dao.GetLichBayWithChuyenBays(soHieuChuyenBay, listLBValidTime);
                List<LichBay> lbcg = new List<LichBay>();
                var listSoLuongVeDaDat = dao.CountSoGheDaDat(int.Parse(ticketLevel), startDate.Year, startDate.Month, startDate.Day);
                foreach (var dict2 in listSoLuongVeDaDat)
                {
                    foreach (var dict1 in LichBay)
                    {
                        if (dict1.MaLB == dict2.LichBayId && dict2.SoLuongGhe - dict2.SoLuongVeDaDat >= tongNguoi)
                        {
                            lbcg.Add(dict1);
                        }
                    }
                }



                var giaVe = dao.get_GiaVe(int.Parse(ticketLevel));
                ViewBag.GiaVe = giaVe;
                ViewBag.soluong = tongNguoi;
                ViewBag.soluongngl = int.Parse(adultNum.ToString());
                ViewBag.soluongtre = int.Parse(childrenNum.ToString());
                ViewBag.daterange = dateRange;
                ViewBag.LichBayArr = LichBay;
                ViewBag.current_day = timeNow;
                ViewBag.cateFlight = cateFlight;
                ViewBag.noidi = dao.getSanBayByID(int.Parse(end.ToString()));
                ViewBag.noiden = dao.getSanBayByID(int.Parse(start.ToString()));
                ViewBag.URL = URL;
                if (Request.Form["one-way"] == null)
                {
                    ViewBag.dayStart = SSI["endDate"];
                }

              

                return View();
            }

            
            var soHieuChuyenBay2 = dao.GetChuyenBayByTuyenBay(dao.getSanBayByID(int.Parse(start.ToString())), dao.getSanBayByID(int.Parse(end.ToString())));

            var listLBValidTime2 = dao.filter_LichBay_With_Valid_Time(startDate, timeNow, 1);
            List<LichBay> LichBay2 = dao.GetLichBayWithChuyenBays(soHieuChuyenBay2, listLBValidTime2);
            List<LichBay> lbcg2 = new List<LichBay>();
            var listSoLuongVeDaDat2 = dao.CountSoGheDaDat(int.Parse(ticketLevel), startDate.Year, startDate.Month, startDate.Day);
            foreach (var dict2 in listSoLuongVeDaDat2)
            {
                foreach (var dict1 in LichBay2)
                {
                    if (dict1.MaLB == dict2.LichBayId && dict2.SoLuongGhe - dict2.SoLuongVeDaDat >= tongNguoi)
                    {
                        lbcg2.Add(dict1);
                    }
                }
            }


            var giaVe1 = dao.get_GiaVe(int.Parse(ticketLevel));
            ViewBag.GiaVe = giaVe1;
            ViewBag.soluong = tongNguoi;
            ViewBag.soluongngl = int.Parse(adultNum.ToString());
            ViewBag.soluongtre = int.Parse(childrenNum.ToString());
            ViewBag.daterange = dateRange;
            ViewBag.LichBayArr = LichBay2;
            ViewBag.current_day = timeNow;
            ViewBag.cateFlight = cateFlight;
            ViewBag.noidi = dao.getSanBayByID(int.Parse(end.ToString()));
            ViewBag.noiden = dao.getSanBayByID(int.Parse(start.ToString()));
            ViewBag.URL = URL;

            return View();
        }           
    }
}