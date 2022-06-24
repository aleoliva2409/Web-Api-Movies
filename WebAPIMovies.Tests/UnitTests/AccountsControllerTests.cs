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
using Moq;
using WebAPIMovies.Controllers;
using WebAPIMovies.DTOs;

namespace WebAPIMovies.Tests.UnitTests
{
    [TestClass]
    public class AccountsControllerTests : BaseTests
    {
        [TestMethod]
        public async Task CreateUser()
        {
            var nameDB = Guid.NewGuid().ToString();
            await UserCreateHelper(nameDB);
            var newContext = BuildContext(nameDB);
            var count = await newContext.Users.CountAsync();

            Assert.AreEqual(1, count);
        }

        [TestMethod]
        public async Task User_NoLoginError()
        {
            var nameDB = Guid.NewGuid().ToString();
            await UserCreateHelper(nameDB);

            var controller = BuildAccountsController(nameDB);
            var userCreds = new UserCredentials() { Email = "example@mail.com", Password = "WrongPassword!" };

            var response = await controller.Login(userCreds);

            Assert.IsNull(response.Value);

            var result = response.Result as BadRequestObjectResult;
            
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task UserLogin()
        {
            var nameDB = Guid.NewGuid().ToString();
            await UserCreateHelper(nameDB);

            var controller = BuildAccountsController(nameDB);
            var userCreds = new UserCredentials() { Email = "example@mail.com", Password = "Example1000!" };

            var response = await controller.Login(userCreds);

            Assert.IsNotNull(response.Value);
            Assert.IsNotNull(response.Value.Token);
        }

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

        private AccountsController BuildAccountsController(string nameDB)
        {
            var context = BuildContext(nameDB);
            var myUserStore = new UserStore<IdentityUser>(context);
            var userManager = BuildUserManager(myUserStore);
            var mapper = ConfigAutoMapper();
            var httpContext = new DefaultHttpContext();
            MockAuth(httpContext);
            var signInManager = SetupSignInManager(userManager, httpContext);
            var myConfiguration = new Dictionary<string, string>()
            {
                { "jwt:key", "SJSJDKDKSNSNCJWSJQOEWOEIFKFLHLHORIEWIQUQIANANXCKDLDLDOROEDLSDXCMSKSOLSLSLSLEWIRPTPUP" }
            };
            
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();

            return new AccountsController(context, mapper, userManager, configuration, signInManager);
        }

        private async Task UserCreateHelper(string nameDB)
        {
            var accountController = BuildAccountsController(nameDB);
            var userCreds = new UserCredentials() { Email = "example@mail.com", Password = "Example1000!" };
            await accountController.Register(userCreds);
        }
    }
}
