using PeopleFlow.UIAutomation.Config;
using PeopleFlow.UIAutomation.Helpers;

namespace PeopleFlow.UIAutomation.StepDefinitions.Dashboard;

[Binding]
public class DashboardSteps
{
    private readonly ScenarioContext _scenarioContext;
    private readonly IPage _page;
    private static readonly AppSettings Settings = ConfigurationHelper.GetAppSettings();

    public DashboardSteps(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
        _page = (IPage)scenarioContext["Page"];
    }

    [Then(@"I should see the Dashboard page")]
    public async Task ThenIShouldSeeTheDashboardPage()
    {
        await _page.WaitForSelectorAsync("text=Dashboard, text=Home, [data-testid='dashboard-page']", 
            new PageWaitForSelectorOptions { Timeout = 5000 });
    }

    [Then(@"I should see the welcome message")]
    public async Task ThenIShouldSeeTheWelcomeMessage()
    {
        var welcomeElement = await _page.QuerySelectorAsync("text=Welcome, text=Hello");
        welcomeElement.Should().NotBeNull("Welcome message should be displayed");
    }

    [Then(@"I should see the dashboard widgets")]
    public async Task ThenIShouldSeeTheDashboardWidgets()
    {
        var widgets = await _page.QuerySelectorAllAsync("[data-testid='dashboard-widget'], .widget, .card");
        widgets.Should().NotBeEmpty("Dashboard should display at least one widget");
    }

    [When(@"I click on the Dashboard menu item")]
    public async Task WhenIClickOnTheDashboardMenuItem()
    {
        await _page.ClickAsync("text=Dashboard, [data-testid='dashboard-menu']");
    }

    [Then(@"the page title should be ""(.*)""")]
    public async Task ThenThePageTitleShouldBe(string expectedTitle)
    {
        var title = await _page.TitleAsync();
        title.Should().Contain(expectedTitle, "Page title should match expected value");
    }
}
