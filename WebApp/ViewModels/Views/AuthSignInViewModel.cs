using Infrastructure.Models;

namespace WebApp.ViewModels.Views;

public class AuthSignInViewModel
{
    public string Title { get; set; } = "Sign In";
    public SignInModel SignInForm { get; set; } = null!;
}
