using System.ComponentModel.DataAnnotations;

namespace WebApp_dotNet.Models.Auctions;

public class CreateBidVms
{
    [Required]
    [Range(0, int.MaxValue)]
    public int Amount { get; set; }
    
    [Required]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]    
    public DateTime DateAdded { get; set; }
    
    public int CurrPrice { get; set; }
    
    public int AuctionId { get; set; }
}