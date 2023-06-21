using BookingWizard.IdentityServer.Configuration;
using BookingWizard.IdentityServer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<AppDbContext>(config =>
{
    config.UseInMemoryDatabase("Memory");
});

// AddIdentity registers the services
builder.Services.AddIdentity<IdentityUser, IdentityRole>(config =>
{
    config.Password.RequiredLength = 4;
    config.Password.RequireDigit = false;
    config.Password.RequireNonAlphanumeric = false;
    config.Password.RequireUppercase = false;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(config =>
{
    config.Cookie.Name = "IdentityServer.Cookie";
    config.LoginPath = "/Auth/Login";
    config.LogoutPath = "/Auth/Logout";
});

// Add services to the container.
builder.Services.AddIdentityServer()
               .AddAspNetIdentity<IdentityUser>()
               .AddInMemoryIdentityResources(Configuration.GetIdentityResources())
               .AddInMemoryApiScopes(Configuration.GetApiScopes())
               .AddInMemoryApiResources(Configuration.GetApiResources())
               .AddInMemoryClients(Configuration.GetClients())
               .AddDeveloperSigningCredential();

builder.Services.AddControllersWithViews();

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

app.UseIdentityServer();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
endpoints.MapDefaultControllerRoute());

using(var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider
        .GetRequiredService<UserManager<IdentityUser>>();

    var user = new IdentityUser("Pavel");
    userManager.CreateAsync(user,"password").GetAwaiter().GetResult();
}
app.Run();
