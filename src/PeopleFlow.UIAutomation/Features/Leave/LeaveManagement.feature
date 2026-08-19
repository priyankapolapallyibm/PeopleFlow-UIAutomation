# ==============================================================================
# Feature  : Leave UI - Apply, Approve & History
# US ID    : US-308
# Iteration: Iteration 14
# Type     : UI
# ==============================================================================

@UI @US-308 @Leave
Feature: Leave Management UI
  As a PeopleFlow user
  I want to apply for leave and track approval status
  So that my leave requests are processed correctly

  Background:
    Given I am logged in as "admin" with password "admin123"

  # TestID: TC-308-01
  @TC-308-01 @Smoke
  Scenario: TC-308-01 Employee opens Apply Leave page and sees leave form
    When I navigate to the Apply Leave page
    Then I should see the Leave Type dropdown
    And I should see the From Date field
    And I should see the To Date field
    And I should see the Apply button

  # TestID: TC-308-02
  @TC-308-02
  Scenario: TC-308-02 Employee applies for Sick leave from 2026-09-01 to 2026-09-03
    When I navigate to the Apply Leave page
    And I select leave type "Sick Leave" from the Leave Type dropdown
    And I enter start date "2026-09-01" in the From Date field
    And I enter end date "2026-09-03" in the To Date field
    And I click the Apply button
    Then I should see a leave application success message

  # TestID: TC-308-03
  @TC-308-03
  Scenario: TC-308-03 Employee cannot apply leave with end date before start date
    When I navigate to the Apply Leave page
    And I select leave type "Sick Leave" from the Leave Type dropdown
    And I enter start date "2026-09-05" in the From Date field
    And I enter end date "2026-09-01" in the To Date field
    And I click the Apply button
    Then I should see a date validation error message

  # TestID: TC-308-04
  @TC-308-04
  Scenario: TC-308-04 Employee sees pending leave request in Leave History table
    When I navigate to the My Leave page
    Then the leave history table should be visible
    And at least one leave request should be listed
