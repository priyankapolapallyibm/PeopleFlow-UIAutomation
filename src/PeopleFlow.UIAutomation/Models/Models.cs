namespace PeopleFlow.UIAutomation.Models;

public class EmployeeModel
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}

public class LeaveTypeModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int EntitlementDays { get; set; }
}

public class VacancyModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string HiringManager { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
}
