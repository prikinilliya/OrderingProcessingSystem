using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace OrderingProcessingSystem.Xml
{
    [XmlRoot(ElementName = "country")]
    public class XmlCountry
    {
        [XmlElement(ElementName = "geo")]
        public string Geo { get; set; }
    }

    [XmlRoot(ElementName = "billingaddress")]
    public class XmlBillingaddress
    {
        [XmlElement(ElementName = "billemail")]
        public string Billemail { get; set; }
        [XmlElement(ElementName = "billfname")]
        public string Billfname { get; set; }
        [XmlElement(ElementName = "billstreet")]
        public string Billstreet { get; set; }
        [XmlElement(ElementName = "billstreetnr")]
        public string Billstreetnr { get; set; }
        [XmlElement(ElementName = "billcity")]
        public string Billcity { get; set; }
        [XmlElement(ElementName = "country")]
        public XmlCountry Country { get; set; }
        [XmlElement(ElementName = "billzip")]
        public string Billzip { get; set; }

        public XmlBillingaddress()
        {
            Country = new XmlCountry();
        }
    }

    [XmlRoot(ElementName = "payment")]
    public class XmlPayment
    {
        [XmlElement(ElementName = "method-name")]
        public string Methodname { get; set; }
        [XmlElement(ElementName = "amount")]
        public string Amount { get; set; }
    }

    [XmlRoot(ElementName = "payments")]
    public class XmlPayments
    {
        [XmlElement(ElementName = "payment")]
        public List<XmlPayment> Payment { get; set; }

        public XmlPayments()
        {
            Payment = new List<XmlPayment>();
        }
    }

    [XmlRoot(ElementName = "orderarticle")]
    public class XmlOrderarticle
    {
        [XmlElement(ElementName = "artnum")]
        public string Artnum { get; set; }
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }
        [XmlElement(ElementName = "amount")]
        public string Amount { get; set; }
        [XmlElement(ElementName = "brutprice")]
        public string Brutprice { get; set; }
    }

    [XmlRoot(ElementName = "articles")]
    public class XmlArticles
    {
        [XmlElement(ElementName = "orderarticle")]
        public List<XmlOrderarticle> Orderarticle { get; set; }

        public XmlArticles()
        {
            Orderarticle = new List<XmlOrderarticle>();
        }
    }

    [XmlRoot(ElementName = "order")]
    public class XmlOrder
    {
        [XmlElement(ElementName = "oxid")]
        public string Oxid { get; set; }
        [XmlElement(ElementName = "orderdate")]
        public string Orderdate { get; set; }
        [XmlElement(ElementName = "status")]
        public string Status { get; set; }
        [XmlElement(ElementName = "billingaddress")]
        public XmlBillingaddress Billingaddress { get; set; }
        [XmlElement(ElementName = "payments")]
        public XmlPayments Payments { get; set; }
        [XmlElement(ElementName = "articles")]
        public XmlArticles Articles { get; set; }

        public XmlOrder()
        {
            Payments = new XmlPayments();
            Articles = new XmlArticles();
        }
    }

    [XmlRoot(ElementName = "orders")]
    public class XmlOrders
    {
        [XmlElement(ElementName = "order")]
        public List<XmlOrder> Orders { get; set; }

        public XmlOrders()
        {
            Orders = new List<XmlOrder>();
        }
    }

}
