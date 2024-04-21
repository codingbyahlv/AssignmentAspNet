using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels.Views;

namespace WebApp.Controllers;

[Authorize]
public class AccountController(AddressManager addressManager, CourseService courseService, SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager) : Controller
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly AddressManager _addressManager = addressManager;
    private readonly CourseService _courseService = courseService;

    #region DetailsGet
    [HttpGet]
    [Route("/details")]
    public async Task<IActionResult> Details()
    {
        AccountDetailsViewModel viewModel = new();
        viewModel = await PopulateAccountDetailsViewModelAsync(viewModel);
        return View(viewModel);
    }
    #endregion

    #region DetailsPost
    [HttpPost]
    [Route("/details")]
    public async Task<IActionResult> Details(AccountDetailsViewModel viewModel)
    {
        if(viewModel.BasicInfoForm != null)
        {
            if (viewModel.BasicInfoForm.FirstName != null && viewModel.BasicInfoForm.LastName != null && viewModel.BasicInfoForm.Email != null)
            {
                bool result = await UpdateBasicInfoForm(viewModel.BasicInfoForm);
                if (!result) ViewData["ErrorMessage"] = "Something went wrong!";
            }
        }

        if(viewModel.AddressInfoForm != null)
        {
            if (viewModel.AddressInfoForm.AddressLine1 != null && viewModel.AddressInfoForm.PostalCode != null && viewModel.AddressInfoForm.City != null)
            {
                bool result = await UpdateAddressInfoForm(viewModel.AddressInfoForm);
                if (!result) ViewData["ErrorMessage"] = "Something went wrong!";
            }
        }

        viewModel = await PopulateAccountDetailsViewModelAsync(viewModel);
        return View(viewModel);
    }
    #endregion

    #region SecurityGet
    [HttpGet]
    public async Task<IActionResult> Security()
    {
        AccountSecurityViewModel viewModel = new();
        UserEntity? user = await _userManager.GetUserAsync(User);
        if (user != null)
            viewModel.IsExternalAccount = user.IsExternalAccount;
        viewModel.ProfileInfo = await PopulateProfileInfoAsync();
        return View(viewModel);
    }
    #endregion

    #region SecurityPost
    [HttpPost]
    public async Task<IActionResult> Security(AccountSecurityViewModel viewModel)
    {
        if(viewModel.SecurityForm != null)
        {
            if(viewModel.SecurityForm.CurrentPassword != null && viewModel.SecurityForm.NewPassword != null && viewModel.SecurityForm.ConfirmPassword != null)
            {
                bool result = await UpdatePassword(viewModel.SecurityForm);
                if (!result)
                {
                    ViewData["ErrorMessage"] = "Something went wrong!";
                }
            }
        }

        if (viewModel.DeleteForm != null)
        {
            if (viewModel.DeleteForm.IsDelete)
            {
                try
                {
                    UserEntity? user = _userManager.GetUserAsync(User).Result;
                    if (user != null)
                    {
                        await _signInManager.SignOutAsync();

                        IdentityResult result = await _userManager.DeleteAsync(user);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                catch { return RedirectToAction("Security", "Account"); }
            }
        }
        viewModel = await PopulateAccountSecurityViewModelAsync(viewModel);
        return View(viewModel);
    }
    #endregion

    #region SavedCoursesGet
    public async Task<IActionResult> SavedCourses()
    {
        AccountSavedCoursesViewModel viewModel = new();
        viewModel.ProfileInfo = await PopulateProfileInfoAsync();

        UserEntity? user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            if(user.SavedCoursesIdList.Count > 0)
            {
                List<int> idList = user.SavedCoursesIdList;
                List<CourseModel> response = await _courseService.GetSavedCoursesAsync(idList);
                viewModel.CoursesSection.CourseList = response;
            }
        }
        return View(viewModel);

    }
    #endregion

    private async Task<bool> UpdatePassword(AccountSecurityFormModel model)
    {
        UserEntity? user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            try
            {
                IdentityResult result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    return true;
                }
            }
            catch { return false; }
        }
        return false;
    }

    private async Task<bool> UpdateBasicInfoForm(BasicInfoModel formModel)
    {
        UserEntity? user = await _userManager.GetUserAsync(User);
        if(user != null)
        {
            user.FirstName = formModel.FirstName;
            user.LastName = formModel.LastName;
            user.Email = formModel.Email;
            user.PhoneNumber = formModel.PhoneNumber;
            user.Biography = formModel.Biography;
            user.UserName = formModel.Email;
                
            IdentityResult result =  await _userManager.UpdateAsync(user);
            if(result.Succeeded)
            {
                return true;
            }
        }
        return false;
    }

    private async Task<bool> UpdateAddressInfoForm(AddressInfoModel formModel)
    {
        AddressEntity address = await _addressManager.GetOneAddressAsync(UserFactory.Create(formModel));
        address ??= await _addressManager.CreateAddressAsync(UserFactory.Create(formModel)); 

        UserEntity? user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            user.AddressId = address.Id;
            IdentityResult result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return true;
            }
        }
        return false;
    }

    private async Task<AccountDetailsViewModel> PopulateAccountDetailsViewModelAsync(AccountDetailsViewModel viewModel)
    {
        UserEntity? user = await _userManager.GetUserAsync(User);
        if (user != null)
            viewModel.IsExternalAccount = user.IsExternalAccount;          
        viewModel.ProfileInfo = await PopulateProfileInfoAsync();
        viewModel.BasicInfoForm ??= await PopulateBasicInfoFormAsync();
        viewModel.AddressInfoForm ??= await PopulateAddressInfoFormAsync();

        return viewModel;
    }

    private async Task<AccountSecurityViewModel> PopulateAccountSecurityViewModelAsync(AccountSecurityViewModel viewModel)
    {
        UserEntity? user = await _userManager.GetUserAsync(User);
        if (user != null)
            viewModel.IsExternalAccount = user.IsExternalAccount;
        viewModel.ProfileInfo = await PopulateProfileInfoAsync();
        viewModel.SecurityForm = new();
        viewModel.DeleteForm = new();
        return viewModel;
    }

    private async Task<AccountAsideInfoModel> PopulateProfileInfoAsync()
    {
        UserEntity? user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            AccountAsideInfoModel model = UserFactory.Create(user.FirstName, user.LastName, user.Email!);
            return model;
        }
        return null!;
    }

    private async Task<BasicInfoModel> PopulateBasicInfoFormAsync()
    {
        UserEntity? user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            BasicInfoModel model = UserFactory.Create(user);
            return model;
        }
        return null!;
    }

    private async Task<AddressInfoModel> PopulateAddressInfoFormAsync()
    {
        UserEntity? user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            if(user.AddressId!= null)
            {
                AddressEntity address = await _addressManager.GetAddressAsync(user.AddressId);
                AddressInfoModel model = UserFactory.Create(address);
                return model;  
            }
        }
        return new AddressInfoModel();
    }
}