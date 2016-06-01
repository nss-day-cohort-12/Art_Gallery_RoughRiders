﻿using Art_Gallery_RoughRiders.Models;
using Art_Gallery_RoughRiders.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                            ArtGalleryCost = ap.ArtPiecePrice / 2,
                            IMG = ap.ArtPieceImage,
                            isSold = ap.ArtPieceSold ? "Sold" : "",
                            boolIsSold = ap.ArtPieceSold
                          }).ToList();

      return View(allArtPieces);

    }

    [HttpGet]
    public ActionResult AddPainting()
    {
      var ExistingArtists = (from a in _context.Artist
                             select a).ToList();
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

        return RedirectToAction("Inventory");
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

      foreach (var item in artSold)
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

    public ActionResult AgentRoster()
    {
      var AgentList = (from a in _context.Agent
                       select new AgentRosterViewModel
                       {
                         IdAgent = a.IdAgent,
                         AgentName = a.AgentFirstName + " " + a.AgentLastName
                       }).ToList();
      return View(AgentList);
    }

    public ActionResult AgentInvoiceDetails(int agentId)
    {

      var invoiceDetails = (from c in _context.Customer
                            join i in _context.Invoice
                            on c.IdCustomer equals i.IdCustomer

                            join ag in _context.Agent
                            on i.IdAgent equals ag.IdAgent

                            join iap in _context.InvoiceArtPiece
                            on i.IdInvoice equals iap.IdInvoice

                            join ap in _context.ArtPiece
                            on iap.IdArtPiece equals ap.IdArtPiece

                            join aw in _context.ArtWork
                            on ap.IdArtWork equals aw.IdArtWork

                            join ar in _context.Artist
                            on aw.IdArtist equals ar.IdArtist

                            where ag.IdAgent == agentId
                            group new
                            {
                              i.IdInvoice,
                              ag.AgentFirstName,
                              ag.AgentLastName,
                              c.CustomerFirstName,
                              c.CustomerLastName,
                              c.CustomerAddress,
                              c.CustomerPhoneNumber,
                              i.PaymentMethod,
                              ap.IdArtPiece,
                              aw.ArtWorkTitle,
                              ar.ArtistName

                            }
                            by i.IdInvoice into newGroup
                            select newGroup).ToList();


      // Create a father VM that will cointains a list of tickets
      InvoiceListViewModel Allinvoices = new InvoiceListViewModel();
      // Create a local VM that IS a list of tickets
      List<InvoiceViewModel> _localInvoices = new List<InvoiceViewModel>();
      decimal TotalSales = new decimal();
      foreach (var item in invoiceDetails)
      {  //create a new view model
        InvoiceViewModel ivm = new InvoiceViewModel();
        ivm.IdInvoice = item.First().IdInvoice;
        ivm.agentName = item.First().AgentFirstName + " " + item.First().AgentLastName;
        ivm.customerName = item.First().CustomerFirstName + " " + item.First().CustomerLastName;
        ivm.customerAddress = item.First().CustomerAddress;
        ivm.customerPhoneNumber = item.First().CustomerPhoneNumber;
        ivm.paymentMethod = item.First().PaymentMethod;
        decimal invoiceTotalSales = new decimal();
        List<APinInvoice> art = new List<APinInvoice>();
        // Get acces to each painting sold in the invoice
        foreach (var curInvoice in item)
        {  //add painting to the view model of each invoice
          APinInvoice singleAP = new APinInvoice();
          singleAP.Artist = curInvoice.ArtistName;
          singleAP.ArtWorkTitle = curInvoice.ArtWorkTitle;
          singleAP.ArtPiecePrice = curInvoice.IdArtPiece;
          art.Add(singleAP);
          invoiceTotalSales += singleAP.ArtPiecePrice;
          //ivm.IdartPiece.Add(curInvoice.IdArtPiece);
        }
        ivm.invoiceTotalSales = invoiceTotalSales;
        TotalSales += invoiceTotalSales;
        ivm.ArtWork = art;
        _localInvoices.Add(ivm);
      }
      Allinvoices.TotalSales = TotalSales;
      Allinvoices.InvoiceModels = _localInvoices;
      return View(Allinvoices);
    }









        
        public ActionResult SignUp(int artId)
        {
            IEnumerable<SelectListItem> selectList =
            from c in _context.Customer
            select new SelectListItem
            {
                Text = c.CustomerFirstName + " " + c.CustomerLastName,
                Value = c.IdCustomer.ToString()
            };

            CurrentCustomersViewModel artWorkList = new CurrentCustomersViewModel
            {
                CustList = selectList,
                artId = artId
            };
            return View(artWorkList);
        }

        [HttpPost]
        public ActionResult SignUp(CurrentCustomersViewModel ccvm)
        {
            var artId = ccvm.artId;
            // Get access to the piece sold
            var PieceSold = (from ap in _context.ArtPiece

                             join aw in _context.ArtWork
                             on ap.IdArtWork equals aw.IdArtWork

                             where ap.IdArtPiece == ccvm.artId
                             select new
                             {
                                 artID = ap.IdArtPiece,
                                 Title = aw.ArtWorkTitle
                             }).Single();

            // Get access to the customer and agent involved in the sale
            var custAndAgentID = (from c in _context.Customer

                                join ag in _context.Agent
                                on c.IdAgent equals ag.IdAgent

                                where c.IdCustomer == ccvm.SelectedCustID
                                select new CustAndAgentViewModel
                                {
                                    cust = c.IdCustomer,
                                    agent = ag.IdAgent,
                                    PaymentMethod = "Visa",
                                    ShippingAddress = c.CustomerAddress,
                                    PieceSold = PieceSold.Title
                                }).Single();

            // Create a new Invoice
            Invoice _invoice = new Invoice
            {
                IdCustomer = ccvm.SelectedCustID,
                IdAgent = custAndAgentID.agent,
                PaymentMethod = custAndAgentID.PaymentMethod,
                ShippingAddress = custAndAgentID.ShippingAddress,
                PieceSold = custAndAgentID.PieceSold
            };
            _context.Invoice.Add(_invoice);
            _context.SaveChanges();

            // Get access to the invoice you just created to connect it to the InvoiceArtPiece and InvoiceLineItem Tables
            var InvoiceId = (from i in _context.Invoice
                             orderby i.IdInvoice
                             select new InvoiceIdViewModel
                             {
                                 ID = i.IdInvoice
                             }).ToList().Last();

            InvoiceArtPiece iap = new InvoiceArtPiece
            {
                IdInvoice = InvoiceId.ID,
                IdArtPiece = ccvm.artId
            };
            _context.InvoiceArtPiece.Add(iap);
            _context.SaveChanges();

            // Access the ArtPiece to update that it has been sold

            var _ap = (from ap in _context.ArtPiece
                       where ap.IdArtPiece == ccvm.artId
                       select new ArtPieceUpdateViewModel
                       {
                           IdArtPiece = ap.IdArtPiece,
                           ArtPieceSold = true,
                           ArtPieceDateCreated = ap.ArtPieceDateCreated,
                           ArtPieceEditionNum = ap.ArtPieceEditionNum,
                           ArtPieceImage = ap.ArtPieceImage,
                           ArtPieceLocation = ap.ArtPieceLocation,
                           ArtPiecePrice = ap.ArtPiecePrice,
                           IdArtWork = ap.IdArtWork
                       }).Single();
            // Create a new local ArtPiece to send and update the database table
            ArtPiece AP = new ArtPiece
            {
                ArtPieceDateCreated = _ap.ArtPieceDateCreated,
                ArtPieceEditionNum = _ap.ArtPieceEditionNum,
                ArtPieceImage = _ap.ArtPieceImage,
                ArtPieceLocation = _ap.ArtPieceLocation,
                ArtPiecePrice = _ap.ArtPiecePrice,
                ArtPieceSold = _ap.ArtPieceSold,
                IdArtPiece = _ap.IdArtPiece,
                IdArtWork = _ap.IdArtWork
            };

         
            //_context.Entry(AP).State = EntityState.Modified;
            _context.ArtPiece.Attach(AP);
            _context.Entry(AP).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Inventory");
        }
    }
}