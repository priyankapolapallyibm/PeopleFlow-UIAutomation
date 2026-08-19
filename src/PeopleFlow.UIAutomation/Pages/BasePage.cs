using Microsoft.Playwright;

namespace PeopleFlow.UIAutomation.Pages;

/// <summary>
/// Base Page Object — all page classes inherit from this.
/// Provides shared IPage reference and common helper methods.
/// </summary>
public abstract class BasePage
{
    protected readonly IPage Page;

    protected BasePage(IPage page)
    {
        Page = page;
    }

    protected async Task WaitForPageLoadAsync()
    {
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }

    protected async Task ClickAsync(string selector)
    {
        await Page.Locator(selector).WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        await Page.Locator(selector).ClickAsync();
    }

    protected async Task FillAsync(string selector, string value)
    {
        await Page.Locator(selector).WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        await Page.Locator(selector).ClearAsync();
        await Page.Locator(selector).FillAsync(value);
    }

    protected async Task<string> GetTextAsync(string selector)
    {
        await Page.Locator(selector).WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        return await Page.Locator(selector).InnerTextAsync();
    }

    protected async Task<bool> IsVisibleAsync(string selector)
    {
        return await Page.Locator(selector).IsVisibleAsync();
    }

    protected async Task SelectDropdownAsync(string selector, string value)
    {
        await Page.Locator(selector).SelectOptionAsync(value);
    }
}
