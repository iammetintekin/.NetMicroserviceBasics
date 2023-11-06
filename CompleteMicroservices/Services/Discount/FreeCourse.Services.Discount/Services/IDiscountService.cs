using FreeCourse.Services.Discount.Models;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.Discount.Services
{
    public interface IDiscountService
    {
        Task<ResponseObject<List<Models.Discount>>> GetAll();
        Task<ResponseObject<Models.Discount>> GetById(int Id);
        Task Save(Models.Discount Discount);
        Task Update(Models.Discount Discount);
        Task DeleteById(int Id);
        Task<ResponseObject<Models.Discount>> GetByCodeAndUserId(string Code,string UserID);

    }
}
