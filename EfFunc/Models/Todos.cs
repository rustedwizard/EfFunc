using System;

namespace EfFunc.Models
{
    //Use Fluent API in DbContext class. So this class can be kept clean
    public class Todos
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Comment { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}