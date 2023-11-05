using FreeCourse.Services.Basket.Dtos;
using FreeCourse.Services.Basket.Services;
using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.Basket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : CustomBaseController
    {
        private readonly IBasketService _basket;
        private readonly ISharedIdentityService _sharedIdentity;

        public BasketController(IBasketService basket, ISharedIdentityService sharedIdentity)
        {
            _basket = basket;
            _sharedIdentity = sharedIdentity;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            var claims = User.Claims;
            // artýk sub içinde Id taþýnýyor
            //{sub: 55c212e3-a3ab-4852-b250-88d50478cca8}
            return CreateActionResultInstance<BasketDto>(await _basket.Get(_sharedIdentity.GetUserId));
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrUpdate(BasketDto basketDto)
        {
            var response = await _basket.SaveOrUpdate(basketDto);
            return CreateActionResultInstance<bool>(response); 
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            var response = await _basket.Delete(_sharedIdentity.GetUserId);
            return CreateActionResultInstance<bool>(response);
        }
    }
}