using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Art_Gallery_RoughRiders.Models;

namespace Art_Gallery_RoughRiders.ViewModels
{
  public class ArtShowViewModel
  {
    public string ShowName { get; set; }
    public string ShowLocation { get; set; }
    public List<Artist> ShowArtists { get; set; }
    public List<Agent> ShowAgents { get; set; }
  }
}