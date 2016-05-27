using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Art_Gallery_RoughRiders.Models
{
  public class ArtShowArtistRoster
  {
    public int IdArtShowArtistRoster { get; set; }
    public int IdArtShow { get; set; }
    public int IdArtist { get; set; }
  }
}
