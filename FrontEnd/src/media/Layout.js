import React from 'react';
import Navigation from '../Navigation';
import { Outlet } from 'react-router-dom';

const Layout = () => {
  return (
    <>
      <Navigation />
      <main style={{ padding: '20px' }}>
        <Outlet />
      </main>
    </>
  );
};

export default Layout;
