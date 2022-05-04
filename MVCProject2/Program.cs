using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MVCProject2;
using MVCProject2.Data;
using MVCProject2.Data.Interfaces;
using MVCProject2.Data.Mocks;
using MVCProject2.Data.Models;
using MVCProject2.Data.Repository;
using MVCProject2.Services;
using System.Configuration;
using System.Data.SqlTypes;
using System.Net;
using System.Security.Claims;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureNonBreakingSameSiteCookies();
builder.Services.AddDbContext<AppDBContent>(options => options.UseSqlServer("name=connectionStrings:DBConnection"));
builder.Services.AddTransient<IAllProducts, ProductRepository>();
builder.Services.AddTransient<IProductsCategory, CategoryRepository>();
builder.Services.AddTransient<IAllOrders, OrdersRepository>();
builder.Services.AddTransient<IUsers, UserRepository>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sp => ShopCart.GetCart(sp));
builder.Services.AddScoped<UserService>();
builder.Services.AddMvc();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = "/login";
    options.AccessDeniedPath = "/denied";
    options.Events = new CookieAuthenticationEvents()
    {
        OnSigningIn = async context =>
        {
            var scheme = context.Properties.Items.Where(k => k.Key == ".AuthScheme").FirstOrDefault();
            var claim = new Claim(scheme.Key, scheme.Value);
            var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
            var userService = context.HttpContext.RequestServices.GetRequiredService(typeof(UserService)) as UserService;
            var nameIdentifier = claimsIdentity.Claims.FirstOrDefault(m => m.Type == ClaimTypes.NameIdentifier)?.Value;
            if(userService != null && nameIdentifier != null)
            {
                try
                {
                var appUser = userService.GetUserByExternalProvider(scheme.Value, nameIdentifier);
                    if (appUser is null)
                    {
                        appUser = userService.AddNewUser(scheme.Value, claimsIdentity.Claims.ToList());
                    }
                    foreach (var r in appUser.RoleList)
                    {
                        claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, r));
                    }
                }
                catch (SqlNullValueException)
                {

                }
                
            }
            claimsIdentity.AddClaim(claim);
            await Task.CompletedTask;
        }
    };
}).AddOpenIdConnect("google", options =>
{
    options.Authority = "https://accounts.google.com";
    options.ClientId = "";
    options.ClientSecret = "";
    options.CallbackPath = "/auth";
    //options.SignedOutCallbackPath = "/google-signout";
    options.SaveTokens = true;
}).AddOpenIdConnect("okta", options =>
{
    options.Authority = "https://dev-94019367.okta.com/oauth2/default";
    options.ClientId = "";
    options.ClientSecret = "";
    options.CallbackPath = "/okta-auth";
    options.SignedOutCallbackPath = "/okta-signout";
    options.ResponseType = "code";
    options.SaveTokens = true;
});

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
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "categoryFilter",
    pattern: "Product/{action}/{category?}", defaults: new { Controller = "Product", action = "List" });
app.MapControllerRoute(
    name: "UserAuthentication",
    pattern: "Login/View/");
using (var scope = app.Services.CreateScope())
{
    AppDBContent content = scope.ServiceProvider.GetRequiredService<AppDBContent>();
    DBObjects.Initial(content);
}
app.Run();
