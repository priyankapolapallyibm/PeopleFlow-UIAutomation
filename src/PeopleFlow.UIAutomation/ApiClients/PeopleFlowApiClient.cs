using Newtonsoft.Json;
using RestSharp;
using PeopleFlow.UIAutomation.Config;
using PeopleFlow.UIAutomation.Helpers;
using PeopleFlow.UIAutomation.Models;

namespace PeopleFlow.UIAutomation.ApiClients;

/// <summary>
/// Fetches test data from the PeopleFlow REST API.
/// Used by step definitions to get dynamic data (users, employees, leave types)
/// instead of hardcoding values in feature files.
/// </summary>
public class PeopleFlowApiClient
{
    private readonly RestClient _client;
    private readonly AppSettings _settings;
    private string? _authToken;

    public PeopleFlowApiClient()
    {
        _settings = ConfigurationHelper.GetAppSettings();
        _client = new RestClient(_settings.ApiBaseUrl);
    }

    /// <summary>Authenticates with API and stores JWT token.</summary>
    public async Task<string> AuthenticateAsync(string username, string password)
    {
        var request = new RestRequest("/auth/login", Method.Post);
        request.AddJsonBody(new { username, password });

        var response = await _client.ExecuteAsync(request);
        var data = JsonConvert.DeserializeObject<dynamic>(response.Content ?? "{}");
        _authToken = data?.token?.ToString() ?? string.Empty;
        _client.AddDefaultHeader("Authorization", $"Bearer {_authToken}");
        return _authToken;
    }

    /// <summary>Gets list of employees from API for use in UI test data.</summary>
    public async Task<List<EmployeeModel>> GetEmployeesAsync()
    {
        var request = new RestRequest("/employees");
        var response = await _client.ExecuteAsync<List<EmployeeModel>>(request);
        return response.Data ?? new List<EmployeeModel>();
    }

    /// <summary>Gets available leave types for the current user.</summary>
    public async Task<List<LeaveTypeModel>> GetLeaveTypesAsync()
    {
        var request = new RestRequest("/leave/types");
        var response = await _client.ExecuteAsync<List<LeaveTypeModel>>(request);
        return response.Data ?? new List<LeaveTypeModel>();
    }

    /// <summary>Gets list of job vacancies from Recruitment API.</summary>
    public async Task<List<VacancyModel>> GetVacanciesAsync()
    {
        var request = new RestRequest("/recruitment/vacancies");
        var response = await _client.ExecuteAsync<List<VacancyModel>>(request);
        return response.Data ?? new List<VacancyModel>();
    }
}
