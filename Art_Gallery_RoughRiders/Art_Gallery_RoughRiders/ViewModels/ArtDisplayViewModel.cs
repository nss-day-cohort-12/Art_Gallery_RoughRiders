using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Art_Gallery_RoughRiders.ViewModels
{
    public class ArtDisplayViewModel
    {
        public int ArtId { get; set; }
        public string   ArtWorkTitle { get; set; }
        public string   ArtistName { get; set; }
        public string   ArtWorkImage { get; set; }
        public string   ArtWorkMedium { get; set; }
        public Decimal ArtWorkPrice { get; set; }
        //public List<SelectListItem> Media { get; set; }
    }
}