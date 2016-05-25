using Art_Gallery_RoughRiders.Models;
using Art_Gallery_RoughRiders.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

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
                               ArtPiecePrice = ap.ArtPiecePrice,
                               IdArtPiece = ap.IdArtPiece,
                               Artist = ar.ArtistName,
                               ArtWorkTitle = aw.ArtWorkTitle,
                               Edition = ap.ArtPieceEditionNum,
                               ArtGalleryCost = ap.ArtPiecePrice/2
                           }).ToList();

            return View(allArtPieces);

        }

        [HttpGet]
        public ActionResult AddPainting()
        {
            var ExistingArtists = (from a in _context.Artist
                                   select a).ToList();

            //DropDownList ArtistDDList = new DropDownList();

            //ArtistDDList.ID = ;
            //ArtistDDList.AutoPostBack = true;
            //Artist existingArtsit = new Artist
            //{
            //    ArtistName = ExistingArtists
            //};

            //AddArtViewModel av = new AddArtViewModel
            //{
            //    ExistingArtist = ExistingArtists
            //};

            return View();
        }

        [HttpPost]
        public ActionResult AddPainting(AddArtViewModel Art)
        {
            // Set the instance of a local view model to set the ID of either the existing artist, or the new artist we will create
            ArtistViewModel local_artist = new ArtistViewModel();

            if (ModelState.IsValid)
            {

                // If the artist the user entered already exists, use that artist's ID
                var _artistNameCheck = (from a in _context.Artist
                                        select new
                                        {
                                            IdArtist = a.IdArtist,
                                            ArtistName = a.ArtistName
                                        }).ToList();

                bool NameCheck = false;
                foreach (var item in _artistNameCheck)
                {

                    if (item.ArtistName == Art.newArtist.ArtistName)
                    {
                        NameCheck = true;
                    }
                }

                
                    // This is the case where the artist already exists in the database
                    if (NameCheck == true)
                    {
                        // Set a new instance of a private artist with the properties from the database
                        var _artist = (from a in _context.Artist
                                       where a.ArtistName == Art.newArtist.ArtistName
                                       select new ArtistViewModel
                                       {
                                           ID = a.IdArtist
                                       }).Single();
                        local_artist = _artist;
                    }
                    else
                    {
                        //Since this is a new Artist: Set local artist model to the data set by the user, passed into the add paiting override method.
                        Artist artist = new Artist
                        {
                            ArtistName = Art.newArtist.ArtistName,
                            ArtistBirthYear = Art.newArtist.ArtistBirthYear,
                            ArtistDeathYear = Art.newArtist.ArtistDeathYear
                        };
                        _context.Artist.Add(artist);
                        _context.SaveChanges();

                        var _artist = (from a in _context.Artist
                                       where a.ArtistName == Art.newArtist.ArtistName
                                       select new ArtistViewModel
                                       {
                                           ID = a.IdArtist
                                       }).Single();
                        local_artist = _artist;
                    }
                

                //Set a private variable = to the artist the user just created.
                
                //Set local artwork model to the data set by the user, passed into the add paiting override method.
                ArtWork artwork = new ArtWork
                {
                    IdArtist = local_artist.ID,
                    ArtWorkDimensions = Art.ArtWork.ArtWorkDimensions,
                    ArtWorkTitle = Art.ArtWork.ArtWorkTitle,
                    ArtWorkYear = Art.ArtWork.ArtWorkYear,
                    ArtWorkMedium = Art.ArtWork.ArtWorkMedium,
                    ArtWorkNumMade = 1,
                    ArtWorkNumInventory = 1,
                    ArtWorkNumSold = 0
                };
                _context.ArtWork.Add(artwork);
                _context.SaveChanges();

                //Set a private variable = to the artwork the user just created.
                var _artWork = (from aw in _context.ArtWork
                               where aw.ArtWorkTitle == Art.ArtWork.ArtWorkTitle
                               select new
                               {
                                   IdArtWork = aw.IdArtWork,
                               }).Single();

                //Set local artpiece model to the data set by the user, passed into the AddPaiting override method.
                ArtPiece artPiece = new ArtPiece
                {
                    IdArtWork = _artWork.IdArtWork,
                    ArtPieceImage = Art.ArtPiece.ArtPieceImage,
                    ArtPieceDateCreated = Art.ArtPiece.ArtPieceDateCreated,
                    ArtPiecePrice = Art.ArtPiece.ArtPiecePrice,
                    ArtPieceSold = false,
                    ArtPieceLocation = Art.ArtPiece.ArtPieceLocation,
                    ArtPieceEditionNum = 1
                };
                _context.ArtPiece.Add(artPiece);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }
        
        //View all sold artwork, cost, and profit, total sales(week/month/year), current profit (week/month/year))
        public ActionResult Accounting()
        {
            


            var artSold = (from ap in _context.ArtPiece
                           join aw in _context.ArtWork
                           on ap.IdArtWork equals aw.IdArtWork

                           join a in _context.Artist
                           on aw.IdArtist equals a.IdArtist

                           where ap.ArtPieceSold == true
                           select new InventoryViewModel
                           {
                               ArtPiecePrice = ap.ArtPiecePrice,
                               IdArtPiece = ap.IdArtPiece,
                               Artist = a.ArtistName,
                               ArtWorkTitle = aw.ArtWorkTitle,
                               Edition = ap.ArtPieceEditionNum,
                               ArtGalleryCost = ap.ArtPiecePrice / 2
                           }).ToList();

            //Calculate gallery ytd cost/profit/total sales and put in private variables.
            decimal _cost = 0;
            decimal _profit = 0;
            decimal _totalSales = 0;

            foreach(var item in artSold)
            {
                _cost += item.ArtGalleryCost;
                _totalSales += item.ArtPiecePrice;
            }
             _profit += _totalSales - _cost;

            YTDSalesViewModel YTD = new YTDSalesViewModel
            {
                Cost = _cost,
                Profit = _profit,
                TotalSales = _totalSales
            };
            //Create a granfather VM that holds 2 ViewModels.
            SalesArtYTDViewModel SA_YTD = new SalesArtYTDViewModel
            {
                IVM = artSold,
                YTD = YTD
            };

            return View(SA_YTD);
        }

    }
}



//[HttpPost]
//public ActionResult CreatePet(Pet pet)
//{
//    if (ModelState.IsValid)
//    {
//        Pet newPet = new Models.Pet
//        {
//            PetName = pet.PetName,
//            IdVet = pet.IdVet,
//            Species = pet.Species,
//            Breed = pet.Breed
//        };
//        _context.Pet.Add(newPet);
//        _context.SaveChanges();
//        return RedirectToAction("Index");
//    }

//    return View();
//}