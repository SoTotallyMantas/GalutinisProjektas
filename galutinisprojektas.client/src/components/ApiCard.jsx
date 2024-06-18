import React from 'react';
import { useNavigate } from 'react-router-dom';
import './ApiCard.css';

const ApiCard = ({ image, title, description, buttonText, buttonLink }) => {
    const navigate = useNavigate();
    return (
        <div className="api-card">
            <img src={image} alt={`${title} logo`} className="api-image" />
            <h2>{title}</h2>
            <p>{description}</p>
            <button onClick={() => navigate(buttonLink)}>{buttonText}</button>
        </div>
    );
};

export default ApiCard;