import React, { useState, useEffect, useCallback } from 'react';
import './Emission.css';
import { FixedSizeList as List } from 'react-window';
import './List.css';
import ComboBox from './ComboBox';

const ElectricityEmission = () => {
    const options = [
        { label: 'megawatt hours (mwh)', value: 'mwh' },
        { label: 'kilowatt hours (kwh)', value: 'kwh' },
    ];

    const [data, setData] = useState([]);
    const [filteredData, setFilteredData] = useState([]);
    const [countryFilter, setCountryFilter] = useState('');
    const [selectedCountry, setSelectedCountry] = useState(null);
    const [electricityUnit, setElectricityUnit] = useState(options[0].value);
    const [electricityValue, setElectricityValue] = useState('');
    const [emissionResult, setEmissionResult] = useState(null);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);

    async function fetchCountryCodes() {
        try {
            const response = await fetch('/CountryCodes');
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            const result = await response.json();
            const transformedData = result.map(item => ({
                id: item.id,
                countryCode: item.countryCode,
                countryName: item.countryName
            }));
            setData(transformedData);
            setFilteredData(transformedData);
        } catch (error) {
            console.error('Error fetching country codes:', error);
        }
    }

    useEffect(() => {
        fetchCountryCodes();
    }, []);

    useEffect(() => {
        const filtered = data.filter(item =>
            item.countryName.toLowerCase().includes(countryFilter.toLowerCase()) ||
            item.countryCode.toLowerCase().includes(countryFilter.toLowerCase())
        );
        setFilteredData(filtered);
    }, [countryFilter, data]);

    const fetchEmissionData = async () => {
        if (!selectedCountry) {
            setError("Please select a country.");
            return;
        }

        setLoading(true);
        setError(null);
        try {
            const response = await fetch(`/CarbonInterface/Electricity?electricity_unit=${electricityUnit}&electricity_value=${electricityValue}&country=${selectedCountry.countryCode}`, {
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

    const handleRowClick = (country) => {
        setSelectedCountry(country);
    };

    const Row = useCallback(({ index, style }) => {
        const row = filteredData[index];
        return (
            <div
                className="list-item"
                style={{
                    ...style,
                    backgroundColor: selectedCountry && selectedCountry.id === row.id ? 'lightgray' : 'white',
                    display: 'flex',
                    cursor: 'pointer'
                }}
                onClick={() => handleRowClick(row)}
            >
                <div style={{ flex: 1, padding: '10px' }}>{row.countryCode}</div>
                <div style={{ flex: 1, padding: '10px' }}>{row.countryName}</div>
            </div>
        );
    }, [filteredData, selectedCountry]);

    return (
        <div className="container">
            <div className="left-section">
                <div>
                    <h1>Electricity Emission</h1>
                </div>
                <ComboBox
                    label="Unit:"
                    options={options}
                    value={electricityUnit}
                    onChange={(e) => setElectricityUnit(e.target.value)}
                />
                <div className="input-group">
                    <label>Value:</label>
                    <input
                        type="number"
                        value={electricityValue}
                        onChange={(e) => setElectricityValue(e.target.value)}
                    />
                </div>
                <div className="input-group">
                    <label>Country Filter:</label>
                    <input
                        type="text"
                        value={countryFilter}
                        onChange={(e) => setCountryFilter(e.target.value)}
                    />
                </div>
                <div className="data-table-container">
                    <div className="table-header">
                        <div className="table-row">
                            <div className="table-cell">Country Code</div>
                            <div className="table-cell">Country Name</div>
                        </div>
                    </div>
                    <List
                        className="electricity-list"
                        height={350}
                        itemCount={filteredData.length}
                        itemSize={35}
                        width={'100%'}
                    >
                        {Row}
                    </List>
                </div>
                <div className="button-group">
                    <button onClick={fetchEmissionData}>Get result</button>
                </div>
            </div>
            <div className="right-section">
                <p>Emission Result</p>
                {loading && <p>Loading...</p>}
                {error && <p>{error}</p>}
                {emissionResult && (
                    <div className="list-item">
                        <div>Country: {emissionResult.attributes.country}</div>
                        <div>Electricity Unit: {emissionResult.attributes.electricity_unit}</div>
                        <div>Electricity Value: {emissionResult.attributes.electricity_value}</div>
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

export default ElectricityEmission;
