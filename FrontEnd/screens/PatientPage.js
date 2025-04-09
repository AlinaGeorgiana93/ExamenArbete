<<<<<<< HEAD
import React from 'react';
import { View, Text, StyleSheet } from 'react-native';
import LinearGradient from 'react-native-linear-gradient';
import '../src/index.css'






=======
import React, { useState } from 'react';
import { createGlobalStyle } from 'styled-components';
import patient1 from '../src/patient1.jpg';

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
    color: #000;
  }
`;

// Simulated list of registered patients
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

  const renderDropdown = (label, name, options) => (
    <div style={{ marginBottom: '20px', textAlign: 'left' }}>
      <label htmlFor={name} style={{ display: 'block', fontWeight: 'bold', marginBottom: '6px' }}>
        {label}
      </label>
      <select
        id={name}
        name={name}
        value={formData[name]}
        onChange={handleChange}
        style={{ width: '100%', padding: '8px', borderRadius: '5px' }}
      >
        <option value="">Select</option>
        {options.map((opt) => (
          <option key={opt.value} value={opt.value}>
            {opt.label}
          </option>
        ))}
      </select>
    </div>
  );

  return (
    <>
      <GlobalStyle />
      <div
        style={{
          width: '100%',
          maxWidth: '500px',
          margin: '40px auto',
          background: 'rgba(255, 255, 255, 0.95)',
          padding: '30px',
          borderRadius: '12px',
          boxShadow: '0 4px 20px rgba(0, 0, 0, 0.2)',
        }}
      >
        <div style={{ textAlign: 'center', marginBottom: '20px' }}>
          <img
            src={patient1}
            alt="Patient"
            style={{ borderRadius: '50%', width: '130px', height: '130px', objectFit: 'cover' }}
          />
          <h2 style={{ marginTop: '15px' }}>
            {selectedPatient ? selectedPatient.name : 'Select a Patient'}
          </h2>
          <p>
            {selectedPatient ? `Personal Number: ${selectedPatient.personalNumber}` : ''}
          </p>
        </div>

        <form>
          <div style={{ marginBottom: '25px', textAlign: 'left' }}>
            <label style={{ display: 'block', fontWeight: 'bold', marginBottom: '6px' }}>
              Select Patient
            </label>
            <select
              value={selectedPatientId}
              onChange={handlePatientChange}
              style={{ width: '100%', padding: '8px', borderRadius: '5px' }}
            >
              <option value="">-- Select Patient --</option>
              {patientOptions.map((patient) => (
                <option key={patient.id} value={patient.id}>
                  {patient.name} - {patient.personalNumber}
                </option>
              ))}
            </select>
          </div>

          {renderDropdown('Mood', 'mood', moodOptions)}
          {renderDropdown('Sleep', 'sleep', sleepOptions)}
          {renderDropdown('Activity', 'activity', activityOptions)}
          {renderDropdown('Appetite', 'appetite', appetiteOptions)}

          <button
            type="button"
            onClick={handleSave}
            style={{
              marginTop: '20px',
              width: '100%',
              padding: '10px',
              backgroundColor: '#00d4ff',
              border: 'none',
              borderRadius: '6px',
              fontWeight: 'bold',
              cursor: 'pointer',
              fontSize: '16px',
            }}
          >
            Save
          </button>
        </form>
      </div>
    </>
  );
};

export default PatientPage;
>>>>>>> a1c43f788560da0e9915da6f5ebc2b5b92a4fa1a
