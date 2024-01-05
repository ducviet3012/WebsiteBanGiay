using BaoCaoTTCM.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace BaoCaoTTCM.ViewComponents
{
    public class HeaderCartViewComponent : ViewComponent
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HeaderCartViewComponent(IHttpContextAccessor httpContextAccessor)
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
