using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Art_Gallery_RoughRiders.Models
{
    public class Artist
    {
        public int IdArtist { get; set; }
        public string ArtistName { get; set; }
        public int ArtistBirthYear { get; set; }
        public int ArtistDeathYear { get; set; }
    }
}