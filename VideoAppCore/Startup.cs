using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TeamWork.Areas.Identity;
using TeamWork.Data;
using TeamWork.Models;
using TeamWork.Data.Rpt;
using TeamWork.Data.Usr;
using TeamWork.Data.Comm;
using TeamWork.Data.Vot;

using Microsoft.AspNetCore.Authentication.Cookies;
using Blazored.SessionStorage;

namespace TeamWork
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
           
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            services.AddSingleton<WeatherForecastService>();

            //신규 DBContext 등록
            services.AddEntityFrameworkSqlServer().AddDbContext<VideoDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            
            string dataConnectionString = Configuration.GetConnectionString("DefaultConnection");

            //DI Container에 서비스 등록
            //services.AddTransient<IReportAsync, ReportEfCoreAsync>();
            //services.AddTransient<IReportAsync>(s => new ReportDapperAsync(dataConnectionString));
            services.AddTransient<IUserAsync, UserEfCoreAsync>();
            services.AddTransient<IReportAsync, ReportDapperAsync>();
            services.AddTransient<ICommonCodeAsync, CommonCodeDapperAsync>();
            services.AddTransient<IVoteQuestionAsync, VoteQuestionDapperAsync>();

            //인증 & Session 
            services.AddScoped<AuthenticationStateProvider, SiteAuthenticationStateProvider>();
            services.AddBlazoredSessionStorage();

            services.AddScoped<ToastService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

            
        }
    }
}
