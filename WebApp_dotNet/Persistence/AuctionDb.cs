using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic.CompilerServices;

namespace WebApp_dotNet.Persistence;

public class AuctionDb
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(128)]
    public string Title { get; set; }
    
    [Required]
    public string UserName { get; set; }
    
    [Required]
    [MaxLength(512)]
    public string Description { get; set; }
    
    [Required]
    [DataType(DataType.Currency)]
    public int StartingPrice { get; set; }
    
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime CreatedDate { get; set; }
    
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime EndDate { get; set; }
    
    //navigation property
    public List<BidDb> BidDbs { get; set; } = new List<BidDb>();
}