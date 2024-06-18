Feature: Retrieve weather forecast data
  As a user
  I want to get weather forecast data
  So that I can plan my activities

  Scenario: Successful retrieval of weather forecast data
    Given the weather forecast endpoint is available
    When I request weather forecast data
    Then the response should be successful and contain weather forecast details
