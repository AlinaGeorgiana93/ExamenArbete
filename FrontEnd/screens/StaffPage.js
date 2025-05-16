import React, { useState, useEffect, use } from 'react';
import styled, { createGlobalStyle } from 'styled-components';
import axios from 'axios';
import { useTranslation } from 'react-i18next';
import { Link, useNavigate } from 'react-router-dom';
import logo1 from '../src/media/logo1.png';
import patient1 from '../src/media/patient1.jpg';
import useStoredUserInfo from '../src/useStoredUserInfo.js';
import FloatingProfile from '../src/FloatingProfile.js';


const GlobalStyle = createGlobalStyle`
  * {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
  }
  body {
    font-family: 'Poppins', sans-serif;
    background: linear-gradient(135deg,rgb(139, 229, 238),rgb(51, 225, 207), #b2dfdb);
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100vh;
    padding-bottom: 200px;
    color: #fff;
  }
`;

const StaffPageContainer = styled.div`
  background-color: #F5ECD5;
  padding: 50px 40px;
  border-radius: 16px;
  width: 100%;
  box-shadow: 0 8px 24px rgba(0, 0, 0, 0.2);
  z-index: 1;
  text-align: center;
  margin-top: 70px;

  @media (max-width: 768px) {
    padding: 30px 20px;
    border-radius: 12px;
  }

  @media (max-width: 480px) {
    padding: 20px 15px;
    border-radius: 10px;
  }
`;

const Title = styled.h1`
  color: #1a5b61;
  font-size: 2.5rem;
  margin-bottom: 20px;
`;

const DropdownWrapper = styled.div`
  position: relative;
  width: 100%;
  
  &::after {
    content: '▼';
    position: absolute;
    right: 16px;
    top: 50%;
    transform: translateY(-50%);
    pointer-events: none;
    color: #555;
    font-size: 0.9rem;
  }
`;

const Dropdown = styled.select`
  padding: 14px 16px;
  font-size: 1rem;
  border: 1px solid #ccc;
  border-radius: 8px;
  margin: 20px 0;
  width: 100%;
  appearance: none;
  background-color: #f9f9f9;
  color: #333;

  &:focus {
    outline: none;
    border-color: #3b878c;
    background-color: #fff;
  }
`;

const WarningText = styled.p`
  color: red;
  margin-top: 10px;
  font-size: 1rem;
`;


const StaffPage = () => {
  const [patients, setPatients] = useState([]);
  const [selectedPatient, setSelectedPatient] = useState('');
  const { t } = useTranslation();
  const navigate = useNavigate();
  const { userName, setUserName, email, setEmail, role } = useStoredUserInfo();

  useEffect(() => {
    axios
      .get('https://localhost:7066/api/Patient/ReadItems')
      .then((response) => {
        if (response.data && response.data.pageItems) {
          setPatients(response.data.pageItems);
        }
      })
      .catch((error) => {
        console.error('Error fetching patients:', error);
      });
  }, []);


  const handlePatientSelect = (e) => {
    const patientId = e.target.value;
    setSelectedPatient(patientId);
    if (patientId) {
      navigate(`/patient/${patientId}`);
    }
  };

  
  return (
    <>
      <GlobalStyle />
      <Link
        to="/"
        style={{ position: 'fixed', top: '15px', right: '15px', zIndex: '2' }}
      >
        <img src={logo1} alt="Logo" style={{ width: '140px' }} />
      </Link>
      <StaffPageContainer>
      <FloatingProfile
      userName={userName}
      email={email}
      role={role}
      setUserName={setUserName}
      setEmail={setEmail}
    />

        <Title>{t('staff_name')}</Title>
        <WarningText>★{t('choose_patient')}</WarningText>

        <DropdownWrapper>
          <Dropdown
            id="patient-select"
            value={selectedPatient}
            onChange={handlePatientSelect}
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
        </DropdownWrapper>
      </StaffPageContainer>

    </>
  );
};

export default StaffPage;
