using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Task510.Models;

namespace Task510.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        readonly RoleManager<IdentityRole> _roleManager;
        readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger,RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            IdentityRole IR = new IdentityRole
            {
                Name="Admin"
            };
            IdentityResult identityResult = await _roleManager.CreateAsync(IR);
            if (identityResult.Succeeded)
                return Json("succuss");
            return Json("Failed");
        }

        public async Task<IActionResult> CreateUser()
        {
            ApplicationUser IR = new ApplicationUser
            {
                UserName="mohamed"
            };
            IdentityResult identityResult = await _userManager.CreateAsync(IR);
            if (identityResult.Succeeded)
                return Json("succuss");
            return Json("Failed");
        }

        public async Task<IActionResult> AssignUserToRole() {

           var user = await  _userManager.FindByNameAsync("mohamed");
            IdentityResult identityResult = await _userManager.RemoveFromRoleAsync(user, "admin");
            
            if (identityResult.Succeeded)
                return Json("succuss");
            return Json("Failed");

        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
