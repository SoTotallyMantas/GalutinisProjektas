import React from 'react';
import './Emission.css';

const OpenWeatherMap = () => {
    return (
        <div className="container">
            <div className="left-section">
                <h1>OpenWeatherMap API</h1>
                <div className="input-group">
                    <label>Latitude:</label>
                    <input type="text" />
                </div>
                <div className="input-group">
                    <label>Longitude:</label>
                    <input type="text" />
                </div>
                <div className="button-group">
                    <button>Get result</button>
                </div>
                <div className="very-big-box">
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