using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Art_Gallery_RoughRiders.Models
{
  public class ArtGalleryDbContext : DbContext
  {
    public DbSet<Agent> Agent { get; set; }
    public DbSet<Artist> Artist { get; set; }
    public DbSet<ArtPiece> ArtPiece { get; set; }
    public DbSet<ArtShow> ArtShow { get; set; }
    public DbSet<ArtWork> ArtWork { get; set; }
    public DbSet<Customer> Customer { get; set; }
    public DbSet<Invoice> Invoice { get; set; }
    public DbSet<InvoiceArtPiece> InvoiceArtPiece { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Agent>()
               .ToTable("Agent")
               .HasKey(a => a.IdAgent);

      modelBuilder.Entity<Artist>()
               .ToTable("Artist")
               .HasKey(a => a.IdArtist);

      modelBuilder.Entity<ArtPiece>()
               .ToTable("ArtPiece")
               .HasKey(a => a.IdArtPiece);

      modelBuilder.Entity<ArtShow>()
               .ToTable("ArtShow")
               .HasKey(a => a.IdArtShow);

      modelBuilder.Entity<ArtWork>()
               .ToTable("ArtWork")
               .HasKey(a => a.IdArtWork);

      modelBuilder.Entity<Customer>()
               .ToTable("Customer")
               .HasKey(c => c.IdCustomer);

      modelBuilder.Entity<Invoice>()
               .ToTable("Invoice")
               .HasKey(i => i.IdInvoice);

      modelBuilder.Entity<InvoiceArtPiece>()
         .ToTable("InvoiceArtPiece")
         .HasKey(a => a.IdInvoiceArtPiece);
        }
  }
}
