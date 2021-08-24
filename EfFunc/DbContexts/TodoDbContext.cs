using EfFunc.Models;
using Microsoft.EntityFrameworkCore;

namespace EfFunc.DbContexts
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions op) : base(op)
        {
        }

        public DbSet<Todos> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Todos>().HasKey(x => x.Id);
            builder.Entity<Todos>().HasIndex(x => x.Id);
            builder.Entity<Todos>().Property(x => x.Name).HasMaxLength(60).IsRequired();
            builder.Entity<Todos>().Property(x => x.Description).HasMaxLength(500).IsRequired();
            builder.Entity<Todos>().Property(x => x.Comment).HasMaxLength(300);
        }
    }
}