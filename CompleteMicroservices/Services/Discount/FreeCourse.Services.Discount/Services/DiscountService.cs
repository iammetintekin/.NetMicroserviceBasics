using Dapper;
using FreeCourse.Shared.Dtos;
using Npgsql;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FreeCourse.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostreSql"));
        }

        public async Task DeleteById(int Id)
        {
            if ((await GetById(Id)).IsSuccessful)
            {
                var status = await _dbConnection.ExecuteAsync("delete from discount where id = @Id", new { Id = Id });
            } 
        }

        public async Task<ResponseObject<List<Models.Discount>>> GetAll()
        {
            // dapper ek fonkiyonlar yazmış.
            var data = await _dbConnection.QueryAsync<Models.Discount>("select * from discount");
            return new ResponseObject<List<Models.Discount>>(data.ToList(), StatusCodes.Status200OK, true);
        }

        public async Task<ResponseObject<Models.Discount>> GetByCodeAndUserId(string Code, string UserID)
        {
            var discount = (await _dbConnection
                .QueryAsync<Models.Discount>
                ("select * from discount where code = @Code and userid = @UserId", 
                new 
                { 
                    Code = Code, 
                    UserId=UserID 
                })).SingleOrDefault();

            return discount is null ? 
                new ResponseObject<Models.Discount>(null, StatusCodes.Status404NotFound, false, new List<string> { "Discount bulunamadı." }): 
                new ResponseObject<Models.Discount>(discount, StatusCodes.Status200OK, true);
        }

        public async Task<ResponseObject<Models.Discount>> GetById(int Id)
        {
            var discount = (await _dbConnection.QueryAsync<Models.Discount>("select * from discount where id = @id", new { id= Id })).SingleOrDefault();
            if(discount is null)
                return new ResponseObject<Models.Discount>(null, StatusCodes.Status404NotFound, false, new List<string> { "Discount bulunamadı."});
            return new ResponseObject<Models.Discount>(discount, StatusCodes.Status200OK, true); 
        }

        public async Task Save(Models.Discount Discount)
        {
            var SaveStatus = await _dbConnection.ExecuteAsync("insert into discount(userid,rate,code) values (@UserId,@Rate,@Code)",Discount); // discountdan mapledi.
        }

        public async Task Update(Models.Discount Discount)
        {
            if((await GetById(Discount.Id)).IsSuccessful)
            {
                var SaveStatus = await _dbConnection
                        .ExecuteAsync("update discount set userid=@UserId, code=@Code, rate=@Rate where id = @Id", new
                        {
                            Id=Discount.Id,
                            UserId = Discount.UserId,
                            Code = Discount.Code,
                            Rate = Discount.Rate
                        }); // discountdan mapledi. 
            }
           
        }
    }
}
