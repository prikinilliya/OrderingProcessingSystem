using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingProcessingSystem.Models
{
    public class Billingaddress
    {
        public int Id { get; set; }
        public string Billemail { get; set; }
        public string Billfname { get; set; }
        public string Billstreet { get; set; }
        public string Billstreetnr { get; set; }
        public string Billcity { get; set; }
        public string Billzip { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}
