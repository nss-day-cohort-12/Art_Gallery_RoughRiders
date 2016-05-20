using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Art_Gallery_RoughRiders.Models
{
  public class ArtGalleryDbContext : DbContext
  {
    public DbSet<Artist> Artist { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Artist>()
               .ToTable("Artist")
               .HasKey(a => a.IdArtist);
    }
  }
}
