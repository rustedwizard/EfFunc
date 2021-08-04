﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EfFunc.Models
{
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