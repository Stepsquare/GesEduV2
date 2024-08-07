using GesEdu.DataLayer.Extensions;
using GesEdu.ServiceLayer.Services;
using GesEdu.ServiceLayer.Extensions;
using GesEdu.Shared.ExceptionHandler;
using Microsoft.AspNetCore.Authentication.Cookies;
using SmartBreadcrumbs.Extensions;
using System.Reflection;
using Serilog;
using Serilog.Events;
using GesEdu.Shared.Resources;
using GesEdu.Shared.Extensions;

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

#region Authentication, Policies, Session

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

builder.Services
    .AddAuthorization(options =>
    {
        options.AddPolicy("AdminOnlyAccess", policy
            => policy.RequireRole(GesEduProfiles.ADMIN));
        options.AddPolicy("ChooseUoPageAccess", policy
            => policy.RequireRole(GesEduProfiles.ADMIN, GesEduProfiles.SIME_DGE));
        options.AddPolicy("MegaAccess", policy
            => policy.RequireRole(GesEduProfiles.ADMIN, GesEduProfiles.MEGA));
        options.AddPolicy("AreaReservadaAccess", policy
            => policy.RequireRole(GesEduProfiles.ADMIN, GesEduProfiles.AREA_RESERVADA));
        options.AddPolicy("AreaReservadaReactAccess", policy
            => policy.RequireAssertion(context => context.User.IsAdmin() || context.User.IsAreaReservadaReactUser()));
        options.AddPolicy("SimeAccess", policy
            => policy.RequireRole(GesEduProfiles.ADMIN, GesEduProfiles.SIME_DGE, GesEduProfiles.SIME_ESC));
        options.AddPolicy("SimeDgeOnlyAccess", policy
            => policy.RequireRole(GesEduProfiles.ADMIN, GesEduProfiles.SIME_DGE));
        options.AddPolicy("SimeEscOnlyAccess", policy
            => policy.RequireRole(GesEduProfiles.ADMIN, GesEduProfiles.SIME_ESC));
        options.AddPolicy("UserManagementAccess", policy
            => policy.RequireAssertion(context => context.User.IsAdmin() || context.User.IsUserManager()));
    });

builder.Services.AddSession();

#endregion

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
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
