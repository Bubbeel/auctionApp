using System.ComponentModel.DataAnnotations;

namespace WebApp_dotNet.Models.Auctions;

public class CreateBidVms
{
    [Required]
    [Range(100, int.MaxValue)]
    public int Amount { get; set; }
}