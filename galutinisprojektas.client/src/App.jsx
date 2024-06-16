import { useEffect, useState } from 'react';
import './App.css';

function App() {
    const [countryCodes, setCountryCodes] = useState([]);
    const [selectedRow, setSelectedRow] = useState(null);

    useEffect(() => {
        fetchCountryCodes();
    }, []);

    const handleRowClick = (index) => {
        setSelectedRow(index);
    };

    const contents = countryCodes.length === 0
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started.</em></p>
        : <div className="table-container">
            <table className="table table-striped" aria-labelledby="tableLabel">
                <thead>
                    <tr>
                        <th>Country Code</th>
                        <th>Country Name</th>
                    </tr>
                </thead>
                <tbody>
                    {countryCodes.map((country, index) =>
                        <tr key={country.id} onClick={() => handleRowClick(index)} style={{ backgroundColor: selectedRow === index ? '#f1f1f1' : 'white' }}>
                            <td>{country.countryCode}</td>
                            <td>{country.countryName}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        </div>;

    return (
        <div className="container">
            <h1 id="tableLabel">Country Codes</h1>
            <p>This component demonstrates fetching data from the server and selecting a row in a table.</p>
            {contents}
        </div>
    );

    async function fetchCountryCodes() {
        try {
            const response = await fetch('/CountryCodes');
            const data = await response.json();
            setCountryCodes(data);
        } catch (error) {
            console.error('Failed to fetch country codes:', error);
        }
    }
}

export default App;
