using EfFunc.DbContexts;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

//This annotation tells Azure function EfFunc.StartUp is the place to start
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