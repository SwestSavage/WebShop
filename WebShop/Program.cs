using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WebShop.DbRepository;
using WebShop.DbRepository.Implementations;
using WebShop.DbRepository.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

// Add services to the container.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new PathString("/Account/Login");
    });

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IRepositoryContextFactory, RepositoryContextFactory>();

builder.Services.AddScoped<IUserRepository>(
    provider => new UserRepository(config.GetConnectionString("DefaultConnection"), 
    provider.GetService<IRepositoryContextFactory>())
    );

builder.Services.AddScoped<IProductsRepository>(
    provider => new ProductsRepository(config.GetConnectionString("DefaultConnection"),
    provider.GetService<IRepositoryContextFactory>())
    );

builder.Services.AddScoped<ICartRepository>(
    provider => new CartRepository(config.GetConnectionString("DefaultConnection"),
    provider.GetService<IRepositoryContextFactory>())
    );

builder.Services.AddScoped<IStorageRepository>(
    provider => new StorageRepository(config.GetConnectionString("DefaultConnection"),
    provider.GetService<IRepositoryContextFactory>())
    );

builder.Services.AddScoped<IProductInfoRepository>(
    provider => new ProductInfoRepository(config.GetConnectionString("DefaultConnection"),
    provider.GetService<IRepositoryContextFactory>())
    );

builder.Services.AddScoped<IOrderRepository>(
    provider => new OrderRepository(config.GetConnectionString("DefaultConnection"),
    provider.GetService<IRepositoryContextFactory>())
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var factory = services.GetRequiredService<IRepositoryContextFactory>();

    factory.CreateDbContext(config.GetConnectionString("DefaultConnection")).Database.Migrate();
}

app.Run();
