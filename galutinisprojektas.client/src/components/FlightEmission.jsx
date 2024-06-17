import React from 'react';
import { useNavigate } from 'react-router-dom';
import './Emission.css';

const FlightEmission = () => {
    const navigate = useNavigate();

    const goToFlightCombustionResult = () => {
        navigate('/flightCombustionResult');
    };

    return (
        <div className="container">
            <div className="left-section">
                <div className="title">Flight Emission</div>
                <div className="input-group">
                    <label>Country:</label>
                    <input type="text" />
                </div>
                <div className="big-box"></div>
                <div className="button-group">
                    <button>Departure</button>
                    <button>Destination</button>
                </div>
                <div className="input-group">
                    <label>Passengers:</label>
                    <input type="text" />
                </div>
                <div className="big-box"></div>
            </div>
            <div className="right-section">
                <div className="flight-info-title">Flight information:</div>
                <div className="big-box"></div>
                <button className="result-button" onClick={goToFlightCombustionResult}>Get result</button>
            </div>
        </div>
    );
}
export default FlightEmission;