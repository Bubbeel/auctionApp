using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApp_dotNet.Core;

namespace WebApp_dotNet.Persistence;

public class bidDb
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(256)]
    public String Description { get; set; }
    
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime LastUpdated { get; set; }
    
    [Required]
    public Status status { get; set; }
    
    //FK and navigation property
    [ForeignKey("ProjectId")]
    public AuctionDb AuctionDb { get; set; }
    
    public int ProjectId { get; set; }
    
}