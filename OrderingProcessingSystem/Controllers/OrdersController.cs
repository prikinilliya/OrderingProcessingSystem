using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderingProcessingSystem.Models;
using OrderingProcessingSystem.Xml;
using OrderingProcessingSystem.Mappers;

namespace OrderingProcessingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrdersContext _context;
        private IMapper mapper;

        public OrdersController(OrdersContext context)
        {
            _context = context;
            mapper = new OrdersMapper(_context); ;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            IEnumerable<Order> orders = await _context.Orders.Include(p => p.Payments)
                                             .Include(p => p.Orderarticles)
                                             .Include("Orderarticles.Article")
                                             .Include(p => p.Billingaddress)
                                             .ThenInclude(p => p.Country).ToListAsync();
            XmlOrders xmlOrders = mapper.MapToXml(orders) as XmlOrders;
            return Ok(xmlOrders);
        }

        // GET: api/Orders/5
        [HttpGet("{Oxid}")]
        public async Task<IActionResult> GetOrder([FromRoute] int oxid)
        {
            var order = new Order();
            try
            {
                order = await _context.Orders.Include(p => p.Payments)
                                             .Include(p => p.Orderarticles)
                                             .Include("Orderarticles.Article")
                                             .Include(p => p.Billingaddress)
                                             .ThenInclude(p => p.Country).FirstOrDefaultAsync(p => p.Oxid == oxid.ToString());
                if (order == null)
                {
                    return NotFound();
                }
                XmlOrders xmlOrders = mapper.MapToXml(order) as XmlOrders;
                return Ok(xmlOrders);
            }
            catch (Exception e)
            {
                return BadRequest(e.StackTrace + '\n' + e.Message);
            }
        }

        // PUT: api/Orders
        //To edit record you must put same xml, but with only 2 fields: oxid and status
        [HttpPut]
        public async Task<IActionResult> PutOrder([FromBody] XmlOrders xmlOrders)
        {
            try
            {
                foreach (XmlOrder xmlOrder in xmlOrders.Orders)
                {
                    Order order = await _context.Orders.FirstOrDefaultAsync(o => o.Oxid == xmlOrder.Oxid);
                    if (order == null)
                    {
                        return BadRequest("Order with Oxid: " + xmlOrder.Oxid + " doesent exist!");
                    }
                    else
                    {
                        order.Status = xmlOrder.Status;
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.StackTrace + '\n' + e.Message);
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<IActionResult> PostOrder([FromBody] XmlOrders xmlOrders)
        {
            string error = String.Empty;
            foreach (XmlOrder xmlOrder in xmlOrders.Orders)
            {
                Order order = await _context.Orders.FirstOrDefaultAsync(o => o.Oxid == xmlOrder.Oxid);
                if (order != null)
                {
                    return BadRequest("Order with Oxid: " + xmlOrder.Oxid + " already exist!");
                }
                else
                {
                    try
                    {
                        order = mapper.MapToObject(xmlOrder) as Order;
                        if (order == null)
                        {
                            return BadRequest("Wrong xml file");
                        }
                        order.Status = "Unprocessed";
                        _context.Orders.Add(order);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        error = e.StackTrace + '\n' + e.Message;
                    }
                }
            }
            return Ok(xmlOrders);
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}