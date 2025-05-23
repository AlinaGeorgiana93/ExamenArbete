import React, { useEffect, useState } from 'react';
import styled, { createGlobalStyle } from 'styled-components';
import { useParams, useNavigate, Link } from 'react-router-dom';
import axios from 'axios';
import { useTranslation } from 'react-i18next';
import logo1 from '../src/media/logo1.png';
import patient1 from '../src/media/patient1.jpg';
import LoadingSpinner from './LoadingSpinner';
import '../language/i18n.js';



const GlobalStyle = createGlobalStyle`
  * {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
  }
`;

const PatientPageContainer = styled.div`
  background-color: #F1F0E8;
  padding: 25px;
  border-radius: 16px;
  max-width: 450px;
  width: 100%;
  box-shadow: 0 8px 20px rgba(0, 0, 0, 0.2);
  position: relative;
  z-index: 1;
  backdrop-filter: blur(6px);
`;

const FormGroup = styled.div`
  margin-bottom: 17px;

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

const ButtonsContainer = styled.div`
  display: flex;
  flex-direction: column;
  gap: 10px;
  margin-top: 20px;
  width: 100%;
`;

const Button = styled.button`
  background: rgb(40, 136, 155);
  color: white;
  font-size: 0.9rem;
  padding: 8px 12px;
  border: none;
  border-radius: 20px;
  cursor: pointer;
  width: 100%;
  font-weight: 600;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
  transition: all 0.2s ease;
  min-height: 36px;
  display: flex;
  align-items: center;
  justify-content: center;

  &:hover {
    background-color: rgb(245, 97, 11);
    transform: scale(1.02);
  }
`;

const CommentsButton = styled(Link)`
  background: rgb(40, 136, 155);
  color: white;
  font-size: 0.9rem;
  padding: 8px 12px;
  border: none;
  border-radius: 20px;
  cursor: pointer;
  width: 100%;
  font-weight: 600;
  text-decoration: none;
  text-align: center;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
  transition: all 0.2s ease;
  min-height: 36px;
  display: flex;
  align-items: center;
  justify-content: center;

  &:hover {
    background-color: rgb(245, 97, 11);
    transform: scale(1.02);
  }
`;

const PatientHeader = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-bottom: 30px;
`;

const PatientImage = styled.img`
  width: 90px;
  height: 90px;
  border-radius: 50%;
  object-fit: cover;
  margin-bottom: 15px;
`;

