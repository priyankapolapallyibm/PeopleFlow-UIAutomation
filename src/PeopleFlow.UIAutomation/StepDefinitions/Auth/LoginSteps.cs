using FluentAssertions;
using Microsoft.Playwright;
using PeopleFlow.UIAutomation.Config;
using PeopleFlow.UIAutomation.Helpers;
using PeopleFlow.UIAutomation.Pages.Auth;
using PeopleFlow.UIAutomation.Pages.Dashboard;

namespace PeopleFlow.UIAutomation.StepDefinitions.Auth;

[Binding]
public class LoginSteps
{
    private readonly ScenarioContext _scenarioContext;
    private readonly LoginPage _loginPage;
    private readonly DashboardPage _dashboardPage;
    private static readonly AppSettings Settings = ConfigurationHelper.GetAppSettings();

    public LoginSteps(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
        var page = (IPage)scenarioContext["Page"];
        _loginPage = new LoginPage(page);
        _dashboardPage = new DashboardPage(page);
    }

    [Given(@"I navigate to the PeopleFlow login page")]
    public async Task GivenINavigateToThePeopleFlowLoginPage()
    {
        await _loginPage.NavigateAsync();
    }

    [Given(@"I am logged in as ""(.*)"" with password ""(.*)""")]
    public async Task GivenIAmLoggedInAs(string username, string password)
    {
        await _loginPage.NavigateAsync();
        await _loginPage.LoginAsync(username, password);
    }

    [When(@"I enter username ""(.*)"" in the Username field")]
    public async Task WhenIEnterUsername(string username)
    {
        await _loginPage.EnterUsernameAsync(username);
    }

    [When(@"I enter password ""(.*)"" in the Password field")]
    public async Task WhenIEnterPassword(string password)
    {
        await _loginPage.EnterPasswordAsync(password);
    }

    [When(@"I click the Login button")]
    public async Task WhenIClickTheLoginButton()
    {
        await _loginPage.ClickLoginAsync();
    }

    [Then(@"I should see the username input field")]
    public async Task ThenIShouldSeeUsernameField()
    {
        var page = (IPage)_scenarioContext["Page"];
        var visible = await page.Locator("input[name='username']").IsVisibleAsync();
        visible.Should().BeTrue("Username field should be visible on login page");
    }

    [Then(@"I should see the password input field")]
    public async Task ThenIShouldSeePasswordField()
    {
        var page = (IPage)_scenarioContext["Page"];
        var visible = await page.Locator("input[type='password']").IsVisibleAsync();
        visible.Should().BeTrue("Password field should be visible on login page");
    }

    [Then(@"I should see the Login button")]
    public async Task ThenIShouldSeeLoginButton()
    {
        var page = (IPage)_scenarioContext["Page"];
        var visible = await page.Locator("button[type='submit']").IsVisibleAsync();
        visible.Should().BeTrue("Login button should be visible");
    }

    [Then(@"I should be redirected to the Dashboard page")]
    [Then(@"I should be on the Dashboard page")]
    public async Task ThenIShouldBeOnDashboard()
    {
        var isDashboard = await _dashboardPage.IsDashboardLoadedAsync();
        isDashboard.Should().BeTrue("User should land on Dashboard after login");
    }

    [Then(@"I should see the navigation menu")]
    public async Task ThenIShouldSeeNavigationMenu()
    {
        var page = (IPage)_scenarioContext["Page"];
        var visible = await page.Locator(".oxd-main-menu").IsVisibleAsync();
        visible.Should().BeTrue("Navigation menu should be visible after login");
    }

    [Then(@"I should see the error message ""(.*)""")]
    public async Task ThenIShouldSeeErrorMessage(string expectedMessage)
    {
        var error = await _loginPage.GetErrorMessageAsync();
        error.Should().Contain(expectedMessage);
    }

    [Then(@"I should see a required field validation error")]
    public async Task ThenIShouldSeeRequiredFieldError()
    {
        var isError = await _loginPage.IsErrorDisplayedAsync();
        isError.Should().BeTrue("A validation error should appear for empty username");
    }
}
