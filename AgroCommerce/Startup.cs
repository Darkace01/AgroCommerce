using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgroCommerce.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AgroCommerce.Core;
using AgroCommerce.Data.Contracts;
using AgroCommerce.Data.Implementations;
using AgroCommerce.Services.Contracts;
using AgroCommerce.Services.Implementations;

namespace AgroCommerce
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddScoped<SignInManager<ApplicationUser>, SignInManager<ApplicationUser>>();
            services.AddScoped<UserManager<ApplicationUser>, UserManager<ApplicationUser>>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<ApplicationUser>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped(serviceType: typeof(IUnitOfWork), implementationType: typeof(UnitOfWork));

            //repos
            services.AddScoped(serviceType: typeof(ICoreRepo<>), implementationType: typeof(CoreRepo<>));

            //services
            services.AddScoped(serviceType: typeof(IFarmService), implementationType: typeof(FarmService));
            services.AddScoped(serviceType: typeof(IAnimalTypeService), implementationType: typeof(AnimalTypeService));
            services.AddScoped(serviceType: typeof(IListingService), implementationType: typeof(ListingService));
            services.AddScoped(serviceType: typeof(ILocationMgtService), implementationType: typeof(LocationMgtService));
            services.AddScoped(serviceType: typeof(IReviewService), implementationType: typeof(ReviewService));
            services.AddScoped(serviceType: typeof(ITransactionService), implementationType: typeof(TransactionService));
            services.AddScoped(serviceType: typeof(IUserAccountService), implementationType: typeof(UserAccountService));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
