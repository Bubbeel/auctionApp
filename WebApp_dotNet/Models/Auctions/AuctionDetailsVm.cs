using System.ComponentModel.DataAnnotations;
using WebApp_dotNet.Core;

namespace WebApp_dotNet.Models.Auctions;

public class AuctionDetailsVm
{
    [ScaffoldColumn(false)]
    public int Id { get; set; }
    [Display(Name = "Title")]
    public string Title { get; set; }
    [Display(Name = "Description")]
    public string Description { get; set; }
    [Display(Name = "Starting Price")]
    public string StartingPrice { get; set; }
    [Display(Name = "Created date")]
    public DateTime CreatedDate { get; set; }
    [Display(Name = "Created by")]
    public string Username { get; set; }
    [Display(Name = "End date")]
    public DateTime EndDate { get; set; }
    public bool IsCompleted { get; set; }

    public List<BidVm> BidVms { get; set; } = new();

    public static AuctionDetailsVm FromAuction(Auction auction)
    {
        var detailsVM = new AuctionDetailsVm()
        {
            Id = auction.Id,
            Title = auction.Title,
            Description = auction.Description,
            CreatedDate = auction.CreatedDate,
            StartingPrice = auction.StartingPrice.ToString(),
            Username = auction.Username,
            EndDate = auction.EndDate,
            IsCompleted = auction.IsCompleted,

        };
        foreach (var bid in auction.Bids)
        {
            detailsVM.BidVms.Add(BidVm.FromBid(bid));
        }
        return detailsVM;
    }
}