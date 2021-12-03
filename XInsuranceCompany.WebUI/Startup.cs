using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using XInsuranceCompany.Core.Helpers;
using XInsuranceCompany.Core.Security;
using XInsuranceCompany.Data.Abstract;
using XInsuranceCompany.Data.Concrete.EntityFramework;
using XInsuranceCompany.Data.Contexts;
using XInsuranceCompany.Service.CarInsuranceOffer;
using XInsuranceCompany.WebUI.ApiService;

namespace XInsuranceCompany.WebUI
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
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddDataProtection();

            services.AddDbContext<XInsuranceCompanyDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:SqlConStr"].ToString(), o =>
                {
                    o.MigrationsAssembly("XInsuranceCompany.Data");
                });
            });

            services.AddScoped<ICarInsuranceOfferService, CarInsuranceOfferService>();
            services.AddScoped<ICarInsuranceOfferRepository, EfCarInsuranceOfferRepository>();
            services.AddScoped<ICommonHelper, CommonHelper>();
            services.AddScoped<IEncryption,Encryption>();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=InsuranceOffer}/{action=Index}/{id?}");
            });
        }
    }
}
