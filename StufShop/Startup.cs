using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StufShop.Models;
using StufShop.Models.Carts;
using StufShop.Models.Stuf;
using StufShop.Repository;
using StufShop.Models.Groups;
using StufShop.Models.Profile;

namespace StufShop
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
            services.AddDbContext<ApplicationDbContext>(option =>
                       option.UseSqlServer(Configuration["Data:StufShop:ConnectionStrings"]));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IStufRepository, StufRepository>();
            services.AddScoped<IStufItemRepository, StufItemRepository>();
            services.AddScoped<ICartRopository, CartRopository>();
            services.AddScoped<ICartItemRepository, CartItemRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();


            services.ConfigureApplicationCookie(option =>
            {
                option.LoginPath = "/Admin/Account/Signin";
            });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {

                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseDeveloperExceptionPage();

            app.UseMvc(route =>
            {
                route.MapRoute(name: "MyArea", template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                route.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");

            });
            ApplicationDbContext.CreateAdminAccount(app.ApplicationServices, Configuration).Wait();
        }
    }
}
