using Jot.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jot.Database
{
    public class KoboContext : DbContext
    {
        public DbSet<Content> Contents => Set<Content>();
        public DbSet<Bookmark> Bookmarks => Set<Bookmark>();
        protected override void OnModelCreating(ModelBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            builder.Entity<Content>()
                .ToTable("Content")
                .HasKey(x => x.ContentId);
            builder.Entity<Bookmark>()
                .ToTable("Bookmark")
                .HasKey(x => x.BookmarkId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(@$"Data Source=KoboReader.sqlite;Mode=ReadOnly");
        }
    }
}
