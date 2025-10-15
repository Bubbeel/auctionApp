using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApp_dotNet.Core.Interfaces;

namespace WebApp_dotNet.Filters
{
    public class AuctionEndedFilter : ActionFilterAttribute
    {
        private readonly IAuctionService _auctionService;

        public AuctionEndedFilter(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.TryGetValue("id", out var idObj) && idObj is int auctionId)
            {
                var auction = _auctionService.GetById(auctionId);
                if (auction != null && auction.EndDate <= DateTime.Now)
                {
                    context.Result = new RedirectToActionResult("Details", "Auctions", new { id = auctionId });
                    context.HttpContext.Items["AuctionEndedMessage"] = "This auction has already ended.";
                }
            }

            base.OnActionExecuting(context);
        }
    }
}