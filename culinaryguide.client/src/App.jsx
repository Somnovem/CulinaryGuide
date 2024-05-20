import React, { useEffect, useState } from 'react';
import './App.css';

function App() {
    const [dishes, setDishes] = useState([]);

    useEffect(() => {
        populateDishData();
    }, []);

    async function populateDishData() {
        try {
            const response = await fetch('home');
            const data = await response.json();
            setDishes(data)
            var list = document.getElementById('user-list');
            list.innerHTML = "";
            var li = document.createElement('li');
            li.innerText = data[0].username;
            list.appendChild(li);
        } catch (error) {
            console.error('Error fetching user data:', error);
        }
    }

    return (
        <div>
            <h1 id="tabelLabel">User List</h1>
            <p>This component demonstrates fetching user data from the server.</p>
            <ul id="user-list">
                {dishes.map((dish, index) => (
                    <li key={index}>{dish.name}</li>
                ))}
            </ul>
        </div>
    );
}

export default App;
