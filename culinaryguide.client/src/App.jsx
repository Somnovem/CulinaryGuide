import React, { useEffect, useState } from 'react';
import './App.css';

function App() {
    const [dishes, setDishes] = useState([]);

    useEffect(() => {
        populateDishData();
    }, []);

    async function populateDishData() {
        try {
            const response = await fetch('https://localhost:7080/home');
            const data = await response.json();
            setDishes(data)
        } catch (error) {
            console.error('Error fetching user data:', error);
        }
    }

    return (
        <div>
            <h1 id="tabelLabel">Dish list</h1>
            <ul id="user-list">
                {
                    dishes.map((item,index) => (
                        <li key={index}>{item.name}</li>
                    ))
                }
            </ul>
        </div>
    );
}

export default App;
