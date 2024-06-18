Feature: IATACodesController

  Scenario: Retrieve all IATA codes
    Given the IATA codes service is available
    When I request all IATA codes
    Then the response should be successful and contain the IATA codes

  Scenario: Retrieve IATA code by ID
    Given the IATA code with ID 1 is available in the service
    When I request the IATA code by ID 1
    Then the response should be successful and contain the IATA code

  Scenario: Retrieve IATA code by code
    Given the IATA code with code "LAX" is available in the service
    When I request the IATA code by code "LAX"
    Then the response should be successful and contain the IATA code by code
