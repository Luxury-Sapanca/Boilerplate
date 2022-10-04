namespace Boilerplate.Domain.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Dummy, GetDummyResponse>().ReverseMap();
        CreateMap<Dummy, CreateDummyRequest>().ReverseMap();
        CreateMap<Dummy, CreateDummyResponse>().ReverseMap();
        CreateMap<Dummy, UpdateDummyRequest>().ReverseMap();
        CreateMap<Dummy, UpdateDummyResponse>().ReverseMap();
    }
}