import React, { useState } from 'react';
import { createGlobalStyle } from 'styled-components';
import patient1 from '../src/patient1.jpg'; // Adjust path if patient1.jpg is in /src

// Global styles
const GlobalStyle = createGlobalStyle`
  * {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
  }
  body {
    font-family: 'Comic Sans MS', cursive, sans-serif;
    background: linear-gradient(135deg, #3B878C, #00d4ff, #006E75, #50D9E6, #1A5B61);
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100vh;
    color: #000; /* Changed to black */
    position: relative;
  }
`;

const PatientPage = () => {
  const [formData, setFormData] = useState({
    mood: '',
    sleep: '',
    activity: '',
    appetite: '',
  });

  // Handle form input changes
  const handleChange = (e) => {
    setFormData((prev) => ({
      ...prev,
      [e.target.name]: e.target.value,
    }));
  };

  // Handle save action (just logging data here)
  const handleSave = () => {
    console.log("Saved Data:", formData);
    alert("Patient data saved!");
  };

  // Render dropdown with label
  const renderDropdown = (label, name) => (
    <div style={{ display: 'flex', alignItems: 'center', marginBottom: '20px', justifyContent: 'space-between' }}>
      <label htmlFor={name} style={{ fontWeight: 'bold', width: '100px', textAlign: 'left' }}>
        {label}:
      </label>
      <select
        id={name}
        name={name}
        value={formData[name]}
        onChange={handleChange}
        style={{ flex: 1, padding: '5px' }}
      >
        <option value="">Select</option>
        {[...Array(11).keys()].map((num) => (
          <option key={num} value={num}>{num}</option>
        ))}
      </select>
    </div>
  );

  return (
    <>
      <GlobalStyle />
      <div style={{ maxWidth: '400px', margin: '50px auto', textAlign: 'center', fontFamily: 'Arial, sans-serif' }}>
        {/* Patient Image */}
        <img
          src={patient1}
          alt="Patient"
          style={{ borderRadius: '50%', marginBottom: '20px', width: '150px', height: '150px', objectFit: 'cover' }}
        />

        {/* Patient Info */}
        <h2>John Doe</h2>
        <p>Personal Number: 123456789</p>

        {/* Dropdowns */}
        {renderDropdown('Mood', 'mood')}
        {renderDropdown('Sleep', 'sleep')}
        {renderDropdown('Activity', 'activity')}
        {renderDropdown('Appetite', 'appetite')}

        {/* Save Button */}
        <button
          onClick={handleSave}
          style={{ padding: '10px 20px', fontWeight: 'bold', cursor: 'pointer', backgroundColor: '#00d4ff', border: 'none', borderRadius: '4px' }}
        >
          Save
        </button>
      </div>
    </>
  );
};

export default PatientPage;
