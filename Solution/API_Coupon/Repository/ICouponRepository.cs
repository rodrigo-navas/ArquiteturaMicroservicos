using API_Coupon.Data.ValueObjects;
using System.Threading.Tasks;

namespace API_Coupon.Repository
{
    public interface ICouponRepository
    {
        Task<CouponVO> GetCouponByCouponCode(string couponCode);
    }
}
