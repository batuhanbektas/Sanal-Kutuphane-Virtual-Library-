




using KutuphaneSatis.Data;
using KutuphaneSatis.Models.Concrete;
using KutuphaneSatis.Repositories.Abstract;
using KutuphaneSatis.Repositories.Concrete;
using KutuphaneSatis.Services.Abstract;
using KutuphaneSatis.Services.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Program.cs içerisindeki builder.Services kısımlarına ekle:


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repository'lerin sisteme tanıtılması (Eksik olan bu!)
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IGenericRepository<CartDetail>, GenericRepository<CartDetail>>();


// Servislerin sisteme tanıtılması
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IUserLRService, UserLRService>();
builder.Services.AddScoped<ICartService, CartService>();


// HttpContextAccessor'ı sisteme dahil ediyoruz
builder.Services.AddHttpContextAccessor();

// Cookie ayarlarını sisteme dahil ediyoruz
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "Kutuphane.Auth"; // Tarayıcıda görünecek çerez adı
        options.LoginPath = "/Home/Login"; // Giriş yapılmamışsa yönlendirilecek sayfa
        options.AccessDeniedPath = "/Home/AccessDenied"; // Yetkisiz erişimde gidilecek sayfa
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
app.UseRouting();

app.UseAuthentication(); // Kimlik doğrulama (Önce bu olmalı)
app.UseAuthorization();  // Yetkilendirme


app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
