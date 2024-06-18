import React, { useEffect, useState } from 'react';
import './Emission.css';
import './List.css';
import ComboBox from './ComboBox';

const FuelCombustionEmission = () => {
    const [fuelTypes, setFuelTypes] = useState([]);
    const [filteredUnits, setFilteredUnits] = useState([]);
    const [selectedFuelType, setSelectedFuelType] = useState('');
    const [selectedFuelUnit, setSelectedFuelUnit] = useState('');
    const [value, setValue] = useState('');
    const [emissionResult, setEmissionResult] = useState(null);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);

    useEffect(() => {
        fetch('/FuelTypes')
            .then(response => response.json())
            .then(data => {
                const uniqueFuelTypesMap = new Map();
                data.forEach(ft => {
                    if (!uniqueFuelTypesMap.has(ft.fuelType)) {
                        uniqueFuelTypesMap.set(ft.fuelType, ft.fuelName);
                    }
                });
                const uniqueFuelTypes = Array.from(uniqueFuelTypesMap.entries()).map(([value, label]) => ({ value, label }));
                setFuelTypes(uniqueFuelTypes);
            })
            .catch(error => console.error('Error fetching fuel types:', error));
    }, []);

    useEffect(() => {
        if (selectedFuelType) {
            fetch(`/FuelTypes/GetByFuelType/${selectedFuelType}`)
                .then(response => response.json())
                .then(data => {
                    setFilteredUnits(data.map(unit => ({ value: unit.fuelUnit, label: unit.fuelUnit })));
                })
                .catch(error => console.error('Error fetching fuel units:', error));
        } else {
            setFilteredUnits([]);
        }
    }, [selectedFuelType]);

    const fetchEmissionData = async () => {
        if (!selectedFuelType || !selectedFuelUnit || !value) {
            setError("Please select fuel type, unit, and enter a value.");
            return;
        }

        setLoading(true);
        setError(null);
        try {
            const response = await fetch(`/CarbonInterface/Fuel?fuel_source_type=${selectedFuelType}&fuel_source_unit=${selectedFuelUnit}&fuel_source_value=${value}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
            });
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            const result = await response.json();
            setEmissionResult(result.data);
        } catch (error) {
            setError(error.toString());
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="container">
            <div className="left-section">
                <div>
                    <h1>Fuel Combustion Emission</h1>
                </div>
                <ComboBox
                    label="Fuel Type:"
                    options={fuelTypes}
                    onSelect={setSelectedFuelType}
                />
                <ComboBox
                    label="Fuel Unit:"
                    options={filteredUnits}
                    onSelect={setSelectedFuelUnit}
                    disabled={!selectedFuelType}
                />
                <div className="input-group">
                    <label>Value:</label>
                    <input
                        type="number"
                        value={value}
                        onChange={(e) => setValue(e.target.value)}
                    />
                </div>
                <button className="result-button" onClick={fetchEmissionData}>Get result</button>
            </div>
            <div className="right-section">
                <p>Emission Result</p>
                {loading && <p>Loading...</p>}
                {error && <p>{error}</p>}
                {emissionResult && (
                    <div className="list-item">
                        <div>Fuel Source Type: {emissionResult.attributes.fuel_source_type}</div>
                        <div>Fuel Source Unit: {emissionResult.attributes.fuel_source_unit}</div>
                        <div>Fuel Source Value: {emissionResult.attributes.fuel_source_value}</div>
                        <div>Carbon (g): {emissionResult.attributes.carbon_g}</div>
                        <div>Carbon (lb): {emissionResult.attributes.carbon_lb}</div>
                        <div>Carbon (kg): {emissionResult.attributes.carbon_kg}</div>
                        <div>Carbon (mt): {emissionResult.attributes.carbon_mt}</div>
                    </div>
                )}
            </div>
        </div>
    );
};

export default FuelCombustionEmission;