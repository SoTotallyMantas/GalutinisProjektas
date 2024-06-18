Feature: Carbon Emissions Estimation

  # Electricity Emission Estimate
  Scenario: Successful carbon emission estimation from electricity usage
    Given the carbon interface service is available
    When I request electricity carbon emission estimate
    Then the response should be successful and contain the electricity estimate

  # Flight Emission Estimate
  Scenario: Successful carbon emission estimation from flight
    Given the carbon interface service is available
    When I request flight carbon emission estimate
    Then the response should be successful and contain the flight estimate

  # Fuel Combustion Emission Estimate
  Scenario: Successful carbon emission estimation from fuel combustion
    Given the carbon interface service is available
    When I request fuel combustion carbon emission estimate
    Then the response should be successful and contain the fuel combustion estimate
