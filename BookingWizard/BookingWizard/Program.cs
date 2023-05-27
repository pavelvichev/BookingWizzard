using BookingWizard.Core.Entities;
using BookingWizard.Core.Interfaces;
using BookingWizard.Infrastrucure.Data;
using BookingWizard.Infrastrucure.Repositories;
using BookingWizard.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BookingWizard;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("HotelsDbConnection"), b => b.MigrationsAssembly("BookingWizard")));
builder.Services.AddScoped<IHotelRepository<Hotel>,HotelRepository>();
builder.Services.AddScoped<IHotelRoomRepository<hotelRoom>,hotelRoomsRepository>();

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
    pattern: "{controller=Hotels}/{action=Hotels}/{id?}");



app.Run();
