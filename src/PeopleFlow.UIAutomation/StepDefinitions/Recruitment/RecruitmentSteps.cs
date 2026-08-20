using PeopleFlow.UIAutomation.Config;
using PeopleFlow.UIAutomation.Helpers;

namespace PeopleFlow.UIAutomation.StepDefinitions.Recruitment;

[Binding]
public class RecruitmentSteps
{
    private readonly ScenarioContext _scenarioContext;
    private readonly IPage _page;
    private static readonly AppSettings Settings = ConfigurationHelper.GetAppSettings();

    public RecruitmentSteps(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
        _page = (IPage)scenarioContext["Page"];
    }

    [Given(@"I navigate to the Recruitment module")]
    public async Task GivenINavigateToTheRecruitmentModule()
    {
        await _page.ClickAsync("text=Recruitment");
    }

    [Then(@"I should see the Vacancies list table")]
    public async Task ThenIShouldSeeTheVacanciesListTable()
    {
        await _page.WaitForSelectorAsync("table, [data-testid='vacancies-table']", new PageWaitForSelectorOptions { Timeout = 5000 });
    }

    [Then(@"the table should contain at least one vacancy")]
    public async Task ThenTheTableShouldContainAtLeastOneVacancy()
    {
        var rowCount = await _page.QuerySelectorAllAsync("table tbody tr, [data-testid='vacancy-row']");
        rowCount.Should().NotBeEmpty("At least one vacancy should exist");
    }

    [When(@"I click the Add Vacancy button")]
    public async Task WhenIClickTheAddVacancyButton()
    {
        await _page.ClickAsync("button:has-text('Add Vacancy'), [data-testid='add-vacancy-btn']");
    }

    [When(@"I enter vacancy title ""(.*)"" in the Vacancy Name field")]
    public async Task WhenIEnterVacancyTitle(string vacancyTitle)
    {
        await _page.FillAsync("input[name='title'], input[placeholder*='Vacancy'], [data-testid='vacancy-title']", vacancyTitle);
    }

    [When(@"I select hiring manager ""(.*)"" from the Hiring Manager dropdown")]
    public async Task WhenISelectHiringManager(string hiringManager)
    {
        await _page.ClickAsync("select[name='hiringManager'], [data-testid='hiring-manager-dropdown']");
        await _page.ClickAsync($"option:has-text('{hiringManager}'), [data-value='{hiringManager}']");
    }

    [When(@"I click the Save button")]
    public async Task WhenIClickTheSaveButton()
    {
        await _page.ClickAsync("button:has-text('Save'), [data-testid='save-btn']");
    }

    [Then(@"I should see the vacancy creation success message")]
    public async Task ThenIShouldSeeTheVacancyCreationSuccessMessage()
    {
        await _page.WaitForSelectorAsync("text=Success, text=Vacancy created, text=added successfully, [data-testid='success-message']", 
            new PageWaitForSelectorOptions { Timeout = 5000 });
    }

    [When(@"I click on the first vacancy in the list")]
    public async Task WhenIClickOnTheFirstVacancyInTheList()
    {
        await _page.ClickAsync("table tbody tr:first-child, [data-testid='vacancy-row']:first-child");
    }

    [Then(@"I should see the Candidates list page")]
    public async Task ThenIShouldSeeTheCandidatesListPage()
    {
        await _page.WaitForSelectorAsync("text=Candidates, text=Applications, [data-testid='candidates-page']", 
            new PageWaitForSelectorOptions { Timeout = 5000 });
    }

    [Then(@"the Add Candidate button should be visible")]
    public async Task ThenTheAddCandidateButtonShouldBeVisible()
    {
        var addButton = await _page.QuerySelectorAsync("button:has-text('Add Candidate'), [data-testid='add-candidate-btn']");
        addButton.Should().NotBeNull("Add Candidate button should be visible");
    }
}
