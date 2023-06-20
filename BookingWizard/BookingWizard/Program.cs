using BookingWizard.DAL.Entities;
using BookingWizard.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BookingWizard;
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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("HotelsDbConnection"), b => b.MigrationsAssembly("BookingWizard.DAL")));
builder.Services.AddScoped<IHotelRepository<Hotel>,HotelRepository>();
builder.Services.AddScoped<IHotelRoomRepository<hotelRoom>,hotelRoomsRepository>();
builder.Services.AddScoped<IUnitOfWork,UnifOfWork>();
builder.Services.AddScoped<IHotelService,HotelService>();
builder.Services.AddScoped<IHotelRoomService,HotelRoomService>();
builder.Services.AddScoped<IBookingService,BookingService>();
builder.Services.AddScoped<IBookingRepository,BookingRepository>();

builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});
builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = "https://localhost:7037";
                options.Audience = "BookingWizard";
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = false
                };
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
    // ����������� ���������
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Hotels}/{action=Hotels}/{id?}");
});




app.Run();
