using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Art_Gallery_RoughRiders.Models
{
  public class ArtShow
  {
    public int IdArtShow { get; set; }
    public int IdArtWork { get; set; }
    public string ArtShowName { get; set; }
    public string ArtShowLocation { get; set; }
    public string ArtShowAgents { get; set; }
    public string ArtShowOverhead { get; set; }
  }
}
