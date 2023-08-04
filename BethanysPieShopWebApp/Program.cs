using BethanysPieShopWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BethanysPieShopDbContextConnection") ?? throw new InvalidOperationException("Connection string 'BethanysPieShopDbContextConnection' not found.");

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPieRepository, PieRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IShoppingCart, ShoppingCart>(ShoppingCart.GetCart);
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews().AddJsonOptions(options => 
{ options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });
builder.Services.AddRazorPages();
builder.Services.AddDbContext<BethanysPieShopDbContext>(options => { options.UseSqlServer(
    builder.Configuration["ConnectionStrings:BethanysPieShopDbContextConnection"]); });

builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<BethanysPieShopDbContext>();

// builder.Services.AddControllers();
//already added controllers with views, this is used when creating only an api with no need of views


builder.Services.AddServerSideBlazor();

var app = builder.Build();

app.UseStaticFiles();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapDefaultControllerRoute();
//Same as default:
//app.MapControllerRoute(name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
// app.MapControllers();
// no need of adding since we already have the MapDefaultControllerRoute;

app.MapBlazorHub();
app.MapFallbackToPage("/app/{*catchall}", "/app/Index");

DbInitializer.Seed(app);

app.Run();