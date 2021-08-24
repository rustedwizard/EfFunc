using System;
using EfFunc.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EfFunc.DbContextFactory
{
    //following code enables Entity Framework to Add Migrations.
    public class TodoDbContextFactory : IDesignTimeDbContextFactory<TodoDbContext>
    {
        public TodoDbContext CreateDbContext(string[] args)
        {
            var opBuilder = new DbContextOptionsBuilder<TodoDbContext>();
            opBuilder.UseSqlServer(Environment.GetEnvironmentVariable("SqlConnection") ??
                                   throw new InvalidOperationException("Connection String is empty"));
            return new TodoDbContext(opBuilder.Options);
        }
    }
}