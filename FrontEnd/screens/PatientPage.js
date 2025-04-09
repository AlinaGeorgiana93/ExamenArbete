import React, { useState } from 'react';
import styled, { createGlobalStyle } from 'styled-components';
import patient1 from '../src/patient1.jpg';
import logo1 from '../src/logo1.png';
import axios from 'axios';


// Global Style
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
  }
`;

// Styled Components
const PageContainer = styled.div`
  background-color: #fff;
  padding: 40px;
  border-radius: 8px;
  width: 100%;
  max-width: 500px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
  color: #000;
`;

const Header = styled.header`
  text-align: center;
  margin-bottom: 30px;
`;

const PatientImage = styled.img`
  border-radius: 50%;
  width: 130px;
  height: 130px;
  object-fit: cover;
`;

const Title = styled.h2`
  color: #333;
  margin-top: 15px;
`;

const SubTitle = styled.p`
  color: #666;
`;

const Label = styled.label`
  font-size: 1.1rem;
  font-weight: bold;
  display: block;
  margin-bottom: 6px;
`;

const Select = styled.select`
  width: 100%;
  padding: 10px;
  border-radius: 5px;
  margin-bottom: 20px;
  border: 1px solid #ccc;

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
  width: 100%;

  &:hover {
    background-color: #00d4ff;
  }
`;

// Patient Options
const patientOptions = [
  { id: 1, name: 'Madi Alabama', personalNumber: '19560831-1111' },
  { id: 2, name: 'John Doe', personalNumber: '19480516-2222' },
  { id: 3, name: 'Jane Smith', personalNumber: '19610228-1212' },
  { id: 4, name: 'Alice Johnson', personalNumber: '19450801-4444' },
  { id: 5, name: 'Bob Brown', personalNumber: '19501110-1331' },
  { id: 6, name: 'Charlie Davis', personalNumber: '19511231-16181' },
];

const moodOptions = [
  { value: 10, label: 'Very happy 😃' },
  { value: 9, label: 'Excited 🤩' },
  { value: 8, label: 'Good mood 😄' },
  { value: 7, label: 'Lovely 😍' },
  { value: 6, label: 'Slightly positive 🙂' },
  { value: 5, label: 'Neutral 😐' },
  { value: 4, label: 'Bored 😒' },
  { value: 3, label: 'Angry 😡' },
  { value: 2, label: 'Very sad 🙁' },
  { value: 1, label: 'Depressed 😢' },
  { value: 0, label: 'Extremely low 😭' },
];

const sleepOptions = [
  { value: 10, label: 'Too much sleep 😃' },
  { value: 9, label: 'Slept more than usual' },
  { value: 8, label: 'OK sleep level 🙂' },
  { value: 7, label: 'Decent sleep' },
  { value: 6, label: 'Medium sleep level 😐' },
  { value: 5, label: 'Low sleep level 🙁' },
  { value: 4, label: 'Poor sleep' },
  { value: 3, label: 'Interrupted sleep' },
  { value: 2, label: 'Very low sleep' },
  { value: 1, label: 'Barely slept' },
  { value: 0, label: 'No sleep at all 🫠' },
];

const activityOptions = [
  { value: 10, label: 'Very intense (Jogging) 🏃‍♂️' },
  { value: 9, label: 'Intense exercise (Training) 🏋️‍♂️' },
  { value: 8, label: 'Active day' },
  { value: 7, label: 'Moderate exercise (Swimming) 🏊‍♂️' },
  { value: 6, label: 'Light exercise' },
  { value: 5, label: 'Normal movement (Walking) 🚶‍♂️' },
  { value: 4, label: 'Light chores' },
  { value: 3, label: 'Light activity (Reading) 📖' },
  { value: 2, label: 'Minimal movement' },
  { value: 1, label: 'Resting' },
  { value: 0, label: 'Completely inactive 🛌' },
];

const appetiteOptions = [
  { value: 10, label: 'Very much 🍴' },
  { value: 9, label: 'Large meal' },
  { value: 8, label: 'Ate quite a lot' },
  { value: 7, label: 'Medium 😋' },
  { value: 6, label: 'Slightly more than usual' },
  { value: 5, label: 'Normal appetite 🙂' },
  { value: 4, label: 'Small portion' },
  { value: 3, label: 'Very little 🍽️' },
  { value: 2, label: 'Ate a bite or two' },
  { value: 1, label: "Didn't eat at all 🤢" },
  { value: 0, label: "Couldn't eat at all 🤮" },
];

const PatientPage = () => {
  const [selectedPatientId, setSelectedPatientId] = useState('');
  const [formData, setFormData] = useState({
    mood: '',
    sleep: '',
    activity: '',
    appetite: '',
  });

  const selectedPatient = patientOptions.find(p => p.id.toString() === selectedPatientId);

  const handlePatientChange = (e) => {
    setSelectedPatientId(e.target.value);
  };

  const handleChange = (e) => {
    setFormData((prev) => ({
      ...prev,
      [e.target.name]: e.target.value,
    }));
  };

  const handleSave = () => {
    console.log("Saved Data:", {
      patient: selectedPatient,
      ...formData,
    });
    alert("Patient data saved!");
  };

  const renderDropdown = (labelText, name, options) => (
    <>
      <Label htmlFor={name}>{labelText}</Label>
      <Select id={name} name={name} value={formData[name]} onChange={handleChange}>
        <option value="">Select</option>
        {options.map((opt) => (
          <option key={opt.value} value={opt.value}>{opt.label}</option>
        ))}
      </Select>
    </>
  );

  return (
     <>
          <GlobalStyle />
          {/* Logo positioned directly in the body, without a container */}
          <img
            src={logo1}
            alt="Logo"
            style={{
              position: 'fixed',
              top: '15px',
              right: '15px',
              width: '150px', // Adjust the logo size as needed
              zIndex: '2', // Ensure it stays above other elements
            }}
          />
      <PageContainer>
        <Header>
          <PatientImage src={patient1} alt="Patient" />
          {/* <Title>{selectedPatient ? selectedPatient.name : 'Select a Patient'}</Title> */}
          <SubTitle>{selectedPatient ? `Personal Number: ${selectedPatient.personalNumber}` : ''}</SubTitle>
        </Header>

        <Label>Select Patient</Label>
        <Select value={selectedPatientId} onChange={handlePatientChange}>
          <option value="">-- Select Patient --</option>
          {patientOptions.map((patient) => (
            <option key={patient.id} value={patient.id}>
              {patient.name}
            </option>
          ))}
        </Select>

        {renderDropdown('Mood', 'mood', moodOptions)}
        {renderDropdown('Sleep', 'sleep', sleepOptions)}
        {renderDropdown('Activity', 'activity', activityOptions)}
        {renderDropdown('Appetite', 'appetite', appetiteOptions)}

        <Button onClick={handleSave}>Save</Button>
      </PageContainer>
    </>
  );
};

export default PatientPage;
