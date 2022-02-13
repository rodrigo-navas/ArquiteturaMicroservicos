using API_Cart.Data.ValueObjects;
using API_Cart.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API_Cart.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartController : ControllerBase
    {
        private ICartRepository _cartRepository;
        //private ICouponRepository _couponRepository;

        public CartController(ICartRepository cartRepository,
            ICouponRepository couponRepository)
        {
            _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
            //_couponRepository = couponRepository ?? throw new ArgumentNullException(nameof(couponRepository));
        }

        [HttpGet("find-cart/{id}")]
        public async Task<ActionResult<CartVO>> FindById(string id)
        {
            var cart = await _cartRepository.FindCartByUserId(id);
            if (cart == null) return NotFound();
            return Ok(cart);
        }

        [HttpPost("add-cart")]
        public async Task<ActionResult<CartVO>> AddCart(CartVO vo)
        {
            var cart = await _cartRepository.SaveOrUpdateCart(vo);
            if (cart == null) return NotFound();
            return Ok(cart);
        }

        [HttpPut("update-cart")]
        public async Task<ActionResult<CartVO>> UpdateCart(CartVO vo)
        {
            var cart = await _cartRepository.SaveOrUpdateCart(vo);
            if (cart == null) return NotFound();
            return Ok(cart);
        }

        [HttpDelete("remove-cart/{id}")]
        public async Task<ActionResult<CartVO>> RemoveCart(int id)
        {
            var status = await _cartRepository.RemoveFromCart(id);
            if (!status) return BadRequest();
            return Ok(status);
        }

        //[HttpPost("apply-coupon")]
        //public async Task<ActionResult<CartVO>> ApplyCoupon(CartVO vo)
        //{
        //    var status = await _cartRepository.ApplyCoupon(vo.CartHeader.UserId, vo.CartHeader.CouponCode);
        //    if (!status) return NotFound();
        //    return Ok(status);
        //}

        //[HttpDelete("remove-coupon/{userId}")]
        //public async Task<ActionResult<CartVO>> ApplyCoupon(string userId)
        //{
        //    var status = await _cartRepository.RemoveCoupon(userId);
        //    if (!status) return NotFound();
        //    return Ok(status);
        //}
    }
}
