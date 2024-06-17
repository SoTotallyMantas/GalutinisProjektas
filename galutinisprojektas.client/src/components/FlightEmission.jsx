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
                <div>
                    <h1>Flight Emission</h1>
                </div>
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
                <p>Flight information:</p>
                <div className="big-box"></div>
                <button className="result-button" onClick={goToFlightCombustionResult}>Get result</button>
            </div>
        </div>
    );
}
export default FlightEmission;