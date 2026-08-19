# PeopleFlow UI Automation

Playwright + SpecFlow (BDD) + POM UI test automation for the PeopleFlow HR application.

## Tech Stack
| Tool | Purpose |
|------|---------|
| **Playwright** | Browser automation |
| **SpecFlow 3** | BDD / Gherkin feature files |
| **NUnit** | Test runner |
| **C# (.NET 8)** | Language |
| **RestSharp** | API client (fetch live test data) |
| **FluentAssertions** | Readable assertions |
| **Allure** | HTML test reports |

## Project Structure
```
src/PeopleFlow.UIAutomation/
├── Features/           # Gherkin .feature files (BDD scenarios)
│   ├── Auth/           # Login scenarios (US-294)
│   ├── Dashboard/      # Dashboard UI (US-296)
│   ├── Employee/       # Employee CRUD/List (US-303, US-304)
│   ├── Leave/          # Leave Management (US-308)
│   └── Recruitment/    # Recruitment UI (US-312, US-313)
├── Pages/              # Page Object Model (POM)
│   ├── BasePage.cs     # Shared helpers for all pages
│   ├── Auth/           # LoginPage
│   ├── Dashboard/      # DashboardPage
│   ├── Employee/       # EmployeeListPage, EmployeeFormPage
│   ├── Leave/          # ApplyLeavePage
│   └── Recruitment/    # RecruitmentPage
├── StepDefinitions/    # SpecFlow step bindings
├── Hooks/              # PlaywrightHooks (browser lifecycle)
├── ApiClients/         # PeopleFlowApiClient (live API test data)
├── Models/             # API response models
├── Config/             # appsettings.json + AppSettings.cs
└── Helpers/            # ConfigurationHelper
```

## Tagging Convention
Each scenario is tagged for filtering and ADO traceability:

| Tag | Purpose |
|-----|---------|
| `@UI` / `@API` / `@Integration` | Test type |
| `@US-294` | Links to ADO User Story 294 |
| `@TC-294-01` | Links to ADO Test Case ID |
| `@Smoke` | Smoke regression suite |

## Running Tests

### Prerequisites
```bash
# Install .NET 8 SDK
# Install Playwright browsers
cd src/PeopleFlow.UIAutomation
dotnet tool install --global Microsoft.Playwright.CLI
pwsh bin/Debug/net8.0/playwright.ps1 install
```

### Run all tests
```bash
dotnet test src/PeopleFlow.UIAutomation/PeopleFlow.UIAutomation.csproj
```

### Run by tag
```bash
# Smoke tests only
dotnet test --filter "Category=Smoke"

# Tests for specific US
dotnet test --filter "Category=US-294"

# Tests by type
dotnet test --filter "Category=UI"
```

### Run headless
```bash
AppSettings__Headless=true dotnet test ...
```

## Connecting to Azure DevOps Test Plan

Test cases in ADO Test Plan 362 are tagged with `@TC-{id}` in the feature files.
After a CI run, use the ADO REST API to update test results:

```
PATCH /_apis/test/Runs/{runId}/Results
```

This marks the corresponding ADO Test Case as Pass/Fail automatically.

## ADO Test Plan
- **Plan ID**: 362  
- **Project**: PeopleFlowNew  
- **Org**: https://dev.azure.com/squaresquad26

## Application URL
- **UI**: http://localhost:80  
- **API**: http://localhost:3000/api
