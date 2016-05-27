using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Art_Gallery_RoughRiders.ViewModels
{
    public class ArtPieceInfoViewModel
    {
        public int IdArtWork { get; set; }
        public int IdInvoice { get; set; }
        public decimal ArtPiecePrice { get; set; }
        public string ArtPieceLoaction { get; set; }
        public int ArtPieceEditionNum { get; set; }
        public string ArtWorkTitle { get; set; }
        public string ArtistName { get; set; }
        public string AgentName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhoneNumber { get; set; }

    }
}