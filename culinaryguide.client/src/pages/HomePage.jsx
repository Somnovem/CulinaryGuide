import React, { useEffect, useState } from 'react';
import './homepage.css';

function HomePage() {
    const [recipes, setRecipes] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        populateDishData();
    }, []);

    async function populateDishData() {
        try {
            const response = await fetch('http://localhost:5000/getrecipes');
            const data = await response.json();
            setRecipes(data);
        } catch (error) {
            setError('Error fetching recipe data');
            console.error('Error fetching recipe data:', error);
        } finally {
            setLoading(false);
        }
    }

    if (loading) {
        return <div>Loading...</div>;
    }

    if (error) {
        return <div>{error}</div>;
    }

    return (
        <div className="recipe-container">
            <h1>Recipes of the day</h1>
            {recipes.map(recipe => (
                <div key={recipe.id} className="recipe-card">
                    <div className="recipe-thumbnail">
                        <img src={recipe.thumbnail} alt={recipe.name}/>
                    </div>
                    <div className="recipe-details">
                        <h2>{recipe.name}</h2>
                        <hr/>
                        <h5>{recipe.description}</h5>
                    </div>
                </div>
            ))}
        </div>
    );
}

export default HomePage;
