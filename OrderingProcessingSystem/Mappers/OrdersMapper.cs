using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderingProcessingSystem.Models;
using OrderingProcessingSystem.Xml;

namespace OrderingProcessingSystem.Mappers
{
    public class OrdersMapper : IMapper
    {
        private readonly OrdersContext context;

        public OrdersMapper(OrdersContext _context)
        {
            context = _context;
        }

        public object MapToObject(object _xmlOrders)
        {
            XmlOrder xmlOrders = _xmlOrders as XmlOrder;
            Order order = new Order();

            order.Oxid = xmlOrders.Oxid;
            order.Orderdate = xmlOrders.Orderdate;

            Billingaddress adress = new Billingaddress();
            adress.Billemail = xmlOrders.Billingaddress.Billemail;
            adress.Billfname = xmlOrders.Billingaddress.Billfname;
            adress.Billstreet = xmlOrders.Billingaddress.Billstreet;
            adress.Billstreetnr = xmlOrders.Billingaddress.Billstreetnr;
            adress.Billcity = xmlOrders.Billingaddress.Billcity;
            adress.Billzip = xmlOrders.Billingaddress.Billzip;
            Country country = GetGeo(xmlOrders.Billingaddress.Country.Geo);
            if (country == null)
            {
                country = new Country() { Geo = xmlOrders.Billingaddress.Country.Geo };
            }
            adress.Country = country;
            order.Billingaddress = adress;

            foreach (XmlPayment payment in xmlOrders.Payments.Payment)
            {
                Payment _payment = new Payment();
                _payment.Methodname = payment.Methodname;
                _payment.Amount = payment.Amount;
                order.Payments.Add(_payment);
            }

            foreach (XmlOrderarticle orderArticle in xmlOrders.Articles.Orderarticle)
            {
                Article article = context.Article.FirstOrDefault(a => a.Artnum == orderArticle.Artnum);
                if (article != null)
                {
                    order.Orderarticles.Add(new Orderarticles() { OrderId = order.Id, ArticleId = article.Id, Order = order, Article = article });
                }
                else
                {
                    Article newArticle = new Article();
                    newArticle.Artnum = orderArticle.Artnum;
                    newArticle.Title = orderArticle.Title;
                    newArticle.Amount = orderArticle.Amount;
                    newArticle.Brutprice = orderArticle.Brutprice;
                    order.Orderarticles.Add(new Orderarticles() { Order = order, Article = newArticle });
                } 
            }
            return order;
        }

        public object MapToXml(object _orders)
        {
            XmlOrders xmlOrders = new XmlOrders();
            Order order = _orders as Order;
            XmlOrder xmlOrder = new XmlOrder();
            xmlOrder.Oxid = order.Oxid;
            xmlOrder.Oxid = order.Oxid;
            xmlOrder.Orderdate = order.Orderdate;
            xmlOrder.Status = order.Status;

            XmlBillingaddress xmlAdress = new XmlBillingaddress();
            xmlAdress.Billemail = order.Billingaddress.Billemail;
            xmlAdress.Billfname = order.Billingaddress.Billfname;
            xmlAdress.Billstreet = order.Billingaddress.Billstreet;
            xmlAdress.Billstreetnr = order.Billingaddress.Billstreetnr;
            xmlAdress.Billcity = order.Billingaddress.Billcity;
            xmlAdress.Billzip = order.Billingaddress.Billzip;
            xmlAdress.Country.Geo = order.Billingaddress.Country.Geo;
            xmlOrder.Billingaddress = xmlAdress;

            foreach (Payment payment in order.Payments)
            {
                XmlPayment xmlPayment = new XmlPayment();
                xmlPayment.Methodname = payment.Methodname;
                xmlPayment.Amount = payment.Amount;
                xmlOrder.Payments.Payment.Add(xmlPayment);
            }

            foreach (Orderarticles orderArcticle in order.Orderarticles)
            {
                XmlOrderarticle xmlOrderArticle = new XmlOrderarticle();
                xmlOrderArticle.Artnum = orderArcticle.Article.Artnum;
                xmlOrderArticle.Title = orderArcticle.Article.Title;
                xmlOrderArticle.Amount = orderArcticle.Article.Amount;
                xmlOrderArticle.Brutprice = orderArcticle.Article.Brutprice;
                xmlOrder.Articles.Orderarticle.Add(xmlOrderArticle);
            }
            xmlOrders.Orders.Add(xmlOrder);
            return xmlOrders;
        }

        public object MapToXml(IEnumerable<object> valueToMap)
        {
            List<Order> orders = valueToMap as List<Order>;
            XmlOrders outerOrdersXml = new XmlOrders();
            foreach (Order order in orders)
            {
                XmlOrders innerOrdersXml = MapToXml(order) as XmlOrders;
                outerOrdersXml.Orders.Add(innerOrdersXml.Orders[0]);
            }
            return outerOrdersXml;
        }

        private Country GetGeo(string geo)
        {
            return context.Countrys.FirstOrDefault(c => c.Geo == geo);
        }
    }
}
