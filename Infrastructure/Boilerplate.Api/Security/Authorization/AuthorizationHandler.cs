namespace Boilerplate.Api.Security.Authorization;

public sealed class AuthorizationHandler : AuthorizationHandler<AuthorizationRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizationRequirement requirement)
    {
        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}