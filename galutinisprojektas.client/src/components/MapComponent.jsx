import React, { useState, useCallback } from 'react';
import { GoogleMap, LoadScript, Marker } from '@react-google-maps/api';

const containerStyle = {
    width: '750px',
    height: '500px',
    margin: '20px'
};

const center = {
    lat: 55.1849352,
    lng: 23.793695
};

const MapComponent = ({ setCoordinates }) => {
    const [marker, setMarker] = useState(null);

    const onMapClick = useCallback((event) => {
        const lat = event.latLng.lat();
        const lng = event.latLng.lng();
        setMarker({ lat, lng });
        setCoordinates({ lat, lng });
    }, [setCoordinates]);

    const options = {
        mapTypeControl: false,
        streetViewControl: false, 
        fullscreenControl: false, 
    };

    return (
        <LoadScript googleMapsApiKey="API-KEY">
            <GoogleMap
                mapContainerStyle={containerStyle}
                center={center}
                zoom={5}
                onClick={onMapClick}
                options={options}
            >
                {marker && <Marker position={marker} />}
            </GoogleMap>
        </LoadScript>
    );
};

export default MapComponent;