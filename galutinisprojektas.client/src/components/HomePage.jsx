import React from 'react';
import Header from './Header';
import ApiCard from './ApiCard';
import CarbonIMG from '/CarbonIMG.svg'
import WeatherIMG from '/WeatherIMG.svg'
import './HomePage.css';

const HomePage = () => {
    return (
        <div className="homepage">
            <Header />
            <div className="api-container">
                <ApiCard
                    image={CarbonIMG}
                    title="CarbonInterface API"
                    description="API provides accurate and comprehensive estimates of carbon emissions for various activities."
                    buttonText="CarbonInterface"
                    buttonLink="/carbonInterface"
                />
                <ApiCard
                    image={WeatherIMG}
                    title="OpenWeatherMap API"
                    description="API offers detailed air quality data, including pollutant levels and air quality indices."
                    buttonText="OpenWeatherMap"
                    buttonLink="/openWeatherMap"
                />
            </div>
        </div>
    );
};

export default HomePage;