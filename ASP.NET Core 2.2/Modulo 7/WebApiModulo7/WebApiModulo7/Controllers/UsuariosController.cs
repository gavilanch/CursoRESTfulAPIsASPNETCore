using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApiModulo7.Contexts;
using WebApiModulo7.Models;

namespace WebApiModulo7.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public UsuariosController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [Route("AsignarUsuarioRol")]
        public async Task<ActionResult> AsignarRolUsuario(EditarRolDTO editarRolDTO)
        {
            var usuario = await userManager.FindByIdAsync(editarRolDTO.UserId);
            await userManager.AddClaimAsync(usuario, new Claim(ClaimTypes.Role, editarRolDTO.RoleName));
            await userManager.AddToRoleAsync(usuario, editarRolDTO.RoleName);
            return Ok();
        }

        [Route("RemoverUsuarioRol")]
        public async Task<ActionResult> RemoverUsuarioRol(EditarRolDTO editarRolDTO)
        {
            var usuario = await userManager.FindByIdAsync(editarRolDTO.UserId);
            await userManager.RemoveClaimAsync(usuario, new Claim(ClaimTypes.Role, editarRolDTO.RoleName));
            await userManager.RemoveFromRoleAsync(usuario, editarRolDTO.RoleName);
            return Ok();
        }
    }

    public class EditarRolDTO
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }
    }
}
