using Microsoft.EntityFrameworkCore;
using SnippetApi.Models;

namespace SnippetApi.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Command> Commands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>(builder => {

                builder.ToTable("Group");
            });

            modelBuilder.Entity<Command>(builder => {

                builder.ToTable("Command");

                builder.HasOne(prop => prop.Group)
                .WithMany(prop => prop.Commands)
                .HasForeignKey(prop => prop.GroupId)
                .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
