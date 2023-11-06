using FreeCourse.Services.Discount.Services;
using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NpgsqlTypes;

namespace FreeCourse.Services.Discount.Controllers
{
    [Route("api/[controller]/[action]")] 
    [ApiController]
    public class DiscountController : CustomBaseController
    {
        private readonly ISharedIdentityService _sharedIdentity;
        private readonly IDiscountService _discountService;

        public DiscountController(ISharedIdentityService sharedIdentity, IDiscountService discountService)
        {
            _sharedIdentity = sharedIdentity;
            _discountService = discountService;
        }

        // userId jwt den alıyoz

        [HttpGet(Name = "GetAll")] 
        public async Task<IActionResult> GetAll()
        {
            var result = await _discountService.GetAll();
            return CreateActionResultInstance<List<Models.Discount>>(result);
        }

        [HttpGet("{id}",Name = "GetById")] 
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _discountService.GetById(id);
            return CreateActionResultInstance<Models.Discount>(result);
        }

        [HttpGet("{Code}", Name = "GetByCode")] 
        public async Task<IActionResult> GetByCode(string Code)
        {
            var UserId = _sharedIdentity.GetUserId;
            var result = await _discountService.GetByCodeAndUserId(Code, UserId);
            return CreateActionResultInstance<Models.Discount>(result);
        }


        [HttpPost(Name = "Save")] 
        public async Task<IActionResult> Save(Models.Discount Discount)
        {
            await _discountService.Save(Discount);
            return CreateActionResultInstance<Models.Discount>(new Shared.Dtos.ResponseObject<Models.Discount>(Discount,204,true));
        }

        [HttpPut(Name = "Update")] 
        public async Task<IActionResult> Update(Models.Discount Discount)
        {
            await _discountService.Update(Discount);
            return CreateActionResultInstance<Models.Discount>(new Shared.Dtos.ResponseObject<Models.Discount>(Discount, 204, true));
        }

        [HttpDelete("{id}", Name = "DeleteById")] 
        public async Task<IActionResult> DeleteById(int id)
        {
            await _discountService.DeleteById(id);
            return CreateActionResultInstance<Models.Discount>(new Shared.Dtos.ResponseObject<Models.Discount>(null, 204, true));

        }
    }
}
