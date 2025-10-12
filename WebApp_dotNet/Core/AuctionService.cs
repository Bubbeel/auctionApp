using System.Data;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore.Query.Internal;
using WebApp_dotNet.Core.Interfaces;

namespace WebApp_dotNet.Core;

public class AuctionService : IAuctionService
{
    private readonly IAuctionPersistence _auctionPersistence;

    public AuctionService(IAuctionPersistence auctionPersistence)
    {
        _auctionPersistence = auctionPersistence;
    }
    public List<Auction> GetAllByUserName(string username)
    {
        List<Auction> auctions = _auctionPersistence.GetAllByUserName(username);
        return auctions;
    }

    public Auction GetById(int auctionId, string username)
    {
        Auction auction = _auctionPersistence.GetById(auctionId, username);
        if (auction == null) throw new DataException("Auction not found");
        return auction;
    }

    public void Add(string username, string title, string description, int startPrice, DateTime endDate)
    {
        if(username == null) throw new ArgumentNullException(nameof(username));
        if (title == null || title.Length > 128) throw new ArgumentNullException(nameof(title));

        Auction auction = new Auction(title, username, description, startPrice, endDate);
        _auctionPersistence.Save(auction);
    }
    private static readonly List<Auction> _auctions = new();

    static AuctionService()
    {
        // Auction a1 = new Auction(1,  "Clair", "Bob", DateTime.Now);
        // Auction a2 = new  Auction(2,  "Obscure", "Bob", DateTime.Now);
        // a1.AddBid(new Core.Bid(1, "Renoir", 120));
        // a2.AddBid(new Core.Bid(1, "Bob", 200));
        // a2.AddBid(new Core.Bid(2, "Alice", 300));
        // _auctions.Add(a1);
        // _auctions.Add(a2);
    }

    public void Save(Auction auction)
    {
        
    }
    
}