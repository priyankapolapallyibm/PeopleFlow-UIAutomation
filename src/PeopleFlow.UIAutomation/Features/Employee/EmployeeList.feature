# ==============================================================================
# Feature  : Employee UI - Search, Filter & Display
# US ID    : US-303
# Iteration: Iteration 11
# Type     : UI
# ==============================================================================

@UI @US-303 @Employee
Feature: Employee List UI
  As an Admin
  I want to view, search and filter the employee list
  So that I can manage employee records efficiently

  Background:
    Given I am logged in as "admin" with password "admin123"
    And I navigate to the Employee List page

  # TestID: TC-303-01
  @TC-303-01 @Smoke @Regression
  Scenario: TC-303-01 Admin opens Employee List page and sees all employees in table
    Then I should see the employee list table
    And the table should contain at least one employee record

  # TestID: TC-303-02
  @TC-303-02 @Regression
  Scenario: TC-303-02 Admin searches employee by name and filtered results appear
    When I enter "John" in the employee name search field
    And I click the Search button
    Then the employee table should show only records matching "John"

  # TestID: TC-303-03
  @TC-303-03 @Regression
  Scenario: TC-303-03 No records found message appears for invalid search
    When I enter "ZZZNONEXISTENT999" in the employee name search field
    And I click the Search button
    Then I should see the "No Records Found" message
