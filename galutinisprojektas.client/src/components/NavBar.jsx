import React from 'react';
import { useNavigate } from 'react-router-dom';
import './NavBar.css';

const NavBar = () => {
    const navigate = useNavigate();

    const goToCarbonInterface = () => {
        navigate('/carbonInterface');
    };

    const goToOpenWeatherMap = () => {
        navigate('/openWeatherMap');
    };
    const goToHome = () => {
        navigate('/');
    };
    return (
        <nav className="navbar">
            <button className="nav-button" onClick={goToHome}>Home</button>
            <button className="nav-button" onClick={goToCarbonInterface}>CarbonInterface API</button>
            <button className="nav-button" onClick={goToOpenWeatherMap}>OpenWeatherMap API</button>
        </nav>
    );
};

export default NavBar;