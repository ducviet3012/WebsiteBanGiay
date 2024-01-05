using BaoCaoTTCM.Models;
namespace BaoCaoTTCM.Repository
{
	public interface ISpTheoHangRepository
	{
		THang GetHang(string hang);
		IEnumerable<THang> GetAllHang();
	}
}
