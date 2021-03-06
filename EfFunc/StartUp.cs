using System;
using EfFunc;
using EfFunc.DbContexts;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

//This annotation tells Azure function EfFunc.StartUp is the place to start
[assembly: FunctionsStartup(typeof(StartUp))]

namespace EfFunc
{
    public class StartUp : FunctionsStartup
    {
        //This method enables Dependency Injection
        public override void Configure(IFunctionsHostBuilder builder)
        {
            //Handling the sql server connection setup
            var connectionString = Environment.GetEnvironmentVariable("SqlConnection");
            _ = builder.Services.AddDbContext<TodoDbContext>(op =>
                op.UseSqlServer(connectionString ?? throw new InvalidOperationException("Connection String is empty")));
        }
    }
}