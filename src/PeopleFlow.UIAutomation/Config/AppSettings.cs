namespace PeopleFlow.UIAutomation.Config;

public class AppSettings
{
    public string BaseUrl { get; set; } = "http://localhost:80";
    public string ApiBaseUrl { get; set; } = "http://localhost:3000/api";
    public string Browser { get; set; } = "chromium";
    public bool Headless { get; set; } = false;
    public int SlowMo { get; set; } = 0;
    public int DefaultTimeout { get; set; } = 30000;
    public bool ScreenshotOnFailure { get; set; } = true;
    public bool VideoOnFailure { get; set; } = false;
    public AdoSettings ADO { get; set; } = new();
}

public class AdoSettings
{
    public string OrgUrl { get; set; } = string.Empty;
    public string Project { get; set; } = string.Empty;
    public string PlanId { get; set; } = string.Empty;
}

public class Credentials
{
    public string AdminUser { get; set; } = string.Empty;
    public string AdminPassword { get; set; } = string.Empty;
    public string ManagerUser { get; set; } = string.Empty;
    public string ManagerPassword { get; set; } = string.Empty;
    public string EmployeeUser { get; set; } = string.Empty;
    public string EmployeePassword { get; set; } = string.Empty;
}
