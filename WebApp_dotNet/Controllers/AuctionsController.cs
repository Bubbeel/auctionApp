using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp_dotNet.Core;
using WebApp_dotNet.Core.Interfaces;
using WebApp_dotNet.Filters;
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
                auction.AuctionIsFinished();
                auctionVms.Add(AuctionVm.FromAuction(auction));
            }
            return View(auctionVms);
        }
        
        public ActionResult IndexAll()
        {
            List<Auction> auctions = _auctionService.GetAllNotCompleted();
            List<AuctionVm> auctionVms = new List<AuctionVm>();
            foreach (Auction auction in auctions)
            {
                auction.AuctionIsFinished();
                auctionVms.Add(AuctionVm.FromAuction(auction));
            }
            return View(auctionVms);
        }
        
        public ActionResult IndexBidFor()
        {
            List<Auction> auctions = _auctionService.GetAll();
            List<AuctionVm> auctionVms = new List<AuctionVm>();
            foreach (Auction auction in auctions)
            {
                auction.AuctionIsFinished();
                foreach (Bid bid in auction.Bids)
                {
                    if (bid.Username == User.Identity.Name)
                    {
                        if (!auctionVms.Any(a => a.Id == auction.Id))
                        {
                            auctionVms.Add(AuctionVm.FromAuction(auction));
                        }
                    }
                }
            }
            return View(auctionVms);
        }

        // GET: AuctionsController/Details/5
        public ActionResult Details(int id)
        {
            Auction auction = _auctionService.GetById(id);
            if (auction == null) return BadRequest(); //HTTP 400
            auction.AuctionIsFinished();
            AuctionDetailsVm detailsVM = AuctionDetailsVm.FromAuction(auction);
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
                    return RedirectToAction("IndexAll");
                }
                return View(createAuctionVms);
            }
            catch //data exception?
            {
                return View(createAuctionVms);
            }
        }

        [ServiceFilter(typeof(AuctionEndedFilter))]
        public ActionResult AddBid(int auctionId, int currentPrice)
        {
            CreateBidVms createBidVms = new CreateBidVms();
            createBidVms.AuctionId = auctionId;
            createBidVms.CurrPrice = currentPrice;
            return View(createBidVms);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(AuctionEndedFilter))]
        public ActionResult AddBid(CreateBidVms createBidVms)
        {
            try
            {
                if (ModelState.IsValid)
                { 
                    _auctionService.AddBid(createBidVms.AuctionId, User.Identity.Name, createBidVms.Amount);
                    return RedirectToAction("Details", new {id = createBidVms.AuctionId});
                }
            }
            catch
            {
                return View(createBidVms);
            }
            return View(createBidVms);
        }

        // GET: AuctionsController/Edit/5
        [ServiceFilter(typeof(AuctionEndedFilter))]
        public ActionResult Edit(int id)
        {
            try
            {
                var auction = _auctionService.GetById(id, User.Identity.Name);
                if (auction == null) return NotFound();

                // Pass only what's editable (the description)
                var vm = new EditAuctionVm
                {
                    Id = auction.Id,
                    Title = auction.Title,
                    Description = auction.Description
                };

                return View(vm);
            }
            catch
            {
                return NotFound();
            }
        }

        // POST: AuctionsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(AuctionEndedFilter))]
        public ActionResult Edit(EditAuctionVm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            try
            {
                _auctionService.EditDescription(vm.Id, User.Identity.Name, vm.Description);
                return RedirectToAction(nameof(Details), new { id = vm.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
        }

        // GET: AuctionsController/Delete/5
        [ServiceFilter(typeof(AuctionEndedFilter))]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AuctionsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(AuctionEndedFilter))]
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
