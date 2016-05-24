using Art_Gallery_RoughRiders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Art_Gallery_RoughRiders.ViewModels
{
    public class AddArtViewModel
    {
        public Artist newArtist { get; set; }
        public DropDownList ExistingArtist { get; set; }
        public ArtPiece ArtPiece { get; set; }
        public ArtWork ArtWork { get; set; }
    }
}