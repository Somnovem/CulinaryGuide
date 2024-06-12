import React from 'react';
import { NavLink } from 'react-router-dom';

export const MainNavigation = () => {
    return (
        <header>
            <nav>
                <ul>
                    <li>
                        <NavLink to="/">
                            Home Page
                        </NavLink>
                    </li>
                    <li>
                        <NavLink to="/recipes/getPage">
                            Recipes
                        </NavLink>
                    </li>
                </ul>
            </nav>
        </header>
    );
};
