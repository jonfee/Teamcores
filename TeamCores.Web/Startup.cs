using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.WebEncoders;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using TeamCores.Common;
using TeamCores.Data;
using TeamCores.Misc;

namespace TeamCores.Web
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //#region 数据库
            //if (bool.Parse(Configuration["Develop"]))
            //{
            //    services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration["Database:DevConnection"], b => b.MigrationsAssembly("Versatile.Web")));
            //}
            //else
            //{
            //    services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration["Database:Connection"], b => b.MigrationsAssembly("Versatile.Web")));
            //}
            //#endregion

            services.AddMemoryCache();
            services.AddSession();

            services.Configure<WebEncoderOptions>(options =>
            {
                //解决中文被HTML编码的问题
                options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
            });

            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSession();
            app.UseCommonMiddleware(Configuration);
            app.UseDataMiddleware(Configuration);
            app.UseMiscMiddleware(Configuration);
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "home",
                    template: "{action=Index}/{id?}",
                    defaults: new { controller = "home" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
