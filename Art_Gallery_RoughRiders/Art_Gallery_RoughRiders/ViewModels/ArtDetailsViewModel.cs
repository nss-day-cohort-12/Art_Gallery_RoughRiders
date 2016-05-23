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
        public int ArtWorkYear { get; set; } 
        public string ArtWorkMedium { get; set; }
        public string ArtWorkDimensions { get; set; }
        public decimal ArtPiecePrice { get; set; }
        public string ArtPieceImage { get; set; }
        public string ArtPieceLocation { get; set; }
        public int InventoryCount { get; set; }
         
    }
}