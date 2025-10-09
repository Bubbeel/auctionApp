using System.Runtime.Serialization;
using WebApp_dotNet.Core.Interfaces;

namespace WebApp_dotNet.Core;

public class MockAuctionService : IAuctionService
{
    public List<Auction> GetAllByUserName(string username)
    {
        return _auctions;
    }

    public Auction GetById(int auctionId)
    {
        return _auctions.Find(a => a.Id == auctionId);
    }

    public void Add(string username, string title)
    {
        throw new NotImplementedException("Not implemented");
    }
    private static readonly List<Auction> _auctions = new();

    static MockAuctionService()
    {
        Auction a1 = new Auction(1,  "Babushka", "Bob", DateTime.Now);
        Auction a2 = new  Auction(2,  "Clair", "Alice", DateTime.Now);
        _auctions.Add(a1);
        _auctions.Add(a2);
    }
    
}