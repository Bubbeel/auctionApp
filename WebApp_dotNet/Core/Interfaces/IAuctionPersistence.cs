namespace WebApp_dotNet.Core.Interfaces;

public interface IAuctionPersistence
{
    List<Auction> GetAllByUserName(string userName);
    List<Auction> GetAll();
    public List<Auction> GetAllNotCompleted();
    List<Auction> GetAllCompleted();
    Auction GetById(int id, String  userName);
    public Auction GetByIdNotCompleted(int id);
    Auction GetById(int id);
    void SaveAuction(Auction auction);
    void UpdateAuctionDescription(int auctionId, string newDescription);
    public void UpdateCurrentPrice(Auction auction);
    void SaveBid(Bid bid);
} 