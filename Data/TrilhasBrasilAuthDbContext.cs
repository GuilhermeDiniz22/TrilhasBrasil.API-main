using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TrilhasBrasil.API.Data //criação de roles para segurança e permissão de operações CRUD
{
    public class TrilhasBrasilAuthDbContext : IdentityDbContext
    {
        public TrilhasBrasilAuthDbContext(DbContextOptions<TrilhasBrasilAuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var userRoleId = "e31087d3-7a1f-4741-88a0-94fd36dff67c";

            var adminRoleId = "f4f7a03e-a4e4-4847-bd12-daaccc4312bc";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                 Id = userRoleId,
                 ConcurrencyStamp = userRoleId,
                 Name = "User",
                 NormalizedName = "User".ToUpper(),
                },

                new IdentityRole
                {
                 Id = adminRoleId,
                 ConcurrencyStamp = adminRoleId,
                 Name = "Admin",
                 NormalizedName = "Admin".ToUpper(),
                },
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}

