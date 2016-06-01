using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Art_Gallery_RoughRiders.ViewModels
{
    public class ArtPieceUpdateViewModel
    {
        public int IdArtPiece { get; set; }
        public int IdArtWork { get; set; }
        public string ArtPieceImage { get; set; }
        public string ArtPieceDateCreated { get; set; }
        public decimal ArtPiecePrice { get; set; }
        public bool ArtPieceSold { get; set; }
        public string ArtPieceLocation { get; set; }
        public int ArtPieceEditionNum { get; set; }
    }
}