# ==============================================================================
# Feature  : Employee CRUD - Create, Edit & Delete
# US ID    : US-304
# Iteration: Iteration 11
# Type     : UI
# ==============================================================================

@UI @US-304 @Employee
Feature: Employee CRUD UI
  As an Admin
  I want to create, edit, and delete employee records
  So that I can keep the employee database up to date

  Background:
    Given I am logged in as "admin" with password "admin123"
    And I navigate to the Employee List page

  # TestID: TC-304-01
  @TC-304-01 @Smoke
  Scenario: TC-304-01 Admin clicks Add Employee button and form opens with required fields
    When I click the Add Employee button
    Then I should see the Add Employee form
    And the First Name field should be visible
    And the Last Name field should be visible

  # TestID: TC-304-02
  @TC-304-02
  Scenario: TC-304-02 Admin fills Employee form with valid data and saves successfully
    When I click the Add Employee button
    And I enter first name "Alice" in the First Name field
    And I enter last name "Walker" in the Last Name field
    And I click the Save button
    Then I should see the success notification
    And the new employee "Alice Walker" should appear in the employee list

  # TestID: TC-304-03
  @TC-304-03
  Scenario: TC-304-03 Admin edits an existing employee and updates phone number
    When I click Edit on the first employee row
    And I update the phone number to "9876543210"
    And I click the Save button
    Then I should see the success notification

  # TestID: TC-304-04
  @TC-304-04
  Scenario: TC-304-04 Admin deletes an employee and confirmation dialog appears
    When I click Delete on the first employee row
    Then I should see the delete confirmation dialog
    When I confirm the deletion
    Then the employee should be removed from the list
