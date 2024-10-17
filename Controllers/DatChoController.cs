using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LTCSDLMayBay.Controllers
{
    public class DatChoController : Controller
    {
        Dao.Dao dao = new Dao.Dao();
        ThongTinHanhKhachController ttkh = new ThongTinHanhKhachController();
        // GET: DatCho
        [AcceptVerbs(HttpVerbs.Post | HttpVerbs.Get)]
        public ActionResult Index()
        {
           var flight = Session["flight"] as List<Dictionary<string, dynamic>>;
            var tttk = Session["ThongTinTimKiem"] as Dictionary<string, dynamic>;
            if (flight.Count() > 0)
            {
                var adultNum = tttk["adultNum"];
                var childrenNum = tttk["childrenNum"];
                string malb = flight[0]["MaLB"];
                int ma = int.Parse(malb);
                int id_mb = dao.db.LichBays.Where(s => s.MaLB == ma).Select(s => s.mayBayId).FirstOrDefault();
                
                var dayghe = dao.GetDayGheInMayBay(id_mb); // trả về kiểu int 
                var ghe = dao.GetGheInMayBay(id_mb); //tra ve dynamic
                var soluongghe = dao.CountSoGhe(id_mb); //tra ve int
                var soluongday = dayghe.Count; //tra ve int
               
                var tempList = new List<dynamic>();

            foreach (var item in ghe)
            {
                dynamic newItem = new System.Dynamic.ExpandoObject();
                newItem.DayGhe = item.DayGhe;
                newItem.GheId = item.GheId;
                newItem.HangVeId = item.HangVeId;
                tempList.Add(newItem);
            }
            


            var list = tempList.Select(item =>
            {
                dynamic newItem = new System.Dynamic.ExpandoObject();
                dynamic s = new ExpandoObject();
                s.DayGhe = item.DayGhe;
                s.GheId = item.GheId;
                s.HangVeId = item.HangVeId;
                return s;
            }).ToList();
               
                var soluong = int.Parse(adultNum.ToString()) + int.Parse(childrenNum.ToString());
                
             
                var noidi = flight[0]["noidi"].ToString();
                var noiden = flight[0]["noiden"].ToString();
                var MaCB = flight[0]["MaCB"].ToString();
                var hangve = tttk["ticketLevel"];
               

                ViewBag.noiden = noiden;
                ViewBag.MaCB = MaCB;
                ViewBag.noidi = noidi;
                ViewBag.dayghe = dayghe;
                ViewBag.soluongghe = soluongghe;
                ViewBag.ghe = list;
                ViewBag.soluongday = soluongday;
                ViewBag.soluong = soluong;
                ViewBag.hangve = int.Parse(hangve);
            }
            
            return View();
            
            
        }





    }

       
    }
