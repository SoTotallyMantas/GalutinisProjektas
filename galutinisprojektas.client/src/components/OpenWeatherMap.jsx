import React, { useState } from 'react';
import MapComponent from './MapComponent';
import './Emission.css';

const OpenWeatherMap = () => {
    const [coordinates, setCoordinates] = useState({ lat: '', lng: '' });

    return (
        <div className="container">
            <div className="left-section">
                <h1>OpenWeatherMap API</h1>
                <MapComponent setCoordinates={setCoordinates} />
                <div className="input-group">
                    <label>Latitude: </label>
                    <input type="text" value={coordinates.lat} readOnly />
                </div>
                <div className="input-group">
                    <label>Longitude: </label>
                    <input type="text" value={coordinates.lng} readOnly />
                </div>
                <div className="button-group">
                    <button>Get result</button>
                </div>
            </div>
            <div className="right-section">
                <p>Pollution Result</p>
                <div className="big-box"></div>
            </div>
        </div>
    );
};

export default OpenWeatherMap;