using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp_dotNet.Core;
using WebApp_dotNet.Core.Interfaces;
using WebApp_dotNet.Models.Auctions;

namespace WebApp_dotNet.Controllers
{
    [Authorize]
    public class AuctionsController : Controller
    {
        
        private IAuctionService _auctionService;

        public AuctionsController(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }
            
        // GET: AuctionsController
        public ActionResult Index()
        {
            List<Auction> auctions = _auctionService.GetAllByUserName(User.Identity.Name);
            List<AuctionVm> auctionVms = new List<AuctionVm>();
            foreach (Auction auction in auctions)
            {
                auctionVms.Add(AuctionVm.FromAuction(auction));
            }
            return View(auctionVms);
        }
        
        public ActionResult IndexAll()
        {
            List<Auction> auctions = _auctionService.GetAll();
            List<AuctionVm> auctionVms = new List<AuctionVm>();
            foreach (Auction auction in auctions)
            {
                auctionVms.Add(AuctionVm.FromAuction(auction));
            }
            return View(auctionVms);
        }

        // GET: AuctionsController/Details/5
        public ActionResult Details(int id)
        {
            Auction auction = _auctionService.GetById(id, User.Identity.Name);
            if (auction == null) return BadRequest(); //HTTP 400
            
            AuctionDetailsVm detailsVM = AuctionDetailsVm.FromAuction(auction);
            return View(detailsVM);
        }

        public ActionResult DetailsNonCreator(int id)
        {
            Auction auction = _auctionService.GetById(id);
            if (auction == null) return BadRequest();
            
            AuctionDetailsVm  detailsVM = AuctionDetailsVm.FromAuction(auction);
            return View(detailsVM);
        }

        // GET: AuctionsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuctionsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateAuctionVms createAuctionVms)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string title = createAuctionVms.Title;
                    string description = createAuctionVms.Description;
                    int startPrice = createAuctionVms.StartingPrice;
                    DateTime endDate = createAuctionVms.EndDate;
                    string username = User.Identity.Name;
                    _auctionService.Add(username, title, description, startPrice, endDate);
                    return RedirectToAction("Index");
                }
                return View(createAuctionVms);
            }
            catch //data exception?
            {
                return View(createAuctionVms);
            }
        }

        public ActionResult AddBid()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBid(int bidAmount, int currentPrice)
        {
            if (bidAmount < currentPrice)
            {
                return RedirectToAction("Index");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    
                }
            }
            catch
            {
                return View("Error");
            }
            
            return View();
        }

        // GET: AuctionsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AuctionsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuctionsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AuctionsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
