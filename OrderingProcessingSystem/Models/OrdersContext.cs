using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OrderingProcessingSystem.Models
{
    public class OrdersContext : DbContext
    {
        public DbSet<Billingaddress> Billingaddresses { get; set; }
        public DbSet<Country> Countrys { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Orderarticles> Orderarticles { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Article> Article { get; set; }


        public OrdersContext(DbContextOptions<OrdersContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=OrdersDB;Trusted_Connection=True;");
        }
    }
}
