import React from 'react';
import styled, { createGlobalStyle } from 'styled-components'; // Correct import
import logo1 from '../src/logo1.png';

// Styled Components
const GlobalStyle = createGlobalStyle`
  * {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
  }
  body {
    font-family: 'Comic Sans MS', cursive, sans-serif;
    background: linear-gradient(135deg, #3B878C, #00d4ff, #006E75, #50D9E6, #1A5B61); /* More colors in the gradient */
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100vh;
    color: #fff;
    position: relative;
  }
`;

const StartPageContainer = styled.div`
  background-color: #fff;
  padding: 40px;
  border-radius: 8px;
  width: 100%;
  max-width: 400px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
  position: relative;
  z-index: 1;
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

const LoginForm = styled.div`
  display: flex;
  flex-direction: column;
`;

const Label = styled.label`
  font-size: 1.1rem;
  margin-bottom: 8px;
`;

const Input = styled.input`
  padding: 12px;
  font-size: 1rem;
  margin-bottom: 15px;
  border: 1px solid #ddd;
  border-radius: 4px;

  &:focus {
    outline: none;
    border-color: #3B878C;
  }
`;

const Button = styled.button`
  padding: 12px;
   background-color: #125358;
  color: white;
  font-size: 1rem;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  transition: background-color 0.3s ease;

  &:hover {
    background-color: #00d4ff;
  }
`;

const Footer = styled.footer`
  text-align: center;
  margin-top: 20px;
  font-size: 1rem;

  p {
    font-size: 0.9rem; /* Smaller font size for the 'Forgot password?' text */
  }

  a {
    color: #081630;
    text-decoration: none;

    &:hover {
      text-decoration: underline;
    }
  }
`;


const LanguageButtons = styled.div`
  display: flex;
  justify-content: center;
  margin-top: 20px;
`;

const LanguageButton = styled.button`
  padding: 10px;
  font-size: 1rem;
  background-color: #125358;
  color: white;
  border: none;
  border-radius: 50%; /* Make buttons round */
  cursor: pointer;
  transition: background-color 0.3s ease;
  margin: 0 10px;
  width: 30px; /* Smaller button size */
  height: 30px; /* Equal height to width for round shape */
  display: flex;
  justify-content: center;
  align-items: center;
  font-size: 0.8rem; /* Smaller font size for the text inside the button */ 

  &:hover {
    background-color: #00d4ff;
  }
`;

const StartPage = () => {
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
      <StartPageContainer>
        <Header>
          <Title>HealthGraph</Title>
          <SubTitle>Welcome</SubTitle>
        </Header>

        <LoginForm>
          <Label>Username</Label>
          <Input type="text" placeholder="Enter username" />
          <Label>Password</Label>
          <Input type="password" placeholder="Enter password" />
          <Button>Login</Button>
        </LoginForm>

        <LanguageButtons>
          <LanguageButton>En</LanguageButton>
          <LanguageButton>Sv</LanguageButton>
        </LanguageButtons>

        <Footer>
          <p>
            Forgot your password? <a href="/forgotpassword">Forgot password</a>
          </p>
        </Footer>
      </StartPageContainer>
    </>
  );
};

export default StartPage;
