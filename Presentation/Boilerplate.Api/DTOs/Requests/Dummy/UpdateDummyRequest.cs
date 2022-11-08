namespace Boilerplate.Api.DTOs.Requests.Dummy;

public class UpdateDummyRequest
{
    [Required(ErrorMessage = "Id is required.")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }
}