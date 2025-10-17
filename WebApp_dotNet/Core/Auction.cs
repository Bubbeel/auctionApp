namespace WebApp_dotNet.Core;

public class Auction
{
    public int Id { get; set; }

    public string Title { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime EndDate { get; set; }

    public string Username { get; set; }

    public string Description { get; set; }

    public int StartingPrice { get; set; }
    public int CurrentPrice { get; set; }
    
    public bool IsCompleted => DateTime.Now >= EndDate;

    private readonly List<Bid> _bids = new();
    public IEnumerable<Bid> Bids => _bids;

    public Auction(string title, string username)
    {
        Title = title;
        CreatedDate = DateTime.Now;
        Username = username;
    }

    public Auction(string title, string username, string description, int startingPrice, DateTime endDate)
    {
        Title = title;
        CreatedDate = DateTime.Now;
        Username = username;
        Description = description;
        StartingPrice = startingPrice;
        CurrentPrice = startingPrice;
        EndDate = endDate;
    }

    public void AddBid(Bid newBid)
    {
        _bids.Add(newBid);
    }
    
    public bool AuctionIsFinished()
    {
        return DateTime.Now >= EndDate;
    }
}