namespace Boilerplate.Domain.Interfaces;

public interface ISoftDelete
{
    public bool IsDeleted { get; set; }
}