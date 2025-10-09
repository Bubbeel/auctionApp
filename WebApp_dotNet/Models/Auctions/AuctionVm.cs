using System.ComponentModel.DataAnnotations;
using WebApp_dotNet.Core;

namespace WebApp_dotNet.Models.Auctions;

public class AuctionVm
{
    [ScaffoldColumn(false)]
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    [Display(Name = "Created date")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
    public DateTime CreatedDate { get; set; }
    
    public bool IsCompleted { get; set; }

    public static AuctionVm FromAuction(Auction auction)
    {
        return new AuctionVm()
        {
            Id = auction.Id,
            Title = auction.Title,
            CreatedDate = auction.CreatedDate,
            IsCompleted = auction.IsCompleted
        };
    }
}