import React from 'react';
import { Link } from 'react-router-dom';
import { Outlet } from 'react-router-dom';
import Navigation from '../Navigation';
import logo1 from './logo1.png'; // Make sure the path is correct

const Layout = () => {
  return (
    <>
      {/* Logo in top-left */}
      <Link to="/" style={{ position: 'fixed', top: '-10px', left: '15px', zIndex: '1001' }}>
        <img src={logo1} alt="Logo" style={{ width: '200px', objectFit: 'contain' }} />
      </Link>

      {/* Bottom Navigation */}
      <Navigation />

      {/* Main content area */}
      <main style={{ padding: '20px' }}>
        <Outlet />
      </main>
    </>
  );
};

export default Layout;
