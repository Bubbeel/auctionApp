using System.ComponentModel.DataAnnotations;

namespace WebApp_dotNet.Models.Auctions;

public class CreateNewAuctions
{
    [Required]
    [StringLength(128, ErrorMessage = "Max 128 Characters!")]
    public string Title { get; set; }
    
    
}