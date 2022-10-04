namespace Boilerplate.Api.Controllers;

[Route("api/dummy")]
[ApiController]
public class DummyController : Controller
{
    private readonly IDummyService _dummyService;

    public DummyController(IDummyService dummyService)
    {
        _dummyService = dummyService;
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetDummyResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<GetDummyResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(List<GetDummyResponse>))]
    [HttpGet]
    public async Task<ActionResult<List<GetDummyResponse>>> GetAsync()
    {
        var result = await _dummyService.GetAllAsync();

        return StatusCode(StatusCodes.Status200OK, result);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetDummyResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetDummyResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GetDummyResponse))]
    [HttpGet("{id}")]
    public async Task<ActionResult<GetDummyResponse>> GetAsync(int id)
    {
        var result = await _dummyService.GetAsync(id);

        return StatusCode(StatusCodes.Status200OK, result);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateDummyResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateDummyResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CreateDummyResponse))]
    [HttpPost]
    public async Task<ActionResult<CreateDummyResponse>> PostAsync([FromBody] CreateDummyRequest createDummyRequest)
    {
        var result = await _dummyService.PostAsync(createDummyRequest);

        return StatusCode(StatusCodes.Status201Created, result);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateDummyResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateDummyResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(UpdateDummyResponse))]
    [HttpPut]
    public async Task<ActionResult<UpdateDummyResponse>> PutAsync([FromBody] UpdateDummyRequest updateDummyRequest)
    {
        var result = await _dummyService.PutAsync(updateDummyRequest);

        return StatusCode(StatusCodes.Status200OK, result);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(bool))]
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteAsync(int id)
    {
        await _dummyService.DeleteAsync(id);

        return StatusCode(StatusCodes.Status200OK);
    }
}