using Data.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Back
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
            services.AddDbContext<ParktrackContext>(options => options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddHttpContextAccessor();

            services.AddMvc();

            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });

            services.AddRazorPages().AddViewLocalization();

            services.AddLocalization(ops => ops.ResourcesPath = "Resources");
            services.AddMvc().AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix, opts => opts.ResourcesPath = "Resources").AddDataAnnotationsLocalization();

            IFileProvider physicalProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory());
            services.AddSingleton<IFileProvider>(physicalProvider);

            services.Configure<RequestLocalizationOptions>(opts =>
            {

                var supportedCultures = new List<CultureInfo>()
                {
                    new CultureInfo("es")
                };
                opts.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("es");
                opts.SupportedCultures = supportedCultures;
                opts.SupportedUICultures = supportedCultures;


            });

            services.Configure<CookieAuthenticationOptions>(options =>
            {
                options.LoginPath = new PathString("/Login");
            });

            CookieAuthenticationOptions cookie = new CookieAuthenticationOptions();
            cookie.LoginPath = new PathString("/Login");
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("User", policy => policy.RequireRole("UserRole"));
                options.AddPolicy("Admin", policy => policy.RequireRole("AdminRole"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            try
            {
                string publicDirectory = Path.Combine(Directory.GetCurrentDirectory(), @"AppData");
                app.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(publicDirectory)
                });
            }
            catch
            {
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);
        }
    }
}
