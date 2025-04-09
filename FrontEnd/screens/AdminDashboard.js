import React from 'react';
import styled, { createGlobalStyle } from 'styled-components'; // Correct import
import logo1 from '../src/logo1.png';
import '../src/index.css'

const GlobalStyle = createGlobalStyle`
  * {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
  }
  body {
    font-family: 'Times New Roman', cursive, sans-serif;
    background: linear-gradient(135deg, #3B878C, #00d4ff, #006E75, #50D9E6, #1A5B61); /* More colors in the gradient */
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100vh;
    color: #fff;
    position: relative;
  }
`;

const AdminDashboardContainer = styled.div`
  background-color: #fff;
  padding: 40px;
  border-radius: 8px;
  width: 100%;
  max-width: 800px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
  position: relative;
  z-index: 1;
  color: #333;
`;

const Header = styled.header`
  text-align: center;
  margin-bottom: 30px;
`;

const Title = styled.h1`
  color: #333;
  font-size: 2rem;
  margin-bottom: 10px;
`;

const SubTitle = styled.p`
  font-size: 1.2rem;
  color: #888;
  margin-bottom: 20px;
`;

const AdminDashboard = () => {
  return (
    <>
      <GlobalStyle />
      {/* Logo positioned directly in the body, without a container */}
      <img
        src={logo1}
        alt="Logo"
        style={{
          position: 'fixed',
          top: '15px',
          right: '15px',
          width: '150px', // Adjust the logo size as needed
          zIndex: '2', // Ensure it stays above other elements
        }}
      />

      <AdminDashboardContainer>
        <Header>
          <Title>Admin Dashboard</Title>
          <SubTitle>Welcome to the Admin Dashboard</SubTitle>
        </Header>

        {/* You can add additional sections or buttons for managing patients, staff, etc. */}
        <div>
          <p>Manage your system's data here!</p>
        </div>
      </AdminDashboardContainer>
    </>
  );
};

export default AdminDashboard;
