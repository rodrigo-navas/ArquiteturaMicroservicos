using API_Coupon.Data.ValueObjects;
using API_Coupon.Model;
using AutoMapper;

namespace API_Coupon.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config => {
                config.CreateMap<CouponVO, Coupon>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
