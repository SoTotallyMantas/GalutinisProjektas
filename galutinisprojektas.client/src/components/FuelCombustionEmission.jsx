import React from 'react';
import './Emission.css';

const FuelCombustionEmission = () => {
    return (
        <div className="container">
            <div className="left-section">
                <div className="title">Fuel Combustion Emission</div>
                <div className="big-box"></div>
                <div className="input-group">
                    <label>Value:</label>
                    <input type="text" />
                </div>
                <button className="result-button">Get result</button>
            </div>
            <div className="right-section">
                <div className="result-title">Emission Result</div>
                <div className="big-box"></div>
            </div>
        </div>
    );
};

export default FuelCombustionEmission;