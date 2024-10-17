using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LTCSDLMayBay.Controllers
{
    public class HomeController : Controller
    {
        Dao.Dao dao = new Dao.Dao();
        public ActionResult Index()
        {
           Session["flight"] = new List<Dictionary<string, dynamic>>();
            ViewBag.level = new SelectList(dao.db.HangVes, "Id", "LoaiHang");
            ViewBag.EndDes = new SelectList(dao.db.SanBays, "MaSB", "DiaChi");
            ViewBag.StartDes = new SelectList(dao.db.SanBays, "MaSB", "DiaChi");
            return View();
        }
        
       



       
    }
}