import React, { useState, useEffect } from 'react';
import styled, { createGlobalStyle } from 'styled-components';
import axios from 'axios';
import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';
import Select from 'react-select'; // Vi använder react-select


const GlobalStyle = createGlobalStyle`
  * {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
    font-size: 1.3rem;
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
  width: 800px; /* ökad bredd */
  box-shadow: 0 8px 24px rgba(0, 0, 0, 0.2);
  text-align: center;
  margin-top: 10px; /* flytta upp */

  @media (max-width: 760px) {
    padding: 30px 20px;
    width: 100%; /* responsiv bredd för mindre skärmar */
    margin-top: 10px;
  }
`;

const Title = styled.h1`
  color: #1a5b61;
  font-size: 3rem;
  margin-bottom: 20px;
`;

const LoggedInInfo = styled.p`
  font-size: 1.9rem;
  color: rgb(12, 53, 57);
  margin-bottom: 50px;
`;

const SelectWrapper = styled.div`
  margin: 20px 0;
  text-align: left;
`;

const StaffPage = () => {
  const [patients, setPatients] = useState([]);
  const [selectedPatient, setSelectedPatient] = useState(null);
  const [staffName, setStaffName] = useState('');
  const { t } = useTranslation();
  const navigate = useNavigate();

  useEffect(() => {
    axios
      .get('https://localhost:7066/api/Patient/ReadItems')
      .then((response) => {
        if (response.data?.pageItems) {
          setPatients(response.data.pageItems);
        }
      })
      .catch((error) => {
        console.error('Error fetching patients:', error);
      });

    // Kontrollera att namn finns i localStorage
    const storedName = localStorage.getItem('userName');
    if (storedName) {
      console.log('Hämtat namn från localStorage:', storedName); // ← debug
      setStaffName(storedName);
    } else {
      console.warn('Inget namn hittades i localStorage!');
    }
  }, []);



  const handlePatientSelect = (selectedOption) => {
    setSelectedPatient(selectedOption);
    if (selectedOption?.value) {
      navigate(`/patient/${selectedOption.value}`);
    }
  };

  // Konverterar patientlistan till rätt format för react-select
  const options = patients.map((patient) => ({
    value: patient.patientId,
    label: `${patient.firstName} ${patient.lastName}`,
  }));

  return (
    <>
      <GlobalStyle />

      <StaffPageContainer>
        <Title>{t('staff_name')}</Title>   <WarningText>★{t('choose_patient')}</WarningText>
        {/* Översatt rubrik */}
        {/* Label och dropdown för att välja patient */}

        <DropdownWrapper>
          <Dropdown
            id="patient-select"
            value={selectedPatient}
            onChange={handlePatientSelect}
            placeholder={t('choose_patient')}
            isClearable
            styles={{
              control: (base) => ({
                ...base,
                fontSize: '1rem',
                borderRadius: '8px',
                padding: '2px',
                backgroundColor: 'white',
              }),
            }}
          />
        </SelectWrapper>
      </StaffPageContainer>
    </>
  );
};

export default StaffPage;
