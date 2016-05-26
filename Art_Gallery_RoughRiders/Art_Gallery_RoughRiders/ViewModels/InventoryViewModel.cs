using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Art_Gallery_RoughRiders.ViewModels
{
    public class InventoryViewModel
    {
        public int IdArtPiece { get; set; }
        public string   ArtWorkTitle { get; set; }
        public string   Artist { get; set; }
        public int   Edition { get; set; }
        public decimal ArtPiecePrice { get; set; }
        public decimal ArtGalleryCost { get; set; }
    }
}