# ==============================================================================
# Feature  : Recruitment UI - Job Postings & Candidates
# US ID    : US-312, US-313
# Iteration: Iteration 17
# Type     : UI
# ==============================================================================

@UI @US-312 @US-313 @Recruitment
Feature: Recruitment UI
  As an HR Manager
  I want to manage job vacancies and candidates
  So that the hiring process is tracked end-to-end

  Background:
    Given I am logged in as "admin" with password "admin123"
    And I navigate to the Recruitment module

  # TestID: TC-312-01
  @TC-312-01 @Smoke @Regression @US-312
  Scenario: TC-312-01 HR Manager sees list of job vacancies on Recruitment page
    Then I should see the Vacancies list table
    And the table should contain at least one vacancy

  # TestID: TC-312-02
  @TC-312-02 @Regression @US-312
  Scenario: TC-312-02 HR Manager adds a new job vacancy with valid details
    When I click the Add Vacancy button
    And I enter vacancy title "Senior QA Engineer" in the Vacancy Name field
    And I select hiring manager "Admin" from the Hiring Manager dropdown
    And I click the Save button
    Then I should see the vacancy creation success message

  # TestID: TC-313-01
  @TC-313-01 @Regression @US-313
  Scenario: TC-313-01 HR Manager views candidate list for a vacancy
    When I click on the first vacancy in the list
    Then I should see the Candidates list page
    And the Add Candidate button should be visible
