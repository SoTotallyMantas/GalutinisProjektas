Feature: Fuel Types Management

  Scenario: Successful retrieval of all fuel types
    Given the fuel types service is available
    When I request all fuel types
    Then the response should be successful and contain the fuel types

  Scenario: Successful retrieval of a fuel type by ID
    Given the fuel type with ID 1 is available in the service
    When I request the fuel type by ID 1
    Then the response should be successful and contain the fuel type

  Scenario: Successful retrieval of a fuel type by name
    Given the fuel type with name "diesel" is available in the service
    When I request the fuel type by name "diesel"
    Then the response should be successful and contain the fuel type by name

