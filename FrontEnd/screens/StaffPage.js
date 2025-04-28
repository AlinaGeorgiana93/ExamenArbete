// Importerar nödvändiga React hooks och bibliotek
import React, { useState, useEffect } from 'react';
import styled, { createGlobalStyle } from 'styled-components'; // För styling med styled-components
import axios from 'axios'; // För att göra HTTP-anrop
import { useTranslation } from 'react-i18next'; // För översättning
import { Link, useNavigate } from 'react-router-dom'; // För routing (t.ex. navigera till en ny sida)
import logo1 from '../src/media/logo1.png'; // Logo-bild

// Global styling som gäller hela sidan
const GlobalStyle = createGlobalStyle`
  * {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
  }
  body {
    font-family: 'Segoe UI', sans-serif;
    background: linear-gradient(135deg, #3B878C, #00d4ff, #006E75, #50D9E6, #1A5B61); 
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100vh;
    padding-bottom: 200px;
    color: #fff;
  }
`;

// Den vita rutan på sidan som innehåller allt innehåll
const StaffPageContainer = styled.div`
  background-color: #ffffff;
  padding: 50px 40px;
  border-radius: 16px;
  width: 100%; /* ändrat från 100% för att minska på små skärmar */
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

// Titel på sidan (t.ex. "Staff Dashboard")
const Title = styled.h1`
  color: #1a5b61;
  font-size: 2.5rem;
  margin-bottom: 20px;
`;

// Wrapper runt dropdownen med en pil ikon
const DropdownWrapper = styled.div`
  position: relative;
  width: 100%;

  &::after {
    content: '▼'; // Pil-symbolen
    position: absolute;
    right: 16px;
    top: 50%;
    transform: translateY(-50%);
    pointer-events: none;
    color: #555;
    font-size: 0.9rem;
  }
`;

// Dropdown-menyn för att välja patient
const Dropdown = styled.select`
  padding: 14px 16px;
  font-size: 1rem;
  border: 1px solid #ccc;
  border-radius: 8px;
  margin: 20px 0;
  width: 100%;
  appearance: none; // Tar bort standardpilen
  background-color: #f9f9f9;
  color: #333;

  &:focus {
    outline: none;
    border-color: #3b878c;
    background-color: #fff;
  }
`;

// Knappen "About Us"
const AboutButton = styled.a`
  display: inline-block;
  padding: 14px 24px;
  background-color: #125358;
  color: white;
  font-size: 1rem;
  text-decoration: none;
  border-radius: 8px;
  transition: background-color 0.3s ease;
  margin-top: 20px;

  &:hover {
    background-color: #00d4ff;
  }
`;
const WarningText = styled.p`
  color: red;
  margin-top: 10px;
  font-size: 1rem;
`;
// Själva StaffPage-komponenten
const StaffPage = () => {
  const [patients, setPatients] = useState([]); // Här sparar vi alla patienter
  const [selectedPatient, setSelectedPatient] = useState(''); // Här sparar vi den valda patientens ID
  const { t } = useTranslation(); // För att översätta texter
  const navigate = useNavigate(); // För att navigera till andra sidor

  // useEffect körs när komponenten laddas första gången
  useEffect(() => {
    // Hämta patienter från API:t
    axios
      .get('https://localhost:7066/api/Patient/ReadItems')
      .then((response) => {
        if (response.data && response.data.pageItems) {
          setPatients(response.data.pageItems); // Spara patientlistan i state
        }
      })
      .catch((error) => {
        console.error('Error fetching patients:', error);
      });
  }, []);

  // När en patient väljs i dropdownen
  const handlePatientSelect = (e) => {
    const patientId = e.target.value;
    setSelectedPatient(patientId); // Sätt den valda patienten i state
    if (patientId) {
      navigate(`/patient/${patientId}`); // Navigera till patientsidan med rätt ID
    }
  };

  return (
    <>
      <GlobalStyle /> {/* Lägger till globala stilar */}
      {/* Logotyp som är klickbar och går till startsidan */}
      <Link
        to="/"
        style={{ position: 'fixed', top: '15px', right: '15px', zIndex: '2' }}
      >
        <img src={logo1} alt="Logo" style={{ width: '140px' }} />
      </Link>
      {/* Huvudsektionen med all innehåll */}
      <StaffPageContainer>
        <Title>{t('staff_name')}</Title>   <WarningText>★ Välj en patient</WarningText>
        {/* Översatt rubrik */}
        {/* Label och dropdown för att välja patient */}
        <label htmlFor="patient-select">{t('select_patient')}</label>
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

export default StaffPage; // Exporterar komponenten så den kan användas i appen
