import React, { useEffect, useState } from 'react';
import './Emission.css';
import ComboBox from './ComboBox';

const FuelCombustionEmission = () => {
    const [fuelTypes, setFuelTypes] = useState([]);
    const [filteredUnits, setFilteredUnits] = useState([]);
    const [selectedFuelType, setSelectedFuelType] = useState('');
    const [value, setValue] = useState('');

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
                    disabled={!selectedFuelType}
                />

                <div className="input-group">
                    <label>Value:</label>
                    <input type="text" />
                </div>
                <button className="result-button">Get result</button>
            </div>
            <div className="right-section">
                <p>Emission Result</p>
                <div className="big-box"></div>
            </div>
        </div>
    );
};

export default FuelCombustionEmission;