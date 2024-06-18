import React from 'react';
import { useLocation } from 'react-router-dom';
import styles from './FlightCombustionResult.module.css';

const EmissionResult = () => {
    const location = useLocation();
    const { result } = location.state || {};

    const { data } = result || {};
    const { attributes } = data || {};
    const { passengers, legs, estimated_at, carbon_g, carbon_lb, carbon_kg, carbon_mt, distance_unit, distance_value } = attributes || {};

    return (
        <div className={styles['emission-result-page']}>
            <div className={styles.container}>
                <div>
                    <h1>Flight Emission Result</h1>
                </div>
                {data ? (
                    <div className={styles['result-container']}>
                        <h2>Emission Details:</h2>
                        <p><strong>Passengers:</strong> {passengers}</p>
                        <p><strong>Legs:</strong></p>
                        <ul>
                            {legs && legs.map((leg, index) => (
                                <li key={index}>
                                    <strong>Departure Airport:</strong> {leg.departure_airport}, <strong>Destination Airport:</strong> {leg.destination_airport}
                                </li>
                            ))}
                        </ul>
                        <p><strong>Estimated At:</strong> {new Date(estimated_at).toLocaleString()}</p>
                        <p><strong>Carbon Emissions:</strong></p>
                        <ul>
                            <li><strong>Grams:</strong> {carbon_g}</li>
                            <li><strong>Pounds:</strong> {carbon_lb}</li>
                            <li><strong>Kilograms:</strong> {carbon_kg}</li>
                            <li><strong>Metric Tons:</strong> {carbon_mt}</li>
                        </ul>
                        <p><strong>Distance:</strong> {distance_value} {distance_unit}</p>
                    </div>
                ) : (
                    <p>No result data available.</p>
                )}
            </div>
        </div>
    );
};

export default EmissionResult;
