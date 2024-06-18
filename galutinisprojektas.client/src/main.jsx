import React from 'react';
import ReactDOM from 'react-dom/client';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import './index.css';
import NavBar from './components/NavBar';
import HomePage from './components/HomePage';
import CarbonInterfacePage from './components/CarbonInterfacePage';
import FlightEmission from './components/FlightEmission';
import ElectricityEmission from './components/ElectricityEmission';
import FuelCombustionEmission from './components/FuelCombustionEmission';
import FlightCombustionResult from './components/FlightCombustionResult';
import OpenWeatherMap from './components/OpenWeatherMap';

ReactDOM.createRoot(document.getElementById('root')).render(
    <React.StrictMode>
        <Router>
            <NavBar />
            <Routes>
                <Route path="/" element={<HomePage />} />
                <Route path="/carbonInterface" element={<CarbonInterfacePage />} />
                <Route path="/flightEmission" element={<FlightEmission />} />
                <Route path="/electricityEmission" element={<ElectricityEmission />} />
                <Route path="/fuelCombustionEmission" element={<FuelCombustionEmission />} />
                <Route path="/flightCombustionResult" element={<FlightCombustionResult />} />
                <Route path="/openWeatherMap" element={<OpenWeatherMap />} />
            </Routes>
        </Router>
    </React.StrictMode>,
);