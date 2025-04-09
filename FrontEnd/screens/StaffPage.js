import React, { useState, useEffect } from 'react';
import axios from 'axios';
import styled from 'styled-components';

const StaffPageContainer = styled.div`
  background-color: #fff;
  padding: 40px;
  border-radius: 8px;
  width: 100%;
  max-width: 500px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
  text-align: center;
  color: #333;
`;

const Title = styled.h1`
  font-size: 2rem;
  margin-bottom: 20px;
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
        <StaffPageContainer>
            <Title>Staff Dashboard</Title>

            <label htmlFor="patient-select">Select Patient:</label>
            <Dropdown
                id="patient-select"
                value={selectedPatient}
                onChange={(e) => setSelectedPatient(e.target.value)}
            >
                <option value="">-- Choose a patient --</option>
                {patients.map((patient) => (
                    <option key={patient._id} value={patient._id}>
                        {patient.firstName} {patient.lastName} {/* Här visas både förnamn och efternamn */}
                    </option>
                ))}
            </Dropdown>

            <AboutButton href="/about">About Us</AboutButton>
        </StaffPageContainer>
    );
};

export default StaffPage;
