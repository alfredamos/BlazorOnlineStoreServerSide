using AutoMapper;
using BlazorOnlineStoreServerSide.Areas.Identity;
using BlazorOnlineStoreServerSide.Contracts;
using BlazorOnlineStoreServerSide.Data;
using BlazorOnlineStoreServerSide.Helpers;
using BlazorOnlineStoreServerSide.Mappings;
using BlazorOnlineStoreServerSide.PhotoHelper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreServerSide
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
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddSingleton<WeatherForecastService>();

            services.AddScoped<ICardDetailRepository, SQLCardDetailRepository>();
            services.AddScoped<ICustomerRepository, SQLCustomerRepository>();
            services.AddScoped<IOrderRepository, SQLOrderRepository>();
            services.AddScoped<IOrderLineItemRepository, SQLOrderLineItemRepository>();
            services.AddScoped<IProductRepository, SQLProductRepository>();

            services.AddScoped<IFileStorageService, InAppStorageService>();

            services.AddHttpContextAccessor();

            services.AddScoped<IOrderCertifyer, OrderCertifyer>();
            services.AddScoped<IPlaceOrder, PlaceOrder>();
            services.AddScoped<IProcessedOrder, ProcessedOrder>();
            services.AddScoped<IAddCardAndAddressToCustomerInfo, AddCardAndAddressToCustomerInfo>();
            services.AddScoped<IShoppingCart, ShoppingCart>();

            services.AddAutoMapper(typeof(Maps));

            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
                options.AppendTrailingSlash = true;
            });

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
