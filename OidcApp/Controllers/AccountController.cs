using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OidcApp.Models;

namespace OidcApp.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        //private readonly OidcOptions oidcOptions;
        private ILogger<AccountController> logger;

        public AccountController(ILogger<AccountController> logger)
        {
            //this.oidcOptions = oidcOptions.CurrentValue;
            this.logger = logger;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            if (this.HttpContext.User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(User user)
        {

            if (this.HttpContext.User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }

            if (ModelState.IsValid)
            {
                var authenticationProperties = new AuthenticationProperties()
                {
                    RedirectUri = "/"
                };

                try
                {
                    await this.HttpContext.ChallengeAsync("Auth0", authenticationProperties);
                }
                catch (Exception ex)
                {
                    this.logger.LogError(ex.Message);
                }
            }
            return View();
        }

        [Authorize]
        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            var authenticationProperties = new AuthenticationProperties()
            {
                RedirectUri = "/"
            };
            this.HttpContext.SignOutAsync(authenticationProperties);

            return View("Login");
        }
    }
}