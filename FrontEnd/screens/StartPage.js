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

const ButtonContainer = styled.div`
  display: flex;
  justify-content: center;
  gap: 10px;
  margin-top: 20px;
  width: 100%;
`;

const Button = styled.button`
  padding: 10px 20px;
  background-color: #125358;
  color: white;
  font-size: 0.9rem;
  border: none;
  border-radius: 30px;
  cursor: pointer;
  transition: all 0.3s ease-in-out;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  width: 48%;

  &:hover {
    background-color: #00d4ff;
    transform: translateY(-2px);
    box-shadow: 0 8px 16px rgba(0, 0, 0, 0.2);
  }

  &:active {
    transform: translateY(1px);
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
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

      console.log('Rigth before the execution of axiosInstance.Post:');
      const response = await axiosInstance.post('/Guest/LoginUser', loginData);
      localStorage.setItem('jwtToken', response.data.item.jwtToken.encryptedToken);
      console.log('jwtToken:', response.data.item.jwtToken.encryptedToken);

      const role = response.data.item.userRole;

      if (role === 'sysadmin') {
        localStorage.setItem('role', 'sysadmin');
        console.log('Sysadmin logged in }');
        setLoginMessage(t('login_success'));
        navigate('/admin');
      } else if (role === 'staff') {
        localStorage.setItem('role', 'staff');
        console.log('Staff logged in');
        setLoginMessage(t('login_success'));
        navigate('/staff');
      } else {
        alert('Access Denied: Invalid role.');
      }

    } catch (error) {
      setLoginError(t('login_failed'));
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
      <Link to="/" style={{ position: 'fixed', top: '15px', right: '15px', zIndex: '2' }}>
        <img src={logo1} alt="Logo" style={{ width: '150px' }} />
      </Link>
      <StartPageContainer>
        <Header>
          <Title>{t('app_title')}</Title>
          <SubTitle>{t('welcome')}</SubTitle>
        </Header>

        {loginMessage && <p style={{ color: 'green', textAlign: 'center' }}>{loginMessage}</p>}

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
            onKeyDown={handleKeyDown}
          />

          {loginError && <p style={{ color: 'red' }}>{loginError}</p>}

          <ButtonContainer>
            <Button onClick={handleLogin} disabled={!username || !password}>
              {t('login')}
            </Button>
          </ButtonContainer>
        </LoginForm>

        <LanguageButtons>
          <LanguageButton onClick={() => changeLanguage('en')}>En</LanguageButton>
          <LanguageButton onClick={() => changeLanguage('sv')}>Sv</LanguageButton>
        </LanguageButtons>

        <Footer>
          <p>
            <a href="/forgotpassword">{t('forgot_password_link')}</a>
          </p>
        </Footer>
      </StartPageContainer>
    </>
  );
};

export default StartPage;
