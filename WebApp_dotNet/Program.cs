using Microsoft.EntityFrameworkCore;
using WebApp_dotNet.Core;
using WebApp_dotNet.Core.Interfaces;
using WebApp_dotNet.Persistence;
using Microsoft.AspNetCore.Identity;
using WebApp_dotNet.Areas.Identity.Data.WebApp_dotNetUser;
using WebApp_dotNet.Data;
using WebApp_dotNet.Filters;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IAuctionService, AuctionService>();

builder.Services.AddDbContext<AuctionDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("ProjectDbConnection")));

builder.Services.AddDbContext<AppIdentityDbContext>(options =>    
    options.UseMySQL(builder.Configuration.GetConnectionString("IdentityDbConnection")));
builder.Services.AddDefaultIdentity<AppIdentityUser>(options => 
    options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AppIdentityDbContext>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IAuctionPersistence, MySqlAuctionPersitence>(); 

builder.Services.AddScoped<AuctionEndedFilter>();


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

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();