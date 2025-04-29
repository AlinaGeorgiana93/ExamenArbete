import React, { useEffect, useState } from 'react';
import styled, { createGlobalStyle } from 'styled-components';
import { useParams, useNavigate, Link } from 'react-router-dom';
import axios from 'axios';
import { useTranslation } from 'react-i18next';
import logo1 from '../src/media/logo1.png';
import patient1 from '../src/media/patient1.jpg';


const GlobalStyle = createGlobalStyle`
  * {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
    font-size: 1.3rem;
  }
  body {
    font-family: 'Times New Roman', cursive, sans-serif;
    background: linear-gradient(135deg, #e0f7f9, #cceae7, #b2dfdb);
    min-height: 100vh;
    color: rgb(29, 29, 29);
    overflow-y: auto;
  }
`;



const PatientPageContainer = styled.div`
  background-color: #ffffffee;
  padding: 30px;
  border-radius: 16px;
  max-width: 450px;
  width: 100%;
  box-shadow: 0 8px 20px rgba(0, 0, 0, 0.2);
  position: relative;
  z-index: 1;
  backdrop-filter: blur(6px);
`;

const FormGroup = styled.div`
  margin-bottom: 20px;

  label {
    display: block;
    margin-bottom: 6px;
    color: #333;
    font-weight: 600;
  }
`;

const Dropdown = styled.select`
  padding: 12px;
  font-size: 1rem;
  border: 1px solid #ccc;
  border-radius: 8px;
  width: 100%;
  background-color: #f9f9f9;
  transition: border-color 0.3s ease;

  &:focus {
    outline: none;
    border-color: #3b878c;
    background-color: #fff;
  }
`;

const Button = styled.button`
  background-color: #125358;
  color: white;
  font-size: 1.1rem;
  padding: 14px 28px;
  border: none;
  border-radius: 30px;
  cursor: pointer;
  margin-top: 25px;
  width: 100%;
  font-weight: bold;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.15);
  transition:
    background-color 0.3s ease,
    transform 0.2s ease;

  &:hover {
    background-color: #00d4ff;
    color: #00363a;
    transform: scale(1.03);
  }
`;

const PatientHeader = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-bottom: 30px;
`;

const PatientImage = styled.img`
  width: 120px;
  height: 120px;
  border-radius: 50%;
  object-fit: cover;
  margin-bottom: 15px;
`;

const PatientName = styled.h2`
  color: black;
  font-size: 2rem;
  font-weight: bold;
  text-align: center;
