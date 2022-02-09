using API_Cart.Data.ValueObjects;
using System.Threading.Tasks;

namespace API_Cart.Repository
{
    public interface ICouponRepository
    {
        Task<CouponVO> GetCoupon(string couponCode, string token);
    }
}
