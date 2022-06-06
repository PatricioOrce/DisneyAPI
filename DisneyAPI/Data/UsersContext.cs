using DisneyAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DisneyAPI.Data
{
    public class UsersContext : IdentityDbContext<Usuario>
    {
        public readonly string Schema = "usuarios";

        public UsersContext(DbContextOptions<UsersContext> options) : base(options)
        {

        }

        protected UsersContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema(Schema);
        }
    }
}
