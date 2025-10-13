using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApp_dotNet.Core;

namespace WebApp_dotNet.Persistence;

public class BidDb
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(128)]
    public string Username { get; set; }
    
    [Required]
    public int Amount { get; set; }
    
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime DateAdded { get; set; }
    
    //FK and navigation property
    [ForeignKey("AuctionId")]
    public AuctionDb AuctionDb { get; set; }
    
    public int AuctionId { get; set; }
    
}