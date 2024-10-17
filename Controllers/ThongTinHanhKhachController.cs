using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LTCSDLMayBay.Controllers
{
    public class ThongTinHanhKhachController : Controller
    {
        Dao.Dao dao = new Dao.Dao();
        [AcceptVerbs(HttpVerbs.Post | HttpVerbs.Get)]
        public ActionResult Index()
        {
            try
            {

                
                URL = "";
                URL_banve = "/thanhtoan";
                ViewBag.URL_banve = URL_banve;
                ViewBag.flight = Session["flight"];
                ViewBag.idnv = Session["idnv"];
                var thongTin = Session["ThongTinTimKiem"] as Dictionary<string, dynamic>;

                var sl_NguoiLon = thongTin["adultNum"];
                var sl_TreEm = thongTin["childrenNum"];
                var ticketLevel=thongTin["ticketLevel"];
               
               
                //string temp = Request.Form["buttonValues"];
                //List<string> resultList = new List<string>(temp.Split(','));

                //Session["GHE"]= resultList;
                //if (Session["GHE"] != null)
                //{
                //    ViewBag.ListGhe1 = 1;
                //}
                //else
                //{
                //    ViewBag.ListGhe1 = 2;
                //}
                // Định nghĩa người lớn và trẻ em tại đây
                var tong = TongTien(ticketLevel);
                Session["soluong"] = int.Parse(sl_NguoiLon.ToString()) + int.Parse(sl_TreEm.ToString());
                ViewBag.sl = Session["soluong"];
                ViewBag.sl_NguoiLon = int.Parse(sl_NguoiLon.ToString());
                ViewBag.sl_TreEm = int.Parse(sl_TreEm.ToString());
                ViewBag.tong = tong;
              
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return RedirectToAction("ThongTinHanhKhach");
            }
            
        }

        public float TongTien(string ticketLevel)
        {
            var giaVe = dao.get_GiaVe(int.Parse(ticketLevel));
            return giaVe;
        }

        string URL_banve = "";
        string URL = "";
       
       
        

    }
}