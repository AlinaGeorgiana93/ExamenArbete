import React from 'react';
import { Link, Outlet } from 'react-router-dom';
import styled from 'styled-components';
import Navigation from '../Navigation';
import logo1 from './logo1.png'; // Make sure the path is correct

const LogoContainer = styled.div`
  position: absolute;
  top: 15px;
  left: 15px;
  z-index: 2;
  margin-left: -50px;
  margin-top: -110px;
`;

const Layout = () => {
  return (
    <>
      <LogoContainer>
        <Link to="/">
          <img src={logo1} alt="Logo" style={{ width: '200px', objectFit: 'contain' }} />
        </Link>
      </LogoContainer>

      <Navigation />

      <main style={{ padding: '20px' }}>
        <Outlet />
      </main>
    </>
  );
};

export default Layout;
