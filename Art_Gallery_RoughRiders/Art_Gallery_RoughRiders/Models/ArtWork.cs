using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Art_Gallery_RoughRiders.Models
{
  public class ArtWork
  {
    public int IdArtWork { get; set; }
    public int IdArtist { get; set; }
    public string ArtWorkTitle { get; set; }
    public int ArtWorkYear { get; set; }
    public string ArtWorkMedium { get; set; }
    public string ArtWorkDimensions { get; set; }
    public int ArtWorkNumMade { get; set; }
    public int ArtWorkNumInventory { get; set; }
    public int ArtWorkNumSold { get; set; }
  }
}
