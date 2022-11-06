namespace Boilerplate.Domain.Entities;

public class Dummy : BaseEntity, ISoftDelete
{
    public string Name { get; set; }

    public bool IsDeleted { get; set; }
}