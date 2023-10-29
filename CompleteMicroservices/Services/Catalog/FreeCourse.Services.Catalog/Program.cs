using FreeCourse.Services.Catalog.Services;
using FreeCourse.Services.Catalog.Services.Abstract;
using FreeCourse.Services.Catalog.Services.Concrete;
using FreeCourse.Services.Catalog.Utilities.AppSettingsConfig;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// classýn baðlý oldðu tüm kullanýmlarý tanýmlar.
builder.Services.AddAutoMapper(typeof(Program));

// appsettings config
builder.Services.Configure<DatabaseConfig>(builder.Configuration.GetSection("Database"));

builder.Services.AddSingleton<IDatabaseConfig>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseConfig>>().Value;
});

builder.Services.AddScoped<ICollectionManager, CollectionManager>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICourseService, CourseService>();

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

app.MapControllers();

app.Run();
