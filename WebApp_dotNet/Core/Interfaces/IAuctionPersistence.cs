namespace WebApp_dotNet.Core.Interfaces;

public interface IAuctionPersistence
{
    List<Auction> GetAllByUserName(string userName);
    Auction GetById(int id, String  userName);
    void Save(Auction auction);
}