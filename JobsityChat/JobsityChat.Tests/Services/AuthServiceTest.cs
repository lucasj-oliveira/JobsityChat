using JobsityChat.Model;
using JobsityChat.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace JobsityChat.Test.Services
{
    public class AuthServiceTest
    {
        private AuthService authService;

        public AuthServiceTest() 
        {
            authService = new AuthService(this.CreateUserManager().Object, new Mock<IConfiguration>().Object);
        }


        [Fact]
        public async Task LoginAsync()
        {
            var userLogin = new LoginModel()
            {
                Username = "Lucas",
                Password = "Lucas123#"
            };
            var response = await authService.Login(userLogin);
            Assert.True(response != null);
        }

        [Fact]
        public async Task RegisterAsync()
        {
            var userRegistration = new RegisterModel()
            {
                Username = "newUser",
                Email = "newUser@email.com",
                Password = "Password123#"

            };
            var response = await authService.Register(userRegistration);
            Assert.True(response.Status != "Error");
        }

        //Had a hard time trying to mock this :/
        private Mock<UserManager<IdentityUser>> CreateUserManager()
        {

            var store = new Mock<IUserStore<IdentityUser>>().Object;
            var options = new Mock<IOptions<IdentityOptions>>();
            var idOptions = new IdentityOptions();
            idOptions.Lockout.AllowedForNewUsers = false;
            options.Setup(o => o.Value).Returns(idOptions);
            var userValidators = new List<IUserValidator<IdentityUser>>();
            var validator = new Mock<IUserValidator<IdentityUser>>();
            userValidators.Add(validator.Object);
            var pwdValidators = new List<PasswordValidator<IdentityUser>>();
            pwdValidators.Add(new PasswordValidator<IdentityUser>());
            var userManager = new Mock<UserManager<IdentityUser>>(store, options.Object, new PasswordHasher<IdentityUser>(),
                userValidators, pwdValidators, new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(), null,
                new Mock<ILogger<UserManager<IdentityUser>>>().Object);
            return userManager;
        }
    }
}
