namespace WebApp_dotNet.Core.Interfaces;

public interface IAuctionService : IAuctionPersistence
{
    
    List<Auction> GetAllByUserName(string username);
    
    Auction GetById(int auctionId, string username);

    void Add(string username, string title, string description, int startPrice, DateTime endDate);
    
    void AddBid(int auctionId, string username, int amount);
}