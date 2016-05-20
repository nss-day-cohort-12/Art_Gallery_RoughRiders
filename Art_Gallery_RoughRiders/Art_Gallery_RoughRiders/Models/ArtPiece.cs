using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Art_Gallery_RoughRiders.Models
{
    public class ArtPiece
    {
        public int IdArtPiece { get; set; }
        public int IdArtWork { get; set; }
        public string ArtPieceImage { get; set; }
        public DateTime ArtPieceDateCreated { get; set; }
        public Decimal ArtPiecePrice { get; set; }
        public bool ArtPieceSold { get; set; }
        public string ArtPieceLocation { get; set; }
        public int ArtPieceEditionNum { get; set; }
    }
}