using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PeliculasAPI.Controllers;
using PeliculasAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PeliculasAPI.Tests.PruebasUnitarias
{
    [TestClass]
    public class CuentasControllerTests: BasePruebas
    {
        [TestMethod]
        public async Task CrearUsuario()
        {
            var nombreBD = Guid.NewGuid().ToString();
            await CrearUsuarioHelper(nombreBD);
            var context2 = ConstruirContext(nombreBD);
            var conteo = await context2.Users.CountAsync();
            Assert.AreEqual(1, conteo);
        }

        [TestMethod]
        public async Task UsuarioNoPuedeLoguearse()
        {
            var nombreBD = Guid.NewGuid().ToString();
            await CrearUsuarioHelper(nombreBD);

            var controller = ConstruirCuentasController(nombreBD);
            var userInfo = new UserInfo() { Email = "ejemplo@hotmail.com", Password = "malPassword" };
            var respuesta = await controller.Login(userInfo);

            Assert.IsNull(respuesta.Value);
            var resultado = respuesta.Result as BadRequestObjectResult;
            Assert.IsNotNull(resultado);
        }

        [TestMethod]
        public async Task UsuarioPuedeLoguearse()
        {
            var nombreBD = Guid.NewGuid().ToString();
            await CrearUsuarioHelper(nombreBD);

            var controller = ConstruirCuentasController(nombreBD);
            var userInfo = new UserInfo() { Email = "ejemplo@hotmail.com", Password = "Aa123456!" };
            var respuesta = await controller.Login(userInfo);
            Assert.IsNotNull(respuesta.Value);
            Assert.IsNotNull(respuesta.Value.Token);
        }

        private async Task CrearUsuarioHelper(string nombreBD)
        {
            var cuentasController = ConstruirCuentasController(nombreBD);
            var userInfo = new UserInfo() { Email = "ejemplo@hotmail.com", Password = "Aa123456!" };
            await cuentasController.CreateUser(userInfo);
        }

        private CuentasController ConstruirCuentasController(string nombreBD)
        {
            var context = ConstruirContext(nombreBD);
            var miUserStore = new UserStore<IdentityUser>(context);
            var userManager = BuildUserManager(miUserStore);
            var mapper = ConfigurarAutoMapper();

            var httpContext = new DefaultHttpContext();
            MockAuth(httpContext);
            var signInManager = SetupSignInManager(userManager, httpContext);

            var miConfiguracion = new Dictionary<string, string>
            {
                {"JWT:key", "ASDJKNKJN4KJ5N4KJSNDKAJSNDAKSJDNK4J5N43KJ53N4KSNDJASKLDNAS" }
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(miConfiguracion)
                .Build();

            return new CuentasController(userManager, signInManager, configuration, context, mapper);
        }

        // Source: https://github.com/dotnet/aspnetcore/blob/master/src/Identity/test/Shared/MockHelpers.cs
        // Source: https://github.com/dotnet/aspnetcore/blob/master/src/Identity/test/Identity.Test/SignInManagerTest.cs
        // Some code was modified to be adapted to our project.

        private UserManager<TUser> BuildUserManager<TUser>(IUserStore<TUser> store = null) where TUser : class
        {
            store = store ?? new Mock<IUserStore<TUser>>().Object;
            var options = new Mock<IOptions<IdentityOptions>>();
            var idOptions = new IdentityOptions();
            idOptions.Lockout.AllowedForNewUsers = false;

            options.Setup(o => o.Value).Returns(idOptions);

            var userValidators = new List<IUserValidator<TUser>>();

            var validator = new Mock<IUserValidator<TUser>>();
            userValidators.Add(validator.Object);
            var pwdValidators = new List<PasswordValidator<TUser>>();
            pwdValidators.Add(new PasswordValidator<TUser>());

            var userManager = new UserManager<TUser>(store, options.Object, new PasswordHasher<TUser>(),
                userValidators, pwdValidators, new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(), null,
                new Mock<ILogger<UserManager<TUser>>>().Object);

            validator.Setup(v => v.ValidateAsync(userManager, It.IsAny<TUser>()))
                .Returns(Task.FromResult(IdentityResult.Success)).Verifiable();

            return userManager;
        }

        private static SignInManager<TUser> SetupSignInManager<TUser>(UserManager<TUser> manager,
            HttpContext context, ILogger logger = null, IdentityOptions identityOptions = null,
            IAuthenticationSchemeProvider schemeProvider = null) where TUser : class
        {
            var contextAccessor = new Mock<IHttpContextAccessor>();
            contextAccessor.Setup(a => a.HttpContext).Returns(context);
            identityOptions = identityOptions ?? new IdentityOptions();
            var options = new Mock<IOptions<IdentityOptions>>();
            options.Setup(a => a.Value).Returns(identityOptions);
            var claimsFactory = new UserClaimsPrincipalFactory<TUser>(manager, options.Object);
            schemeProvider = schemeProvider ?? new Mock<IAuthenticationSchemeProvider>().Object;
            var sm = new SignInManager<TUser>(manager, contextAccessor.Object, claimsFactory, options.Object, null, schemeProvider, new DefaultUserConfirmation<TUser>());
            sm.Logger = logger ?? (new Mock<ILogger<SignInManager<TUser>>>()).Object;
            return sm;
        }
    
         private Mock<IAuthenticationService> MockAuth(HttpContext context)
        {
            var auth = new Mock<IAuthenticationService>();
            context.RequestServices = new ServiceCollection().AddSingleton(auth.Object).BuildServiceProvider();
            return auth;
        }
    
    }
}
