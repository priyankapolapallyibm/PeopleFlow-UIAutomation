using Microsoft.Playwright;

namespace PeopleFlow.UIAutomation.Pages.Leave;

/// <summary>
/// POM: Apply Leave Page
/// </summary>
public class ApplyLeavePage : BasePage
{
    private const string LeaveTypeDropdown = ".oxd-select-wrapper:first-child";
    private const string FromDateInput = "input[placeholder='yyyy-dd-mm']:first-child";
    private const string ToDateInput = "input[placeholder='yyyy-dd-mm']:last-child";
    private const string CommentsInput = "textarea";
    private const string ApplyButton = "button[type='submit']:has-text('Apply')";
    private const string SuccessToast = ".oxd-toast--success";
    private const string ValidationError = ".oxd-input-field-error-message";

    public ApplyLeavePage(IPage page) : base(page) { }

    public async Task SelectLeaveTypeAsync(string leaveType) => await SelectDropdownAsync(LeaveTypeDropdown, leaveType);

    public async Task SetFromDateAsync(string date) => await FillAsync(FromDateInput, date);

    public async Task SetToDateAsync(string date) => await FillAsync(ToDateInput, date);

    public async Task EnterCommentsAsync(string comment) => await FillAsync(CommentsInput, comment);

    public async Task ClickApplyAsync() => await ClickAsync(ApplyButton);

    public async Task<bool> IsSuccessToastVisibleAsync() => await IsVisibleAsync(SuccessToast);

    public async Task<string> GetValidationErrorAsync() => await GetTextAsync(ValidationError);
}