const PatientName = styled.h2`
  color: black;
  font-size: 1,5rem;
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

  const token = localStorage.getItem('jwtToken');

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
  }, [patientId]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const headers = { Authorization: `Bearer ${token}` };
        const [moodResponse, activityResponse, appetiteResponse, sleepResponse] = await Promise.all([
          axios.get('https://localhost:7066/api/MoodKind/ReadItems', { headers }),
          axios.get('https://localhost:7066/api/ActivityLevel/ReadItems', { headers }),
          axios.get('https://localhost:7066/api/AppetiteLevel/ReadItems', { headers }),
          axios.get('https://localhost:7066/api/SleepLevel/ReadItems', { headers }),
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

  useEffect(() => {
    setSelectedMoodKind(localStorage.getItem(`selectedMoodKind-${patientId}`) || '');
    setSelectedActivityLevel(localStorage.getItem(`selectedActivityLevel-${patientId}`) || '');
    setSelectedAppetiteLevel(localStorage.getItem(`selectedAppetiteLevel-${patientId}`) || '');
    setSelectedSleepLevel(localStorage.getItem(`selectedSleepLevel-${patientId}`) || '');
  }, [patientId]);

  const handleSelectChange = (key, setter) => (e) => {
    const value = e.target.value;
    setter(value);
    localStorage.setItem(`${key}-${patientId}`, value);
  };

  const handleSave = async () => {
    try {
      const moodData = moodKinds.find((m) => m.moodKindId === selectedMoodKind);
      const activityData = activityLevels.find((a) => a.activityLevelId === selectedActivityLevel);
      const appetiteData = appetiteLevels.find((a) => a.appetiteLevelId === selectedAppetiteLevel);
      const sleepData = sleepLevels.find((s) => s.sleepLevelId === selectedSleepLevel);

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
        patientId,
        moodKindId: selectedMoodKind,
        activityLevelId: selectedActivityLevel,
        appetiteLevelId: selectedAppetiteLevel,
        sleepLevelId: selectedSleepLevel,
        date: new Date().toISOString(),
      });

      localStorage.removeItem(`selectedMoodKind-${patientId}`);
      localStorage.removeItem(`selectedActivityLevel-${patientId}`);
      localStorage.removeItem(`selectedAppetiteLevel-${patientId}`);
      localStorage.removeItem(`selectedSleepLevel-${patientId}`);

      navigate(`/review/${patientId}`, { state: saveData });
    } catch (err) {
      console.error('Error saving data:', err);
      alert('Could not save data');
    }
  };

  if (loading) return <LoadingSpinner />;
  if (!patient) return <div>{t('no_patient_found')}</div>;

  const sortedMoodKinds = [...moodKinds].sort((a, b) => a.rating - b.rating);
  const sortedActivityLevels = [...activityLevels].sort((a, b) => a.rating - b.rating);
  const sortedAppetiteLevels = [...appetiteLevels].sort((a, b) => a.rating - b.rating);
  const sortedSleepLevels = [...sleepLevels].sort((a, b) => a.rating - b.rating);

  return (
    <>
      <GlobalStyle />
      <div>
        <PatientHeader>
          <PatientImage src={patient1} alt="Patient" />
          <PatientName>{patient.firstName} {patient.lastName}</PatientName>
        </PatientHeader>

        <PatientPageContainer>
          {/* MoodKind */}
          <FormGroup>
            <label htmlFor="moodkind-select">{t('select_moodkind')}</label>
            <Dropdown
              id="moodkind-select"
              value={selectedMoodKind}
              onChange={handleSelectChange('selectedMoodKind', setSelectedMoodKind)}
            >
              <option value="">{t('choose_moodkind')}</option>
              {sortedMoodKinds.map((mood) => (
                <option key={mood.moodKindId} value={mood.moodKindId}>
                  {t(`moodkinds.${mood.name}`)} ({mood.rating})
                </option>
              ))}
            </Dropdown>
          </FormGroup>

          {/* ActivityLevel */}
          <FormGroup>
            <label htmlFor="activitylevel-select">{t('select_activitylevel')}</label>
            <Dropdown
              id="activitylevel-select"
              value={selectedActivityLevel}
              onChange={handleSelectChange('selectedActivityLevel', setSelectedActivityLevel)}
            >
              <option value="">{t('choose_activitylevel')}</option>
              {sortedActivityLevels.map((activity) => (
                <option key={activity.activityLevelId} value={activity.activityLevelId}>
                  {t(`activities.${activity.name}`)} ({activity.rating})
                </option>
              ))}
            </Dropdown>
          </FormGroup>

          {/* AppetiteLevel */}
          <FormGroup>
            <label htmlFor="appetitelevel-select">{t('select_appetitelevel')}</label>
            <Dropdown
              id="appetitelevel-select"
              value={selectedAppetiteLevel}
              onChange={handleSelectChange('selectedAppetiteLevel', setSelectedAppetiteLevel)}
            >
              <option value="">{t('choose_appetitelevel')}</option>
              {sortedAppetiteLevels.map((appetite) => (
                <option key={appetite.appetiteLevelId} value={appetite.appetiteLevelId}>
                  {t(`appetitelevels.${appetite.name}`)} ({appetite.rating})
                </option>
              ))}
            </Dropdown>
          </FormGroup>

          {/* SleepLevel */}
          <FormGroup>
            <label htmlFor="sleeplevel-select">{t('select_sleeplevel')}</label>
            <Dropdown
              id="sleeplevel-select"
              value={selectedSleepLevel}
              onChange={handleSelectChange('selectedSleepLevel', setSelectedSleepLevel)}
            >
              <option value="">{t('choose_sleeplevel')}</option>
              {sortedSleepLevels.map((sleep) => (
                <option key={sleep.sleepLevelId} value={sleep.sleepLevelId}>
                  {t(`sleeplevels.${sleep.name}`)} ({sleep.rating})
                </option>
              ))}
            </Dropdown>
          </FormGroup>


          <ButtonsContainer>
            <Button onClick={handleSave}>{t('save_button')}</Button>
            <CommentsButton to={`/comments/${patientId}`} state={{ from: 'patient' }}>
              {patient
                ? t('Add a comment for', { name: patient.firstName })
                : t('Add Or View Comments')}
            </CommentsButton>
          </ButtonsContainer>
        </PatientPageContainer>

      </div>
    </>
  );
}

export default PatientPage;
