using System.Data;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Quic;
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

    public List<Auction> GetAll()
    {
        List<Auction> auctions = _auctionPersistence.GetAll();
        return auctions;
    }

    public Auction GetById(int auctionId, string username)
    {
        Auction auction = _auctionPersistence.GetById(auctionId, username);
        if (auction == null) throw new DataException("Auction not found");
        return auction;
    }

    public Auction GetById(int auctionId)
    {
        Auction auction = _auctionPersistence.GetById(auctionId);
        if (auction == null) throw new DataException("Auction not found");
        return auction;
    }
    
    public void Add(string username, string title, string description, int startPrice, DateTime endDate)
    {
        if(username == null) throw new ArgumentNullException(nameof(username));
        if (title == null || title.Length > 128) throw new ArgumentNullException(nameof(title));

        Auction auction = new Auction(title, username, description, startPrice, endDate);
        _auctionPersistence.SaveAuction(auction);
    }

    public void AddBid(int auctionId, string username, int amount)
    {
        if (username == null) throw new ArgumentNullException(nameof(username));
        if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount));

        Bid bid = new Bid(username, amount, auctionId);
        Auction auction = GetById(auctionId);
        if (auction.CurrentPrice < amount)
        {
            auction.CurrentPrice = amount;   
            _auctionPersistence.SaveBid(bid);
            _auctionPersistence.UpdateCurrentPrice(auction);
        }
        else
        {
            Console.Out.WriteLine("Invalid Bid");
            throw new DataException("Invalid bid");
        }
    }

    public void EditDescription(int auctionId, string username, string newDescription)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentNullException(nameof(username));
        if (string.IsNullOrWhiteSpace(newDescription))
            throw new ArgumentException("Description cannot be empty.", nameof(newDescription));
        var auction = _auctionPersistence.GetById(auctionId);
        if (auction == null)
            throw new DataException("Auction not found");
        if (!auction.Username.Equals(username, StringComparison.OrdinalIgnoreCase))
            throw new UnauthorizedAccessException("You can only edit your own auctions.");
        
        _auctionPersistence.UpdateAuctionDescription(auctionId, newDescription);
    }
    

    private static readonly List<Auction> _auctions = new();

    public void UpdateAuctionDescription(int auctionId, string newDescription)
    {
    }

    public void SaveAuction(Auction auction)
    {
        throw new NotImplementedException();
    }

    public void SaveBid(Bid bid)
    {
        throw new NotImplementedException();
    }

    public void UpdateCurrentPrice(Auction auction)
    {
        throw new NotImplementedException();
    }
}