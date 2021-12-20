using HADI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HADI.Controllers
{
  
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if(User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                //True
                return RedirectToAction("index", "administration");
            }
            else if(User.Identity.IsAuthenticated && User.IsInRole("Manager"))
            {
                return RedirectToAction("checker", "checklist");
            }
            else
            {
                //False
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

      
    }
}
