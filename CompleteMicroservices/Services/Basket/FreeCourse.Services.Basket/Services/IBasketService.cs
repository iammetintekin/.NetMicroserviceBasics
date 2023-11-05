using FreeCourse.Services.Basket.Dtos;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.Basket.Services
{
    public interface IBasketService
    {
        Task<ResponseObject<BasketDto>> Get(string UserID);
        Task<ResponseObject<bool>> SaveOrUpdate(BasketDto basketDto);
        Task<ResponseObject<bool>> Delete(string UserId);
    }
}
