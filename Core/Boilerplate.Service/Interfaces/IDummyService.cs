using Boilerplate.Domain.Requests.Dummy;
using Boilerplate.Domain.Responses.Dummy;

namespace Boilerplate.Service.Interfaces;

public interface IDummyService
{
    Task<List<GetDummyResponse>> GetAllAsync();

    Task<GetDummyResponse> GetAsync(int id);

    Task<CreateDummyResponse> PostAsync(CreateDummyRequest createDummyRequest);

    Task<UpdateDummyResponse> PutAsync(UpdateDummyRequest updateDummyRequest);

    Task DeleteAsync(int id);
}