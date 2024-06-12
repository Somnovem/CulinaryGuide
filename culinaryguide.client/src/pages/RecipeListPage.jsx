import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import './homepage.css'
function RecipeListPage() {
    const { page= 1 } = useParams();
    const { pageSize = 12} = useParams();

    const [recipes, setRecipes] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        fillPage();
    }, []);
    
    async function fillPage() {
        try {
            const response = await fetch(`http://localhost:5000/recipes/getPage`);
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
            <h2>Recipe page {page}</h2>
            <div>
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
        </div>
    );
}

export default RecipeListPage;
