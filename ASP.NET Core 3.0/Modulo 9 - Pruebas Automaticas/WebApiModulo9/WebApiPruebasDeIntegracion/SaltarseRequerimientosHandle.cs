using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPruebasDeIntegracion
{
    public class SaltarseRequerimientosHandle : IAuthorizationHandler
    {
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            foreach (var requirement in context.PendingRequirements.ToList()) 
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
