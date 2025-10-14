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
        var auctionDbs = _dbContext.AuctionDbs.Where(p => p.UserName == userName).ToList();
        List<Auction> results = new List<Auction>();
        foreach (AuctionDb adb in auctionDbs)
        {
            Auction auction =  _mapper.Map<Auction>(adb);
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
            results.Add(auction);
        }
        return results;
    }

    public Auction GetById(int id, String userName) //to be done, a bit confused on implementation
    {
        AuctionDb auctionDb = _dbContext.AuctionDbs.Where(a => a.Id == id && a.UserName.Equals(userName)).Include(a => a.BidDbs).FirstOrDefault();
        if (auctionDb == null) throw new Exception("Auction not found");
        
        Auction auction = _mapper.Map<Auction>(auctionDb);
        foreach (var bidDb in auctionDb.BidDbs)
        {
            Bid bid =  _mapper.Map<Bid>(bidDb);
            auction.AddBid(bid);
        }
        return auction;
    }
    
    //this is supposed to get the details of the auction we clicked "details" on, so that
    //everybody can see the details of the auction, might be wrong
    public Auction GetById(int id) 
    {
        AuctionDb auctionDb = _dbContext.AuctionDbs.Where(a => a.Id == id).FirstOrDefault();
        if (auctionDb == null) throw new Exception("Auction not found");
        
        Auction auction = _mapper.Map<Auction>(auctionDb);
        foreach (var bidDb in auctionDb.BidDbs)
        {
            Bid bid =  _mapper.Map<Bid>(bidDb);
            auction.AddBid(bid);
        }
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
        Console.Out.WriteLine("Before mapper");
        BidDb bidDb = _mapper.Map<BidDb>(bid);
        Console.Out.WriteLine("After mapper");
        _dbContext.BidDbs.Add(bidDb);
        Console.Out.WriteLine("Saving to database...");
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
    
}