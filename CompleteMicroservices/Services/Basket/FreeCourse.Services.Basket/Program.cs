using FreeCourse.Services.Basket.AppSettingsConfig;
using FreeCourse.Services.Basket.Services;
using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var requireAuthorizPlicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
// sub claimini mapleme yapmasýn diyoruz
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter(requireAuthorizPlicy)); // globalde authenticate olmuþ kullanýcý
    //
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<AppsettingsRedisInfo>(builder.Configuration.GetSection("RedisSettings"));
builder.Services.AddHttpContextAccessor(); // eklenmiþ olmalý

// configleri ayara çevirdik
builder.Services.AddSingleton<RedisService>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<AppsettingsRedisInfo>>().Value;
    var redis = new RedisService(settings.Host, settings.Port);
    redis.Connect();
    return redis;
});
builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.Authority = builder.Configuration["IdentityServerUrl"];
    opt.Audience = "resource_basket";
    opt.RequireHttpsMetadata = false;

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
