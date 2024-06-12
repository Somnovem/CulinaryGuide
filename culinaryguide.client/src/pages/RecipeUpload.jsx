import React, { useState } from 'react';

function RecipeUpload() {
    const [formData, setFormData] = useState({
        name: '',
        calories: '',
        ingredients: '',
        description: '',
        instructions: '',
        typeId: '',
        cuisineId: '',
        mealTime: '',
        username: ''
    });
    const [file, setFile] = useState(null);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData({
            ...formData,
            [name]: value
        });
    };

    const handleFileChange = (e) => {
        setFile(e.target.files[0]);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        const data = new FormData();
        for (let key in formData) {
            data.append(key, formData[key]);
        }
        data.append('file', file);

        try {
            const response = await fetch('http://localhost:5000/upload', {
                method: 'POST',
                body: data
            });
            const result = await response.json();
            console.log(result);
        } catch (error) {
            console.error('Error:', error);
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <input type="text" name="name" value={formData.name} onChange={handleChange} placeholder="Name" />
            <input type="number" name="calories" value={formData.calories} onChange={handleChange} placeholder="Calories" />
            <input type="text" name="ingredients" value={formData.ingredients} onChange={handleChange} placeholder="Ingredients" />
            <input type="text" name="description" value={formData.description} onChange={handleChange} placeholder="Description" />
            <input type="text" name="instructions" value={formData.instructions} onChange={handleChange} placeholder="Instructions" />
            <input type="text" name="typeId" value={formData.typeId} onChange={handleChange} placeholder="TypeId" />
            <input type="text" name="cuisineId" value={formData.cuisineId} onChange={handleChange} placeholder="CuisineId" />
            <input type="text" name="mealTime" value={formData.mealTime} onChange={handleChange} placeholder="MealTime" />
            <input type="text" name="username" value={formData.username} onChange={handleChange} placeholder="Username" />
            <input type="file" name="file" onChange={handleFileChange} />
            <button type="submit">Upload</button>
        </form>
    );
}

export default RecipeUpload;