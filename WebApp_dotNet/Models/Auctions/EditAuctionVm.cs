using System.ComponentModel.DataAnnotations;

namespace WebApp_dotNet.Models.Auctions
{
    public class EditAuctionVm
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}