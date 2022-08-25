using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UniversityManagementWebApp.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("UniversityDbContextConnection") ?? throw new InvalidOperationException("Connection string 'UniversityDbContextConnection' not found.");

builder.Services.AddDbContext<UniversityDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<UniversityAppUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddDefaultUI().AddEntityFrameworkStores<UniversityDbContext>();


builder.Services.AddAuthorization(o => {
    o.AddPolicy("AdminApprovalPage", builder => builder.RequireRole("Admin"));
    o.AddPolicy("OperatorPage", builder => builder.RequireRole("Operator"));

});

// Add services to the container.
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
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
