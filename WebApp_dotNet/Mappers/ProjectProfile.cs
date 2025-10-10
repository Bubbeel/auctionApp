using AutoMapper;
using WebApp_dotNet.Core;
using WebApp_dotNet.Persistence;

namespace WebApp_dotNet.Mappers;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<ProjectDb, Auction>().ReverseMap();
    }
}