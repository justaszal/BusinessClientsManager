using AutoMapper;
using BusinessClientsManager.Models;

namespace BusinessClientsManager.Services;

public class BusinessClientProfile : Profile
{
    public BusinessClientProfile()
    {
        CreateMap<BusinessClient, GetBusinessClientsResponse>();
        //CreateMap<IEnumerable<BusinessClient>, IEnumerable<GetBusinessClientsResponse>>();
    }
}
