using FluentAssertions;
using Microsoft.Playwright;
using PeopleFlow.UIAutomation.Pages.Leave;
using PeopleFlow.UIAutomation.ApiClients;

namespace PeopleFlow.UIAutomation.StepDefinitions.Leave;

[Binding]
public class LeaveSteps
{
    private readonly ScenarioContext _scenarioContext;
    private readonly ApplyLeavePage _applyLeavePage;
    private readonly PeopleFlowApiClient _apiClient;

    public LeaveSteps(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
        var page = (IPage)scenarioContext["Page"];
        _applyLeavePage = new ApplyLeavePage(page);
        _apiClient = new PeopleFlowApiClient();
    }

    [When(@"I navigate to the Apply Leave page")]
    public async Task WhenINavigateToApplyLeave()
    {
        var page = (IPage)_scenarioContext["Page"];
        await page.GotoAsync("http://localhost:80/web/index.php/leave/applyLeave");
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }

    [When(@"I navigate to the My Leave page")]
    public async Task WhenINavigateToMyLeave()
    {
        var page = (IPage)_scenarioContext["Page"];
        await page.GotoAsync("http://localhost:80/web/index.php/leave/viewMyLeaveList");
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }

    [Then(@"I should see the Leave Type dropdown")]
    public async Task ThenIShouldSeeLeaveTypeDropdown()
    {
        var page = (IPage)_scenarioContext["Page"];
        var visible = await page.Locator(".oxd-select-wrapper").First.IsVisibleAsync();
        visible.Should().BeTrue("Leave Type dropdown should be visible");
    }

    [Then(@"I should see the From Date field")]
    public async Task ThenIShouldSeeFromDateField()
    {
        var page = (IPage)_scenarioContext["Page"];
        var visible = await page.Locator("input[placeholder='yyyy-dd-mm']").First.IsVisibleAsync();
        visible.Should().BeTrue("From Date field should be visible");
    }

    [Then(@"I should see the To Date field")]
    public async Task ThenIShouldSeeToDateField()
    {
        var page = (IPage)_scenarioContext["Page"];
        var visible = await page.Locator("input[placeholder='yyyy-dd-mm']").Last.IsVisibleAsync();
        visible.Should().BeTrue("To Date field should be visible");
    }

    [Then(@"I should see the Apply button")]
    public async Task ThenIShouldSeeApplyButton()
    {
        var page = (IPage)_scenarioContext["Page"];
        var visible = await page.Locator("button[type='submit']").IsVisibleAsync();
        visible.Should().BeTrue("Apply button should be visible");
    }

    [When(@"I select leave type ""(.*)"" from the Leave Type dropdown")]
    public async Task WhenISelectLeaveType(string leaveType)
    {
        // Get leave types from API for dynamic data validation
        var leaveTypes = await _apiClient.GetLeaveTypesAsync();
        await _applyLeavePage.SelectLeaveTypeAsync(leaveType);
    }

    [When(@"I enter start date ""(.*)"" in the From Date field")]
    public async Task WhenIEnterStartDate(string date)
    {
        await _applyLeavePage.SetFromDateAsync(date);
    }

    [When(@"I enter end date ""(.*)"" in the To Date field")]
    public async Task WhenIEnterEndDate(string date)
    {
        await _applyLeavePage.SetToDateAsync(date);
    }

    [When(@"I click the Apply button")]
    public async Task WhenIClickApply()
    {
        await _applyLeavePage.ClickApplyAsync();
    }

    [Then(@"I should see a leave application success message")]
    public async Task ThenIShouldSeeLeaveSuccess()
    {
        var success = await _applyLeavePage.IsSuccessToastVisibleAsync();
        success.Should().BeTrue("Leave application success message should appear");
    }

    [Then(@"I should see a date validation error message")]
    public async Task ThenIShouldSeeDateValidationError()
    {
        var error = await _applyLeavePage.GetValidationErrorAsync();
        error.Should().NotBeNullOrEmpty("A date validation error should be displayed");
    }

    [Then(@"the leave history table should be visible")]
    public async Task ThenLeaveHistoryTableVisible()
    {
        var page = (IPage)_scenarioContext["Page"];
        var visible = await page.Locator(".oxd-table-body").IsVisibleAsync();
        visible.Should().BeTrue("Leave history table should be visible");
    }

    [Then(@"at least one leave request should be listed")]
    public async Task ThenAtLeastOneLeaveRequestListed()
    {
        var page = (IPage)_scenarioContext["Page"];
        var rows = page.Locator(".oxd-table-row");
        var count = await rows.CountAsync();
        count.Should().BeGreaterThan(1, "At least one leave request should appear in history");
    }
}
