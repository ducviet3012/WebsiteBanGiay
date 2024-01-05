using Microsoft.AspNetCore.Mvc;
using BaoCaoTTCM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System.Text.Json;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BaoCaoTTCM.Controllers
{
    public class CartController : Controller
    {
       QlbanGiayContext _db = new QlbanGiayContext();


        public List<CartItem> Carts
        {
            get
            {
                var data = HttpContext.Session.Get<List<CartItem>>("GioHang");
                if (data == null)
                {
                    data = new List<CartItem>();
                }
                return data;
            }
        }


        public IActionResult Index()
		{
			return View(Carts);
		}

        public IActionResult AddToCart(int id, int numberproduct)
        {
            var myCart = Carts;
            var item = myCart.SingleOrDefault(p => p.Id == id);
            if (item == null)
            {
                //var kt = from b in _db.TSizes
                //         join a in _db.TSanPhams on b.MaSp equals a.MaSp
                //         join c in _db.TKichThuocs on b.MaSize equals c.MaSize
                //         where b.MaSp.Equals(id.ToString());

				var product = _db.TSanPhams.SingleOrDefault(p => p.MaSp == id.ToString());
                //var kt = _db.TSizes.SingleOrDefault(p => p.MaSp == id.ToString());
                item = new CartItem
                {
                    Id = id,
                    Name = product.TenSp,
                    Anh = product.Anh,
                    //Size = kt.MaSize,
                    Gia = (float)product.DonGiaBan,
                    SoLuong = numberproduct
                };
                myCart.Add(item);
            }
            else
            {
                item.SoLuong += numberproduct;

            }
            HttpContext.Session.Set("GioHang", myCart);
            return RedirectToAction("Index","Cart");
        }

		public IActionResult Update(int id,int number)
		{
			var myCart = Carts;
			var item = myCart.SingleOrDefault(p => p.Id == id);
			if (item != null)
			{
				item.SoLuong = number;
			}
			HttpContext.Session.Set("GioHang", myCart);
			return RedirectToAction("Index", "Cart");
		}


		[HttpPost]
    public IActionResult Remove(int id)
    {
			var myCart = Carts;
			var item = myCart.SingleOrDefault(p => p.Id == id);
			if (item != null)
            {
				myCart.Remove(item);
            }
			HttpContext.Session.Set("GioHang", myCart);
			return Json(new { success = true });

	}

        public IActionResult Checkout()
        {
            //Lay gio hang ra de xu ly
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
            var taikhoanID = HttpContext.Session.GetString("Makh");
            MuaHangVM model = new MuaHangVM();
            if (taikhoanID != null)
            {
                var khachhang = _db.TKhachHangs.AsNoTracking().SingleOrDefault(x => x.MaKh == taikhoanID);
                model.FullName = khachhang.TenKh;
                model.Phone = khachhang.DienThoai;
                model.Address = khachhang.DiaChi;
            }
            ViewBag.GioHang = cart;
            return View();
        }
    }
}
