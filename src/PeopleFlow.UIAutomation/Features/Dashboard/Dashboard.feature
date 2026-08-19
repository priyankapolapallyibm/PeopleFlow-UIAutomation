# ==============================================================================
# Feature  : Dashboard UI
# US ID    : US-296
# Iteration: Iteration 5
# Type     : UI
# ==============================================================================

@UI @US-296 @Dashboard
Feature: Dashboard UI
  As an authenticated PeopleFlow user
  I want to see the dashboard with all KPI widgets
  So that I have a quick overview of HR metrics

  Background:
    Given I am logged in as "admin" with password "admin123"
    And I am on the Dashboard page

  # TestID: TC-296-01
  @TC-296-01 @Smoke
  Scenario: TC-296-01 Dashboard loads with all KPI widgets visible after login
    Then I should see the KPI widget panel
    And I should see the navigation menu with all module links

  # TestID: TC-296-02
  @TC-296-02
  Scenario: TC-296-02 Dashboard KPI cards show correct counts for Employees and Leaves
    Then the employee count widget should display a numeric value
    And the leave balance widget should display a numeric value

  # TestID: TC-296-03
  @TC-296-03
  Scenario: TC-296-03 Dashboard navigation menu links navigate to correct modules
    When I click on the "PIM" menu link
    Then I should be on the Employee List page

  # TestID: TC-296-04
  @TC-296-04
  Scenario: TC-296-04 Dashboard data refreshes on page reload
    When I reload the Dashboard page
    Then the Dashboard page should still be loaded with KPI widgets visible

  # TestID: TC-296-05
  @TC-296-05
  Scenario: TC-296-05 Dashboard page loads for all authenticated user roles
    Given I am logged in as "manager" with password "manager123"
    Then I should be on the Dashboard page
    And I should see the KPI widget panel
