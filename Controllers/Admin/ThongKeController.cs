using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;

namespace LTCSDLMayBay.Controllers.Admin
{
    public class ThongKeController : Controller
    {
        Dao.Dao dao = new Dao.Dao();
        // GET: ThongKe
        public ActionResult Index()
        {
            int month;
            if (!string.IsNullOrEmpty(Request.QueryString["month"]))
            {
                month = int.Parse(Request.QueryString["month"].ToString());
            }
            else
            {

                month = DateTime.Now.Month;
            }

            var sumOfTotal = dao.db.HoaDons
                           .Where(hd => hd.NgayLap.Month == month)
                           .Sum(hd => hd.TongTien);

            // Truy vấn 2: Tính tổng tiền và số lần bay của từng tuyến bay trong tháng 5
            var query = from a in dao.db.TuyenBays
                        join b in dao.db.ChuyenBays on a.MaTuyenBay equals b.tuyenBayId
                        join c in dao.db.LichBays on b.MaCB equals c.chuyenBayId
                        join d in dao.db.HoaDons on c.MaLB equals d.lichBayId
                        where d.NgayLap.Month == month
                        group d by a.MaTuyenBay into g
                        select new
                        {
                            MaTuyenBay = g.Key,
                            TongTien = g.Sum(x => x.TongTien),
                            SoLanBay = g.Count()

                        };


            var tempList = query.Select(item => new
            {
                MaTuyenBay = item.MaTuyenBay,
                TongTien = item.TongTien,
                SoLanBay = item.SoLanBay
            }).ToList();


            var list = tempList.Select(item =>
            {
                dynamic s = new ExpandoObject();
                s.MaTuyenBay = item.MaTuyenBay;
                s.TongTien = item.TongTien;
                s.SoLanBay = item.SoLanBay;
                return s;
            }).ToList();



            ViewBag.results = list;
            //var sum = TongDoanhThu(month.Month);
            //ViewBag.results = results;
            ViewBag.sum = sumOfTotal;
            return View();

        }

        
    }
}