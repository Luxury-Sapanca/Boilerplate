namespace Boilerplate.Api.Requests.Dummy;

public class CreateDummyRequest
{
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }
}