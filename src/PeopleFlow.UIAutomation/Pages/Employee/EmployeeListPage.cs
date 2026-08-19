using Microsoft.Playwright;

namespace PeopleFlow.UIAutomation.Pages.Employee;

/// <summary>
/// POM: Employee List Page — PIM Module
/// </summary>
public class EmployeeListPage : BasePage
{
    private const string AddEmployeeButton = "button:has-text('Add')";
    private const string SearchInput = "input[placeholder='Type for hints...']";
    private const string SearchButton = "button[type='submit']";
    private const string EmployeeTable = ".oxd-table-body";
    private const string EmployeeRows = ".oxd-table-row";
    private const string NoRecordsText = ".oxd-text:has-text('No Records Found')";

    public EmployeeListPage(IPage page) : base(page) { }

    public async Task<bool> IsEmployeeListLoadedAsync() => await IsVisibleAsync(EmployeeTable);

    public async Task ClickAddEmployeeAsync() => await ClickAsync(AddEmployeeButton);

    public async Task SearchByNameAsync(string name)
    {
        await FillAsync(SearchInput, name);
        await ClickAsync(SearchButton);
        await WaitForPageLoadAsync();
    }

    public async Task<int> GetEmployeeCountAsync()
    {
        var rows = Page.Locator(EmployeeRows);
        return await rows.CountAsync() - 1; // minus header
    }

    public async Task<bool> IsNoRecordsFoundAsync() => await IsVisibleAsync(NoRecordsText);

    public async Task ClickEditOnRowAsync(int rowIndex)
    {
        var editBtn = Page.Locator($".oxd-table-row:nth-child({rowIndex + 1}) button:has-text('Edit')");
        await editBtn.ClickAsync();
        await WaitForPageLoadAsync();
    }

    public async Task ClickDeleteOnRowAsync(int rowIndex)
    {
        var deleteBtn = Page.Locator($".oxd-table-row:nth-child({rowIndex + 1}) button:has-text('Delete')");
        await deleteBtn.ClickAsync();
    }
}
