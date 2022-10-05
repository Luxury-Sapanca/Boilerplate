namespace Boilerplate.Service.Interfaces;

public interface IDummyService
{
    Task<List<DummyDto>> GetAllAsync();

    Task<DummyDto> GetAsync(int id);

    Task<DummyDto> PostAsync(DummyDto dummyDto);

    Task<DummyDto> PutAsync(DummyDto dummyDto);

    Task DeleteAsync(int id);
}