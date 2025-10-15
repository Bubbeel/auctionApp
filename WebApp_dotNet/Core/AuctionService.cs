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
    
    public List<Auction> GetAllNotCompleted()
    {
        List<Auction> auctions = _auctionPersistence.GetAllNotCompleted();
        return auctions;
    }
    
    public List<Auction> GetAllCompleted()
    {
        List<Auction> auctions = _auctionPersistence.GetAllCompleted();
        return auctions;
    }

    public Auction GetById(int auctionId, string username)
    {
        Auction auction = _auctionPersistence.GetById(auctionId, username);
        if (auction == null) throw new DataException("Auction not found");
        return auction;
    }

    public Auction GetByIdNotCompleted(int auctionId)
    {
        Auction auction = _auctionPersistence.GetByIdNotCompleted(auctionId);
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
        if (username != auction.Username)
        {
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
        else
        {
            Console.Out.WriteLine("You can't bid for your own auction");
            throw new DataException("You can't bid for your own auction");
        }
    }
    
    private static readonly List<Auction> _auctions = new();

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