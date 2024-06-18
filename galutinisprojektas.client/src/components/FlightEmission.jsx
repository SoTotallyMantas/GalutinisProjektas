import React, { useState, useEffect, useCallback } from 'react';
import { useNavigate } from 'react-router-dom';
import { FixedSizeList as List } from 'react-window';
import styles from './FlightResult.module.css';

const FlightEmission = () => {
    const navigate = useNavigate();
    const [selectedRow, setSelectedRow] = useState(null);
    const [data, setData] = useState([]);
    const [filteredData, setFilteredData] = useState([]);
    const [countryFilter, setCountryFilter] = useState('');
    const [measurementUnit, setMeasurementUnit] = useState('mi');
    const [passengers, setPassengers] = useState('');
    const [flightSegments, setFlightSegments] = useState([{ departure: null, destination: null }]);
    const [currentSegmentIndex, setCurrentSegmentIndex] = useState(null);

    async function fetchIATACodes() {
        try {
            const response = await fetch('/IATACodes');
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            const result = await response.json();
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
        setFilteredData(filtered);
    }, [countryFilter, data]);

    const goToFlightCombustionResult = () => {
        if (validatePassengers()) {
            const legs = flightSegments.map(segment => ({
                departure_airport: segment.departure.IATACode,
                destination_airport: segment.destination.IATACode
            }));

            const requestBody = {
                type: 'flight',
                passengers: parseInt(passengers, 10),
                legs: legs,
                distance_unit: measurementUnit
            };

            fetch('/CarbonInterface/Flight', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(requestBody)
            })
                .then(response => {
                    if (!response.ok) {
                        throw new Error('Network response was not ok');
                    }
                    return response.json();
                })
                .then(data => {
                    console.log('Success:', data);
                    navigate('/flightCombustionResult', { state: { result: data } });
                })
                .catch(error => {
                    console.error('Error:', error);
                });
        } else {
            alert('Passengers must be a positive number.');
        }
    };

    const validatePassengers = () => {
        const passengersNumber = parseInt(passengers, 10);
        return passengersNumber > 0;
    };

    const handleRowClicked = (row) => {
        setSelectedRow(row);
    };

    const handleSetDeparture = () => {
        if (selectedRow !== null && currentSegmentIndex !== null) {
            setFlightSegments(prev =>
                prev.map((segment, index) =>
                    index === currentSegmentIndex ? { ...segment, departure: selectedRow } : segment
                )
            );
            setSelectedRow(null);
        }
    };

    const handleSetDestination = () => {
        if (selectedRow !== null && currentSegmentIndex !== null) {
            setFlightSegments(prev =>
                prev.map((segment, index) =>
                    index === currentSegmentIndex ? { ...segment, destination: selectedRow } : segment
                )
            );
            setSelectedRow(null);
        }
    };

    const handlePassengersChange = (e) => {
        const value = e.target.value;
        if (value === '' || /^\d+$/.test(value)) {
            setPassengers(value);
        }
    };

    const handleAddNewSegment = () => {
        setFlightSegments([...flightSegments, { departure: null, destination: null }]);
        setCurrentSegmentIndex(flightSegments.length);
    };

    const handleSegmentClick = (index) => {
        setCurrentSegmentIndex(prevIndex => prevIndex === index ? null : index);
    };

    const handleDeleteSegment = (index) => {
        if (flightSegments.length > 1) {
            const updatedSegments = flightSegments.filter((_, i) => i !== index);
            setFlightSegments(updatedSegments);
            setCurrentSegmentIndex(null);
        }
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

    return (
        <div className={styles["flight-emission-container"]}>
            <div className={styles["left-section"]}>
                <div>
                    <h1>Flight Emission</h1>
                </div>
                <div className={styles["input-group"]}>
                    <label>Country:</label>
                    <input
                        type="text"
                        value={countryFilter}
                        onChange={(e) => setCountryFilter(e.target.value)}
                    />
                </div>
                <div className={styles["data-table-container"]}>
                    <div className={styles["table-header"]}>
                        <div className={styles["table-row"]}>
                            <div className={styles["table-cell"]}>IATA Code</div>
                            <div className={styles["table-cell"]}>City</div>
                            <div className={styles["table-cell"]}>Airport Name</div>
                            <div className={styles["table-cell"]}>Country</div>
                        </div>
                    </div>
                    <List
                        height={350}
                        itemCount={filteredData.length}
                        itemSize={35}
                        width={'100%'}
                    >
                        {Row}
                    </List>
                </div>
                <div className={styles["button-group"]}>
                    <button
                        className={styles["button"]}
                        onClick={handleSetDeparture}
                        disabled={!selectedRow || currentSegmentIndex === null}
                    >
                        Set as Departure
                    </button>
                    <button
                        className={styles["button"]}
                        onClick={handleSetDestination}
                        disabled={!selectedRow || currentSegmentIndex === null}
                    >
                        Set as Destination
                    </button>
                </div>
                <div className={`${styles["input-group"]} ${styles["passengers"]}`}>
                    <label>Passengers:</label>
                    <input
                        type="text"
                        value={passengers}
                        onChange={handlePassengersChange}
                    />
                </div>
                <div className={styles["input-group"]}>
                    <label>Measurement Unit:</label>
                    <select value={measurementUnit} onChange={(e) => setMeasurementUnit(e.target.value)}>
                        <option value="mi">Miles</option>
                        <option value="km">Kilometers</option>
                    </select>
                </div>
            </div>
            <div className={styles["right-section"]}>
                <div className={styles["flight-info-table"]}>
                    <h2>Flight Information:</h2>
                    <table>
                        <thead>
                            <tr>
                                <th>Departure Airport</th>
                                <th>Destination Airport</th>
                                <th>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {flightSegments.map((segment, index) => (
                                <tr
                                    key={index}
                                    onClick={() => handleSegmentClick(index)}
                                    style={{
                                        backgroundColor: index === currentSegmentIndex ? '#f1f1f1' : 'white',
                                        cursor: 'pointer',
                                        position: 'relative'
                                    }}
                                >
                                    <td>{segment.departure ? `${segment.departure.IATACode} - ${segment.departure.airportName}, ${segment.departure.city}, ${segment.departure.country}` : 'Not selected'}</td>
                                    <td>{segment.destination ? `${segment.destination.IATACode} - ${segment.destination.airportName}, ${segment.destination.city}, ${segment.destination.country}` : 'Not selected'}</td>
                                    <td className={styles["actions-container"]}>
                                        {index === currentSegmentIndex && (
                                            <>
                                                <button className={styles["delete-button"]} onClick={(e) => { e.stopPropagation(); handleDeleteSegment(index); }}>Delete</button>
                                                <button className={styles["add-segment-button"]} onClick={(e) => { e.stopPropagation(); handleAddNewSegment(); }}>Add</button>
                                            </>
                                        )}
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>
                <div className={styles["button-group"]}>
                    <button
                        className={styles["button"]}
                        onClick={goToFlightCombustionResult}
                        disabled={flightSegments.length === 0 || !flightSegments.every(segment => segment.departure && segment.destination) || !validatePassengers()}
                    >
                        Get result
                    </button>
                </div>
            </div>
        </div>
    );
};

export default FlightEmission;
