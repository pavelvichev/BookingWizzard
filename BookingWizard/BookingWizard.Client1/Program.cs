using BookingWizard.Client.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
					.AddJwtBearer(options =>
					{
						options.RequireHttpsMetadata = true;
						options.TokenValidationParameters = new TokenValidationParameters
						{
							ValidateIssuer = true,

							ValidIssuer = AuthOptions.ISSUER,

							ValidateAudience = true,

							ValidAudience = AuthOptions.AUDIENCE,

							ValidateLifetime = true,

							IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),

							ValidateIssuerSigningKey = true,
						};
					});

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

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

app.UseEndpoints(endpoints =>
    endpoints.MapDefaultControllerRoute());

app.Run();