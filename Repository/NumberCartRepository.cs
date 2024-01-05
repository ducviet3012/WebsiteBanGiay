using BaoCaoTTCM.Models;

namespace BaoCaoTTCM.Repository
{
    public class NumberCartRepository 
    {
        private readonly QlbanGiayContext _context;
        public NumberCartRepository(QlbanGiayContext context)
        {
            _context = context;
        }

        //public IEnumerable<CartItem> GetAll()
        //{
        //    return _context.;
        //}

        //public CartModel GetSl(int sl)
        //{
        //    return _context.CartModels.Find(sl);
        //}
    }
}
