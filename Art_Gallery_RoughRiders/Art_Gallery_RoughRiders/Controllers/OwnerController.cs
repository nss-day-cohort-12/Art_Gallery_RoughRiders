using Art_Gallery_RoughRiders.Models;
using Art_Gallery_RoughRiders.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Art_Gallery_RoughRiders.Controllers
{
    public class OwnerController : Controller
    {
        private ArtGalleryDbContext _context = new ArtGalleryDbContext();

        // GET: Owner
        public ActionResult OwnerIndex()
        {

            return View();
        }
        public ActionResult Inventory()
        {
            var allArtPieces = (from ap in _context.ArtPiece
                           join aw in _context.ArtWork                         
                           on ap.IdArtWork equals aw.IdArtWork

                           join ar in _context.Artist
                           on aw.IdArtist equals ar.IdArtist

                           select new InventoryViewModel
                           {
                               IdArtPiece = ap.IdArtPiece,
                               Artist = ar.ArtistName,
                               ArtWorkTitle = aw.ArtWorkTitle,
                               Edition = ap.ArtPieceEditionNum
                           }).ToList();

            return View(allArtPieces);

        }
    }
}