using EfFunc.Models;
using Microsoft.EntityFrameworkCore;

namespace EfFunc.DbContexts
{
    public class TodoDbContext : DbContext
    {
        public DbSet<Todos> todos { get; set; }

        public TodoDbContext(DbContextOptions op) : base(op)
        { }

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