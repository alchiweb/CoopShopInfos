using System;
using System.Threading.Tasks;
using CoopShopInfos.Models;
using CoopShopInfos.Services;
using CoopShopInfos.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoopShopInfos.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            //var user = new ApplicationUser
            //{
            //    UserName = model.UserName
            //};

            var result = await _signInManager.PasswordSignInAsync(
                model.UserName, model.Password,
                isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
                return RedirectToAction("Decode", "Home");

            ModelState.AddModelError(string.Empty, "Login Failed");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var confirmationCode =
                    await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var callbackurl = Url.Action(
                    controller: "Account",
                    action: "ConfirmEmail",
                    values: new { userId = user.Id, code = confirmationCode },
                    protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(
                    email: user.Email,
                    subject: "Confirm Email",
                    message: callbackurl);

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
                return RedirectToAction("Index", "Home");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return View("ConfirmEmail");

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
                return View();

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return RedirectToAction("ForgotPasswordEmailSent");

            if (!await _userManager.IsEmailConfirmedAsync(user))
                return RedirectToAction("ForgotPasswordEmailSent");

            var confirmationCode =
                await _userManager.GeneratePasswordResetTokenAsync(user);

            var callbackurl = Url.Action(
                controller: "Account",
                action: "ResetPassword",
                values: new { userId = user.Id, code = confirmationCode },
                protocol: Request.Scheme);

            await _emailSender.SendEmailAsync(
                email: user.Email,
                subject: "Reset Password",
                message: callbackurl);

            return RedirectToAction("ForgotPasswordEmailSent");
        }

        public IActionResult ForgotPasswordEmailSent()
        {
            return View();
        }

        public IActionResult ResetPassword(string userId, string code)
        {
            if (userId == null || code == null)
                throw new ApplicationException("Code must be supplied for password reset.");

            var model = new ResetPasswordViewModel { Code = code };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return RedirectToAction("ResetPasswordConfirm");

            var result = await _userManager.ResetPasswordAsync(
                user, model.Code, model.Password);
            if (result.Succeeded)
                return RedirectToAction("ResetPasswordConfirm");

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(model);
        }
        
        public IActionResult ResetPasswordConfirm()
        {
            return View();
        }

    }
}