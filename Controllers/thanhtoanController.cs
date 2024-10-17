using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PayPal.Api;

namespace LTCSDLMayBay.Controllers
{
    public class thanhtoanController : Controller
    {
        Dao.Dao dao = new Dao.Dao();
        private string CCCD_nguoiLon = "";
        ThongTinHanhKhachController ttkh = new ThongTinHanhKhachController();
        // GET: thanhtoan

        [AcceptVerbs(HttpVerbs.Post | HttpVerbs.Get)]
        public ActionResult Index()
        {
            if (Request.HttpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase))
            {
                Session["email"] = Request.Form["email"];
                Session["sodienthoai"] = Request.Form["sdt"];
                var nguoiLon = new List<Dictionary<string, dynamic>>();
                var treEm = new List<Dictionary<string, dynamic>>();
                var thongtintimkiem = Session["ThongTinTimKiem"] as Dictionary<string, dynamic>;
                var adultNum = thongtintimkiem["adultNum"];
                var childrenNum = thongtintimkiem["childrenNum"];
                var ticketLevel = thongtintimkiem["ticketLevel"];
                ViewBag.adultNum = int.Parse(adultNum.ToString());
                ViewBag.childrenNum = int.Parse(childrenNum.ToString());
                var tong = ttkh.TongTien(ticketLevel);
                ViewBag.tong = tong;
                for (int i = 0; i < Convert.ToInt32(adultNum); i++)
                {
                    var nguoiLonInfo = new Dictionary<string, dynamic>
                    {
                        ["hoten"] = Request.Form["hoten" + i],
                        ["gioitinh"] = Request.Form["gioitinh" + i],
                        ["ngaySinh"] = Request.Form["ngaysinh_Nglon" + i],
                        ["CCCD"] = Request.Form["CCCD" + i]
                    };

                    if (Request.Form["nguoiLon_diCung"] != null)
                    {
                        if (nguoiLonInfo["hoten"].Equals(Request.Form["nguoiLon_diCung"]))
                        {
                            CCCD_nguoiLon = nguoiLonInfo["CCCD"];
                        }
                    }

                    nguoiLon.Add(nguoiLonInfo);
                }
                ViewBag.nguoiLon = nguoiLon;

                for (int j = 0; j < Convert.ToInt32(childrenNum); j++)
                {
                    var treEmInfo = new Dictionary<string, dynamic>
                    {
                        ["CCCD_NglondiCung"] = CCCD_nguoiLon,
                        ["hotenTreEm"] = Request.Form["hoten_TreEm" + j],
                        ["gioitinhTreEm"] = Request.Form["gioitinh_TreEm" + j],
                        ["ngaySinhTreEm"] = Request.Form["ngaySinh_TreEm" + j]
                    };

                    treEm.Add(treEmInfo);
                }
               
                var thongtinLienHe = new Dictionary<string, dynamic>
                {
                    ["Email"] = Request.Form["email"],
                    ["SĐT"] = Request.Form["sdt"]
                };
                Session["nguoiLon"] = nguoiLon ;
                var nl= Session["nguoiLon"] as  List<Dictionary<string, dynamic>>;
                
                Session["nguoiLon"] = nl;
                var te = Session["treEm"] as List<Dictionary<string, dynamic>>;
                te = treEm;
                Session["treEm"] = te;

                Session["lienHe"] = thongtinLienHe;
                var lh = Session["lienHe"] as Dictionary<string, dynamic>;


                ViewBag.nguoiLon = nl;
                //ViewBag.treEm = thongtinLienHe;
                var loaive = thongtintimkiem["cateFlight"].ToString();
                var ma_hd = new List<int> { CheckMaDatCho() };

                if (loaive.Equals("round-trip"))
                {
                    ma_hd.Add(CheckMaDatCho());
                }

                Session["maHoaDon"] = ma_hd;
                ViewBag.maHD=ma_hd;


                return View();

            }

            return View();
        }

        

        private int LayMaDatCho()
        {
            int maxId = dao.db.PhieuDatChos.Max(b => (int)b.MaPhieu);
           int maDatCho = maxId + 1;
            return maDatCho;
        }

        public int CheckMaDatCho()
        {
            var layma = LayMaDatCho();
            var ma_DatCho = dao.truyVanMaPDC(layma); // Assuming dao is an instance of a class containing TruyVanMaPDC method
          

            if (layma.Equals(ma_DatCho))
            {
                return CheckMaDatCho();
            }
            else
            {
                return layma;
            }
        }
        private static APIContext GetAPIContext()
        {
            var config = ConfigManager.Instance.GetProperties();
            string accessToken = new OAuthTokenCredential(config).GetAccessToken();
            return new APIContext(accessToken);
        }

        [HttpPost]
        public JsonResult ExecutePayment(string orderId)
        {
            var apiContext = GetAPIContext();
            var paymentExecution = new PaymentExecution() { payer_id = orderId };
            var payment = new Payment() { id = orderId };

            try
            {
                var executedPayment = payment.Execute(apiContext, paymentExecution);

                if (executedPayment.state.ToLower() != "approved")
                {
                    return Json(new { success = false });
                }
            }
            catch (PayPal.PaymentsException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

            return Json(new { success = true });
        }
    }
}