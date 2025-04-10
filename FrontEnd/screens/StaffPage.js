import React, { useState } from 'react';
import styled, { createGlobalStyle } from 'styled-components';
import axios from 'axios';  // Import axios for API calls
import { useEffect } from 'react';  // Import useEffect for side effects
import '../language/i18n.js';
import { useNavigate, Link } from 'react-router-dom';  // Import Link for navigation
import { useTranslation } from 'react-i18next';
import logo1 from '../src/media/logo1.png';
import { useDispatch } from 'react-redux';
import { setLanguage } from '../language/languageSlice'; 
import { getI18n } from 'react-i18next'; 


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

const StaffPageContainer = styled.div`
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

const Dropdown = styled.select`
  padding: 12px;
  font-size: 1rem;
  border: 1px solid #ddd;
  border-radius: 4px;
  margin: 20px 0;
  width: 100%;

  &:focus {
    outline: none;
    border-color: #3B878C;
  }
`;

const AboutButton = styled.a`
  display: inline-block;
  padding: 12px 20px;
  background-color: #125358;
  color: white;
  font-size: 1rem;
  text-decoration: none;
  border-radius: 4px;
  transition: background-color 0.3s ease;

  &:hover {
    background-color: #00d4ff;
  }
`;

const StaffPage = () => {
  const [patients, setPatients] = useState([]);  // Här sparas patientdata
  const [selectedPatient, setSelectedPatient] = useState('');
  const dispatch = useDispatch();
  const { t } = useTranslation();
  

  useEffect(() => {
    // Hämta patienter från backend (API)
    axios.get('https://localhost:7066/api/patients')  // Backend API URL (med HTTPS)
      .then(response => {
        setPatients(response.data);  // Sätt data från API i state
      })
      .catch(error => {
        console.error("Error fetching patients:", error);
      });
  }, []);  // Tom array så den bara körs en gång när komponenten laddas

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

      <StaffPageContainer>
  <Title>{t('staff_name')}</Title>

  <label htmlFor="patient-select">{t('select_patient')}</label>
  <Dropdown
    id="patient-select"
    value={selectedPatient}
    onChange={(e) => setSelectedPatient(e.target.value)}
  >
    <option value="">{t('choose_patient')}</option>
    {patients.map((patient) => (
      <option key={patient._id} value={patient._id}>
        {patient.firstName} {patient.lastName}
      </option>
    ))}
  </Dropdown>

  <AboutButton href="/about">{t('about-us')}</AboutButton>
</StaffPageContainer>

    </>
  );
};

export default StaffPage;
