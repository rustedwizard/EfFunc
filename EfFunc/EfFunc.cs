using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using EfFunc.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using EfFunc.Models;

namespace EfFunc
{
    public class EfFunc
    {
        private readonly TodoDbContext _context;

        public EfFunc(TodoDbContext context)
        {
            _context = context;
        }

        [FunctionName("Get")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "get")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Get all Todo item request recieved");
            var todos = await _context.todos.ToListAsync();
            return new OkObjectResult(todos);
        }

        [FunctionName("Add")]
        public async Task<IActionResult> AddOne(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "addone")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Add one new item request recieved");
            var reqBody = await new StreamReader(req.Body).ReadToEndAsync();
            var t = JsonConvert.DeserializeObject<Todos>(reqBody);
            _ = await _context.AddAsync(t);
            var c = await _context.SaveChangesAsync();
            if (c == 0)
            {
                return new BadRequestObjectResult($"Following object have not been insert into database \n {t}");
            }
            return new OkObjectResult(t);
        }
    }
}