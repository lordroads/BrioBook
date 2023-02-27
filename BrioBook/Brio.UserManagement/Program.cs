using Brio.Database.DAL;
using Brio.UserManagement.Models;
using Brio.UserManagement.Sevices;
using Brio.UserManagement.Sevices.Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Brio.UserManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure DI

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddControllers();

            #endregion

            #region Configure EF 

            builder.Services.AddDbContext<BrioDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration["DatabaseOptions:ConnectionString"]);
            });

            builder.Services.Configure<DatabaseOptions>(options =>
            {
                builder.Configuration.GetSection("DatabaseOptions").Bind(options);
            });

            #endregion

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new OpenApiInfo { Title = "Brio.UserManagement", Version = "v1" });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment() | app.Environment.IsEnvironment("Local"))
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
            }

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Users}/{action=GetAll}/{id?}");

            app.Run();
        }
    }
}