namespace Boilerplate.Api.Controllers;

[Authorize(Policy = nameof(AuthorizationRequirement))]
[Route("api/dummy")]
[ApiController]
public class DummyController : Controller
{
    private readonly IDummyService _dummyService;
    private readonly IMapper _mapper;

    public DummyController(IDummyService dummyService, IMapper mapper)
    {
        _dummyService = dummyService;
        _mapper = mapper;
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetDummyResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(List<GetDummyResponse>))]
    [HttpGet]
    public async Task<ActionResult<List<GetDummyResponse>>> GetAsync()
    {
        var result = await _dummyService.GetAllAsync();

        return Ok(_mapper.Map<List<GetDummyResponse>>(result));
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetDummyResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GetDummyResponse))]
    [HttpGet("{id}")]
    public async Task<ActionResult<GetDummyResponse>> GetAsync(int id)
    {
        var result = await _dummyService.GetAsync(id);

        return Ok(_mapper.Map<GetDummyResponse>(result));
    }

    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateDummyResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CreateDummyResponse))]
    [HttpPost]
    public async Task<ActionResult<CreateDummyResponse>> PostAsync([FromBody] CreateDummyRequest createDummyRequest)
    {
        var dummyDto = _mapper.Map<DummyDto>(createDummyRequest);
        var result = await _dummyService.PostAsync(dummyDto);

        return Created(nameof(PostAsync), _mapper.Map<CreateDummyResponse>(result));
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateDummyResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(UpdateDummyResponse))]
    [HttpPut]
    public async Task<ActionResult<UpdateDummyResponse>> PutAsync([FromBody] UpdateDummyRequest updateDummyRequest)
    {
        var dummyDto = _mapper.Map<DummyDto>(updateDummyRequest);
        var result = await _dummyService.PutAsync(dummyDto);

        return Ok(_mapper.Map<UpdateDummyResponse>(result));
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(void))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(void))]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        await _dummyService.DeleteAsync(id);

        return Ok();
    }
}
