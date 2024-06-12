import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';

function FullRecipePage() {
    const { id= "75730875-5cda-4454-72da-08dc86e0948a" } = useParams();
    
    const [recipe, setRecipe] = useState({});
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        fillPage();
    }, []);

    async function fillPage() {
        try {
            const response = await fetch(`http://localhost:5000/recipes/getFull/${id}`);
            const data = await response.json();
            setRecipe(data);
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
            <h1>{recipe.name}</h1>
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
        </div>
    );
}

export default FullRecipePage;
