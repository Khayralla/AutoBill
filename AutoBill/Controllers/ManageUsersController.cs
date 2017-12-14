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
    [Route("ManageUsers")]
    public class ManageUsersController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly ILogger _logger;

        public ManageUsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [Route("")] // Combines to define the route template "ManageUsers"
        [Route("Index")] // Combines to define the route template "ManageUsers/Index"
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
        [Route("Register")]
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

        [HttpDelete("{email}")]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromQuery]string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
                if (user != null)
                {
                    var admins = await _userManager.GetUsersInRoleAsync(Constants.AdministratorRole);

                    // make sure we are not deleteing admin account.
                    var admin = admins.FirstOrDefault(a => a.Email == email);
                    if (admin == null)
                        await _userManager.DeleteAsync(user);
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}