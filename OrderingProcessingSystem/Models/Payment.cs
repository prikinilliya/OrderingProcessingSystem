using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingProcessingSystem.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string Methodname { get; set; }
        public string Amount { get; set; }

        public int OrderId { get; set; }
    }
}
