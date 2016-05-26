using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Art_Gallery_RoughRiders.ViewModels
{
    public class SalesArtYTDViewModel
    {
        public List<InventoryViewModel> IVM { get; set; }
        public YTDSalesViewModel YTD { get; set; }
    }
}