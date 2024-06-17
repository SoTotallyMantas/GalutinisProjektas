import React, { useState } from 'react';
import { FixedSizeList as List } from 'react-window';
import MapComponent from './MapComponent';
import './Emission.css';
import './List.css';

const OpenWeatherMap = () => {
    const [coordinates, setCoordinates] = useState({ lat: 0, lng: 0 });
    const [pollutionData, setPollutionData] = useState([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);

    const fetchPollutionData = async () => {
        setLoading(true);
        setError(null);
        try {
            const response = await fetch(`/OpenWeatherMap?latitude=${coordinates.lat}&longitude=${coordinates.lng}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
            });
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            const result = await response.json();
            console.log('API response:', result);

            if (result && result.list && Array.isArray(result.list)) {
                const transformedData = result.list.map(item => ({
                    dt: item.dt,
                    aqi: item.main.aqi,
                    co: item.components.co,
                    no: item.components.no,
                    no2: item.components.no2,
                    o3: item.components.o3,
                    so2: item.components.so2,
                    pm2_5: item.components.pm2_5,
                    pm10: item.components.pm10,
                    nh3: item.components.nh3,
                }));
                setPollutionData(transformedData);
            } else {
                throw new Error('Unexpected data format');
            }
        } catch (error) {
            setError(error.toString());
        } finally {
            setLoading(false);
        }
    };

    const Row = ({ index, style }) => (
        <div className="list-item">
            <div>Date and Time: {new Date(pollutionData[index].dt * 1000).toLocaleString()}</div>
            <div>Air Quality Index (AQI): {pollutionData[index].aqi}</div>
            <div>Carbon Monoxide (CO): {pollutionData[index].co}</div>
            <div>Nitrogen Monoxide (NO): {pollutionData[index].no}</div>
            <div>Nitrogen Dioxide (NO2): {pollutionData[index].no2}</div>
            <div>Ozone (O3): {pollutionData[index].o3}</div>
            <div>Sulphur Dioxide (SO2): {pollutionData[index].so2}</div>
            <div>Fine Particle Matter (PM2.5): {pollutionData[index].pm2_5}</div>
            <div>Coarse Particle Matter (PM10): {pollutionData[index].pm10}</div>
            <div>Ammonia (NH3): {pollutionData[index].nh3}</div>
        </div>
    );

    return (
        <div className="container">
            <div className="left-section">
                <h1>OpenWeatherMap API</h1>
                <MapComponent setCoordinates={setCoordinates} />
                <div className="input-group">
                    <label>Latitude: </label>
                    <input
                        type="number"
                        name="lat"
                        value={coordinates.lat}
                        onChange={(e) => setCoordinates({ ...coordinates, lat: parseFloat(e.target.value) })}
                    />
                </div>
                <div className="input-group">
                    <label>Longitude: </label>
                    <input
                        type="number"
                        name="lng"
                        value={coordinates.lng}
                        onChange={(e) => setCoordinates({ ...coordinates, lng: parseFloat(e.target.value) })}
                    />
                </div>
                <div className="button-group">
                    <button onClick={fetchPollutionData}>Get result</button>
                </div>
            </div>
            <div className="right-section">
                <p>Pollution Results</p>
                {loading && <p>Loading...</p>}
                {error && <p>{error}</p>}
                <List
                    height={600}
                    itemCount={pollutionData.length}
                    itemSize={100}
                    width={800}
                >
                    {Row}
                </List>
            </div>
        </div>
    );
};

export default OpenWeatherMap;