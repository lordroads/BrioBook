using BrioBook.Client.Services;
using BrioBook.Client.Services.Impl;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BrioBook.Account
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add Service Integrations

            builder.Services.AddScoped<HttpClient>(); //TODO: Add Polly
            builder.Services.AddScoped<IAuthenticationServiceClient, AuthenticationServiceClient>();
            builder.Services.AddScoped<IConfirmClient, ConfirmClient>();
            builder.Services.AddScoped<IUsersClient, UsersClient>();

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