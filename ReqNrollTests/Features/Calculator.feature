Feature: Retrieve air pollution data
  As a user
  I want to get air pollution data for specific coordinates
  So that I can be informed about air quality

  Scenario: Successful retrieval of air pollution data
    Given I have the latitude 34.05 and longitude -118.25
    When I request air pollution data
    Then the response should be successful and contain air pollution details

  Scenario: Failed retrieval of air pollution data due to invalid coordinates
    Given I have the latitude 999 and longitude -999
    When I request air pollution data
    Then the response should indicate an invalid request
