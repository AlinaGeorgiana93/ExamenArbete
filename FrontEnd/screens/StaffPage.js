import React, { useState, useEffect } from 'react';
import styled, { createGlobalStyle } from 'styled-components';
import axios from 'axios';
import { useTranslation } from 'react-i18next';
import { Link, useNavigate } from 'react-router-dom'; // ← useNavigate tillagd
import logo1 from '../src/media/logo1.png';

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

const Title = styled.h1`
  color: #333;
  font-size: 2rem;
  margin-bottom: 10px;
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
  const [patients, setPatients] = useState([]);
  const [selectedPatient, setSelectedPatient] = useState('');
  const { t } = useTranslation();
  const navigate = useNavigate(); // ← Navigation-funktion

  useEffect(() => {
    axios.get('https://localhost:7066/api/Patient/ReadItems')
      .then(response => {
        if (response.data && response.data.pageItems) {
          setPatients(response.data.pageItems);
        }
      })
      .catch(error => {
        console.error("Error fetching patients:", error);
      });
  }, []);

  // Funktion för att hantera val av patient
  const handlePatientSelect = (e) => {
    const patientId = e.target.value;
    setSelectedPatient(patientId);
    if (patientId) {
      navigate(`/patient/${patientId}`); // Skicka till patientsidan
    }
  };

  return (
    <>
      <GlobalStyle />
      <Link to="/" style={{ position: 'fixed', top: '15px', right: '15px', zIndex: '2' }}>
        <img src={logo1} alt="Logo" style={{ width: '150px' }} />
      </Link>

      <StaffPageContainer>
        <Title>{t('staff_name')}</Title>

        <label htmlFor="patient-select">{t('select_patient')}</label>
        <Dropdown
          id="patient-select"
          value={selectedPatient}
          onChange={handlePatientSelect} // ← Här är ändringen
        >
          <option value="">{t('choose_patient')}</option>
          {patients.length > 0 ? (
            patients.map((patient) => (
              <option key={patient.patientId} value={patient.patientId}>
                {patient.firstName} {patient.lastName}
              </option>
            ))
          ) : (
            <option disabled>{t('no_patients_available')}</option>
          )}
        </Dropdown>

        <AboutButton href="/about">{t('about-us')}</AboutButton>
      </StaffPageContainer>
    </>
  );
};

export default StaffPage;
