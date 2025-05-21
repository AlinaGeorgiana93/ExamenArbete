import React, { useState } from 'react';
import { useTranslation } from 'react-i18next';
import styled, { createGlobalStyle } from 'styled-components';
import { useDispatch } from 'react-redux';
import { setLanguage } from '../language/languageSlice.js';
import { getI18n } from 'react-i18next';
import '../language/i18n.js';
import { useNavigate, Link } from 'react-router-dom';
import axiosInstance from '../src/axiosInstance.js';
import logo1 from '../src/media/logo1.png';

const GlobalStyle = createGlobalStyle`
  * {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
  }
`;
const LogoContainer = styled.div`
  position: absolute;
  top: 15px;
  left: 15px;
  z-index: 2;
  margin-left: -50px;
  margin-top: -110px;
`;

const PageWrapper = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 100vh; /* Full height of the viewport */
  position: relative;
  z-index: 0;
`;

const StartPageContainer = styled.div`
  background-color:#F1F0E8;
  padding: 40px;
  border-radius: 12px; /* Rounded corners for soft look */
  width: 100%;
  max-width: 400px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
  position: relative;
  z-index: 1;
`;

const Header = styled.header`
  text-align: center;
  margin-bottom: 30px;
`;

const Title = styled.h1`
  color: #3a3a3a;
  font-size: 2rem;
  margin-bottom: 10px;
  font-weight: 600;
`;

const SubTitle = styled.p`
  font-size: 1.2rem;
  margin-bottom: 20px;
  color:rgb(58, 53, 53);
  font-size: 22px;
  font-weight: 600;
  opacity: 0.9;
`;

const LoginForm = styled.div`
  display: flex;
  flex-direction: column;
`;

const Label = styled.label`
  font-size: 1.1rem;
  margin-bottom: 8px;
  color: #333; /* Adjust color to match your design */
  font-weight: 600; /* Make the label text a bit bolder */
  display: flex;
  justify-content: flex-start;
  align-items: center;
  text-align: left;
  min-width: 100%;  /* Ensure the label takes the full width without resizing */
  white-space: nowrap;  /* Prevent text wrapping */
`;


const Input = styled.input`
  padding: 12px;
  font-size: 1rem;
  margin-bottom: 15px;
  border: 1px solid #ddd;
  border-radius: 4px;
  background-color: #f3f3f3; /* Soft background for input */
  transition: border-color 0.3s ease, background-color 0.3s ease;

  &:focus {
    outline: none;
    border-color: #8ACCD5 /* Soft blue for focus */
    background-color: #ffffff; /* Lighten background on focus */
  }

  &::placeholder {
    color: #aaa;
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
  background-color: rgb(40, 136, 155);
  color: white;
  font-size: 0.9rem;
  border: none;
  flex-direction: column;
  border-radius: 6px;
  display: flex;

  cursor: pointer;
  transition: all 0.3s ease-in-out;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  width: 48%;

  &:hover {
    background-color: #8ACCD5;
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

const LanguageButtonContainer = styled.div`
  position: absolute;
  top: 15px;
  right: 15px;
`;

const LanguageButton = styled.button`
  padding: 5px 10px;
  font-size: 1rem;
  background: none;
  color: #125358;
  border: none;
  cursor: pointer;
  transition: color 0.3s ease;
  font-weight: bold;

  &:hover {
    color:  #8ACCD5;
  }

  &:focus {
    outline: none;
  }
`;


const StartPage = () => {
  const dispatch = useDispatch();
  const { t, i18n } = useTranslation();
  const navigate = useNavigate();
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [loginError, setLoginError] = useState('');
  const [loginMessage, setLoginMessage] = useState('');
  const [loading, setLoading] = useState(false);

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
      console.log('jwtToken for user after login:', response.data.item.jwtToken.encryptedToken);
      localStorage.setItem('userName', response.data.item.userName);

      const role = response.data.item.userRole;

      if (role === 'sysadmin') {
        localStorage.setItem('role', 'sysadmin');
        localStorage.setItem('userName', response.data.item.userName);
        localStorage.setItem('email', response.data.item.email);
        console.log('Admin login response:', response.data.item);

        setLoginMessage(t('login_success'));
        navigate('/admin');
      } else if (role === 'usr') {
        localStorage.setItem('role', response.data.item.userRole);
        localStorage.setItem('userName', response.data.item.userName);
        localStorage.setItem('email', response.data.item.email);

        console.log('Staff login response:', response.data.item);

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
        const response = await axiosInstance.post('/Guest/LoginStaff', loginDataStaff);
        console.log('Staff Login Response:', response);

        const token = response.data.item.jwtToken.encryptedToken;
        const role = response.data.item.userRole;

        localStorage.setItem('jwtToken', token);
        localStorage.setItem('userName', response.data.item.userName);
        localStorage.setItem('email', response.data.item.email);
        localStorage.setItem('role', role);
        localStorage.setItem('staffId', response.data.item.staffId);

        console.log('jwtToken for Staff after login:', token);
        console.log('Staff login response:', response.data.item);
        console.log('Staff ID:', response.data.item.staffId);

        // Set login message
        setLoginMessage(t('login_success'));

        // Role-based navigation
        if (role === 'sysadmin') {
          console.log("Sysadmin logged in. You have admin privileges.");
          navigate('/admin'); // <-- Change this to your actual admin route
        } else if (role === 'usr') {
          console.log("Regular user logged in.");
          navigate('/staff'); // <-- Staff dashboard
        }

      } catch (error) {
        console.error("Login failed:", error);
        setLoginMessage(t('login_failed'));
      } // ✅ Add this missing closing brace here

    }

    //login admin : sysadmin1 ....password: Sysadmin1! (Uppercase, lowercase, number, special character, 6 characters minimum)
    //login staff : Charlie1 ....password: Charlie1!(Uppercase, lowercase, number, special character, 6 characters minimum)
    //login admin : Mike1 ....password: Mike1! (Uppercase, lowercase, number, special character. 6 characters minimum)
    //login staff : Alice1 ....password: Alice1!(Uppercase, lowercase, number, special character, 6 characters minimum)
  };

  const handleKeyDown = (e) => {
    if (e.key === 'Enter') handleLogin();
  };

  const changeLanguage = (lang) => {
    dispatch(setLanguage(lang));
    const i18nInstance = getI18n();
    i18nInstance.changeLanguage(lang);
  };

  return (
    <>
      <GlobalStyle />
        <LogoContainer>
             <Link to="/">
               <img src={logo1} alt="Logo" style={{ width: '200px', objectFit: 'contain' }} />
             </Link>
           </LogoContainer>
      <PageWrapper>
      <StartPageContainer>
        <LanguageButtonContainer>
          <LanguageButton onClick={() => changeLanguage(i18n.language === 'en' ? 'sv' : 'en')}>
            {i18n.language === 'en' ? 'SV' : 'EN'}
          </LanguageButton>
        </LanguageButtonContainer>
        <Header>
          <Title>{t('app_title')}</Title>
          <SubTitle>{t('welcome')}</SubTitle>
        </Header>

        {loginMessage && (
          <p style={{ color: 'green', textAlign: 'center' }}>{loginMessage}</p>
        )}

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

        <Footer>
        
        </Footer>

      </StartPageContainer>
      </PageWrapper>
    </>
  );
};

export default StartPage;