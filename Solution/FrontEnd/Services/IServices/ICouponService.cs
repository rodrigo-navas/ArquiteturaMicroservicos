using FrontEnd.Models;
using System.Threading.Tasks;

namespace FrontEnd.Services.IServices
{
    public interface ICouponService
    {
        Task<CouponViewModel> GetCoupon(string code, string token);
    }
}
