using Boilerplate.Api.Middlewares;
using Boilerplate.Domain.Exceptions;

namespace Boilerplate.Api.Tests.Middlewares;

public class ExceptionMiddlewareTests
{
    private readonly Mock<ILogger<ExceptionMiddleware>> _mockLogger;

    public ExceptionMiddlewareTests()
    {
        _mockLogger = new Mock<ILogger<ExceptionMiddleware>>();
    }

    [Fact]
    public async Task ExceptionMiddleware_DummyException_ShouldReturnReturnInternalServerErrorStatusCode()
    {
        //Arrange
        var mockProductApiException = new DummyException("test");
        Task MockNextMiddleware(HttpContext _) => Task.FromException(mockProductApiException);
        var httpContext = new DefaultHttpContext();
        var exceptionHandlingMiddleware = new ExceptionMiddleware(MockNextMiddleware, _mockLogger.Object);

        //Act
        await exceptionHandlingMiddleware.Invoke(httpContext);

        //Assert
        Assert.Equal(HttpStatusCode.InternalServerError, (HttpStatusCode)httpContext.Response.StatusCode);
    }

    [Fact]
    public async Task ExceptionMiddleware_RandomException_ShouldReturnReturnInternalServerErrorStatusCode()
    {
        //Arrange
        var mockException = new Exception("test");
        Task MockNextMiddleware(HttpContext _) => Task.FromException(mockException);
        var httpContext = new DefaultHttpContext();
        var exceptionHandlingMiddleware = new ExceptionMiddleware(MockNextMiddleware, _mockLogger.Object);

        //Act
        await exceptionHandlingMiddleware.Invoke(httpContext);

        //Assert
        Assert.Equal(HttpStatusCode.InternalServerError, (HttpStatusCode)httpContext.Response.StatusCode);
    }
}