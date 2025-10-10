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
        foreach (AuctionDb pdb in auctionDbs)
        {
            Auction auction =  _mapper.Map<Auction>(pdb);
            results.Add(auction);
        }
        return results;
    }

    public Auction GetById(int id, String userName) //to be done, a bit confused on implementation
    {
        AuctionDb auctionDb = _dbContext.AuctionDbs.Where(p => p.Id == id && p.UserName.Equals(userName)).Include(p => p.BidDbs).FirstOrDefault();
        if (auctionDb == null) throw new Exception("Auction not found");
        
        Auction auction = _mapper.Map<Auction>(auctionDb);
        foreach (var bidDb in auctionDb.BidDbs)
        {
            Bid bid =  _mapper.Map<Bid>(bidDb);
            auction.AddBid(bid);
        }
        return auction;
    }

    public void Save(Auction auction)
    {
        AuctionDb adb = _mapper.Map<AuctionDb>(auction);
        _dbContext.AuctionDbs.Add(adb);
        _dbContext.SaveChanges();
    }
}