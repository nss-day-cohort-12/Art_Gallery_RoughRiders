using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Art_Gallery_RoughRiders.Models
{
    public class Customer
    {
        public int IdCustomer { get; set; }
        public int IdAgent { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLasttName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhoneNumber { get; set; }
    }
}