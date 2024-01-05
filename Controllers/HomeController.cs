using Azure;
using BaoCaoTTCM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BaoCaoTTCM.Controllers
{
    public class HomeController : Controller
    {
        QlbanGiayContext db = new QlbanGiayContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int? page)
        {
            int pageSize = 8;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;
			var lstsanpham = db.TSanPhams.AsNoTracking().OrderBy(x => x.TenSp);
			PagedList<TSanPham> lst = new PagedList<TSanPham>(lstsanpham, pageNumber, pageSize);
			return View(lst);
		}

		public IActionResult SanPhamTheoHang(String mahang, int? page)
		{
			int pageSize = 8;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;
			var listsp = db.TSanPhams.AsNoTracking().Where(x => x.MaHang == mahang).OrderBy(x => x.TenSp);
			PagedList<TSanPham> lst = new PagedList<TSanPham>(listsp, pageNumber, pageSize);
			ViewBag.mahang = mahang;
			return View(lst);
		}

        public IActionResult ChiTietSanPham(string maSp)
        {
            var sanpham = db.TSanPhams.SingleOrDefault(x => x.MaSp == maSp);
            var anhSp = db.TAnhs.Where(x => x.MaSp == maSp).ToList();
            ViewBag.anhSp = anhSp;
            var kt = from b in db.TSizes join a in db.TSanPhams on b.MaSp equals a.MaSp
                                         join c in db.TKichThuocs on b.MaSize equals c.MaSize
                     where b.MaSp == maSp
                     select new{ maSp = a.MaSp , c.SoSize};
            ViewBag.kt = kt;
            return View(sanpham);
        }
        [HttpGet]
        public IActionResult TimKiem(string name)
        {
            var results = db.TSanPhams.Where(x => x.TenSp.Contains(name)).ToList();
            return View(results);
		}

        public IActionResult Shop(int? page)
        {
            int pageSize = 50;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
			var lstsanpham = db.TSanPhams.AsNoTracking().OrderBy(x => x.TenSp);
			PagedList<TSanPham> lst = new PagedList<TSanPham>(lstsanpham, pageNumber, pageSize);
			return View(lst);
		}
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}