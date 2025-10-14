namespace WebApp_dotNet.Core;

public class Bid
{
    public int  Id { get; set; }
    public string Username { get; set; }
    public DateTime DateAdded { get; set; }
    public int Amount {get; set;}
    
    public int AuctionId { get; set; }

    public Bid(string username, int amount, int auctionId)
    {
        Username = username;
        DateAdded = DateTime.Now;
        Amount = amount;
        AuctionId = auctionId;
    }
}