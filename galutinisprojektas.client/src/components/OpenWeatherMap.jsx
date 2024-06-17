import React from 'react';
import './Emission.css';

const OpenWeatherMap = () => {
    return (
        <div className="container">
            <div className="left-section">
                <div className="title">OpenWeatherMap API</div>
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
                <div className="result-title">Pollution Result</div>
                <div className="big-box"></div>
            </div>
        </div>
    );
};

export default OpenWeatherMap;