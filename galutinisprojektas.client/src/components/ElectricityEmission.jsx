import React, { useState, useEffect, useCallback } from 'react';
import { FixedSizeList as List } from 'react-window';
import './Emission.css';
import ComboBox from './ComboBox';

const ElectricityEmission = () => {
    const [selectedRow, setSelectedRow] = useState(null);
    const [data, setData] = useState([]);
    const [filteredData, setFilteredData] = useState([]);
    const [countryFilter, setCountryFilter] = useState('');
    const [unit, setUnit] = useState('');
    const [value, setValue] = useState('');
    const [emissionResult, setEmissionResult] = useState(null);
    const [selectedCountry, setSelectedCountry] = useState('');

    async function fetchIATACodes() {
        try {
            const response = await fetch('/IATACodes');
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            const result = await response.json();
            console.log('Fetched IATA Codes:', result); // Debug log
            const transformedData = result.map(item => ({
                id: item.id,
                IATACode: item.iata,
                city: item.city,
                airportName: item.airportName,
                country: item.country
            }));
            setData(transformedData);
            setFilteredData(transformedData);
        } catch (error) {
            console.error('Error fetching IATA codes:', error);
        }
    }

    useEffect(() => {
        fetchIATACodes();
    }, []);

    useEffect(() => {
        const filtered = data.filter(item =>
            item.country.toLowerCase().includes(countryFilter.toLowerCase())
        );
        console.log('Filtered Data:', filtered); // Debug log
        setFilteredData(filtered);
    }, [countryFilter, data]);

    const handleRowClicked = (row) => {
        setSelectedRow(row);
        setSelectedCountry(row.country);
        console.log('Selected Country:', row.country); // Debug log
    };

    const Row = useCallback(({ index, style }) => {
        const row = filteredData[index];
        return (
            <div
                style={{
                    ...style,
                    backgroundColor: selectedRow && selectedRow.id === row.id ? '#f1f1f1' : 'white',
                    cursor: 'pointer',
                    display: 'flex'
                }}
                onClick={() => handleRowClicked(row)}
            >
                <div style={{ flex: 1, padding: '10px' }}>{row.IATACode}</div>
                <div style={{ flex: 1, padding: '10px' }}>{row.city}</div>
                <div style={{ flex: 1, padding: '10px' }}>{row.airportName}</div>
                <div style={{ flex: 1, padding: '10px' }}>{row.country}</div>
            </div>
        );
    }, [filteredData, selectedRow]);

    const options = [
        { label: 'megawatt hours (mwh)', value: 'mwh' },
        { label: 'kilowatt hours (kmh)', value: 'kmh' },
    ];

    const handleGetResult = async () => {
        if (!selectedCountry || !unit || !value) {
            alert('Please select a country, unit, and enter a value.');
            console.log('Missing Parameters:', { selectedCountry, unit, value }); // Debug log
            return;
        }

        console.log('Request Parameters:', {
            unit,
            value,
            country: selectedCountry
        }); // Debug log

        try {
            const response = await fetch(`/CarbonInterface/Electricity?electricity_unit=${unit}&electricity_value=${value}&country=${selectedCountry}`);
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            const result = await response.json();
            setEmissionResult(result);
        } catch (error) {
            console.error('Error fetching emission data:', error);
        }
    };

    return (
        <div className="container">
            <div className="left-section">
                <div>
                    <h1>Electricity Emission</h1>
                </div>
                <ComboBox
                    label="Unit:"
                    options={options}
                    onChange={(e) => {
                        setUnit(e.target.value);
                        console.log('Selected Unit:', e.target.value); // Debug log
                    }}
                />
                <div className="input-group">
                    <label>Value:</label>
                    <input
                        type="text"
                        value={value}
                        onChange={(e) => setValue(e.target.value)}
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
                            <div className="table-cell">IATA Code</div>
                            <div className="table-cell">City</div>
                            <div className="table-cell">Airport Name</div>
                            <div className="table-cell">Country</div>
                        </div>
                    </div>
                    {filteredData.length > 0 ? (
                        <List
                            height={350}
                            itemCount={filteredData.length}
                            itemSize={35}
                            width={'100%'}
                        >
                            {Row}
                        </List>
                    ) : (
                        <div style={{ padding: '10px', textAlign: 'center' }}>No data available</div>
                    )}
                </div>
                <div className="button-group">
                    <button onClick={handleGetResult}>Get result</button>
                </div>
            </div>
            <div className="right-section">
                <p>Emission Result</p>
                <div className="big-box">
                    {emissionResult && (
                        <pre>{JSON.stringify(emissionResult, null, 2)}</pre>
                    )}
                </div>
            </div>
        </div>
    );
};

export default ElectricityEmission;