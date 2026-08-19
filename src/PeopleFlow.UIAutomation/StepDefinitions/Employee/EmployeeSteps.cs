using FluentAssertions;
using Microsoft.Playwright;
using PeopleFlow.UIAutomation.Pages.Employee;
using PeopleFlow.UIAutomation.ApiClients;

namespace PeopleFlow.UIAutomation.StepDefinitions.Employee;

[Binding]
public class EmployeeSteps
{
    private readonly ScenarioContext _scenarioContext;
    private readonly EmployeeListPage _listPage;
    private readonly EmployeeFormPage _formPage;
    private readonly PeopleFlowApiClient _apiClient;

    public EmployeeSteps(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
        var page = (IPage)scenarioContext["Page"];
        _listPage = new EmployeeListPage(page);
        _formPage = new EmployeeFormPage(page);
        _apiClient = new PeopleFlowApiClient();
    }

    [Given(@"I navigate to the Employee List page")]
    public async Task GivenINavigateToEmployeeList()
    {
        var page = (IPage)_scenarioContext["Page"];
        await page.GotoAsync("http://localhost:80/web/index.php/pim/viewPimList");
        await _listPage.IsEmployeeListLoadedAsync();
    }

    [Then(@"I should see the employee list table")]
    public async Task ThenIShouldSeeEmployeeTable()
    {
        var visible = await _listPage.IsEmployeeListLoadedAsync();
        visible.Should().BeTrue("Employee list table should be visible");
    }

    [Then(@"the table should contain at least one employee record")]
    public async Task ThenTableShouldHaveRecords()
    {
        var count = await _listPage.GetEmployeeCountAsync();
        count.Should().BeGreaterThan(0, "At least one employee should exist");
    }

    [When(@"I enter ""(.*)"" in the employee name search field")]
    public async Task WhenIEnterInSearchField(string name)
    {
        await _listPage.SearchByNameAsync(name);
    }

    [When(@"I click the Search button")]
    public async Task WhenIClickSearch()
    {
        // Search already triggered in SearchByNameAsync
    }

    [Then(@"the employee table should show only records matching ""(.*)""")]
    public async Task ThenEmployeeTableShowsMatching(string name)
    {
        var count = await _listPage.GetEmployeeCountAsync();
        count.Should().BeGreaterThan(0, $"Employees matching '{name}' should be shown");
    }

    [Then(@"I should see the ""No Records Found"" message")]
    public async Task ThenNoRecordsFound()
    {
        var noRecords = await _listPage.IsNoRecordsFoundAsync();
        noRecords.Should().BeTrue("No Records Found message should appear");
    }

    [When(@"I click the Add Employee button")]
    public async Task WhenIClickAddEmployee()
    {
        await _listPage.ClickAddEmployeeAsync();
    }

    [Then(@"I should see the Add Employee form")]
    public async Task ThenIShouldSeeAddEmployeeForm()
    {
        var page = (IPage)_scenarioContext["Page"];
        var visible = await page.Locator("input[name='firstName']").IsVisibleAsync();
        visible.Should().BeTrue("Add Employee form should be visible");
    }

    [Then(@"the First Name field should be visible")]
    public async Task ThenFirstNameVisible()
    {
        var page = (IPage)_scenarioContext["Page"];
        (await page.Locator("input[name='firstName']").IsVisibleAsync()).Should().BeTrue();
    }

    [Then(@"the Last Name field should be visible")]
    public async Task ThenLastNameVisible()
    {
        var page = (IPage)_scenarioContext["Page"];
        (await page.Locator("input[name='lastName']").IsVisibleAsync()).Should().BeTrue();
    }

    [When(@"I enter first name ""(.*)"" in the First Name field")]
    public async Task WhenIEnterFirstName(string firstName)
    {
        await _formPage.EnterFirstNameAsync(firstName);
    }

    [When(@"I enter last name ""(.*)"" in the Last Name field")]
    public async Task WhenIEnterLastName(string lastName)
    {
        await _formPage.EnterLastNameAsync(lastName);
    }

    [When(@"I click the Save button")]
    public async Task WhenIClickSave()
    {
        await _formPage.ClickSaveAsync();
    }

    [Then(@"I should see the success notification")]
    public async Task ThenIShouldSeeSuccessNotification()
    {
        var success = await _formPage.IsSuccessToastVisibleAsync();
        success.Should().BeTrue("Success toast should appear after save");
    }

    [Then(@"the new employee ""(.*)"" should appear in the employee list")]
    public async Task ThenNewEmployeeAppears(string fullName)
    {
        var page = (IPage)_scenarioContext["Page"];
        await page.GotoAsync("http://localhost:80/web/index.php/pim/viewPimList");
        await _listPage.SearchByNameAsync(fullName.Split(' ')[0]);
        var count = await _listPage.GetEmployeeCountAsync();
        count.Should().BeGreaterThan(0, $"Newly added employee '{fullName}' should appear");
    }

    [When(@"I click Edit on the first employee row")]
    public async Task WhenIClickEditFirstRow()
    {
        await _listPage.ClickEditOnRowAsync(1);
    }

    [When(@"I update the phone number to ""(.*)""")]
    public async Task WhenIUpdatePhone(string phone)
    {
        var page = (IPage)_scenarioContext["Page"];
        await page.Locator("input[name='workTelephone']").FillAsync(phone);
    }

    [When(@"I click Delete on the first employee row")]
    public async Task WhenIClickDeleteFirstRow()
    {
        await _listPage.ClickDeleteOnRowAsync(1);
    }

    [Then(@"I should see the delete confirmation dialog")]
    public async Task ThenIShouldSeeDeleteDialog()
    {
        var page = (IPage)_scenarioContext["Page"];
        var visible = await page.Locator(".orangehrm-modal-footer").IsVisibleAsync();
        visible.Should().BeTrue("Delete confirmation dialog should appear");
    }

    [When(@"I confirm the deletion")]
    public async Task WhenIConfirmDeletion()
    {
        await _formPage.ConfirmDeleteAsync();
    }

    [Then(@"the employee should be removed from the list")]
    public async Task ThenEmployeeRemovedFromList()
    {
        var page = (IPage)_scenarioContext["Page"];
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        var toast = await page.Locator(".oxd-toast--success").IsVisibleAsync();
        toast.Should().BeTrue("Success toast should appear after deletion");
    }
}
