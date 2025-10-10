using System.ComponentModel.DataAnnotations;

namespace WebApp_dotNet.Persistence;

public class AuctionDb
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(128)]
    public string Title { get; set; }
    
    [Required]
    public String UserName { get; set; }
    
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime CreatedDate { get; set; }
    
    //navigation property
    public List<bidDb> BidDbs { get; set; } = new List<bidDb>();
}