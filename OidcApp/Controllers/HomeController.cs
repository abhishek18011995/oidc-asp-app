using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OidcApp.Models;

namespace OidcApp.Controllers
{
    [Authorize]
    [Route("/")]
    //[Route("home")]
    public class HomeController : Controller
    {
        //[AllowAnonymous]
        //[Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        //[AllowAnonymous]
        [Route("privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        //[AllowAnonymous]
        [Route("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
