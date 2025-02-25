using Microsoft.AspNetCore.Authorization;

namespace BibliotecaAPITests.Utilidades
{
    public class AllowAnonymousHandler : IAuthorizationHandler
    {
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            foreach (var requirement in context.PendingRequirements)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
