using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Art_Gallery_RoughRiders.Models
{
    public class Invoice
    {
        public int IdInvoice { get; set; }
        public int IdCustomer { get; set; }
        public int IdAgent { get; set; }
        public string PaymentMethod { get; set; }
        public string ShippingAddress { get; set; }
        public string PriceSole { get; set; }
    }