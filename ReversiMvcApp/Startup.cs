using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReversiMvcApp.Data;
using System;
using ReversiMvcApp.Hubs;
using ReversiMvcApp.Logic.Interfaces;
using ReversiMvcApp.Logic;

namespace ReversiMvcApp
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
            services.AddDbContext<ReversiDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("ReversiConnection")));

            services.AddHttpClient("reversiClient", c => c.BaseAddress = new Uri(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("ApiConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedEmail = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ReversiDbContext>();
            services.AddControllersWithViews();

            services.AddScoped<ISpelLogic, SpelLogic>();
            services.AddScoped<IUserLogic, UserLogic>();
            services.AddScoped<IUitslagLogic, UitslagLogic>();
            services.AddScoped<ISpelerLogic, SpelerLogic>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials()
                           .WithOrigins("http://localhost:5000");
                    });
            });
            services.AddSignalR();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors();
            app.UseWebSockets();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ReversiHub>("/reversiHub", options =>
                {
                    options.TransportMaxBufferSize = 65536000;
                });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}",
                    defaults: new { controller = "Home", action = "Index" }
                    );
                endpoints.MapRazorPages();
            });
        }
    }
}
