
using BaoCaoTTCM.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BaoCaoTTCM.ViewComponents
{
	public class HangSpMenuViewComponent:ViewComponent
	{
		private readonly ISpTheoHangRepository _hangsp;
		public HangSpMenuViewComponent(ISpTheoHangRepository hangsp)
		{
			_hangsp = hangsp;
		}
		public IViewComponentResult Invoke()
		{
			var loaisp = _hangsp.GetAllHang().OrderBy(x => x.MaHang).ToList();
			return View(loaisp);
		}
	}
}
