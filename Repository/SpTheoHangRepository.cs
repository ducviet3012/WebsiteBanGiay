using BaoCaoTTCM.Models;
using Microsoft.EntityFrameworkCore;

namespace BaoCaoTTCM.Repository
{
	public class SpTheoHangRepository : ISpTheoHangRepository
	{
		private readonly QlbanGiayContext _context;
		public SpTheoHangRepository(QlbanGiayContext context)
		{
			_context = context;
		}


		public IEnumerable<THang> GetAllHang()
		{
			return _context.THangs;
		}

		public THang GetHang(string hang)
		{
			return _context.THangs.Find(hang);
		}
	}
}
