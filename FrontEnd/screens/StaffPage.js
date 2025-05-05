// Importerar nödvändiga React hooks och bibliotek
import React, { useState, useEffect } from 'react';
import styled, { createGlobalStyle } from 'styled-components'; // För styling med styled-components
import axios from 'axios'; // För att göra HTTP-anrop
import { useTranslation } from 'react-i18next'; // För översättning
import { Link, useNavigate } from 'react-router-dom'; // För routing (t.ex. navigera till en ny sida)
import logo1 from '../src/media/logo1.png'; // Logo-bild
import patient1 from '../src/media/patient1.jpg';

// Global styling som gäller hela sidan
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

// Den vita rutan på sidan som innehåller allt innehåll
const StaffPageContainer = styled.div`
  background-color: #F5ECD5;
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

const WarningText = styled.p`
  color: red;
  margin-top: 10px;
  font-size: 1rem;
`;

const FloatingProfile = styled.div`
  position: fixed;
  bottom: 32px;
  right: 20px;
  background-color: #ffffff;
  border-radius: 8px;
  padding: 10px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  cursor: pointer;
  z-index: 1000;
`;

const ProfileHeader = styled.div`
  display: flex;
  align-items: center;
  gap: 10px;
`;

const ProfileDetails = styled.div`
  margin-top: 10px;
  font-size: 0.9rem;
  color: #333;
`;


// Själva StaffPage-komponenten
const StaffPage = () => {
  const [patients, setPatients] = useState([]); // Här sparar vi alla patienter
  const [selectedPatient, setSelectedPatient] = useState(''); // Här sparar vi den valda patientens ID
  const { t } = useTranslation(); // För att översätta texter
  const navigate = useNavigate(); // För att navigera till andra sidor
  const [showDetails, setShowDetails] = useState(false);
  const [nameOf, setNameOf] = useState(''); // State for storing user name

  // useEffect to check and set nameOf from localStorage
  useEffect(() => {
    const storedName = localStorage.getItem('userName');
    if (storedName) {
      console.log('Hämtat namn från localStorage:', storedName); // ← debug
      setNameOf(storedName); // Set the name if it's found in localStorage
    } else {
      console.warn('Inget namn hittades i localStorage!');
    }
  }, []); // Empty dependency array ensures this runs once on mount

  // useEffect to fetch patients from the API
  useEffect(() => {
    axios
      .get('https://localhost:7066/api/Patient/ReadItems')
      .then((response) => {
        if (response.data && response.data.pageItems) {
          setPatients(response.data.pageItems); // Save patient list in state
        }
      })
      .catch((error) => {
        console.error('Error fetching patients:', error);
      });
  }, []); // Empty dependency array ensures this runs once on mount

  // When a patient is selected from the dropdown
  const handlePatientSelect = (e) => {
    const patientId = e.target.value;
    setSelectedPatient(patientId); // Set the selected patient in state
    if (patientId) {
      navigate(`/patient/${patientId}`); // Navigate to patient page with the right ID
    }
  };

  const userData = {
    name: nameOf, // Using nameOf state here
    email: localStorage.getItem('email'),
    role: localStorage.getItem('role'),
  };

  return (
    <>
      <GlobalStyle /> {/* Lägger till globala stilar */}
      <Link
        to="/"
        style={{ position: 'fixed', top: '15px', right: '15px', zIndex: '2' }}
      >
        <img src={logo1} alt="Logo" style={{ width: '140px' }} />
      </Link>
      <StaffPageContainer>
        <FloatingProfile onClick={() => setShowDetails(prev => !prev)}>
          <ProfileHeader>
            <img
              src={patient1}
              alt="User Avatar"
              style={{ width: '40px', height: '40px', borderRadius: '50%' }}
            />
            <span>{userData.name}</span>
          </ProfileHeader>

          {showDetails && (
            <ProfileDetails>
              <div><strong>Email:</strong> {userData.email}</div>
              <div><strong>Role:</strong> {userData.role}</div>
            </ProfileDetails>
          )}
        </FloatingProfile>

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

export default StaffPage; // Exporterar komponenten så den kan användas i appen
