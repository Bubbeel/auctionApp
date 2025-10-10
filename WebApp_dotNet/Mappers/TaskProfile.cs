using AutoMapper;
using WebApp_dotNet.Persistence;

namespace WebApp_dotNet.Mappers;

public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<bidDb, Task>().ReverseMap();
    }
}