using BookingWizard.IdentityServer.Configuration;
using BookingWizard.IdentityServer.Data;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ConnectionString");

builder.Services.AddDbContext<AppDbContext>(config =>
{
    config.UseSqlServer(connectionString);
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


var assembly = typeof(AppDbContext).Assembly.GetName().Name;

    
builder.Services.AddIdentityServer()
               .AddAspNetIdentity<IdentityUser>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                        sql => sql.MigrationsAssembly(assembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                        sql => sql.MigrationsAssembly(assembly));
                })               
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


    var rolesManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { "Admin", "Guest", "Owner" };

    foreach (var role in roles)
    {
        if (!await rolesManager.RoleExistsAsync(role))
            await rolesManager.CreateAsync(new IdentityRole(role));
    }

    var userManager = scope.ServiceProvider
        .GetRequiredService<UserManager<IdentityUser>>();

    string name = "pavel";
    string password ="password";
    if(await userManager.FindByNameAsync(name) == null)
    {
        var user = new IdentityUser
        {
            UserName = name,

        };
      await userManager.CreateAsync(user, password);

        await userManager.AddToRoleAsync(user, "Admin");
    }

    

                


    scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

    var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

    context.Database.Migrate();
    
    if (!context.Clients.Any())
    {
        foreach (var client in Configuration.GetClients())
        {
            context.Clients.Add(client.ToEntity());
        }
        context.SaveChanges();
    }

    if (!context.IdentityResources.Any())
    {
        foreach (var resource in Configuration.GetIdentityResources())
        {
            context.IdentityResources.Add(resource.ToEntity());
        }
        context.SaveChanges();
    }

    if (!context.ApiScopes.Any())
    {
        foreach (var resource in Configuration.GetApiScopes())
        {
            context.ApiScopes.Add(resource.ToEntity());
        }
        context.SaveChanges();
    }
}
app.Run();
