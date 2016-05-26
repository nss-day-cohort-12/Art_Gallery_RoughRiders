using Art_Gallery_RoughRiders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Art_Gallery_RoughRiders.ViewModels
{
    public class AgentCustomerViewModel
    {
        public string AgentName { get; set; }
        public int IdInvoice { get; set; }
        public int IdCustomer { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public int InvoiceDate { get; set; }
    }
}