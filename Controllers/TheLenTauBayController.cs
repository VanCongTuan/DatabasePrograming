using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LTCSDLMayBay.Controllers
{
    public class TheLenTauBayController : Controller
    {
        // GET: TheLenTauBay
        Dao.Dao dao = new Dao.Dao();
        public ActionResult Index()
        {
            //if (Session["idnv"] != null)
            //{
            //    var flight = Session["flight"] as List<Dictionary<string, dynamic>>;
            //    DateTime ngayDi = DateTime.ParseExact(flight[0]["NgayKhoiHanh"].ToString(), "yyyy-MM-dd HH:mm:ss", null);
            //    string maCB = flight[0]["MaCB"].ToString();
            //    string ma_datcho = Session["madatcho"].ToString();
            //    int venl = (int)Session["ve_banNgl"];
            //    int vete = (int)Session["ve_banTreEm"];
            //    List<string> nguoi_Lon = (List<string>)Session["nguoiLon"];
            //    List<string> tre_Em = (List<string>)Session["treEm"];
            //    int lenNL = nguoi_Lon.Count;
            //    int lenTE = tre_Em.Count;
            //    int tongsoluong = lenNL + lenTE;
            //    var hk = nguoi_Lon.Concat(tre_Em).ToList();

            //    return View("TheLenTauBay", new
            //    {
            //        ga_di = flight[0]["noidi"].ToString(),
            //        ga_den = flight[0]["noiden"].ToString(),
            //        lenNL = lenNL,
            //        so_ghe = Session["GHE"].ToString(),
            //        hk = hk,
            //        maCB = maCB,
            //        ngayDi = ngayDi.ToString("yyyy-MM-dd"),
            //        gioDi = ngayDi.ToString("HH:mm:ss"),
            //        ma_datcho = ma_datcho,
            //        soluong = tongsoluong,
            //        vete = vete,
            //        venl = venl
            //    });
            //}
            //else
            //{
            //    //string ma_datcho = Session["madatchocheckin"].ToString() ;
            //    string ma_datcho = "1";
            //    var thongtincb = Session["id_mb"] as Dictionary<string, dynamic>;
            //    DateTime ngay_gio_bay = DateTime.ParseExact(int.Parse(thongtincb["ngaykhoihanh"].ToString()), "yyyy-MM-dd HH:mm:ss", null);
            //    var hk_ve = dao.GetMaVeForHanhKhach(thongtincb["malb"].ToString(),int.Parse( ma_datcho));

            //    return View("TheLenTauBay", new
            //    {
            //        ga_di = thongtincb["noidi"].ToString(),
            //        ga_den = thongtincb["noiden"].ToString(),
            //        maCB = thongtincb["macb"].ToString(),
            //        ngayDi = ngay_gio_bay.ToString("yyyy-MM-dd"),
            //        gioDi = ngay_gio_bay.ToString("HH:mm:ss"),
            //        ma_datcho = ma_datcho,
            //        hk_ve = hk_ve,
            //        soluong = hk_ve.Count,
            //        hangve = hk_ve.First().LoaiHang
            //    });
            //}
             return View();
        }
    }
}