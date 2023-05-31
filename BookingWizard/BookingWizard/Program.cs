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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("HotelsDbConnection"), b => b.MigrationsAssembly("BookingWizard")));
builder.Services.AddScoped<IHotelRepository<Hotel>,HotelRepository>();
builder.Services.AddScoped<IHotelRoomRepository<hotelRoom>,hotelRoomsRepository>();
builder.Services.AddScoped<IUnitOfWork,UnifOfWork>();
builder.Services.AddScoped<IHotelService,HotelService>();
builder.Services.AddScoped<IHotelRoomService,HotelRoomService>();

builder.Services.AddAutoMapper(typeof(MapProfile));


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Hotels}/{action=Hotels}/{id?}/{searchString?}");



app.Run();
