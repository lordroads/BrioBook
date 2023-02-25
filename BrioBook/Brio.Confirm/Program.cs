using Brio.Confirm.Models;
using Brio.Confirm.Services;
using Brio.Confirm.Services.Impl;
using Brio.Database.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Brio.Confirm
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

            #endregion

            builder.Services.AddScoped<IConfirmService, ConfirmService>();
            builder.Services.AddScoped<IConfirmRepository, ConfirmRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddControllers();

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

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new OpenApiInfo { Title = "Brio.Confirm", Version = "v1" });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Confirm}/{action=Create}/{id?}");

            app.Run();
        }
    }
}