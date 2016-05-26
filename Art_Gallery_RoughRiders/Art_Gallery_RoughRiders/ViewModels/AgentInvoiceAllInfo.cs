using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Art_Gallery_RoughRiders.ViewModels
{
    public class AgentInvoiceAllInfo
    {
        public List<ArtPieceInfoViewModel> ArtPieceList { get; set; }
        public AgentCustomerViewModel AgentCustomerVM { get; set; }
    }
}