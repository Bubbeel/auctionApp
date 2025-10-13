using AutoMapper;
using WebApp_dotNet.Core;
using WebApp_dotNet.Persistence;

namespace WebApp_dotNet.Mappers;

public class BidProfile : Profile
{
    public BidProfile()
    {
        CreateMap<BidDb, Bid>().ReverseMap();
    }
}