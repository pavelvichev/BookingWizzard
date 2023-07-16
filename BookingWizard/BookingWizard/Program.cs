using BookingWizard.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using BookingWizard.DAL.Repositories;
using BookingWizard.DAL.Data;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.Cookies;
using Knoema.Localization;
using Knoema.Localization.EFProvider;
using System.Web.Mvc;
using Knoema.Localization.Web;
using Microsoft.AspNetCore.Mvc.Razor;
using Knoema.Localization.Mvc;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.Extensions.Localization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using BookingWizard.Infrastructure;
using BookingWizard.DAL.Interfaces.IBookingRepo;
using BookingWizard.DAL.Interfaces.IHotelRepo;
using BookingWizard.DAL.Repositories.BookingRepo;
using BookingWizard.DAL.Repositories.HotelRepo;
using BookingWizard.DAL.Repositories.HotelRoomsRepo;
using BookingWizard.DAL.Interfaces.IHotelRoomsRepo;
using BookingWizard.DAL.Entities.Hotels;
using BookingWizard.DAL.Interfaces.IUsersRepo;
using BookingWizard.DAL.Repositories.UsersRepo;
using BookingWizard.BLL.Interfaces.IUsersServices;
using BookingWizard.BLL.Services.UsersServiceImpls;
using BookingWizard.BLL.Interfaces.IHotelsServices;
using BookingWizard.BLL.Services.HotelsServiceImpls;
using BookingWizard.BLL.Services.RoomsServiceImpls;
using BookingWizard.BLL.Services.BookingServiceImpls;
using BookingWizard.BLL.Interfaces.IBookingServices;
using BookingWizard.BLL.Interfaces.IHotelRoomsServices;
using BookingWizard.DAL.Entities.HotelRooms;

var builder = WebApplication.CreateBuilder(args);
var loggerFactory = LoggerFactory.Create(builder =>
{
	builder.AddConsole(); 
});



builder.Services.AddDbContext<BookingDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("HotelsDbConnection")));
builder.Services.AddDbContext<IdentityServerContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityString")));

builder.Services.AddScoped<IPhotoRoomsService, PhotoRoomsService>();
builder.Services.AddScoped<IPhotoRoomsRepository, PhotoRoomRepository>();
builder.Services.AddScoped<IPhotoHotelsRepository, PhotoHotelRepository>();
builder.Services.AddScoped<IPhotoHotelsService, PhotoHotelsService>();
builder.Services.AddScoped<IHotelRepository<Hotel>, HotelRepository>();
builder.Services.AddScoped<IHotelRoomsRepository, HotelRoomRepository>();
builder.Services.AddScoped<IUnitOfWork, UnifOfWork>();
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<IHotelRoomService, HotelRoomService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IReviewsRepository, ReviewRepository>();
builder.Services.AddScoped<IReviewsService, ReviewsService>();
builder.Services.AddScoped<IPrivilegesRepository, PrivilegesRepository>();




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

builder.Services.AddSession();

builder.Services.AddControllers();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddControllersWithViews(options =>
{
	options.ModelBinderProviders.Insert(0, new CustomDateTimeModelBinderProvider());
})
	.AddDataAnnotationsLocalization()
	.AddViewLocalization();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
                    new CultureInfo("en"),
                    new CultureInfo("uk"),
                   
                };

    options.DefaultRequestCulture = new RequestCulture("uk");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});



var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


var supportedCultures = new[]
           {
              new CultureInfo("en"),
              new CultureInfo("uk"),
           };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("uk"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});
app.UseSession();

app.UseRouting();

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();



app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "MyRoute",
    pattern: "Admin/MyHotels/{userId?}",
    defaults: new { controller = "Admin", action = "MyHotels" }
);
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Hotels}/{action=Hotels}/{id?}/{searchString?}");
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "BookingRoute",
    pattern: "Booking/BookingInfo/{bookingJson}",
    defaults: new { controller = "Booking", action = "BookingInfo" }
);
});

app.Run();
