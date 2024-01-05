using BaoCaoTTCM.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace BaoCaoTTCM.Controllers
{
	public class AccessController : Controller
	{
        QlbanGiayContext db = new QlbanGiayContext();
		[HttpGet]
		public IActionResult Login()
		{
			if(HttpContext.Session.GetString("UserName") == null)
			{
				return View();
			}	
			return RedirectToAction("Index","Home");
		}
		[HttpPost]
		public IActionResult Login(TUser user)
		{
            TempData["DKTB"] = "";
            if (HttpContext.Session.GetString("UserName") == null)
			{
				string pass = MD5Hash(user.Password);
				var u = db.TUsers.Where(x=>x.UserName.Equals(user.UserName)&& x.Password.Equals(pass) && x.LoaiUser == null).FirstOrDefault();
				if(u != null)
				{
					HttpContext.Session.SetString("UserName", u.UserName.ToString());
					return RedirectToAction("Index", "Cart");
				}
                var v = db.TUsers.Where(x => x.UserName.Equals(user.UserName) && x.Password.Equals(pass) && x.LoaiUser == 0).FirstOrDefault();
                if (v != null)
                {
                    HttpContext.Session.SetString("UserName", v.UserName.ToString());
                    return RedirectToAction("Index", "Admin");
                }

            }
            TempData["DKTB"] = "Thông tin tài khoản hoặc mật khẩu bạn chưa chính xác";
            return View();
		}
		[HttpPost]
		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			HttpContext.Session.Remove("UserName");
			return RedirectToAction("Login", "Access");
		}


		[HttpGet]
		public IActionResult DangKy()
		{
			return View();
		}
		[HttpPost]
		public IActionResult DangKy(DangKyModel model)
		{
			var a = model.User;
			var b = model.KhachHang;
			TempData["DKTB"] = "";
			var tk = db.TUsers.FirstOrDefault(x => x.UserName == a.UserName);
			if (tk != null)
			{
				TempData["DKTB"] = "Tên đăng nhập đã tồn tại";
			}
			else
			{
				var c = db.TKhachHangs.Max(x => x.MaKh);
				string ma = maHdTd(c.ToString());
				TKhachHang kh = new TKhachHang
				{
					MaKh = ma,
					TenKh = b.TenKh,
					GioiTinh = b.GioiTinh,
					DiaChi= b.DiaChi,
					DienThoai = b.DienThoai
				};
				db.TKhachHangs.Add(kh);
				db.SaveChanges();
				TUser lkh = new TUser
				{
					UserName = a.UserName,
					Password = MD5Hash(a.Password)
				};
				db.TUsers.Add(lkh);
				db.SaveChanges();
				return RedirectToAction("Login", "Access");
			}
			return View();
		}

        [HttpGet]
        public IActionResult DoiMK()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DoiMK(ChangePassword model)
        {
			var tk = HttpContext.Session.GetString("UserName");
            var oldPass = MD5Hash(model.OldPassword.Trim());
            var newPass = MD5Hash(model.NewPassword.Trim());

            if (model.NewPassword != model.ConfirmPassword)
            {
                TempData["DKTB"] = "Mật khẩu mới không khớp";
                return View();
            }
			var tk1 = db.TUsers.Find(tk);
            if (tk1 != null && tk1.Password == oldPass)
            {
                tk1.Password = newPass;
                db.TUsers.Update(tk1);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Đổi mật khẩu thành công";
                return RedirectToAction("login", "Access");
            }
            else
            {
                TempData["DKTB"] = "Mật khẩu cũ không đúng";
                return View();
            }
        }




        private string MD5Hash(string input)
		{
			using (MD5 md5hash = MD5.Create())
			{
				byte[] data = md5hash.ComputeHash(Encoding.UTF8.GetBytes(input));
				StringBuilder sBuilder = new StringBuilder();
				for (int i = 0; i < data.Length; i++)
				{
					sBuilder.Append(data[i].ToString("x2"));
				}
				return sBuilder.ToString();
			}
		}
		public string maHdTd(string a)
		{
			var c = db.TKhachHangs.Max(x => x.MaKh);
			string maHD = a;
			Match match = Regex.Match(maHD, @"\d+");
			int soHD = int.Parse(match.Value);
			soHD++;
			string soHDString = soHD.ToString().PadLeft(match.Value.Length, '0');
			string maHDMoi = maHD.Substring(0, match.Index) + soHDString;
			return maHDMoi;
		}
	}
}
