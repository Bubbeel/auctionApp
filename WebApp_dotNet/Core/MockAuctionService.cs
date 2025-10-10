using System.Runtime.Serialization;
using WebApp_dotNet.Core.Interfaces;

namespace WebApp_dotNet.Core;

public class MockAuctionService : IAuctionService
{
    public List<Auction> GetAllByUserName(string username)
    {
        return _auctions;
    }

    public Auction GetById(int auctionId, string username)
    {
        return _auctions.Find(a => a.Id == auctionId && a.Username == username);
    }

    public void Add(string username, string title)
    {
        throw new NotImplementedException("Not implemented");
    }
    private static readonly List<Auction> _auctions = new();

    static MockAuctionService()
    {
        Auction a1 = new Auction(1,  "Clair", "Bob", DateTime.Now);
        Auction a2 = new  Auction(2,  "Obscure", "Bob", DateTime.Now);
        a1.AddBid(new Core.Bid(1, "Renoir", 120));
        a2.AddBid(new Core.Bid(1, "Bob", 200));
        a2.AddBid(new Core.Bid(2, "Alice", 300));
        _auctions.Add(a1);
        _auctions.Add(a2);
    }
    
}