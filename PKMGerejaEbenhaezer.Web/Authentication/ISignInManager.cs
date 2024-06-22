namespace PKMGerejaEbenhaezer.Web.Authentication
{
    public interface ISignInManager
    {
        Task<bool> SignInAsync(string userName, string password, bool rememberMe);
        Task<bool> SignOut();
    }
}
