using AutoMapper;
using WebApp_dotNet.Core;
using WebApp_dotNet.Persistence;

namespace WebApp_dotNet.Mappers;

public class AuctionProfile : Profile
{
    public AuctionProfile()
    {
        CreateMap<AuctionDb, Auction>().ReverseMap();
    }
}