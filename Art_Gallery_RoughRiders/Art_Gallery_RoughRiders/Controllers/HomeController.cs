using Art_Gallery_RoughRiders.Models;
using Art_Gallery_RoughRiders.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Art_Gallery_RoughRiders.Controllers
{
    public class HomeController : Controller
    {
        private ArtGalleryDbContext _context = new ArtGalleryDbContext();
         
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Customer()
        {
            //Get access to all the art.
            var artWork = (from aw in _context.ArtWork
                           join ar in _context.Artist
                           on aw.IdArtist equals ar.IdArtist
                           select new ArtDisplayViewModel
                           {
                               ArtId = aw.IdArtWork,
                               ArtWorkTitle = aw.ArtWorkTitle,
                               ArtistName = ar.ArtistName
                           }).ToList();

            return View(artWork);
        }

        //Pass in the ID of the single piece of art that the user clicked on. Use the ID to filter data.
        public ActionResult ArtDetails(int artId)
        {

            var artDetail = (from aw in _context.ArtWork
                             where aw.IdArtWork == artId

                             join ar in _context.Artist
                             on aw.IdArtist equals ar.IdArtist

                             join ap in _context.ArtPiece
                             on aw.IdArtWork equals ap.IdArtWork

                             group ap by new
                             {
                                 aw.IdArtWork,
                                 aw.ArtWorkTitle,
                                 ar.ArtistName,
                                 ap.ArtPieceImage,
                                 ap.ArtPiecePrice,
                                 aw.ArtWorkYear,
                                 aw.ArtWorkDimensions,
                                 ap.ArtPieceLocation

                             }
                             into g

                             select new ArtDetailsViewModel
                             {
                                 ArtWorkTitle=g.Key.ArtWorkTitle,
                                 Artist = g.Key.ArtistName,
                                 ArtWorkYear = g.Key.ArtWorkYear,
                                 ArtPieceLocation = g.Key.ArtPieceLocation,
                                 ArtPiecePrice = g.Key.ArtPiecePrice,
                                 InventoryCount = g.Count()


                             });
            var artdetails = artDetail;

            return View(artDetail.ToList());
        }
    }
}