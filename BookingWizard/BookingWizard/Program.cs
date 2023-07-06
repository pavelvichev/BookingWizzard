using BookingWizard.DAL.Entities;
using BookingWizard.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using BookingWizard.DAL.Interfaces;
using BookingWizard.DAL.Entities;
using BookingWizard.DAL.Repositories;
using BookingWizard.BLL.Interfaces;
using BookingWizard.BLL.Services;
using BookingWizard.DAL.Data;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using BookingWizard.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Globalization;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);
var loggerFactory = LoggerFactory.Create(builder =>
{
	builder.AddConsole(); // Пример конфигурации для записи в консоль
						  // Здесь вы можете добавить другие провайдеры логгирования, если необходимо
});


// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.ModelBinderProviders.Insert(0, new CustomDateTimeModelBinderProvider());
});


builder.Services.AddDbContext<BookingDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("HotelsDbConnection")));

builder.Services.AddScoped<IHotelRepository<Hotel>, HotelRepository>();
builder.Services.AddScoped<IHotelRoomRepository<hotelRoom>, hotelRoomsRepository>();
builder.Services.AddScoped<IUnitOfWork, UnifOfWork>();
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<IHotelRoomService, HotelRoomService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddSingleton(loggerFactory);

builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

builder.Services.AddAuthentication(config =>
{
    config.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, config =>
    {
        config.Authority = "https://localhost:7037";
        config.ClientId = "client_id_mvc";
        config.ClientSecret = "client_secret_mvc";
        config.SaveTokens = true;
        config.ResponseType = "code";

        config.Scope.Add("role.scope");
        config.Scope.Add("openid");
        config.Scope.Add("profile");
      
       
    });



builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.UseEndpoints(endpoints =>
{
    // определение маршрутов
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Hotels}/{action=Hotels}/{id?}/{searchString?}");
});




app.Run();
