using BaoCaoTTCM.Models;

namespace BaoCaoTTCM.Repository
{
    public class HeaderCartRepository 
    {
        private readonly QlbanGiayContext _context;
        public HeaderCartRepository(QlbanGiayContext context)
        {
            _context = context;
        }
        //public IEnumerable<CartItem> GetAll()
        //{
        //    return _context.Model;
        //}

        //public CartModel GetSp(int id)
        //{
        //    return _context.CartModels.Find(id);
        //}
    }
}
