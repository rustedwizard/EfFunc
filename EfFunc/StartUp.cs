using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EfFunc.DbContexts;
using System;

[assembly: FunctionsStartup(typeof(EfFunc.StartUp))]

namespace EfFunc
{
    public class StartUp : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var connectionString = Environment.GetEnvironmentVariable("SqlConnection");
            _ = builder.Services.AddDbContext<TodoDbContext>(op =>
                  SqlServerDbContextOptionsExtensions.UseSqlServer(op, connectionString));
        }
    }
}