import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import axios from 'axios';

function PatientPage() {
  const{ patientId } = useParams();
  const [patient, setPatient] = useState(null);
  const [loading, setLoading] = useState(true);

  const [moodKinds, setMoodKinds] = useState([]);
  const [selectedMoodKind, setSelectedMoodKind] = useState('');

  useEffect(() => {
    axios.get(`https://localhost:7066/api/Patient/ReadItem?id=${patientId}`)
      .then(res => {
        setPatient(res.data.item);
        setLoading(false);
      })
      .catch(err => {
        console.error(err);
        setLoading(false);
      });
    console.log("Patient ID from URL:", patientId);
  }, [patientId]);

  useEffect(() => {
    axios.get('https://localhost:7066/api/MoodKind/ReadItems')
      .then(response => {
        if (response.data && response.data.pageItems) {
          setMoodKinds(response.data.pageItems);
        }
      })
      .catch(error => {
        console.error("Error fetching MoodKinds:", error);
        if (error.response) {
          console.error('Status:', error.response.status); // like 404, 500
          console.error('Response:', error.response.data); // backend error message
        } else if (error.request) {
          console.error('Request made but no response received:', error.request);
        } else {
          console.error('General Error Message:', error.message);
        }
      });
  }, []);
  

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
      console.error(err);
      alert("Kunde inte spara symptom");
    }
  };
  

  if (loading) return <div>Laddar patient...</div>;
  if (!patient) return <div>Ingen patient hittad.</div>;

  return (
  <div className="p-4">
    <h1 className="text-2xl font-bold mb-4">
      {patient.firstName} {patient.lastName}
    </h1>

    <div className="mb-4">
      <label>MoodKind:</label>
      <select
        value={selectedMoodKind}
        onChange={e => setSelectedMoodKind(e.target.value)}
        className="block border p-2 w-full"
      >
        <option value="">Välj MoodKind</option>
        {moodKinds.map(m => (
          <option key={m.moodKindId} value={m.moodKindId}>
            {m.label}
          </option>
        ))}
      </select>
    </div>

    <button
      onClick={handleSave}
      className="bg-blue-600 text-white px-4 py-2 rounded"
    >
      Spara Symptom
    </button>
  </div>
);
}

export default PatientPage;
