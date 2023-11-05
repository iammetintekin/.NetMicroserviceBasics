using FreeCourse.Services.Basket.Dtos;
using FreeCourse.Shared.Dtos;
using System.Text.Json;

namespace FreeCourse.Services.Basket.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;
        public BasketService(RedisService redisService)
        {
            _redisService = redisService;            
        }
        public async Task<ResponseObject<bool>> Delete(string UserId)
        {
            var IsExist = await _redisService.GetDatabase().KeyDeleteAsync(UserId);
            return IsExist ?
              new ResponseObject<bool>(IsExist, StatusCodes.Status200OK, true) :
              new ResponseObject<bool>(IsExist, StatusCodes.Status404NotFound, false, new List<string> { "Basket Couldn't found for delete" });
        }

        public async Task<ResponseObject<BasketDto>> Get(string UserID)
        {
            var IsExist = await _redisService.GetDatabase().StringGetAsync(UserID);
            if (string.IsNullOrEmpty(IsExist))
            {
                return new ResponseObject<BasketDto>(null, StatusCodes.Status404NotFound, false,new List<string> { "Basket Not Found"});
            }
            return new ResponseObject<BasketDto>(JsonSerializer.Deserialize<BasketDto>(IsExist), StatusCodes.Status200OK, true); 
        }

        public async Task<ResponseObject<bool>> SaveOrUpdate(BasketDto basketDto)
        {
            var status = await _redisService.GetDatabase().StringSetAsync(basketDto.UserId, JsonSerializer.Serialize(basketDto));
            if(!status)
                return new ResponseObject<bool>(status,StatusCodes.Status404NotFound,false);
            return status ? 
                new ResponseObject<bool>(status, StatusCodes.Status200OK, true):
                new ResponseObject<bool>(status, StatusCodes.Status500InternalServerError, false,new List<string> { "Basket Couldn't found"});
        }
    }
}
