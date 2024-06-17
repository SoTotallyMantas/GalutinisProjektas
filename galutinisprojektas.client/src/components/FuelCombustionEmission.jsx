import React from 'react';
import './Emission.css';

const FuelCombustionEmission = () => {
    return (
        <div className="container">
            <div className="left-section">
                <div>
                    <h1>Fuel Combustion Emission</h1>
                </div>
                <div className="big-box"></div>
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