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
                               IdArtPiece = ap.IdArtPiece,
                               Artist = ar.ArtistName,
                               ArtWorkTitle = aw.ArtWorkTitle,
                               Edition = ap.ArtPieceEditionNum
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
            if (ModelState.IsValid)
            {
                //Set local artist model to the data set by the user, passed into the add paiting override method.
                Artist artist = new Artist
                {
                    ArtistName = Art.newArtist.ArtistName,
                    ArtistBirthYear = Art.newArtist.ArtistBirthYear,
                    ArtistDeathYear = Art.newArtist.ArtistDeathYear
                };
                _context.Artist.Add(artist);
                _context.SaveChanges();

                //Set a private variable = to the artist the user just created.
                var _artist = (from a in _context.Artist
                               where a.ArtistName == Art.newArtist.ArtistName
                               select new Artist
                               {
                                   IdArtist = a.IdArtist,
                                   ArtistName = a.ArtistName,
                                   ArtistBirthYear = a.ArtistBirthYear,
                                   ArtistDeathYear = a.ArtistDeathYear
                               }).Single();
                
                //Set local artwork model to the data set by the user, passed into the add paiting override method.
                ArtWork artwork = new ArtWork
                {
                    IdArtist = _artist.IdArtist,
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
                               select new ArtWork
                               {
                                   IdArtWork = aw.IdArtWork,
                                   IdArtist = aw.IdArtist,
                                   ArtWorkTitle = aw.ArtWorkTitle,
                                   ArtWorkYear = aw.ArtWorkYear,
                                   ArtWorkMedium = aw.ArtWorkMedium,
                                   ArtWorkDimensions = aw.ArtWorkDimensions,
                                   ArtWorkNumMade = aw.ArtWorkNumMade,
                                   ArtWorkNumInventory = aw.ArtWorkNumInventory,
                                   ArtWorkNumSold = aw.ArtWorkNumSold
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