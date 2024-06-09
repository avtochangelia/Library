using Application.Shared.Behaviors;
using Application.Shared.Configurations;
using Application.Shared.Configurations.Validators;
using Application.Shared.Exceptions;
using Application.Shared.Helpers;
using Application.Shared.Infrastructure;
using Application.Shared.Services.Abstract;
using Application.Shared.Services.Concrete;
using Application.UserManagement.Commands.RegisterUser;
using Domain.AuthorManagement.Repositories;
using Domain.BookManagement.Repositories;
using Domain.Shared;
using Domain.UserManagement;
using Domain.UserManagement.Repositories;
using FluentValidation;
using Infrastructure.DataAccess;
using Infrastructure.Repositories.AuthorManagement;
using Infrastructure.Repositories.BookManagement;
using Infrastructure.Repositories.UserManagement;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Serilog;
using Serilog.Events;
using Shared.Constants;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

namespace DI;

public class DependencyResolver(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    public string DbConnection { get; } = configuration.GetConnectionString("Default");

    public IServiceCollection Resolve(IServiceCollection services)
    {
        services ??= new ServiceCollection();

        var appsettings = new AppSettings();
        Configuration.Bind(appsettings);
        Configuration.BindJwtConfigValidAudiences(appsettings);

        ValidateConfiguration(appsettings);

        services.AddCors(options =>
        {
            options.AddPolicy(
                "AllowAnyOrigin",
                builder => builder.AllowAnyOrigin()
                                  .AllowAnyMethod()
                                  .AllowAnyHeader());
        });

        services.AddSingleton(appsettings);

        services.AddDbContext<EFDbContext>(opt =>
            opt.UseNpgsql(DbConnection));

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.PostgreSQL(
                connectionString: DbConnection,
                tableName: "Logs",
                schemaName: "public",
                needAutoCreateTable: true)
            .CreateLogger();

        services.AddLogging(loggingBuilder =>
            loggingBuilder.ClearProviders().AddSerilog(Log.Logger, dispose: true));

        services.AddSingleton<ITokenService, TokenService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddMediatR(new[]
        {
            typeof(RegisterUser).GetTypeInfo().Assembly,
        });

        services.AddValidatorsFromAssembly(typeof(RegisterUserHandler).GetTypeInfo().Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

        ConfigureIdentity(services);
        ConfgiureJwt(services, appsettings);

        services.AddScoped(p =>
        {
            var applicationContext = new ApplicationContext();
            var accessor = p.GetService<IHttpContextAccessor>();

            if (accessor != null && accessor.HttpContext != null)
            {
                var authHeader = accessor.HttpContext.Request.Headers[HeaderNames.Authorization].ToString();

                if (!string.IsNullOrEmpty(authHeader))
                {
                    var token = authHeader.Replace("Bearer ", string.Empty, StringComparison.OrdinalIgnoreCase);

                    if (!string.IsNullOrEmpty(token))
                    {
                        var jwtSecurityToken = new JwtSecurityToken(jwtEncodedString: token);

                        if (jwtSecurityToken != null)
                        {
                            var roleName = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == UserClaims.Role)?.Value;
                            var username = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == UserClaims.Username)?.Value;
                            var userId = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == UserClaims.UserId)?.Value;

                            applicationContext.Username = username;
                            applicationContext.UserId = userId;
                        }
                    }
                }
            }

            return applicationContext;
        });

        return services;
    }

    public static void ConfigureIdentity(IServiceCollection services)
    {
        var builder = services.AddIdentity<User, IdentityRole>(o =>
        {
            o.Password.RequireDigit = false;
        })
        .AddEntityFrameworkStores<EFDbContext>()
        .AddDefaultTokenProviders();
    }

    public static void ConfgiureJwt(IServiceCollection services, AppSettings appSettings)
    {
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.Audience = appSettings.JwtConfig.ValidAudience;
            options.TokenValidationParameters = TokenHelper.GetTokenValidationParameters(appSettings);
            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                    {
                        context.Response.Headers.Append("IS-TOKEN-EXPIRED", "true");
                    }

                    return Task.CompletedTask;
                },
            };
        });
    }

    internal static void ValidateConfiguration(AppSettings appSettings)
    {
        var validator = new AppSettingsValidator();
        var validationResult = validator.Validate(appSettings);
        if (!validationResult.IsValid)
        {
            throw new MissingAppsettingsException(validationResult.Errors.Select(error => error.ErrorMessage).ToArray());
        }
    }
}