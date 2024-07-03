using GesEdu.DataLayer.Extensions;
using GesEdu.ServiceLayer.Services;
using GesEdu.ServiceLayer.Extensions;
using GesEdu.Shared.ExceptionHandler;
using Microsoft.AspNetCore.Authentication.Cookies;
using SmartBreadcrumbs.Extensions;
using System.Reflection;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient("sigefeClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["SigefeWebservices:BaseUrl"] ?? throw new ArgumentNullException());
    client.DefaultRequestHeaders.Add("username", builder.Configuration["SigefeWebservices:Username"]);
    client.DefaultRequestHeaders.Add("password", builder.Configuration["SigefeWebservices:Password"]);
});

//Data Layer Dependency Injection
builder.Services.AddDataLayerDependencies(builder.Configuration);

//Service Layer Dependency Injection
builder.Services.AddServiceLayerDependencies();

//Serilog Logging
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

#region SmartBreadcrums

builder.Services.AddBreadcrumbs(Assembly.GetExecutingAssembly(), options =>
{
    options.TagName = "div";
    options.TagClasses = "d-flex";
    options.OlClasses = "breadcrumb";
    options.LiClasses = "breadcrumb-item";
    options.ActiveLiClasses = "breadcrumb-item active";
});

#endregion

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "gesedu_token";
        options.LoginPath = "/Login";
        options.LogoutPath = "/Logout";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    });

builder.Services.AddSession();

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandler>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
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
