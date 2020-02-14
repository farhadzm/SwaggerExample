using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swagger_Example.Web.SwaggerConfig;

namespace Swagger_Example.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddAuthorization();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.AccessDeniedPath = "/Login";
                    options.Cookie.HttpOnly = true;
                    options.LoginPath = "/Login";
                    options.LogoutPath = "/Login";
                    options.ExpireTimeSpan = TimeSpan.FromDays(15);
                    options.SlidingExpiration = true;
                    options.Cookie.IsEssential = true;
                    options.ReturnUrlParameter = "returnUrl";
                });
            services.AddMvc();
            services.AddSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseSwaggerAndUI();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                          name: "default",
                          template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
