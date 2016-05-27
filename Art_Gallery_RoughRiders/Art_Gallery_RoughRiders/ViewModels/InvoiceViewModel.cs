using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Art_Gallery_RoughRiders.ViewModels
{
    public class InvoiceViewModel
    {
        //public IGrouping<int, ArtPieceInfoViewModel> ArtPieceInfoViewModel { get; set; }
        public int IdInvoice { get; set; }
        public string agentName { get; set; }
        public string customerName { get; set; }
        public string customerAddress { get; set; }
        public string customerPhoneNumber { get; set; }
        public string paymentMethod { get; set; }
        public List<APinInvoice> ArtWork { get; set; }
    }
}