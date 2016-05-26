using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Art_Gallery_RoughRiders.Models
{
    public class InvoiceArtPiece
    {
        public int IdInvoiceArtPiece { get; set; }
        public int IdArtPiece { get; set; }
        public int IdInvoice { get; set; }
    }
}