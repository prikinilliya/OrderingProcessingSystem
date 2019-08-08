using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingProcessingSystem.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Oxid { get; set; }
        public string Orderdate { get; set; }
        public string Status { get; set; }
        public List<Payment> Payments { get; set; }
        public List<Orderarticles> Orderarticles { get; set; }

        public int BillingaddressId { get; set; }
        public Billingaddress Billingaddress { get; set; }

        public Order()
        {
            Payments = new List<Payment>();
            Orderarticles = new List<Orderarticles>();
            Billingaddress = new Billingaddress() { Country = new Country()};
        }
    }
}
