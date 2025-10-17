using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApp_dotNet.Core;
using WebApp_dotNet.Core.Interfaces;

namespace WebApp_dotNet.Persistence;

public class MySqlAuctionPersitence : IAuctionPersistence
{
    private readonly AuctionDbContext _dbContext;
    private readonly IMapper _mapper;

    public MySqlAuctionPersitence(AuctionDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public List<Auction> GetAllByUserName(string userName)
    {
        var auctionDbs = _dbContext.AuctionDbs.Where(a => a.UserName == userName).ToList();
        List<Auction> results = new List<Auction>();
        foreach (AuctionDb adb in auctionDbs)
        {
            Auction auction =  _mapper.Map<Auction>(adb);
            var bidsDb = _dbContext.BidDbs.Where(b => b.AuctionId == auction.Id).OrderByDescending(b => b.Amount).ToList();
            foreach (var bidDb in bidsDb)
            {
                Bid bid =  _mapper.Map<Bid>(bidDb);
                auction.AddBid(bid);
            }
            results.Add(auction);
        }
        return results;
    }

    public List<Auction> GetAll()
    {
        var auctionDbs = _dbContext.AuctionDbs.ToList();
        List<Auction> results = new List<Auction>();
        foreach (AuctionDb adb in auctionDbs)
        {
            Auction auction = _mapper.Map<Auction>(adb);
            var bidsDb = _dbContext.BidDbs.Where(b => b.AuctionId == auction.Id).OrderByDescending(b => b.Amount).ToList();
            foreach (var bidDb in bidsDb)
            {
                Bid bid =  _mapper.Map<Bid>(bidDb);
                auction.AddBid(bid);
            }
            results.Add(auction);
        }
        return results;
    }
    public List<Auction> GetAllNotCompleted()
    {
        var auctionDbs = _dbContext.AuctionDbs.Where(a => a.EndDate > DateTime.Now).ToList();
        List<Auction> results = new List<Auction>();
        foreach (AuctionDb adb in auctionDbs)
        {
            Auction auction = _mapper.Map<Auction>(adb);
            var bidsDb = _dbContext.BidDbs.Where(b => b.AuctionId == auction.Id).OrderByDescending(b => b.Amount).ToList();
            foreach (var bidDb in bidsDb)
            {
                Bid bid =  _mapper.Map<Bid>(bidDb);
                auction.AddBid(bid);
            }
            results.Add(auction);
        }
        return results;
    }
    
    public List<Auction> GetAllCompleted()
    {
        var auctionDbs = _dbContext.AuctionDbs.Where(a => a.EndDate <= DateTime.Now).ToList();
        List<Auction> results = new List<Auction>();
        foreach (AuctionDb adb in auctionDbs)
        {
            Auction auction = _mapper.Map<Auction>(adb);
            var bidsDb = _dbContext.BidDbs.Where(b => b.AuctionId == auction.Id).OrderByDescending(b => b.Amount).ToList();
            foreach (var bidDb in bidsDb)
            {
                Bid bid =  _mapper.Map<Bid>(bidDb);
                auction.AddBid(bid);
            }
            results.Add(auction);
        }
        return results;
    }

    public Auction GetById(int id, String userName) 
    {
        AuctionDb auctionDb = _dbContext.AuctionDbs.Where(a => a.Id == id && a.UserName.Equals(userName) && a.EndDate > DateTime.Now).Include(a => a.BidDbs).FirstOrDefault();
        if (auctionDb == null) throw new Exception("Auction not found");
        
        Auction auction = _mapper.Map<Auction>(auctionDb);
        foreach (var bidDb in auctionDb.BidDbs)
        {
            Bid bid =  _mapper.Map<Bid>(bidDb);
            auction.AddBid(bid);
        }
        return auction;
    }
    
    public Auction GetByIdNotCompleted(int id) 
    {
        AuctionDb auctionDb = _dbContext.AuctionDbs.Where(a => a.Id == id && a.EndDate > DateTime.Now).FirstOrDefault();
        if (auctionDb == null) throw new Exception("Auction not found");
        Auction auction = _mapper.Map<Auction>(auctionDb);
        var bidsDb = _dbContext.BidDbs.Where(b => b.AuctionId == id).OrderByDescending(b => b.Amount).ToList();
        foreach (var bidDb in bidsDb)
        {
            Bid bid =  _mapper.Map<Bid>(bidDb);
            auction.AddBid(bid);
        }
        Console.Out.WriteLine("bids: " + auction.Bids.FirstOrDefault());
        return auction;
    }
    
    public Auction GetById(int id)
    {
        AuctionDb auctionDb = _dbContext.AuctionDbs.Where(a => a.Id == id).FirstOrDefault();
        if (auctionDb == null) throw new Exception("Auction not found");
        Auction auction = _mapper.Map<Auction>(auctionDb);
        var bidsDb = _dbContext.BidDbs.Where(b => b.AuctionId == id).OrderByDescending(b => b.Amount).ToList();
        foreach (var bidDb in bidsDb)
        {
            Bid bid =  _mapper.Map<Bid>(bidDb);
            auction.AddBid(bid);
        }
        Console.Out.WriteLine("bids: " + auction.Bids.FirstOrDefault());
        return auction;
    }

    public void SaveAuction(Auction auction)
    {
        AuctionDb adb = _mapper.Map<AuctionDb>(auction);
        _dbContext.AuctionDbs.Add(adb);
        _dbContext.SaveChanges();
    }

    public void SaveBid(Bid bid)
    {
        BidDb bidDb = _mapper.Map<BidDb>(bid);
        _dbContext.BidDbs.Add(bidDb);
        _dbContext.SaveChanges();
    }
    
    public void UpdateAuctionDescription(int auctionId, string newDescription)
    {
        
        var auctionDb = _dbContext.AuctionDbs.FirstOrDefault(a => a.Id == auctionId);
        if (auctionDb == null)
            throw new Exception("Auction not found");

        auctionDb.Description = newDescription;
        _dbContext.AuctionDbs.Update(auctionDb);
        _dbContext.SaveChanges();
    }

    public void UpdateCurrentPrice(Auction auction)
    {
        var auctionDb = _dbContext.AuctionDbs.FirstOrDefault(a => a.Id == auction.Id);
        if (auctionDb == null)
        {
            throw new Exception($"Auction with ID {auction.Id} not found.");
        }

        auctionDb.CurrentPrice = auction.CurrentPrice;
        _dbContext.SaveChanges();
    }
    
}