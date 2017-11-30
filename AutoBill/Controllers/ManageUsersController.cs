using AutoBill.Models;
using AutoBill.Models.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AutoBill.Controllers
{
    [Authorize(Roles = Constants.AdministratorRole)]
    public class ManageUsersController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly ILogger _logger;

        public ManageUsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var admins = await _userManager.GetUsersInRoleAsync(Constants.AdministratorRole);
            var everyone = await _userManager.Users.ToArrayAsync();

            everyone = everyone.Except(admins).ToArray();

            var model = new ManageUsersViewModel
            {
                Administrators = admins,
                Everyone = everyone
            };
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            //ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                   // _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    //await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                    //await _signInManager.SignInAsync(user, isPersistent: false);
                   // _logger.LogInformation("User created a new account with password.");
                   
                }

                //AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return RedirectToAction(nameof(Index));
        }
    }
}