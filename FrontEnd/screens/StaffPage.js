import React, { useState, useEffect, use } from 'react';
import styled, { createGlobalStyle } from 'styled-components';
import axios from 'axios';
import { useTranslation } from 'react-i18next';
import { Link, useNavigate } from 'react-router-dom';
import patientsImage from '../src/media/patients.png';


const GlobalStyle = createGlobalStyle`
  * {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
  }
`;


const StaffPageContainer = styled.div`
  background-color: #F1F0E8;
  padding: 50px 50px;
  border-radius: 16px;
  width: 100%;
  box-shadow: 0 8px 24px rgba(0, 0, 0, 0.2);
  z-index: 1;
  text-align: center;
  margin-top: 100px; /* slightly moved down */

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
const Patients = styled.img`
  width: 90px;
  height: 90px;
  border-radius: 50%;
  object-fit: cover;
  margin: 0 auto 25px auto;
  display: block;
  box-shadow: 0 4px 15px rgba(0,0,0,0.15);
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
          <Patients src={patientsImage} alt="Patients" />
      <StaffPageContainer>
       
        <Title>{t('choose_patient')}</Title>

        <DropdownWrapper>
          <Dropdown
            id="patient-select"
            value={selectedPatient}
            onChange={handlePatientSelect}
          >
            <option value="">{t('Patient')}</option>
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
