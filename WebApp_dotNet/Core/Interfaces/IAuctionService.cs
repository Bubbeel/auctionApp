namespace WebApp_dotNet.Core.Interfaces;

public interface IAuctionService
{
    
    List<Auction> GetAllByUserName(string username);
    
    Auction GetById(int auctionId);

    void Add(string username, string title);
}