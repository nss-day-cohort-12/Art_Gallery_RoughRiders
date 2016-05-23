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
                               ArtWorkTitle = aw.ArtWorkTitle,
                               ArtistName = ar.ArtistName
                           }).ToList();            

            return View(artWork);
        }
    }
}