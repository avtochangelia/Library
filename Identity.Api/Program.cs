using Identity.Api;
using Infrastructure.DataAccess;
using Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();
startup.Configure(app, builder.Environment);

using var serviceScope = app.Services.GetService<IServiceScopeFactory>()!.CreateScope();
var context = serviceScope.ServiceProvider.GetService<EFDbContext>();
context!.Database.Migrate();

EFDatabaseInitializer.Initialize(serviceScope);

app.Run();