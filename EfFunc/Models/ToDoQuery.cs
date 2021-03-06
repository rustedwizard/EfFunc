using System;

namespace EfFunc.Models
{
    public class ToDoQuery
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Comment { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }


        public bool CompareToTodo(Todos todo)
        {
            var res = true;
            if (Name != null && todo.Name.Contains(Name)) return false;
            if (Description != null && todo.Description.Contains(Description)) return false;
            return res;
        }
    }
}