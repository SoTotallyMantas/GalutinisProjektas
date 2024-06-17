import React from 'react';
import './Emission.css';
import ComboBox from './ComboBox';

const ElectricityEmission = () => {
    const options = [
        { label: 'megawatt hours (mwh)', value: 'mwh' },
        { label: 'kilowatt hours (kmh)', value: 'kmh' },
    ];
    return (
        <div className="container">
            <div className="left-section">
                <div>
                    <h1>Electricity Emission</h1>
                </div>
                <ComboBox
                    label="Unit:"
                    options={options}
                />
                <div className="input-group">
                    <label>Value:</label>
                    <input type="text" />
                </div>
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
                <p>Emission Result</p>
                <div className="big-box"></div>
            </div>
        </div>
    );
};

export default ElectricityEmission;