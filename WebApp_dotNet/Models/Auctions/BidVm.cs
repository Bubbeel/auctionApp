using System.ComponentModel.DataAnnotations;
using WebApp_dotNet.Core;

namespace WebApp_dotNet.Models.Auctions;

public class BidVm
{
    [ScaffoldColumn((false))] 
    public int Id { get; set; }

    public string Username { get; set; }

    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
    public DateTime DateAdded { get; set; }
    
    public int Amount { get; set; }
    
    public int CurrAmount { get; set; }

    public int AuctionId { get; set; }

    public static BidVm FromBid(Bid bid)
    {
        return new BidVm()
        {
            Id = bid.Id,
            Username = bid.Username,
            DateAdded = bid.DateAdded,
            Amount = bid.Amount
        };
    }
}
