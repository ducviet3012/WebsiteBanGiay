using BaoCaoTTCM.Models;
using BaoCaoTTCM.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor();


builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromSeconds(1000);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

var connectionString = builder.Configuration.GetConnectionString("QlBanValiContext");
builder.Services.AddDbContext<QlbanGiayContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddScoped<ISpTheoHangRepository, SpTheoHangRepository>();

//builder.Services.AddDbContext<QlbanGiayContext>(x => x.UseSqlServer(connectionString));
//builder.Services.AddScoped<INumberCartRepository, NumberCartRepository>();
builder.Services.AddSession();
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
app.UseSession();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
