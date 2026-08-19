using Microsoft.Playwright;

namespace PeopleFlow.UIAutomation.Pages.Employee;

/// <summary>
/// POM: Add/Edit Employee Form Page
/// </summary>
public class EmployeeFormPage : BasePage
{
    private const string FirstNameInput = "input[name='firstName']";
    private const string MiddleNameInput = "input[name='middleName']";
    private const string LastNameInput = "input[name='lastName']";
    private const string EmployeeIdInput = "input[name='employeeId']";
    private const string SaveButton = "button[type='submit']:has-text('Save')";
    private const string SuccessToast = ".oxd-toast--success";
    private const string ConfirmDeleteButton = ".orangehrm-modal-footer button:has-text('Yes, Delete')";

    public EmployeeFormPage(IPage page) : base(page) { }

    public async Task EnterFirstNameAsync(string firstName) => await FillAsync(FirstNameInput, firstName);
    public async Task EnterLastNameAsync(string lastName) => await FillAsync(LastNameInput, lastName);
    public async Task EnterEmployeeIdAsync(string empId) => await FillAsync(EmployeeIdInput, empId);

    public async Task ClickSaveAsync() => await ClickAsync(SaveButton);

    public async Task<bool> IsSuccessToastVisibleAsync() => await IsVisibleAsync(SuccessToast);

    public async Task ConfirmDeleteAsync() => await ClickAsync(ConfirmDeleteButton);
}
