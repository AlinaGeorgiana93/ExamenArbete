import React, { useEffect, useState } from 'react';
import { useSearchParams, Link } from 'react-router-dom';
import axios from "axios";
import { useTranslation } from 'react-i18next';
import styled, { createGlobalStyle } from 'styled-components';
import patient1 from '../src/media/patient1.jpg';

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

const PatientPage = () => {
  const { t } = useTranslation();
  const [searchParams] = useSearchParams();
  const id = searchParams.get("id");

  const [formData, setFormData] = useState({
    patientId: '',
    graphId: '',
    graph: '',
    appetites: '',
    moods: '',
    activities: '',
    sleeps: '',
    firstName: '',
    lastName: '',
    personalNumber: '',
  });

  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (!id) return;

    const fetchData = async () => {
      try {
        const [patientRes, moodRes, sleepRes, activityRes, appetiteRes] = await Promise.all([
          axios.get(`/api/patient?id=${id}`),
          axios.get(`/api/Mood?patientId=${id}`),
          axios.get(`/api/Sleep?patientId=${id}`),
          axios.get(`/api/Activity?patientId=${id}`),
          axios.get(`/api/Appetite?patientId=${id}`),
        ]);

        const patient = patientRes.data.item;
        const latestMood = moodRes.data.at(-1)?.value || '';
        const latestSleep = sleepRes.data.at(-1)?.value || '';
        const latestActivity = activityRes.data.at(-1)?.value || '';
        const latestAppetite = appetiteRes.data.at(-1)?.value || '';

        setFormData({
          patientId: patient.patientId,
          graphId: patient.graphId,
          graph: patient.graph,
          firstName: patient.firstName,
          lastName: patient.lastName,
          personalNumber: patient.personalNumber,
          moods: latestMood,
          sleeps: latestSleep,
          activities: latestActivity,
          appetites: latestAppetite,
        });
      } catch (err) {
        console.error("Failed to fetch data", err);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [id]);

  const handleChange = (field) => (e) => {
    setFormData((prev) => ({
      ...prev,
      [field]: e.target.value
    }));
  };

  const handleSave = async () => {
    const { patientId, moods, sleeps, activities, appetites } = formData;

    if (!patientId || !moods || !sleeps || !activities || !appetites) {
      alert("Please fill all fields.");
      return;
    }

    try {
      await axios.post('/api/saveData', {
        patientId,
        moods,
        sleeps,
        activities,
        appetites
      });

      alert("Patient data saved successfully!");
    } catch (error) {
      console.error("Error saving data", error);
      alert("Failed to save data.");
    }
  };

  if (loading) return <div>Loading patient data...</div>;

  return (
    <>
      <GlobalStyle />
      <PageContainer>
        <Header>
          <PatientImage src={patient1} alt="Patient" />
          <h2>
            {formData.firstName} {formData.lastName}
          </h2>
        </Header>

        <Label>Mood</Label>
        <Select value={formData.moods} onChange={handleChange("moods")}>
          <option value="">Select Mood</option>
          {Array.from({ length: 11 }, (_, i) => (
            <option key={i} value={i}>{i}</option>
          ))}
        </Select>

        <Label>Sleep</Label>
        <Select value={formData.sleeps} onChange={handleChange("sleeps")}>
          <option value="">Select Sleep</option>
          {Array.from({ length: 11 }, (_, i) => (
            <option key={i} value={i}>{i}</option>
          ))}
        </Select>

        <Label>Activity</Label>
        <Select value={formData.activities} onChange={handleChange("activities")}>
          <option value="">Select Activity</option>
          {Array.from({ length: 11 }, (_, i) => (
            <option key={i} value={i}>{i}</option>
          ))}
        </Select>

        <Label>Appetite</Label>
        <Select value={formData.appetites} onChange={handleChange("appetites")}>
          <option value="">Select Appetite</option>
          {Array.from({ length: 11 }, (_, i) => (
            <option key={i} value={i}>{i}</option>
          ))}
        </Select>

        <Button onClick={handleSave}>Save</Button>

        <br /><br />
        <Link to="/">← Back to Home</Link>
      </PageContainer>
    </>
  );
};

export default PatientPage;
