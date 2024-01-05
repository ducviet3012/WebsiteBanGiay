using BaoCaoTTCM.Models;

namespace BaoCaoTTCM.Repository
{
    public interface INumberCartRepository
    {
        CartItem GetSl(int sl);
        IEnumerable<CartItem> GetAll();
    }
}
