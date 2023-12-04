using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyMoviesList.Shared.Models;
using System.Reflection.Emit;

namespace MyMoviesList.Server.Data
{
    public class AppicationDbContext: IdentityDbContext
    {
        public AppicationDbContext(DbContextOptions<AppicationDbContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Movie>().HasKey(m => new { m.Titulo });
            builder.Entity<UserInfo>().HasKey(m => new { m.Email });

            var listConverter = new ValueConverter<List<string>, string>(
            v => string.Join(',', v),
            v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            );

            builder.Entity<Movie>()
                .Property(e => e.Generos)
                .HasConversion(listConverter);

        }
        public DbSet<Movie> Movies { get; set; }

        public DbSet<UserInfo> Users => Set<UserInfo>();

    }
}