`;

function PatientPage() {
  const { patientId } = useParams();
  const [patient, setPatient] = useState(null);
  const [loading, setLoading] = useState(true);

  const [moodKinds, setMoodKinds] = useState([]);
  const [activityLevels, setActivityLevels] = useState([]);
  const [appetiteLevels, setAppetiteLevels] = useState([]);
  const [sleepLevels, setSleepLevels] = useState([]);

  const [selectedMoodKind, setSelectedMoodKind] = useState('');
  const [selectedActivityLevel, setSelectedActivityLevel] = useState('');
  const [selectedAppetiteLevel, setSelectedAppetiteLevel] = useState('');
  const [selectedSleepLevel, setSelectedSleepLevel] = useState('');

  const { t } = useTranslation();
  const navigate = useNavigate();

  const formatLabel = (str) => {
    return str
      .replace(/_/g, ' ')
      .replace(/\b\w/g, (char) => char.toUpperCase());
  };

  // Fetch patient details by patientId
  useEffect(() => {
    axios
      .get(`https://localhost:7066/api/Patient/ReadItem?id=${patientId}`)
      .then((res) => {
        setPatient(res.data.item);
        setLoading(false);
      })
      .catch((err) => {
        console.error('Error fetching patient:', err);
        setLoading(false);
      });
    console.log('Patient ID from URL:', patientId);
  }, [patientId]);

  // Fetch available MoodKinds, ActivityLevels, AppetiteLevels, SleepLevels
  const token = localStorage.getItem('jwtToken');
  console.log('Retrieved token from localStorage:', token);

  if (!token) {
    console.error('No token found in localStorage.');
    return;
  }

  useEffect(() => {
    const fetchData = async () => {
      try {
        const [
          moodResponse,
          activityResponse,
          appetiteResponse,
          sleepResponse,
        ] = await Promise.all([
          axios.get('https://localhost:7066/api/MoodKind/ReadItems', {
            headers: { Authorization: `Bearer ${token}` },
          }),
          axios.get('https://localhost:7066/api/ActivityLevel/ReadItems', {
            headers: { Authorization: `Bearer ${token}` },
          }),
          axios.get('https://localhost:7066/api/AppetiteLevel/ReadItems', {
            headers: { Authorization: `Bearer ${token}` },
          }),
          axios.get('https://localhost:7066/api/SleepLevel/ReadItems', {
            headers: { Authorization: `Bearer ${token}` },
          }),
        ]);

        setMoodKinds(moodResponse.data.pageItems || []);
        setActivityLevels(activityResponse.data.pageItems || []);
        setAppetiteLevels(appetiteResponse.data.pageItems || []);
        setSleepLevels(sleepResponse.data.pageItems || []);
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };
    fetchData();
  }, [token]);

  const handleSelectChange = (setter) => (e) => {
    setter(e.target.value);
  };

  const handleSave = async () => {
    try {
      const moodData = moodKinds.find((m) => m.moodKindId === selectedMoodKind);
      const activityData = activityLevels.find(
        (a) => a.activityLevelId === selectedActivityLevel
      );
      const appetiteData = appetiteLevels.find(
        (a) => a.appetiteLevelId === selectedAppetiteLevel
      );
      const sleepData = sleepLevels.find(
        (s) => s.sleepLevelId === selectedSleepLevel
      );

      const saveData = {
        patientId: patientId,
        moodKindId: selectedMoodKind,
        activityLevelId: selectedActivityLevel,
        appetiteLevelId: selectedAppetiteLevel,
        sleepLevelId: selectedSleepLevel,
        date: new Date().toISOString(),
        moodKind: moodData,
        activityLevel: activityData,
        appetiteLevel: appetiteData,
        sleepLevel: sleepData,
        moodRating: moodData?.rating,
        activityRating: activityData?.rating,
        appetiteRating: appetiteData?.rating,
        sleepRating: sleepData?.rating,
      };

      await axios.post('https://localhost:7066/api/Graph/CreateItem', {
        patientId: patientId,
        moodKindId: selectedMoodKind,
        activityLevelId: selectedActivityLevel,
        appetiteLevelId: selectedAppetiteLevel,
        sleepLevelId: selectedSleepLevel,
        date: new Date().toISOString(),
      });

      const existingData = JSON.parse(
        localStorage.getItem('graphData') || '[]'
      );
      localStorage.setItem(
        'graphData',
        JSON.stringify([...existingData, saveData])
      );

      navigate(`/review/${patientId}`, { state: saveData });
    } catch (err) {
      console.error('Error saving data:', err);
      alert('Could not save data');
    }
  };

  if (loading) return <div>{t('loading_patient')}</div>;
  if (!patient) return <div>{t('no_patient_found')}</div>;

  return (
    <>
      <GlobalStyle />
      <Link
        to="/"
        style={{ position: 'fixed', top: '15px', right: '15px', zIndex: '2' }}
      >
        <img src={logo1} alt="Logo" style={{ width: '150px' }} />
      </Link>

      <div>
        <PatientHeader>
          <PatientImage src={patient1} alt="Patient" />
          <PatientName>
            {patient.firstName} {patient.lastName}
          </PatientName>
        </PatientHeader>

        <PatientPageContainer>
          <FormGroup>
            <label htmlFor="moodkind-select">{t('select_moodkind')}</label>
            <Dropdown
              id="moodkind-select"
              value={selectedMoodKind}
              onChange={handleSelectChange(setSelectedMoodKind)}
            >
              <option value="">{t('choose_moodkind')}</option>
              {moodKinds.map((mood) => (
                <option key={mood.moodKindId} value={mood.moodKindId}>
                  {mood.label} {mood.rating}
                </option>
              ))}
            </Dropdown>
          </FormGroup>

          <FormGroup>
            <label htmlFor="activitylevel-select">
              {t('select_activitylevel')}
            </label>
            <Dropdown
              id="activitylevel-select"
              value={selectedActivityLevel}
              onChange={handleSelectChange(setSelectedActivityLevel)}
            >
              <option value="">{t('choose_activitylevel')}</option>
              {activityLevels.map((activity) => (
                <option
                  key={activity.activityLevelId}
                  value={activity.activityLevelId}
                >
                  {activity.label} {activity.rating}
                </option>
              ))}
            </Dropdown>
          </FormGroup>

          <FormGroup>
            <label htmlFor="appetitelevel-select">
              {t('select_appetitelevel')}
            </label>
            <Dropdown
              id="appetitelevel-select"
              value={selectedAppetiteLevel}
              onChange={handleSelectChange(setSelectedAppetiteLevel)}
            >
              <option value="">{t('choose_appetitelevel')}</option>
              {appetiteLevels.map((appetite) => (
                <option
                  key={appetite.appetiteLevelId}
                  value={appetite.appetiteLevelId}
                >
                  {appetite.label} {appetite.rating}
                </option>
              ))}
            </Dropdown>
          </FormGroup>

          <FormGroup>
            <label htmlFor="sleeplevel-select">{t('select_sleeplevel')}</label>
            <Dropdown
              id="sleeplevel-select"
              value={selectedSleepLevel}
              onChange={handleSelectChange(setSelectedSleepLevel)}
            >
              <option value="">{t('choose_sleeplevel')}</option>
              {sleepLevels.map((sleep) => (
                <option key={sleep.sleepLevelId} value={sleep.sleepLevelId}>
                  {sleep.label} {sleep.rating}
                </option>
              ))}
            </Dropdown>
          </FormGroup>

          <Button onClick={handleSave}>{t('save_button')}</Button>
        </PatientPageContainer>
      </div>
    </>
  );
}

export default PatientPage;
