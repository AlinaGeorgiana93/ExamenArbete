import React, { useState } from 'react';
import { useTranslation } from 'react-i18next';
import styled, { createGlobalStyle } from 'styled-components';
import logo1 from '../src/media/logo1.png';
import { useDispatch } from 'react-redux';
import { setLanguage } from '../language/languageSlice.js';
import { getI18n } from 'react-i18next';
import '../language/i18n.js';
import { useNavigate, Link } from 'react-router-dom';
import axiosInstance from '../src/axiosInstance.js';

const GlobalStyle = createGlobalStyle`
  * {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
    font-size: 1.2rem;
  }

  body {
    font-family: 'Helvetica Neue', Arial, sans-serif;
    background: linear-gradient(135deg, #e0f7f9, #cceae7, #b2dfdb);
    min-height: 100vh;
    overflow-y: auto;
    transition: background 0.6s ease-in-out;
  }
`;

const LoginContainer = styled.div`
  position: relative;
  background-color: #fff;
  padding: 80px 60px;
  border-radius: 16px;
  width: 600px;
  max-width: 90%;
  box-shadow: 0 8px 24px rgba(0, 0, 0, 0.2);
  margin: 80px auto;
  text-align: center;
  z-index: 2;
`;

const Title = styled.h1`
  color: #1a5b61;
  font-size: 2.5rem;
  margin-bottom: 10px;
`;

const SubTitle = styled.p`
  font-size: 1.2rem;
  color: #444;
  margin-bottom: 30px;
`;

const FormField = styled.div`
  margin-bottom: 20px;
  text-align: left;
`;

const Label = styled.label`
  display: block;
  margin-bottom: 8px;
  font-weight: bold;
  color: #1a5b61;
`;

const Input = styled.input`
  width: 100%;
  padding: 12px;
  font-size: 1rem;
  border: 1px solid #ddd;
  border-radius: 8px;

  &:focus {
    outline: none;
    border-color: #00d4ff;
  }
`;

const Button = styled.button`
  padding: 12px 20px;
  background-color: #125358;
  color: white;
  font-size: 1rem;
  border: none;
  border-radius: 30px;
  cursor: pointer;
  width: 100%;
  margin-top: 20px;

  &:hover {
    background-color: #00d4ff;
  }
`;

const LanguageButtons = styled.div`
  display: flex;
  justify-content: center;
  margin-top: 20px;
`;

const LanguageButton = styled.button`
  background-color: #125358;
  color: white;
  border: none;
  border-radius: 50%;
  width: 35px;
  height: 35px;
  margin: 0 10px;
  cursor: pointer;

  &:hover {
    background-color: #00d4ff;
  }
`;

const Footer = styled.footer`
  margin-top: 30px;
  font-size: 0.9rem;

  a {
    color: #1a5b61;
    text-decoration: none;

    &:hover {
      text-decoration: underline;
    }
  }
`;

const StartPage = () => {
  const dispatch = useDispatch();
  const { t } = useTranslation();
  const navigate = useNavigate();
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [loginError, setLoginError] = useState('');
  const [loginMessage, setLoginMessage] = useState('');

  const handleLogin = async () => {
    try {
      const loginData = {
        userNameOrEmail: username,
        password: password,
      };

      let response = await axiosInstance.post('/Guest/LoginUser', loginData);
      localStorage.setItem(
        'jwtToken',
        response.data.item.jwtToken.encryptedToken
      );
      localStorage.setItem('userName', response.data.item.userName);

      const role = response.data.item.userRole;

      if (role === 'sysadmin') {
        localStorage.setItem('role', 'sysadmin');
        localStorage.setItem('userName', response.data.item.userName);
        setLoginMessage(t('login_success'));
        navigate('/admin');
      } else if (role === 'usr') {
        localStorage.setItem('role', 'usr');
        localStorage.setItem('userName', response.data.item.userName);
        setLoginMessage(t('login_success'));
        navigate('/staff');
      } else {
        alert('Access Denied: Invalid role.');
      }
    } catch (error) {
      const loginDataStaff = {
        userNameOrEmail: username,
        password: password,
      };

      try {
        const response = await axiosInstance.post(
          '/Guest/LoginStaff',
          loginDataStaff
        );
        localStorage.setItem(
          'jwtToken',
          response.data.item.jwtToken.encryptedToken
        );

        const role = response.data.item.userRole;

        if (role === 'usr') {
          localStorage.setItem('role', 'usr');
          localStorage.setItem('userName', response.data.item.userName); 
          setLoginMessage(t('login_success'));
          navigate('/staff');
        } else {
          alert('Access Denied: Invalid role.');
        }
      } catch (staffError) {
        setLoginError(t('login_failed'));
      }
    }
  };

  const handleKeyDown = (e) => {
    if (e.key === 'Enter') handleLogin();
  };

  const changeLanguage = (lang) => {
    dispatch(setLanguage(lang));
    const i18n = getI18n();
    i18n.changeLanguage(lang);
  };

  return (
    <>
      <GlobalStyle />

      <Link to="/" style={{ position: 'fixed', top: '20px', right: '20px', zIndex: 2 }}>
        <img src={logo1} alt="Logo" style={{ width: '150px' }} />
      </Link>

      <LoginContainer>
        <Title>{t('app_title')}</Title>
        <SubTitle>{t('welcome')}</SubTitle>

        {loginMessage && <p style={{ color: 'green' }}>{loginMessage}</p>}
        {loginError && <p style={{ color: 'red' }}>{loginError}</p>}

        <FormField>
          <Label>{t('username')}</Label>
          <Input
            type="text"
            placeholder={t('enter_username')}
            value={username}
            onChange={(e) => setUsername(e.target.value)}
          />
        </FormField>

        <FormField>
          <Label>{t('password')}</Label>
          <Input
            type="password"
            placeholder={t('enter_password')}
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            onKeyDown={handleKeyDown}
          />
        </FormField>

        <Button onClick={handleLogin} disabled={!username || !password}>
          {t('login')}
        </Button>

        <LanguageButtons>
          <LanguageButton onClick={() => changeLanguage('en')}>En</LanguageButton>
          <LanguageButton onClick={() => changeLanguage('sv')}>Sv</LanguageButton>
        </LanguageButtons>

        <Footer>
          <p><a href="/forgotpassword">{t('forgot_password_link')}</a></p>
        </Footer>
      </LoginContainer>
    </>
  );
};

export default StartPage;
