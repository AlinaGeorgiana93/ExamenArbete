import React, { useEffect, useState } from 'react';
import styled, { createGlobalStyle } from 'styled-components';
import { useParams, useNavigate, Link } from 'react-router-dom';
import axios from 'axios';
import { useTranslation } from 'react-i18next';
import logo1 from '../src/media/logo1.png';

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
    position: relative;
  }
`;

// Styled Components
const PatientPageContainer = styled.div`
  background-color: #fff;
  padding: 40px;
  border-radius: 8px;
  width: 100%;
  max-width: 400px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
  position: relative;
  z-index: 1;
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

const Button = styled.button`
  background-color: #125358;
  color: white;
  font-size: 1rem;
  padding: 12px 20px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  transition: background-color 0.3s ease;

  &:hover {
    background-color: #00d4ff;
  }
`;

const LogoLink = styled(Link)`
  position: fixed;
  top: 15px;
  right: 15px;
  z-index: 2;

  img {
    width: 150px;
  }
`;

function PatientPage() {
  const { patientId } = useParams();
  const [patient, setPatient] = useState(null);
  const [loading, setLoading] = useState(true);

  const [moodKinds, setMoodKinds] = useState([]);
  const [selectedMoodKind, setSelectedMoodKind] = useState('');
  const { t } = useTranslation();
  const navigate = useNavigate();

  // Fetch patient details by patientId
  useEffect(() => {
    axios.get(`https://localhost:7066/api/Patient/ReadItem?id=${patientId}`)
      .then(res => {
        setPatient(res.data.item);
        setLoading(false);
      })
      .catch(err => {
        console.error('Error fetching patient:', err);
        setLoading(false);
      });
    console.log("Patient ID from URL:", patientId);
  }, [patientId]);

  // Fetch available MoodKinds
  useEffect(() => {
    // Update the mood kinds API to remove pagination
axios.get('https://localhost:7066/api/MoodKind/ReadItems')
.then(response => {
    if (response.data && response.data.pageItems) {
    setMoodKinds(response.data.pageItems); // This should work fine if pageItems is returned
  } else {
    console.error("Error: No moodKinds found in response");
  }
})
.catch(error => {
  console.error("Error fetching moodkinds:", error);
});

  }, []);

  // ✅ Handle the MoodKind selection
  const handleMoodKindSelect = (e) => {
    const moodKindId = e.target.value;
    setSelectedMoodKind(moodKindId);
    if (moodKindId) {
      navigate(`/moodkind/${moodKindId}`); // Adjust route if needed
    }
  };

  // ✅ Save moodKind selection
  const handleSave = async () => {
    if (!selectedMoodKind) {
      alert("Vänligen välj ett MoodKind.");
      return;
    }

    try {
      await axios.post('https://localhost:7066/api/Graph/CreateItem', {
        patientId: patientId,
        moodKindId: selectedMoodKind,
        date: new Date().toISOString(),
      });

      alert("Symptom sparad!");
    } catch (err) {
      console.error("Error saving symptom:", err);
      alert("Kunde inte spara symptom");
    }
  };

  if (loading) return <div>Laddar patient...</div>;
  if (!patient) return <div>Ingen patient hittad.</div>;

  return (
    <>
      <GlobalStyle />
      <LogoLink to="/">
        <img src={logo1} alt="Logo" />
      </LogoLink>

      <div className="p-4">
        <h1 className="text-2xl font-bold mb-4">
          {patient.firstName} {patient.lastName}
        </h1>

        <PatientPageContainer>
          <label htmlFor="moodkind-select">{t('select_moodkind')}</label>
          <Dropdown
            id="moodkind-select"
            value={selectedMoodKind}
            onChange={handleMoodKindSelect}
          >
            <option value="">{t('choose_moodkind')}</option>
            {moodKinds.length > 0 ? (
              moodKinds.map((moodkind) => (
                <option key={moodkind.moodKindId} value={moodkind.moodKindId}>
                  {moodkind.label} {moodkind.rating}
                </option>
              ))
            ) : (
              <option disabled>{t('no_moodkinds_available')}</option>
            )}
          </Dropdown>
        </PatientPageContainer>

        <Button onClick={handleSave}>
          Spara Symptom
        </Button>
      </div>
    </>
  );
}

export default PatientPage;
