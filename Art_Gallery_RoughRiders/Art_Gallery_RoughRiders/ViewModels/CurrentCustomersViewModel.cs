using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Art_Gallery_RoughRiders.ViewModels
{
    public class CurrentCustomersViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        // This property will hold a state, selected by user
        [Required]
        [Display(Name = "State")]
        public string State { get; set; }

        // This property will hold all available states for selection
        public IEnumerable<SelectListItem> CustList { get; set; }
        public int SelectedCustID { get; set; }

        public int artId { get; set; }
    }
}
