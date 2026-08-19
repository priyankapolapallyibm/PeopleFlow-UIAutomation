using Microsoft.Playwright;
using PeopleFlow.UIAutomation.Config;
using PeopleFlow.UIAutomation.Helpers;

namespace PeopleFlow.UIAutomation.Hooks;

/// <summary>
/// Manages Playwright browser lifecycle per test scenario.
/// Shared via ScenarioContext so all Page Objects access the same IPage.
/// </summary>
[Binding]
public class PlaywrightHooks
{
    private readonly ScenarioContext _scenarioContext;
    private static IPlaywright? _playwright;
    private static IBrowser? _browser;
    private IBrowserContext? _browserContext;
    private IPage? _page;

    private static readonly AppSettings Settings = ConfigurationHelper.GetAppSettings();

    public PlaywrightHooks(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [BeforeTestRun]
    public static async Task BeforeTestRun()
    {
        _playwright = await Playwright.CreateAsync();
        var launchOptions = new BrowserTypeLaunchOptions
        {
            Headless = Settings.Headless,
            SlowMo = Settings.SlowMo
        };

        _browser = Settings.Browser.ToLower() switch
        {
            "firefox" => await _playwright.Firefox.LaunchAsync(launchOptions),
            "webkit" => await _playwright.Webkit.LaunchAsync(launchOptions),
            _ => await _playwright.Chromium.LaunchAsync(launchOptions)
        };
    }

    [BeforeScenario]
    public async Task BeforeScenario()
    {
        var contextOptions = new BrowserNewContextOptions
        {
            ViewportSize = new ViewportSize { Width = 1440, Height = 900 },
            RecordVideoDir = Settings.VideoOnFailure ? "TestResults/Videos" : null
        };

        _browserContext = await _browser!.NewContextAsync(contextOptions);
        _browserContext.SetDefaultTimeout(Settings.DefaultTimeout);
        _page = await _browserContext.NewPageAsync();

        _scenarioContext["Page"] = _page;
        _scenarioContext["BrowserContext"] = _browserContext;
    }

    [AfterScenario]
    public async Task AfterScenario()
    {
        if (_scenarioContext.TestError != null && Settings.ScreenshotOnFailure)
        {
            var screenshotDir = "TestResults/Screenshots";
            Directory.CreateDirectory(screenshotDir);
            var fileName = $"{screenshotDir}/{_scenarioContext.ScenarioInfo.Title.Replace(" ", "_")}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            await _page!.ScreenshotAsync(new PageScreenshotOptions { Path = fileName, FullPage = true });
        }

        await _browserContext!.CloseAsync();
    }

    [AfterTestRun]
    public static async Task AfterTestRun()
    {
        await _browser!.CloseAsync();
        _playwright?.Dispose();
    }
}
