import React from 'react';
import ApiCard from './ApiCard';
import './CarbonInterfacePage.css';
import Plant from '/Plant.svg';
import Car from '/Cars.svg';
import Plane from '/Plane.svg';

const CarbonInterfacePage = () => {
    return (
        <div className="carbon-interface-page">
            <main>
                <h1 className="title">CarbonInterface API</h1>
                <div className="emission-options">
                    <ApiCard
                        image={Plane}
                        title="Flight emission"
                        description="Calculate carbon emissions for flights."
                        buttonText="Flight Emission"
                        buttonLink="/flightEmission"
                    />
                    <ApiCard
                        image={Car}
                        title="Fuel combustion emission"
                        description="Calculate carbon emissions from fuel."
                        buttonText="Fuel Combustion Emission"
                        buttonLink="/fuelCombustionEmission"
                    />
                    <ApiCard
                        image={Plant}
                        title="Electricity emission"
                        description="Calculate carbon emissions from electricity."
                        buttonText="Electricity Emission"
                        buttonLink="/electricityEmission"
                    />

                </div>
            </main>
        </div>
    );
}

export default CarbonInterfacePage;