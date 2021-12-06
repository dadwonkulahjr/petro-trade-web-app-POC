using HADI.Data;
using HADI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HADI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManger;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;
        public AccountController(UserManager<IdentityUser> userManger, SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager, AppDbContext applicationDbContext)
        {
            _userManger = userManger;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = applicationDbContext;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid) { return View(model); }


            IdentityUser newUser = new()
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManger.CreateAsync(newUser, model.Password);

            if (result.Succeeded)
            {
                ////first create a roleStore
                var roleStore = new RoleStore<IdentityRole>(_context);
                //////Second step
                var roleManager = new RoleManager<IdentityRole>(roleStore, null, null, null, null);
                await roleManager.CreateAsync(new IdentityRole("Manager"));
                await _userManger.AddToRoleAsync(newUser, "Manager");
                await _signInManager.SignInAsync(newUser, isPersistent: false);
                return RedirectToAction("checker", "checklist");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }


            return View();
        }


        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await _userManger.FindByEmailAsync(email);
            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email is already in use");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {

                    return RedirectToAction("index", "home");

                }

                ModelState.AddModelError(string.Empty, "Invalid Login attemp");
            }
            return View();
        }

        [HttpGet]
        public IActionResult AllUsers()
        {
            var users = _userManger.Users;
            return View(users);
        }

        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManger.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id {id} cannot be found";
                return View("NotFound");
            }

            var userClaims = await _userManger.GetClaimsAsync(user);
            var userRoles = await _userManger.GetRolesAsync(user);

            var model = new EditUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Claims = userClaims.Select(a => a.Value).ToList(),
                Roles = userRoles
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await _userManger.FindByIdAsync(model.Id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                user.Email = model.Email;
                user.UserName = model.UserName;

                var result = await _userManger.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("AllUsers");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }


        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManger.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                var result = await _userManger.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("AllUsers");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("AllUsers");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(string userId)
        {
            ViewBag.userId = userId;

            var user = await _userManger.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id {userId} cannot be found";
                return View("NotFound");
            }

            var model = new List<RoleInUserVewModel>();

            foreach (var role in _roleManager.Roles.ToList())
            {
                var roleInUserViewModel = new RoleInUserVewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                if (await _userManger.IsInRoleAsync(user, role.Name))
                {
                    roleInUserViewModel.IsSelected = true;
                }
                else
                {
                    roleInUserViewModel.IsSelected = false;
                }

                model.Add(roleInUserViewModel);
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }



        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("login", "account");
        }


    }
}
