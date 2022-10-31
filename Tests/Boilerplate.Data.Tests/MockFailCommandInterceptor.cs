namespace Boilerplate.Data.Tests;

public class MockFailCommandInterceptor : DbCommandInterceptor
{
    public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result, CancellationToken cancellationToken = new())
    {
        if (command.CommandText.StartsWith("INSERT"))
        {
            throw new Exception();
        }

        return base.ReaderExecutingAsync(command, eventData, result, cancellationToken);
    }
}