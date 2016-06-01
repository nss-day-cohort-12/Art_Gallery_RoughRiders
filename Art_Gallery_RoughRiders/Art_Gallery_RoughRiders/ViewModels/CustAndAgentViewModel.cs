using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Art_Gallery_RoughRiders.ViewModels
{
    public class CustAndAgentViewModel
    {
        public int cust { get; set; }
        public int agent { get; set; }
        public string PaymentMethod { get; set; }
        public string ShippingAddress { get; set; }
        public string PieceSold { get; set; }
    }
}