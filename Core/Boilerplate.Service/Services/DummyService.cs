using Boilerplate.Data.Repository;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Exceptions;
using Boilerplate.Domain.Requests.Dummy;
using Boilerplate.Domain.Responses.Dummy;
using Boilerplate.Service.Interfaces;

namespace Boilerplate.Service.Services;

public class DummyService : IDummyService
{
    private readonly IGenericRepository<Dummy> _dummyRepository;
    private readonly IMapper _mapper;

    public DummyService(IGenericRepository<Dummy> dummyRepository, IMapper mapper)
    {
        _dummyRepository = dummyRepository;
        _mapper = mapper;
    }

    public async Task<List<GetDummyResponse>> GetAllAsync()
    {
        var dummies = await _dummyRepository.GetAllAsync();

        return _mapper.Map<List<GetDummyResponse>>(dummies);
    }

    public async Task<GetDummyResponse> GetAsync(int id)
    {
        var dummy = await _dummyRepository.GetAsync(id);

        return _mapper.Map<GetDummyResponse>(dummy);
    }

    public async Task<CreateDummyResponse> PostAsync(CreateDummyRequest createDummyRequest)
    {
        var existingDummy = await _dummyRepository.GetAsync(s => s.Name == createDummyRequest.Name);

        if (existingDummy != null)
        {
            throw new DummyException($"There is a dummy. Name: '{createDummyRequest.Name}'");
        }

        var dummy = await _dummyRepository.AddAsync(_mapper.Map<Dummy>(createDummyRequest));

        return _mapper.Map<CreateDummyResponse>(dummy);
    }

    public async Task<UpdateDummyResponse> PutAsync(UpdateDummyRequest updateDummyRequest)
    {
        var existingDummy = await _dummyRepository.GetAsync(updateDummyRequest.Id);

        if (existingDummy == null)
        {
            throw new DummyException($"Dummy is not found while updating. DummyId: '{updateDummyRequest.Id}'");
        }

        existingDummy = _mapper.Map<Dummy>(updateDummyRequest);
        var updatedDummy = await _dummyRepository.UpdateAsync(existingDummy);

        return _mapper.Map<UpdateDummyResponse>(updatedDummy);
    }

    public async Task DeleteAsync(int id)
    {
        var dummy = await _dummyRepository.GetAsync(id);

        if (dummy == null)
        {
            throw new DummyException($"Dummy is not found while deleting. DummyId: '{id}'");
        }

        await _dummyRepository.DeleteAsync(dummy);
    }
}