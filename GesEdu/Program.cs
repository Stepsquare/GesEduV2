using GesEdu.Datalayer.Context;
using GesEdu.Datalayer.UnitOfWork;
using GesEdu.ServiceLayer.Helpers;
using GesEdu.ServiceLayer.Services;
using GesEdu.Shared.Interfaces.IConfiguration;
using GesEdu.Shared.Interfaces.IHelpers;
using GesEdu.Shared.Interfaces.ISevices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SmartBreadcrumbs.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

#region DB Connection
var connectionString = builder.Configuration.GetConnectionString("SqlServerConnection");
builder.Services.AddDbContext<GesEduDbContext>(x => x.UseSqlServer(connectionString));
#endregion

builder.Services.AddHttpContextAccessor();

#region Dependency Injection

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IGenericRestRequests, GenericRestRequests>();
builder.Services.AddScoped<ILoginServices, LoginServices>();
builder.Services.AddScoped<INoticiasServices, NoticiasServices>();
builder.Services.AddScoped<IUserServices, UserServices>();

#endregion

#region SmartBreadcrums

builder.Services.AddBreadcrumbs(Assembly.GetExecutingAssembly(), options =>
{
    options.TagName = "nav";
    options.TagClasses = "";
    options.OlClasses = "breadcrumb";
    options.LiClasses = "breadcrumb-item";
    options.ActiveLiClasses = "breadcrumb-item active";
});

#endregion

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "GesEduCookie";
                    options.LoginPath = "/Login";
                    options.ExpireTimeSpan = new TimeSpan(0, 45, 0);
                    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                    options.LogoutPath = "/Logout";
                });

builder.Services.AddSession();

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

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

app.UseAuthentication();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
