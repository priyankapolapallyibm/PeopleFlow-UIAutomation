using Microsoft.Playwright;

namespace PeopleFlow.UIAutomation.Pages.Dashboard;

/// <summary>
/// POM: Dashboard Page — http://localhost:80/dashboard
/// </summary>
public class DashboardPage : BasePage
{
    private const string WelcomeMessage = ".oxd-text--h6";
    private const string EmployeeCountWidget = "[data-v-widget='employee-count'] .oxd-text";
    private const string LeaveWidget = "[data-v-widget='leave-approved'] .oxd-text";
    private const string NavigationMenu = ".oxd-main-menu";
    private const string PimMenuLink = "a[href='/web/index.php/pim/viewPimList']";
    private const string LeaveMenuLink = "a[href='/web/index.php/leave/viewLeaveList']";
    private const string RecruitmentMenuLink = "a[href='/web/index.php/recruitment/viewRecruitmentModule']";
    private const string QuickLaunchPanel = ".orangehrm-quick-launch";

    public DashboardPage(IPage page) : base(page) { }

    public async Task<bool> IsDashboardLoadedAsync() => await IsVisibleAsync(NavigationMenu);

    public async Task<string> GetWelcomeMessageAsync() => await GetTextAsync(WelcomeMessage);

    public async Task<bool> AreKpiWidgetsVisibleAsync() => await IsVisibleAsync(QuickLaunchPanel);

    public async Task NavigateToPimAsync() => await ClickAsync(PimMenuLink);

    public async Task NavigateToLeaveAsync() => await ClickAsync(LeaveMenuLink);

    public async Task NavigateToRecruitmentAsync() => await ClickAsync(RecruitmentMenuLink);

    public async Task ReloadPageAsync()
    {
        await Page.ReloadAsync();
        await WaitForPageLoadAsync();
    }
}
