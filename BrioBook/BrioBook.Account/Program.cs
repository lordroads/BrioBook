using BrioBook.Account.Services;
using BrioBook.Account.Services.Impl;
using BrioBook.Users.DAL;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BrioBook.Account
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add Service Integrations

            builder.Services.AddSingleton<UsersDbContext>();
            builder.Services.AddTransient<IManagerAccounts, ManagerAccounts>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();

            #endregion

            #region Authentication

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Index";
                    options.LogoutPath = "/Account/Index";
                });
            builder.Services.AddAuthorization();

            //builder.Services.AddSingleton<IAuthenticateService, AuthenticateService>();

            #endregion

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapDefaultControllerRoute();

            app.Run();
        }
    }
}