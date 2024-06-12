import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import HomePage from './pages/HomePage';
import RecipeListPage from './pages/RecipeListPage';
import FullRecipePage from './pages/FullRecipePage.jsx';
import { RootLayout } from './NavigationUtility/RootLayout.jsx';
import RecipeUpload from "./pages/RecipeUpload.jsx";

function App() {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<RootLayout />}>
                    <Route index element={<HomePage />} />
                    <Route path="recipes/:page" element={<RecipeListPage />} />
                    <Route path="recipes/:page/:pageSize" element={<RecipeListPage />} />
                    <Route path="recipes/full/:id" element={<FullRecipePage />} />
                    <Route path="recipes/uploadRecipe" element={<RecipeUpload />} />
                </Route>
            </Routes>
        </Router>
    );
}

export default App;
