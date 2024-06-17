import React from 'react';
import './Emission.css';

const ElectricityEmission = () => {
    return (
        <div className="container">
            <div className="left-section">
                <div className="title">Electricity Emission</div>
                <div className="big-box"></div>
                <div className="input-group">
                    <label>Country:</label>
                    <input type="text" />
                </div>
                <div className="big-box"></div>
                <div className="button-group">
                    <button>Select</button>
                    <button>Get result</button>
                </div>
            </div>
            <div className="right-section">
                <div className="result-title">Emission Result</div>
                <div className="big-box"></div>
            </div>
        </div>
    );
};

export default ElectricityEmission;