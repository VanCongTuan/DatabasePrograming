using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace LTCSDLMayBay.Controllers
{
    public class LoginController : Controller
    {
        Dao.Dao dao = new Dao.Dao();
        [HttpGet]
        public ActionResult Index()
        {

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Index(string username, string password)
        {

            string mk = ComputeSHA256Hash(password);
            var user = dao.db.TaiKhoans.SingleOrDefault(u => u.Username == mk && u.Password == password);


            if (user != null)
            {
                var hoten = dao.GetHotenByAccount(username, password);
                var userRole = user.UserRole.ToString();
                FormsAuthentication.SetAuthCookie(username, false);

                if (hoten != null && hoten.HoVaTen != null)
                {
                    Session["hotennv"] = new
                    {
                        hovaTen = hoten.HoVaTen,
                        role = userRole
                    };
                    Session["id"] = hoten.Id;
                }




                if (userRole == "Laplich")
                {
                    return RedirectToAction("LapLich");

                }
                if (userRole == "Banve")
                {
                    return RedirectToAction("BanVe");

                }
                if (userRole == "Admin")
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });

                }
            }
            else
            {
                ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không chính xác, vui lòng thử lại.";
                return RedirectToAction("Index");

            }


            return View();

        }
        public static string ComputeSHA256Hash(string rawData)
        {
            // Tạo một đối tượng SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Tạo một mảng bytes từ chuỗi đầu vào
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Chuyển đổi mảng bytes thành chuỗi hex
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public ActionResult LapLich()
        {
            var hotenSession = Session["hotennv"] as dynamic;
            if (hotenSession != null)
            {
                string hoTen = hotenSession.hovaTen;
                string role = hotenSession.role;

                // Lưu tên vào ViewBag để sử dụng trong View
                ViewBag.ten = hoTen;
            }
            ViewBag.TuyenBay = new SelectList(dao.db.TuyenBays, "MaTuyenBay", "MaTuyenBay");
            ViewBag.MaChuyen = new SelectList(dao.db.ChuyenBays, "MaCB", "TenCB");
            ViewBag.MayBay = new SelectList(dao.db.MayBays, "MaMb", "TenMB");
            ViewBag.SanBay= new SelectList(dao.db.SanBays, "MaSB", "DiaChi");
            return View();
        }
        public ActionResult BanVe()
        {
            var hotenSession = Session["hotennv"] as dynamic;
            if (hotenSession != null)
            {
                string hoTen = hotenSession.hovaTen;
                string role = hotenSession.role;

                // Lưu tên vào ViewBag để sử dụng trong View
                ViewBag.ten = hoTen;
            }
            Session["flight"] = new List<Dictionary<string, dynamic>>();
            ViewBag.level = new SelectList(dao.db.HangVes, "Id", "LoaiHang");
            ViewBag.EndDes = new SelectList(dao.db.SanBays, "MaSB", "DiaChi");
            ViewBag.StartDes = new SelectList(dao.db.SanBays, "MaSB", "DiaChi");
            return View();
        }
        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // Xóa phiên
            Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddDays(-1);
            Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
