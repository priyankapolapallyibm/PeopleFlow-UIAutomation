using Microsoft.Playwright;
using PeopleFlow.UIAutomation.Config;
using PeopleFlow.UIAutomation.Helpers;

namespace PeopleFlow.UIAutomation.Pages.Auth;

/// <summary>
/// POM: Login Page — http://localhost:80/auth/login
/// </summary>
public class LoginPage : BasePage
{
    private static readonly AppSettings Settings = ConfigurationHelper.GetAppSettings();

    // Locators
    private const string UsernameInput = "input[name='username']";
    private const string PasswordInput = "input[type='password']";
    private const string LoginButton = "button[type='submit']";
    private const string ErrorMessage = ".oxd-alert-content-text";
    private const string DashboardHeader = ".oxd-topbar-header-breadcrumb";

    public LoginPage(IPage page) : base(page) { }

    public async Task NavigateAsync()
    {
        await Page.GotoAsync($"{Settings.BaseUrl}/auth/login");
        await WaitForPageLoadAsync();
    }

    public async Task EnterUsernameAsync(string username) => await FillAsync(UsernameInput, username);

    public async Task EnterPasswordAsync(string password) => await FillAsync(PasswordInput, password);

    public async Task ClickLoginAsync() => await ClickAsync(LoginButton);

    public async Task LoginAsync(string username, string password)
    {
        await EnterUsernameAsync(username);
        await EnterPasswordAsync(password);
        await ClickLoginAsync();
        await WaitForPageLoadAsync();
    }

    public async Task<bool> IsDashboardVisibleAsync() => await IsVisibleAsync(DashboardHeader);

    public async Task<string> GetErrorMessageAsync() => await GetTextAsync(ErrorMessage);

    public async Task<bool> IsErrorDisplayedAsync() => await IsVisibleAsync(ErrorMessage);
}
