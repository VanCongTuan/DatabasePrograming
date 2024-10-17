using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;

namespace LTCSDLMayBay.Controllers
{
    public class CheckinController : Controller
    {
        Dao.Dao dao = new Dao.Dao();
        // GET: Checkin
        [AcceptVerbs(HttpVerbs.Post | HttpVerbs.Get)]
        public ActionResult Index()
        {
            if (Request.HttpMethod == "POST")
            {
                
                string matdatcho = Request.Form["madatcho"].ToString();
                int MaChoKH = int.Parse(matdatcho);
                
                    var TTKhachHangNL = dao.GetHanhKhachNguoiLonByMaPhieu(MaChoKH);
                    URL_CI = "/DatCho";                   
                    
                    if (TTKhachHangNL == null )
                    {
                        string err = "Mã đặt chổ của bạn không đúng hoặc đã được xử lý, vui lòng kiểm tra lại";
                        ViewBag.err = err;
                        return View("Checkin",err);
                    }

                    var TTChuyen = dao.GetLichBayByMaPhieu(MaChoKH);
                    var TTKhachHangTE = dao.getHanhKhachTreEmByMaPhieu(MaChoKH);
                    Session["CheckInFlight"] = GetCheckInFlight(TTChuyen);
                    Session["CheckInHKNL"] = GetCheckInHanhKhach(TTKhachHangNL);
                    Session["CheckInHKTE"] = GetCheckInHanhKhach(TTKhachHangTE);
                    Session["madatchocheckin"] = MaChoKH;
                    var TTchuyenbay = GetCheckInFlight(TTChuyen);
                    var KhachHangNL = GetCheckInHanhKhach(TTKhachHangNL);
                    var KhachHangTE = GetCheckInHanhKhach(TTKhachHangTE);
                //if (Session["CheckInFlight"] != null && Session["CheckInHKNL"] != null )
                //{
                //    ViewBag.TT = "1";
                //}else
                //{
                //    ViewBag.TT = "2";
                //}
                var tempList = new List<dynamic>();

                foreach (var item in TTchuyenbay)
                {
                    dynamic newItem = new System.Dynamic.ExpandoObject();
                    newItem.MaLB = item.MaLB;
                    newItem.MaCB = item.MaCB;
                    newItem.MayBay = dao.GetMayBay(item.idMayBay);
                    newItem.idMayBay = item.idMayBay;
                    newItem.Noidi = item.Noidi;
                    newItem.Noiden = item.Noiden;
                    newItem.NgayKhoiHanh = item.NgayKhoiHanh;
                    newItem.ThoiGianDi = item.ThoiGianDi;

                    tempList.Add(newItem);
                }

                var list = tempList.Select(item =>
                {
                    dynamic s = new ExpandoObject();
                    s.MaLB = item.MaLB;
                    s.MaCB = item.MaCB;
                    s.MayBay = dao.GetMayBay(item.idMayBay);
                    s.idMayBay = item.idMayBay;
                    s.Noidi = item.Noidi;
                    s.Noiden = item.Noiden;
                    s.NgayKhoiHanh = item.NgayKhoiHanh;
                    s.ThoiGianDi = item.ThoiGianDi;
                    return s;
                }).ToList();
                var tempListNL = new List<dynamic>();

                foreach (var item in KhachHangNL)
                {
                    dynamic newItem = new System.Dynamic.ExpandoObject();
                    
                    newItem.Hoten = item.Hoten;
                    newItem.NgaySinh = item.NgaySinh;
                    newItem.GioiTinh = item.GioiTinh;                   
                    newItem.SDT = item.SDT;
                    newItem.CCCD = item.CCCD;
                   
                    tempListNL.Add(newItem);
                }

                
                if (KhachHangNL!=null)
                {
                    ViewBag.TT = "1";
                }
                else {

                    ViewBag.TT= "2";
                }
                ViewBag.TTchuyenbay = list;
                //ViewBag.TTKhachHangNL = tempListNL;
                //ViewBag.TTKhachHangNL1 = KhachHangNL;
                //ViewBag.TTKhachHangTE =KhachHangTE;                                       

            }
            return View();
        }

        public dynamic GetCheckInFlight(dynamic TTChuyen)
        {
            var LichBay = new List<dynamic>();
            foreach (var lb in TTChuyen)
            {
                var tuyenBay = dao.GetTuyenBayByIDChuyenBay(lb.ChuyenBayId);
                var id_SbDi = tuyenBay.id_SbDi;
                var id_SbDen = tuyenBay.id_SbDen;
                var noidi = dao.getSanBayByID(id_SbDi);
                var noiden = dao.getSanBayByID(id_SbDen);

                LichBay.Add(new
                {
                    MaLB = lb.LichBayId,
                    MaCB = lb.ChuyenBayId,
                    MayBay = dao.GetMayBay(lb.MayBayId),
                    idMayBay = lb.MayBayId,
                    Noidi = noidi,
                    Noiden = noiden,
                    NgayKhoiHanh = lb.NgayBay,
                    ThoiGianDi = lb.ThoiGianBay
                });
            }
            return LichBay;
        }

        public dynamic GetCheckInHanhKhach(dynamic TTKhachHang)
        {
            var TTHanhKhachs = new List<dynamic>();
            foreach (var hk in TTKhachHang)
            {
                             

                if (hk.id_NguoiLon != null)
                {
                    if (hk.sdt != null && hk.CCCD != null)
                    {
                          
                        TTHanhKhachs.Add(new
                        { 
                            Hoten = hk.HoTen,
                            NgaySinh = hk.NgSinh,
                            GioiTinh = hk.GioiTinh,
                            HangVeId = hk.HangVeId,
                            SDT = hk.Sdt,
                            CCCD = hk.CCCD,
                           
                        NguoiBaoHo = dao.GetNguoiBaoHoById(hk.HanhKhachId)
                        }) ;
                }
                    else
                    {
                        var tt = new
                        {
                            Hoten = hk.HoTen,
                            NgaySinh = hk.NgSinh,
                            GioiTinh = hk.GioiTinh,
                            HangVeId = hk.HangVeId,                            
                            NguoiBaoHo = dao.GetNguoiBaoHoById(hk.HanhKhachId)
                        };
                        TTHanhKhachs.Add(tt);
                    }



                }
                else
                {
                    var tt = new
                    {
                        Hoten = hk.HoTen,
                        NgaySinh = hk.NgSinh,
                        GioiTinh = hk.GioiTinh,
                        HangVeId = hk.HangVeId,
                       

                    };
                    TTHanhKhachs.Add(tt);
                }
               
            }
            return TTHanhKhachs;
        }

        string URL_CI = "";

       

        public ActionResult delete_ssCI()
        {
            APIController aPIController = new APIController();
            aPIController.DeleteSession();
            return RedirectToAction("Checkin", "CheckinController");
        }       

        
        }
}