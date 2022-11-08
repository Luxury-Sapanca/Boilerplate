using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Boilerplate.Api.Security.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Extensions.Logging;

namespace Boilerplate.Api.Tests.Security;

public class AuthorizationHandlerTests
{
    private readonly AuthorizationHandler _authorizationHandler;

    public AuthorizationHandlerTests()
    {
        _authorizationHandler = new AuthorizationHandler();
    }

    [Fact]
    public async Task AuthorizationHandler_HandleRequirementAsync_WithGivenRequirement_ShouldBeSuccessful()
    {
        //Arrange    
        var requirements = new[] { new AuthorizationRequirement() };
        var user = new ClaimsPrincipal(
            new ClaimsIdentity(
                new[] 
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, "test"),
                },
                JwtBearerDefaults.AuthenticationScheme)
        );
        var context = new AuthorizationHandlerContext(requirements, user, null);

        //Act
        await _authorizationHandler.HandleAsync(context);

        //Assert
        Assert.True(context.HasSucceeded);
    }



    [Fact]
    public async Task AuthorizationHandler_HandleRequirementAsync_WithGivenRequirement_ShouldBeFailed()
    {
        //Arrange    
        var requirements = new[] { new AuthorizationRequirement() };
        var user = new ClaimsPrincipal();
        var context = new AuthorizationHandlerContext(requirements, user, null);

        //Act
        await _authorizationHandler.HandleAsync(context);

        //Assert
        Assert.True(context.HasFailed);
    }
}