using FluentValidation;
using FluentValidation.AspNetCore;
using Library.App.Middlewares;
using Library.App.Models.Configs;
using Library.App.Models.ViewModels.Identity.Validators;
using Library.App.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Library.App;

public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();

        services.Configure<ApisConfig>(Configuration.GetSection("ApisConfig"));

        services.AddHttpClient<IdentityService>();
        services.AddHttpClient<AuthorService>();
        services.AddHttpClient<BookService>();
        services.AddHttpClient<AccountService>();

        services.AddScoped<IdentityService>();
        services.AddScoped<AuthorService>();
        services.AddScoped<BookService>();
        services.AddScoped<AccountService>();

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<RegisterViewModelValidator>();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSession();
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Account/Login";
            });

        services.AddDistributedMemoryCache();
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });
    }

    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        if (!env.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseSession();
        app.UseMiddleware<TokenMiddleware>();
        app.UseMiddleware<JwtMiddleware>();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}