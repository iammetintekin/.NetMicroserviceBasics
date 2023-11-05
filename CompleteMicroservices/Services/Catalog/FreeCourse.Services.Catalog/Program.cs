using FreeCourse.Services.Catalog.Services;
using FreeCourse.Services.Catalog.Services.Abstract;
using FreeCourse.Services.Catalog.Services.Concrete;
using FreeCourse.Services.Catalog.Utilities.AppSettingsConfig;
using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter()); // tüm kontrollerlara authorize ekler.
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.Authority = builder.Configuration["IdentityServerUrl"];
    opt.Audience = "resource_catalog";
    opt.RequireHttpsMetadata =false;

});
// classýn baðlý oldðu tüm kullanýmlarý tanýmlar.
builder.Services.AddAutoMapper(typeof(Program));

// appsettings config
builder.Services.Configure<DatabaseConfig>(builder.Configuration.GetSection("Database"));

builder.Services.AddSingleton<IDatabaseConfig,DatabaseConfig>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseConfig>>().Value;
});
builder.Services.AddHttpContextAccessor(); // eklenmiþ olmalý
builder.Services.AddScoped<ICollectionManager, CollectionManager>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();

//-----------
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
