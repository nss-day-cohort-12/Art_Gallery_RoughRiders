using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Art_Gallery_RoughRiders.ViewModels
{
    public class ArtDetailsViewModel
    {
        public string ArtWorkTitle { get; set; }
        public string Artist { get; set; }
        public string ArtWorkYear { get; set; }
        public string ArtWorkMedium { get; set; }
        public string ArtWorkDimensions { get; set; } 
    }
}