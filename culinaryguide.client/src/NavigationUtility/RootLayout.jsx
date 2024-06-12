import React from 'react';
import { MainNavigation } from './MainNavigation.jsx';
import { Outlet } from 'react-router-dom';

export const RootLayout = () => {
    return (
        <div style={{ margin: '10px', padding: '10px' }}>
            <MainNavigation />
            <Outlet />
        </div>
    );
};
