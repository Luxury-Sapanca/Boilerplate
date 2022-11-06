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

    public async Task<List<DummyDto>> GetAllAsync()
    {
        var dummies = await _dummyRepository.GetAllAsync();

        return _mapper.Map<List<DummyDto>>(dummies);
    }

    public async Task<DummyDto> GetAsync(int id)
    {
        var dummy = await _dummyRepository.GetAsync(id);

        return _mapper.Map<DummyDto>(dummy);
    }

    public async Task<DummyDto> PostAsync(DummyDto dummyDto)
    {
        var existingDummy = await _dummyRepository.GetAsync(s => s.Name == dummyDto.Name);

        if (existingDummy != null)
        {
            throw new DummyException($"There is a dummy. Name: '{dummyDto.Name}'");
        }

        var dummy = await _dummyRepository.AddAsync(_mapper.Map<Dummy>(dummyDto));

        return _mapper.Map<DummyDto>(dummy);
    }

    public async Task<DummyDto> PutAsync(DummyDto dummyDto)
    {
        var existingDummy = await _dummyRepository.GetAsync(dummyDto.Id);

        if (existingDummy == null)
        {
            throw new DummyException($"Dummy is not found while updating. DummyId: '{dummyDto.Id}'");
        }

        existingDummy = _mapper.Map<Dummy>(dummyDto);
        var updatedDummy = await _dummyRepository.UpdateAsync(existingDummy);

        return _mapper.Map<DummyDto>(updatedDummy);
    }

    public async Task DeleteAsync(int id)
    {
        var dummy = await _dummyRepository.GetAsync(id);

        if (dummy == null)
        {
            throw new DummyException($"Dummy is not found while deleting. DummyId: '{id}'");
        }

        await _dummyRepository.SoftDeleteAsync(dummy);
    }
}