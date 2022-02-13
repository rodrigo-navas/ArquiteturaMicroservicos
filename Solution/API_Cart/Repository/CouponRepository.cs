using API_Cart.Data.ValueObjects;
using System.Threading.Tasks;

namespace API_Cart.Repository
{
    public class CouponRepository : ICouponRepository
    {
        public CouponRepository()
        {
        }

        public async Task<CouponVO> GetCoupon(string couponCode, string token)
        {
            return null;
        }
    }
}
