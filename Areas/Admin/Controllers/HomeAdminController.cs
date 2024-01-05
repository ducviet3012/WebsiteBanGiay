using Azure;
using BaoCaoTTCM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using X.PagedList;

namespace BaoCaoTTCM.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    public class HomeAdminController : Controller
    {
        QlbanGiayContext db = new QlbanGiayContext();
        public IActionResult Index()
        {
            return View();
        }

        // Danh mục sản phẩm
        [Route("danhmucsanpham")]
        public IActionResult DanhMucSanPham(int? page)
        {
            int pageSize = 8;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;
			var lstsanpham = db.TSanPhams.AsNoTracking().OrderBy(x => x.MaSp);
			PagedList<TSanPham> lst = new PagedList<TSanPham>(lstsanpham, pageNumber, pageSize);
			return View(lst);
		}

        // Thêm sản phẩm mới
        [Route("ThemSanPhamMoi")]
        [HttpGet]
        public IActionResult ThemSanPhamMoi()
        {
            ViewBag.MaHang = new SelectList(db.THangs.ToList(), "MaHang", "TenHang");  
            return View();
        }
        [Route("ThemSanPhamMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemSanPhamMoi(TSanPham SanPham)
        {
            if (ModelState.IsValid)
            {
                db.TSanPhams.Add(SanPham);
                db.SaveChanges();
                TempData["Message"] = "Thêm sản phẩm mới thành công";
                return RedirectToAction("DanhMucSanPham");
            }
            return View(SanPham);
        }

        // Sửa sản phẩm
        [Route("SuaSanPham")]
        [HttpGet]
        public IActionResult SuaSanPham(String masp)
        {
            ViewBag.MaHang = new SelectList(db.THangs.ToList(), "MaHang", "TenHang");
            var sanPham = db.TSanPhams.Find(masp);
            return View(sanPham);
        }

        [Route("SuaSanPham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaSanPham(TSanPham SanPham)
        {
            if (ModelState.IsValid)
            {
                db.Update(SanPham);
                db.SaveChanges();
                TempData["Message"] = "Sửa sản phẩm thành công";
                return RedirectToAction("DanhMucSanPham", "HomeAdmin");
            }
            return View(SanPham);
        }

        // Xóa sản phẩm 
        [Route("XoaSanPham")]
        [HttpGet]
        public IActionResult XoaSanPham(String masp)
        {
            TempData["Message"] = "";
            var anhSanPhams = db.TAnhs.Where(x => x.MaSp == masp);
            if (anhSanPhams.Any()) db.RemoveRange(anhSanPhams);
            db.Remove(db.TSanPhams.Find(masp));
            db.SaveChanges();
            TempData["Message"] = "Sản phẩm đã được xóa";
            return RedirectToAction("DanhMucSanPham", "Admin");
        }

        // Nhân viên
        [Route("nhanvien")]
        public IActionResult NhanVien(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.TNhanViens.AsNoTracking().OrderBy(x => x.MaNv);
            PagedList<TNhanVien> lst = new PagedList<TNhanVien>(lstsanpham, pageNumber, pageSize);
            return View(lst);
        }

        // Thêm nhân viên

        [Route("ThemNhanVienMoi")]
        [HttpGet]
        public IActionResult ThemNhanVienMoi()
        {
            return View();
        }
        [Route("ThemNhanVienMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemNhanVienMoi(TNhanVien nhanvien)
        {
            if (ModelState.IsValid)
            {
                db.TNhanViens.Add(nhanvien);
                db.SaveChanges();
                TempData["Message"] = "Thêm sản phẩm mới thành công";
                return RedirectToAction("NhanVien");
            }
            return View(nhanvien);
        }
        // Sửa nhân viên
		[Route("SuaNhanVien")]
		[HttpGet]
		public IActionResult SuaNhanVien(String manv)
		{
			var sanPham = db.TNhanViens.Find(manv);
			return View(sanPham);
		}

		[Route("SuaNhanVien")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult SuaNhanVien(TNhanVien nhanvien)
		{
			if (ModelState.IsValid)
			{
				db.Update(nhanvien);
				db.SaveChanges();
				TempData["Message"] = "Sửa thông tin nhân viên thành công";
				return RedirectToAction("nhanvien", "HomeAdmin");
			}
			return View(nhanvien);
		}

        // Xóa nhân viên
        [Route("XoaNhanVien")]
        [HttpGet]
        public IActionResult XoaNhanVien(String manv)
        {
            TempData["Message"] = "";
            var hdb = db.THoaDonBans.Where(x => x.MaNv == manv);
            if (hdb.Any()) db.RemoveRange(manv);
            db.Remove(db.TNhanViens.Find(manv));
            db.SaveChanges();
            TempData["Message"] = "Thông tin nhân viên đã được xóa";
            return RedirectToAction("nhanvien", "Admin");
        }

        // Khách hàng
        [Route("khachhang")]
        public IActionResult KhachHang(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.TKhachHangs.AsNoTracking().OrderBy(x => x.MaKh);
            PagedList<TKhachHang> lst = new PagedList<TKhachHang>(lstsanpham, pageNumber, pageSize);
            return View(lst);
        }

        // Sửa khách hàng
        [Route("SuaKhachHang")]
        [HttpGet]
        public IActionResult SuaKhachHang(String makh)
        {
            var kh = db.TKhachHangs.Find(makh);
            return View(kh);
        }

        [Route("SuaKhachHang")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaKhachHang(TKhachHang khachhang)
        {
            if (ModelState.IsValid)
            {
                db.Update(khachhang);
                db.SaveChanges();
                TempData["Message"] = "Sửa thông tin khách hàng thành công";
                return RedirectToAction("khachhang", "HomeAdmin");
            }
            return View(khachhang);
        }

        // Xóa khách hàng
        [Route("XoaKhachHang")]
        [HttpGet]
        public IActionResult XoaKhachHang(String makh)
        {
            TempData["Message"] = "";
            var kh = db.THoaDonBans.Where(x => x.MaKh == makh);
            if (kh.Any()) db.RemoveRange(makh);
            db.Remove(db.TKhachHangs.Find(makh));
            db.SaveChanges();
            TempData["Message"] = "Thông tin khách hàng đã được xóa";
            return RedirectToAction("khachhang", "Admin");
        }

        // Hóa đơn nhập
        [Route("hoadonnhap")]
        public IActionResult HoaDonNhap(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.THoaDonNhaps.AsNoTracking().OrderBy(x => x.SoHdn);
            PagedList<THoaDonNhap> lst = new PagedList<THoaDonNhap>(lstsanpham, pageNumber, pageSize);
            return View(lst);
        }

        // Thêm hóa đơn nhập

        [Route("ThemHDN")]
        [HttpGet]
        public IActionResult ThemHDN()
        {
            ViewBag.MaNv = new SelectList(db.TNhanViens.ToList(), "MaNv","MaNv");
            ViewBag.MaNcc = new SelectList(db.TNhaCungCaps.ToList(), "MaNcc","MaNcc");
            return View();
        }
        [Route("ThemHDN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemHDN(THoaDonNhap hdn)
        {
            if (ModelState.IsValid)
            {
                db.THoaDonNhaps.Add(hdn);
                db.SaveChanges();
                TempData["Message"] = "Thêm Hóa đơn mới thành công";
                return RedirectToAction("hoadonnhap","Admin");
            }
            return View(hdn);
        }

        // Sửa hóa đơn nhập
        [Route("SuaHDN")]
        [HttpGet]
        public IActionResult SuaHDN(String sohdn)
        {
            var kh = db.THoaDonNhaps.Find(sohdn);
            ViewBag.MaNv = new SelectList(db.TNhanViens.ToList(), "MaNv", "MaNv");
            ViewBag.MaNcc = new SelectList(db.TNhaCungCaps.ToList(), "MaNcc", "MaNcc");
            return View(kh);
        }

        [Route("SuaHDN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaHDN(THoaDonNhap hdn)
        {
            if (ModelState.IsValid)
            {
                db.Update(hdn);
                db.SaveChanges();
                TempData["Message"] = "Sửa hóa đơn nhập thành công";
                return RedirectToAction("hoadonnhap","Admin");
            }
            return View(hdn);
        }

        // Xóa hóa đơn nhập
        [Route("XoaHDN")]
        [HttpGet]
        public IActionResult XoaHDN(String sohdn)
        {
            TempData["Message"] = "";
            var kt = db.TChiTietHdns.Where(x => x.SoHdn == sohdn).ToList();
            if (kt.Count() > 0)
            {
                TempData["Message"] = "Không xóa được hóa đơn này";
                return RedirectToAction("hoadonnhap", "Admin");
            }
            db.Remove(db.THoaDonNhaps.Find(sohdn));
            db.SaveChanges();
            TempData["Message"] = "Hóa đơn nhập đã được xóa";
            return RedirectToAction("hoadonnhap", "Admin");
        }

        // Chi tiết HDN
        [Route("chitiethdn")]
        public IActionResult ChiTietHDN(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.TChiTietHdns.AsNoTracking().OrderBy(x => x.SoHdn);
            PagedList<TChiTietHdn> lst = new PagedList<TChiTietHdn>(lstsanpham, pageNumber, pageSize);
            return View(lst);
        }

        // Thêm chi tiết hóa đơn nhập

        [Route("themchitiethdn")]
        [HttpGet]
        public IActionResult ThemChiTietHDN()
        {
            ViewBag.SoHdn = new SelectList(db.THoaDonNhaps.ToList(), "SoHdn", "SoHdn");
            return View();
        }
        [Route("themchitiethdn")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemChiTietHDN(TChiTietHdn hdn)
        {
            if (ModelState.IsValid)
            {
                db.TChiTietHdns.Add(hdn);
                db.SaveChanges();
                TempData["Message"] = "Thêm Hóa đơn mới thành công";
                return RedirectToAction("chitiethdn", "Admin");
            }
            return View(hdn);
        }

        // Sửa chi tiết hóa đơn nhập
        [Route("suachitiethdn")]
        [HttpGet]
        public IActionResult SuaChiTietHDN(String sohdn, string masp)
        {
            var kh = db.TChiTietHdns.Find(sohdn,masp);
            ViewBag.SoHdn = new SelectList(db.THoaDonNhaps.ToList(), "SoHdn", "SoHdn");
            return View(kh);
        }
        [Route("suachitiethdn")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaChiTietHDN(TChiTietHdn hdn)
        {
            if (ModelState.IsValid)
            {
                db.Update(hdn);
                db.SaveChanges();
                TempData["Message"] = "Sửa hóa đơn nhập thành công";
                return RedirectToAction("chitiethdn", "Admin");
            }
            return View(hdn);
        }

        // Xóa hóa đơn nhập
        [Route("xoachitiethdn")]
        [HttpGet]
        public IActionResult XoaChiTietHDN(String sohdn, string masp)
        {
            TempData["Message"] = "";
            db.Remove(db.TChiTietHdns.Find(sohdn, masp));
            db.SaveChanges();
            TempData["Message"] = "Chi tiết hóa đơn nhập đã được xóa";
            return RedirectToAction("chitiethdn", "Admin");
        }


        // Acount
        [Route("acount")]
        public IActionResult Acount(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.TUsers.AsNoTracking().OrderBy(x => x.UserName);
            PagedList<TUser> lst = new PagedList<TUser>(lstsanpham, pageNumber, pageSize);
            return View(lst);
        }

        // Thêm acount

        [Route("ThemAcount")]
        [HttpGet]
        public IActionResult ThemAcount()
        {
            return View();
        }
        [Route("ThemAcount")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemAcount(TUser user)
        {
            if (ModelState.IsValid)
            {
                user.Password = MD5Hash(user.Password);
                db.TUsers.Add(user);
                db.SaveChanges();
                TempData["Message"] = "Thêm Acount mới thành công";
                return RedirectToAction("NhanVien");
            }
            return View(user);
        }

        // Sửa Acount
        [Route("SuaAcount")]
        [HttpGet]
        public IActionResult SuaAcount(String user)
        {
            var kh = db.TUsers.Find(user);
            return View(kh);
        }

        [Route("SuaAcount")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaAcount(TUser user)
        {
            if (ModelState.IsValid)
            {
                user.Password = MD5Hash(user.Password);
                db.Update(user);
                db.SaveChanges();
                TempData["Message"] = "Sửa Acount thành công";
                return RedirectToAction("Acount", "HomeAdmin");
            }
            return View(user);
        }

        // Xóa Acount
        [Route("XoaAcount")]
        [HttpGet]
        public IActionResult XoaAcount(String user)
        {
            TempData["Message"] = "";
            db.Remove(db.TUsers.Find(user));
            db.SaveChanges();
            TempData["Message"] = "Acount đã được xóa";
            return RedirectToAction("Acount", "Admin");
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
    }
}
