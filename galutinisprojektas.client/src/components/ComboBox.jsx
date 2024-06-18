import React, { useState } from 'react';
import "./ComboBox.css";

const ComboBox = ({ label, options, onSelect, disable }) => {
    const [selectedOption, setSelectedOption] = useState('');

    const handleChange = (event) => {
        const value = event.target.value;
        setSelectedOption(value);
        if (onSelect) {
            onSelect(value);
        }
    };

    return (
        <div className="combo-box">
            <label>{label}</label>
            <select id="combo-box" value={selectedOption} onChange={handleChange}>
                <option value="">Select an option</option>
                {options.map((option, index) => (
                    <option key={index} value={option.value}>{option.label}</option>
                ))}
            </select>
        </div>
    );
};

export default ComboBox;