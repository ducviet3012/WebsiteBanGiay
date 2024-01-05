using BaoCaoTTCM.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace BaoCaoTTCM.ViewComponents
{
    public class NumberCartViewComponent : ViewComponent
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NumberCartViewComponent(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
            return View(cart);
        }
    }
}
