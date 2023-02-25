using Brio.Authentication.Models;
using Brio.Authentication.Services;
using Brio.Authentication.Services.Impl;
using Brio.Database.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Brio.Authentication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Configs files

            builder.Configuration
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true);

            builder.Services.Configure<Settings>(options =>
            {
                builder.Configuration.GetSection("Settings:ConnectionStrings:ConfirmService").Bind(options);
            });

            #endregion

            builder.Services.AddScoped<HttpClient>();
            builder.Services.AddScoped<IConfirmServiceClient, ConfirmServiceClient>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            #region Configure EF

            builder.Services.AddDbContext<BrioDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration["DatabaseOptions:ConnecrionStringLocal"]);
            });

            builder.Services.Configure<DatabaseOptions>(options =>
            {
                builder.Configuration.GetSection("DatabaseOptions").Bind(options);
            });

            #endregion

            #region Configure Swagger

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new OpenApiInfo { Title = "Brio.Authentication", Version = "v1" });
            });

            #endregion

            var app = builder.Build();

            if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Local"))
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
            }

            app.UseRouting();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Authentication}/{action=Login}/{id?}");

            app.Run();
        }
    }
}