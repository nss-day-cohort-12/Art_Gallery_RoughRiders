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
        public ActionResult Customer(string searchString, 
                                     string searchMedia, 
                                     int? searchId,
                                     int? minPrice,
                                     int? maxPrice)
        {

          var artWork = (from aw in _context.ArtWork
                              join ar in _context.Artist
                              on aw.IdArtist equals ar.IdArtist
                              join ap in _context.ArtPiece
                              on aw.IdArtWork equals ap.IdArtWork
                              where ap.ArtPieceSold == false
                              group ap by new
                              {
                                aw.IdArtWork,
                                aw.ArtWorkTitle,
                                ar.ArtistName,
                                ap.ArtPieceImage,
                                aw.ArtWorkMedium,
                                ap.ArtPiecePrice
                              }
                              into g
                              select new ArtDisplayViewModel
                              {
                                  ArtId = g.Key.IdArtWork,
                                  ArtWorkTitle = g.Key.ArtWorkTitle,
                                  ArtistName = g.Key.ArtistName,
                                  ArtWorkImage = g.Key.ArtPieceImage,
                                  ArtWorkMedium = g.Key.ArtWorkMedium,
                                  ArtWorkPrice = g.Key.ArtPiecePrice
                              });
 
          // Filter by artist name
          if (!string.IsNullOrEmpty(searchString) && searchId == 1)
          {
            artWork = artWork.Where(aw => aw.ArtistName.Contains(searchString));
          }

          // Filter by artwork medium
          if (!string.IsNullOrEmpty(searchMedia) && searchId == 2)
          {
            artWork = artWork.Where(aw => aw.ArtWorkMedium.Contains(searchMedia));
          }

          // Filter by price range
          if ((minPrice != null || maxPrice != null) && searchId == 3)
          {
            if (minPrice == null)
            {
              artWork = artWork.Where(aw => aw.ArtWorkPrice <= maxPrice);
            }
            else if (maxPrice == null)
            {
              artWork = artWork.Where(aw => aw.ArtWorkPrice >= minPrice);
            }
            else
            {
              artWork = artWork.Where(aw => aw.ArtWorkPrice <= maxPrice && aw.ArtWorkPrice >= minPrice);
            }
          }

          ModelState.Clear();
          return View(artWork.ToList());
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
                                 ap.ArtPieceLocation,
                                 aw.ArtWorkMedium
                             }
                             into g
                             select new ArtDetailsViewModel
                             {
                                 IdArtWork = g.Key.IdArtWork,
                                 ArtWorkTitle=g.Key.ArtWorkTitle,
                                 Artist = g.Key.ArtistName,
                                 ArtWorkYear = g.Key.ArtWorkYear,
                                 ArtPieceLocation = g.Key.ArtPieceLocation,
                                 ArtPiecePrice = g.Key.ArtPiecePrice,
                                 ArtPieceImage = g.Key.ArtPieceImage,
                                 ArtWorkDimensions = g.Key.ArtWorkDimensions,
                                 ArtWorkMedium = g.Key.ArtWorkMedium,
                                 InventoryCount = g.Count()
                             });
            
            return View(artDetail.Single());
        }

    public ActionResult Events()
    {
      var events = (from ash in _context.ArtShow
                    join asar in _context.ArtShowArtistRoster
                    on ash.IdArtShow equals asar.IdArtShow
                    join asag in _context.ArtShowAgentRoster
                    on ash.IdArtShow equals asag.IdArtShow
                    join ar in _context.Artist
                    on asar.IdArtist equals ar.IdArtist
                    join ag in _context.Agent
                    on asag.IdAgent equals ag.IdAgent
                    group ash by new
                    {
                      ash.ArtShowName,
                      ash.ArtShowLocation
                    }
                    into g
                    select new ArtShowViewModel
                    {
                      ShowName = g.Key.ArtShowName,
                      ShowLocation = g.Key.ArtShowLocation
                    }

      );
        
      return View(events);
    }
  }
}