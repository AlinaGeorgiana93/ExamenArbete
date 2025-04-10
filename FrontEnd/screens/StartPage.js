import React, { useState } from 'react';
import { useTranslation } from 'react-i18next';
import styled, { createGlobalStyle } from 'styled-components'; 
import logo1 from '../src/media/logo1.png';
import { useDispatch } from 'react-redux';
import { setLanguage } from '../language/languageSlice'; 
import { getI18n } from 'react-i18next';  // <-- Use getI18n instead of i18n
import '../language/i18n.js';
import { useNavigate, Link } from 'react-router-dom';  // Import Link for navigation
import axios from 'axios';  // Import axios for API calls


const GlobalStyle = createGlobalStyle`
  * {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
  }
  body {
    font-family: 'Times New Roman', cursive, sans-serif;
    background: linear-gradient(135deg, #3B878C, #00d4ff, #006E75, #50D9E6, #1A5B61); 
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
    font-size: 0.9rem; 
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
  margin-top: 5px;
`;

const LanguageButton = styled.button`
  padding: 10px;
  font-size: 1rem;
  background-color: #125358;
  color: white;
  border: none;
  border-radius: 50%;
  cursor: pointer;
  transition: background-color 0.3s ease;
  margin: 0 10px;
  width: 30px;
  height: 30px;
  display: flex;
  justify-content: center;
  align-items: center;
  font-size: 0.8rem;

  &:hover {
    background-color: #00d4ff;
  }
`;

const NavigateButton = styled.button`
  padding: 12px;
  background-color: #125358;
  color: white;
  font-size: 1rem;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  transition: background-color 0.3s ease;
  margin-top: 20px;
  width: 100%;

  &:hover {
    background-color: #00d4ff;
  }
`;

const StartPage = () => {
  const dispatch = useDispatch();
  const { t } = useTranslation();
  const navigate = useNavigate(); 
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [loginError, setLoginError] = useState('');
  
  const handleLogin = async () => {
    const loginData = {
      userNameOrEmail: username,  // Use state values
      password: password,
    };
  
    console.log('Sending login data:', loginData);  // Log to check the sent data
  
    try {
      const response = await axios.post(
        'https://localhost:7066/api/Guest/LoginUser',
        loginData,
        {
          headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
          },
        }
      );
  
      // Handle success response
      console.log('Login successful:', response.data);  // Full response
  
      // Save token to localStorage
      localStorage.setItem('jwtToken', response.data.item.jwtToken.encryptedToken);
  
      // Check the role and navigate accordingly
      const role = response.data.item.userRole;  // Change 'role' to 'userRole'
      console.log('Role from response:', role);  // Log the role for debugging
  
      if (role === 'sysadmin') {
        localStorage.setItem('role', 'sysadmin');
        navigate('/admin');  // Navigate to the admin page
      } else if (role === 'staff') {
        localStorage.setItem('role', 'staff');
        navigate('/staff');  // Navigate to the staff page
      } else {
        alert('Access Denied: Invalid role.');
      }
    } catch (error) {
      console.error('Login failed:', error.response?.data || error.message);
      alert('Login failed: Check your credentials or backend connection.');
    }
  };
  
  // Define the changeLanguage function here
  const changeLanguage = (lang) => {
    dispatch(setLanguage(lang));  // Dispatch Redux action
    const i18n = getI18n();  // Get i18n instance
    i18n.changeLanguage(lang);    // Change language using i18n
  };

  const goToAdminDashboard = () => {
    navigate('/admin');  // ✅ Navigate to AdminDashboard route
  };

  const goToPatientPage = () => {
    navigate('/patient');  // ✅ Navigate to PatientPage route
  };
  const goToStaffPage = () => {
    navigate('/staff');  // ✅ Navigate to StaffPage route
  }

  return (
    <>
      <GlobalStyle />
      {/* Logo clickable to navigate to the start page */}
      <Link to="/" style={{ position: 'fixed', top: '15px', right: '15px', zIndex: '2' }}>
        <img
          src={logo1}
          alt="Logo"
          style={{
            width: '150px', // Adjust logo size as needed
          }}
        />
      </Link>
      <StartPageContainer>
        <Header>
          <Title>{t('app_title')}</Title>
          <SubTitle>{t('welcome')}</SubTitle>
        </Header>

        <LoginForm>
  <Label>{t('username')}</Label>
  <Input
    type="text"
    placeholder={t('enter_username')}
    value={username}
    onChange={(e) => setUsername(e.target.value)}
  />

  <Label>{t('password')}</Label>
  <Input
    type="password"
    placeholder={t('enter_password')}
    value={password}
    onChange={(e) => setPassword(e.target.value)}
  />

  {loginError && <p style={{ color: 'red' }}>{loginError}</p>}

  <Button onClick={handleLogin}>{t('login')}</Button>
</LoginForm>


        <LanguageButtons>
          <LanguageButton onClick={() => changeLanguage('en')}>En</LanguageButton>
          <LanguageButton onClick={() => changeLanguage('sv')}>Sv</LanguageButton>
        </LanguageButtons>

        {/* Navigation buttons at the bottom of the page */}
        <NavigateButton onClick={goToAdminDashboard}>
          {t('Go to Admin Dashboard')}
        </NavigateButton>
        <NavigateButton onClick={goToPatientPage}>
          {t('Go to Patient Page')}
        </NavigateButton>
        <NavigateButton onClick={goToStaffPage}>
          {t('Go to Staff Page')}
        </NavigateButton>

        <Footer>
          <p>
            {t('forgot_password')} <a href="/forgotpassword">{t('forgot_password_link')}</a>
          </p>
        </Footer>
      </StartPageContainer>
    </>
  );
};

export default StartPage;
