using Infrastructure.Entities;
using Infrastructure.Factories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApp.ViewModels.Views;

namespace WebApp.Controllers;

public class AuthController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager) : Controller
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;

    // Local login - Individual Account
    #region SignInGet
    [HttpGet]
    [Route("/signin")]
    public IActionResult SignIn()
    {
        if (_signInManager.IsSignedIn(User))
        {
            return RedirectToAction("Details", "Account");
        }

        AuthSignInViewModel viewModel = new();
        return View(viewModel);
    }
    #endregion

    #region SignInPost
    [HttpPost]
    [Route("/signin")]
    public async Task<IActionResult> SignIn(AuthSignInViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(viewModel.SignInForm.Email, viewModel.SignInForm.Password, viewModel.SignInForm.RememberMe, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Details", "Account");
            }
        }

        ViewData["ErrorMessage"] = "Incorrect email or password";
        return View(viewModel);
    }
    #endregion

    #region SignUpGet
    [HttpGet]
    [Route("/signup")]
    public IActionResult SignUp()
    {
        if (_signInManager.IsSignedIn(User))
        {
            return RedirectToAction("Details", "Account");
        }

        AuthSignUpViewModel viewModel = new();
        return View(viewModel);
    }
    #endregion

    #region SignUpPost
    [HttpPost]
    [Route("/signup")]
    public async Task<IActionResult> SignUp(AuthSignUpViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            bool exists = await _userManager.Users.AnyAsync(x => x.Email == viewModel.SignUpForm.Email);
            if (exists)
            {
                ViewData["ErrorMessage"] = "User with the same email already exists!!!";
                return View(viewModel);
            }

            UserEntity userEntity = UserFactory.Create(viewModel.SignUpForm);
            IdentityResult result = await _userManager.CreateAsync(userEntity, viewModel.SignUpForm.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("SignIn", "Auth");
            }
        }

        return View(viewModel);
    }
    #endregion

    #region SignOut
    [HttpGet]
    [Route("/signout")]
    public new async Task<IActionResult> SignOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
    #endregion


    // External login - Facebook
    #region Facebook
    [HttpGet]
    public IActionResult Facebook()
    {
        var authProps = _signInManager.ConfigureExternalAuthenticationProperties("Facebook", Url.Action("FacebookCallback"));
        return new ChallengeResult("Facebook", authProps);
    }

    [HttpGet]
    public async Task<IActionResult> FacebookCallback()
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info != null)
        {
            UserEntity userEntity = new()
            {
                FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName)!,
                LastName = info.Principal.FindFirstValue(ClaimTypes.Surname)!,
                Email = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                UserName = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                IsExternalAccount = true, 
            };

            UserEntity? user = await _userManager.FindByEmailAsync(userEntity.Email);
            if (user == null)
            {
                IdentityResult result = await _userManager.CreateAsync(userEntity);
                if (result.Succeeded)
                {
                    user = await _userManager.FindByEmailAsync(userEntity.Email);
                }
            }

            if(user != null)
            {
                if(user.FirstName != userEntity.FirstName || user.LastName != userEntity.LastName || user.Email != userEntity.Email) 
                {
                    user.FirstName = userEntity.FirstName;
                    user.LastName = userEntity.LastName;
                    user.Email = userEntity.Email;
                    user.IsExternalAccount = true;

                    await _userManager.UpdateAsync(user);
                }
                await _signInManager.SignInAsync(user, isPersistent: false);

                if (HttpContext.User != null)
                {
                    return RedirectToAction("Details", "Account");
                };
            }
        }
        ModelState.AddModelError("InvalidFacebookAuthentication", "Failed to autenticate with Facebook");
        ViewData["StatusMessage"] = "Failed to autenticate with Facebook";
        return RedirectToAction("SignIn", "Auth");
    }
    #endregion

    // External login - Google
    #region Google
    [HttpGet]
    public IActionResult Google()
    {
        var authProps = _signInManager.ConfigureExternalAuthenticationProperties("Google", Url.Action("GoogleCallback"));
        return new ChallengeResult("Google", authProps);
    }

    [HttpGet]
    public async Task<IActionResult> GoogleCallback()
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info != null)
        {
            UserEntity userEntity = new()
            {
                FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName)!,
                LastName = info.Principal.FindFirstValue(ClaimTypes.Surname)!,
                Email = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                UserName = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                IsExternalAccount = true,
            };

            UserEntity? user = await _userManager.FindByEmailAsync(userEntity.Email);
            if (user == null)
            {
                IdentityResult result = await _userManager.CreateAsync(userEntity);
                if (result.Succeeded)
                {
                    user = await _userManager.FindByEmailAsync(userEntity.Email);
                }
            }

            if (user != null)
            {
                if (user.FirstName != userEntity.FirstName || user.LastName != userEntity.LastName || user.Email != userEntity.Email)
                {
                    user.FirstName = userEntity.FirstName;
                    user.LastName = userEntity.LastName;
                    user.Email = userEntity.Email;
                    user.IsExternalAccount = true;

                    await _userManager.UpdateAsync(user);
                }
                await _signInManager.SignInAsync(user, isPersistent: false);

                if (HttpContext.User != null)
                {
                    return RedirectToAction("Details", "Account");
                };
            }
        }
        ModelState.AddModelError("InvalidGoogleAuthentication", "Failed to autenticate with Google");
        ViewData["StatusMessage"] = "Failed to autenticate with Google";
        return RedirectToAction("SignIn", "Auth");
    }
    #endregion
}
