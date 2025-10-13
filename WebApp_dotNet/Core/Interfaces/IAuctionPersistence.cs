namespace WebApp_dotNet.Core.Interfaces;

public interface IAuctionPersistence
{
    List<Auction> GetAllByUserName(string userName);
    List<Auction> GetAll();
    Auction GetById(int id, String  userName);
    Auction GetById(int id);
    void SaveAuction(Auction auction);
    void SaveBid(Bid bid);
}