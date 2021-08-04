using EfFunc.DbContexts;
using EfFunc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
            log.LogInformation($"{DateTime.Now.ToUniversalTime()}: Get all Todo item request recieved");
            var todos = await _context.todos.ToListAsync();
            return new OkObjectResult(todos);
        }

        [FunctionName("Add")]
        public async Task<IActionResult> AddOne(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "addone")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"{DateTime.Now.ToUniversalTime()}: Add one new item request recieved");
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

        [FunctionName("AddMany")]
        public async Task<IActionResult> AddMany(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "addmany")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"{DateTime.Now.ToUniversalTime()}: Add many items request recieved");
            var reqBody = await new StreamReader(req.Body).ReadToEndAsync();
            var list = JsonConvert.DeserializeObject<List<Todos>>(reqBody);
            foreach (var t in list)
            {
                _ = await _context.AddAsync(t);
            }
            var c = await _context.SaveChangesAsync();
            if (c != list.Count)
            {
                return new BadRequestObjectResult($"Not all object have been saved to databae \n {list}");
            }
            return new OkObjectResult(list);
        }

        [FunctionName("Search")]
        public async Task<IActionResult> Search(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "search")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"{DateTime.Now.ToUniversalTime()}: Search Request recieved");
            var reqBody = await new StreamReader(req.Body).ReadToEndAsync();
            var query = JsonConvert.DeserializeObject<ToDoQuery>(reqBody);
            var res = await _context.todos.Where(x =>
                query.Name != null ? x.Name.Contains(query.Name) : true &&
                query.Description != null ? x.Description.Contains(query.Description) : true).ToListAsync();
            return new OkObjectResult(res);
        }
    }
}