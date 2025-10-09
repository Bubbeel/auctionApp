namespace WebApp_dotNet.Core;

public class Auction
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public DateTime CreatedDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public string Username { get; set; }
    
    public string Description { get; set; }
    public DateTime descriptionLastUpdated;
    private DateTime DescriptionLastUpdated{get => descriptionLastUpdated;}

    public int StartingPrice { get; set; }
    public bool IsCompleted { get; set; }

    private List<Bid> _bids = new List<Bid>();
    public IEnumerable<Bid> Bid => _bids;

    public Auction(string title, string username)
    {
        Title = title;
        CreatedDate = DateTime.Now;
        Username = username;
    }

    public Auction(int id, string title, string username, DateTime createdDate)
    {
        Id = id;
        Title = title;
        CreatedDate = createdDate;
        Username = username;
    }
    // public bool IsCompleted()
    // {
    //     
    // }
}