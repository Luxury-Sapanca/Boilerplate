namespace Boilerplate.Api.Security.Authorization;

public sealed class AuthorizationHandler : AuthorizationHandler<AuthorizationRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizationRequirement requirement)
    {
        if (context.User.Identity is not {IsAuthenticated: true})
        {
            context.Fail();
        }

        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}