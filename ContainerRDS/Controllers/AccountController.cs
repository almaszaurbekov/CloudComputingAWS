using System;
using System.Threading.Tasks;
using DataAccess.JsonModels;
using DataAccess.Models;
using DataAccess.Services;
using DataAccess.Static;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace ContainerRDS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<OkObjectResult> Login(string email, string password)
        {
            if (SDHelper.IsValueNotNull(email) && SDHelper.IsValueNotNull(password))
            {
                var authenticateRequest =
                    await AuthenticationRequest(email, password);
                return Ok(authenticateRequest);
            }

            return Ok(new LoginJsonModel("Email or password are empty", false));
        }

        private async Task<LoginJsonModel> AuthenticationRequest(string email, string password)
        {
            try
            {
                var anyUserWithEmail = await AnyUserWithEmail(email);
                if (anyUserWithEmail)
                    return await AuthenticationResult(email, password);
                return new LoginJsonModel("There is no user with such email", false);
            }
            catch (Exception ex)
            {
                return new LoginJsonModel() { Error = ex.Message };
            }
        }

        private async Task<LoginJsonModel> AuthenticationResult(string email, string password)
        {
            var authResult = await signInManager
                .PasswordSignInAsync(email, password, false, false);
            if (authResult.Succeeded)
                return new LoginJsonModel(email);
            return new LoginJsonModel("Incorrect password", false);
        }

        private async Task<bool> AnyUserWithEmail(string email)
        {
            var user = await userManager
                .FindByEmailAsync(email);
            return user != null;
        }

        [HttpPost("register")]
        public async Task<OkObjectResult> Register(string email, string password,
            string username = null, string phone = null)
        {
            if (SDHelper.IsValueNotNull(email) && SDHelper.IsValueNotNull(password))
            {
                var registrationRequest =
                    await RegistrationRequest(email, password);
                return Ok(registrationRequest);
            }

            return Ok(new UserJsonModel("Email or password are empty", false));
        }

        private async Task<RegisterJsonModel> RegistrationRequest(string email, string password)
        {
            try
            {
                var noUserWithEmail = await AnyUserWithEmail(email) == false;
                if (noUserWithEmail)
                    return await RegistrationResult(email, password);
                return new RegisterJsonModel("A user with the same email address already exists", false);
            }
            catch (Exception ex)
            {
                return new RegisterJsonModel(ex.Message, false);
            }
        }

        private async Task<RegisterJsonModel> RegistrationResult(string email, string password)
        {
            var user = new User() { Email = email };
            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "user");

                var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                EmailService emailService = new EmailService();
                await emailService.SendEmailAsync(email, "Confirm your account",
                    $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");

                return new RegisterJsonModel()
                {
                    IsSuccess = true,
                    Result = "Для завершения регистрации проверьте электронную почту и перейдите по ссылке, указанной в письме"
                };
            }

            return new RegisterJsonModel()
            {
                IsSuccess = false,
                Error = "Какая-то ошибка"
            };
        }
    }
}