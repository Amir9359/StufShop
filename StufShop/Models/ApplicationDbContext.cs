using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StufShop.Models;
using StufShop.Models.Carts;
using StufShop.Models.Groups;
using StufShop.Models.Profile;
using StufShop.Models.Stuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StufShop.Models
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<User> User { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItems> cartItems { get; set; }
        public DbSet<Stuf.Stuf> Stufs { get; set; }
        public DbSet<StufItem> StufItems { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<CartItemPayed> CartItemPayeds { get; set; }
        public DbSet<CartPayed> CartPayeds { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> op) : base(op)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CartPayed>()
                .HasMany(a => a.CartItemPayed)
                .WithOne(u => u.CartPayed)
                .HasForeignKey(aa => aa.CartPayedId);

            base.OnModelCreating(builder);

        }
        public static async Task CreateAdminAccount(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var servicescope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                UserManager<User> Usermanager = servicescope.ServiceProvider.GetRequiredService<UserManager<User>>();
                RoleManager<IdentityRole> roleManager = servicescope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                string username = configuration["Data:AdminUser:Name"];
                string email = configuration["Data:AdminUser:Email"];
                string password = configuration["Data:AdminUser:Password"];
                string role = configuration["Data:AdminUser:Role"];
                if (await Usermanager.FindByNameAsync(username) == null)
                {
                    if (await roleManager.FindByNameAsync(role) == null)
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                    var user = new User
                    {
                        UserName = username,
                        Email = email
                    };
                    IdentityResult result = await Usermanager.CreateAsync(user, password);
                    if (result.Succeeded)
                    {
                        await Usermanager.AddToRoleAsync(user, role);
                    }
                }

            }

        }

    }

}
