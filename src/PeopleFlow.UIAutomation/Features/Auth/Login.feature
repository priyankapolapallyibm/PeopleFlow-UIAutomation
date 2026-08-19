# ==============================================================================
# Feature  : Auth - Login UI
# US ID    : US-294
# Iteration: Iteration 2
# Type     : UI
# ==============================================================================

@UI @US-294 @Auth
Feature: Login UI
  As a PeopleFlow user
  I want to log in via the login page at http://localhost:80/auth/login
  So that I can access the HR management system

  Background:
    Given I navigate to the PeopleFlow login page

  # TC-289-01 | TestID: TC-294-01
  @TC-294-01 @Smoke @Regression
  Scenario: TC-294-01 Login page loads with username and password fields
    Then I should see the username input field
    And I should see the password input field
    And I should see the Login button

  # TC-289-02 | TestID: TC-294-02
  @TC-294-02 @Smoke @Regression
  Scenario: TC-294-02 Admin logs in with valid credentials and lands on Dashboard
    When I enter username "admin" in the Username field
    And I enter password "admin123" in the Password field
    And I click the Login button
    Then I should be redirected to the Dashboard page
    And I should see the navigation menu

  # TC-289-03 | TestID: TC-294-03
  @TC-294-03 @Regression
  Scenario: TC-294-03 Login fails with incorrect password and shows error
    When I enter username "admin" in the Username field
    And I enter password "wrongpassword" in the Password field
    And I click the Login button
    Then I should see the error message "Invalid credentials"

  # TC-289-04 | TestID: TC-294-04
  @TC-294-04 @Regression
  Scenario: TC-294-04 Login fails with empty username field
    When I enter username "" in the Username field
    And I enter password "admin123" in the Password field
    And I click the Login button
    Then I should see a required field validation error

  # TC-289-05 | TestID: TC-294-05
  @TC-294-05 @Regression
  Scenario Outline: TC-294-05 Multiple user roles can log in successfully
    When I enter username "<username>" in the Username field
    And I enter password "<password>" in the Password field
    And I click the Login button
    Then I should be redirected to the Dashboard page

    Examples:
      | username | password    |
      | admin    | admin123    |
      | manager  | manager123  |
