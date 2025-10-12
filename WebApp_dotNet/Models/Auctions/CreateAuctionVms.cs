using System.ComponentModel.DataAnnotations;

namespace WebApp_dotNet.Models.Auctions;

public class CreateAuctionVms
{
    [Required]
    [StringLength(128, ErrorMessage = "Max 128 Characters!")]
    public string Title { get; set; }

    [Required]
    //[Editable()]
    [StringLength(512, ErrorMessage = "Max 512 Characters!")]
    public string Description { get; set; }

    [Required]
    [Range(10, 1000000,  ErrorMessage = "Price must be between {1} and {2}!")]
    public int StartingPrice { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

}