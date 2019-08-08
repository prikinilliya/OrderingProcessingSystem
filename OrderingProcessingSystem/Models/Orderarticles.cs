﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingProcessingSystem.Models
{
    public class Orderarticles
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public int? OrderId { get; set; }

        public int? ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
