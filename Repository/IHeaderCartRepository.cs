using BaoCaoTTCM.Models;

namespace BaoCaoTTCM.Repository
{
    public interface IHeaderCartRepository
    {
        CartItem GetSp(int id);
        IEnumerable<CartItem> GetAll();
    }
}
