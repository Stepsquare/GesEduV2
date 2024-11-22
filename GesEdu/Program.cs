using GesEdu.DataLayer.Extensions;
using GesEdu.ServiceLayer.Extensions;
using GesEdu.Shared.ExceptionHandler;
using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;
using GesEdu.Shared.Resources;
using GesEdu.Shared.Extensions;
using GesEdu.Helpers.ExportPdf.Extensions;
using GesEdu.Helpers.Breadcrumbs;

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

//Pdf Export Dependency Injection
builder.Services.AddPdfExportDependencies();

//Breadcrumb service
builder.Services.AddBreadcrumbService();

//Serilog Logging
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

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
    .AddAuthorizationBuilder()
    .AddPolicy("AdminOnlyAccess", policy
            => policy.RequireRole(GesEduProfiles.ADMIN))
    .AddPolicy("ChooseUoPageAccess", policy
            => policy.RequireRole(GesEduProfiles.ADMIN, GesEduProfiles.SIME_DGE))
    .AddPolicy("MegaAccess", policy
            => policy.RequireRole(GesEduProfiles.ADMIN, GesEduProfiles.MEGA))
    .AddPolicy("AreaReservadaAccess", policy
            => policy.RequireRole(GesEduProfiles.ADMIN, GesEduProfiles.AREA_RESERVADA))
    .AddPolicy("AreaReservadaReactAccess", policy
            => policy.RequireAssertion(context => context.User.IsAdmin() || context.User.IsAreaReservadaReactUser()))
    .AddPolicy("SimeAccess", policy
            => policy.RequireRole(GesEduProfiles.ADMIN, GesEduProfiles.SIME_DGE, GesEduProfiles.SIME_ESC))
    .AddPolicy("SimeDgeOnlyAccess", policy
            => policy.RequireRole(GesEduProfiles.ADMIN, GesEduProfiles.SIME_DGE))
    .AddPolicy("SimeEscOnlyAccess", policy
            => policy.RequireRole(GesEduProfiles.ADMIN, GesEduProfiles.SIME_ESC))
    .AddPolicy("UserManagementAccess", policy
            => policy.RequireAssertion(context => context.User.IsAdmin() || context.User.IsUserManager()));

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
