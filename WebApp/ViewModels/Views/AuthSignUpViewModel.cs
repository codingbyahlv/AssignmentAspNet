using Infrastructure.Models;

namespace WebApp.ViewModels.Views;

public class AuthSignUpViewModel
{
    public string Title { get; set; } = "SignUp";
    public SignUpModel SignUpForm { get; set; } = null!;
}
