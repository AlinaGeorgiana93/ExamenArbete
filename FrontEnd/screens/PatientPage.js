import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import axios from 'axios';

function PatientPage() {
  const { PatientId } = useParams(); // URL parameter
  const [patient, setPatient] = useState(null);
  const [loading, setLoading] = useState(true);

  const [moods, setMoods] = useState([]);
  const [activities, setActivities] = useState([]);
  const [appetites, setAppetites] = useState([]);
  const [sleeps, setSleeps] = useState([]);

  const [selectedMood, setSelectedMood] = useState('');
  const [selectedActivity, setSelectedActivity] = useState('');
  const [selectedAppetite, setSelectedAppetite] = useState('');
  const [selectedSleep, setSelectedSleep] = useState('');

  // Fetch patient info
  useEffect(() => {
    axios.get(`/api/Patient/ReadItem?id=${PatientId}`)
      .then(res => {
        setPatient(res.data.item);
        setLoading(false);
      })
      .catch(err => {
        console.error(err);
        setLoading(false);
      });
  }, [PatientId]);

  // Fetch dropdown options
  useEffect(() => {
    const fetchOptions = async () => {
      try {
        const [moodRes, activityRes, appetiteRes, sleepRes] = await Promise.all([
          axios.get('/api/Mood/ReadItems'),
          axios.get('/api/Activity/ReadItems'),
          axios.get('/api/Appetite/ReadItems'),
          axios.get('/api/Sleep/ReadItems'),
        ]);
        setMoods(moodRes.data.pageItems || []);
        setActivities(activityRes.data.pageItems || []);
        setAppetites(appetiteRes.data.pageItems || []);
        setSleeps(sleepRes.data.pageItems || []);
      } catch (err) {
        console.error("Error fetching dropdown options:", err);
      }
    };
    fetchOptions();
  }, []);

  const handleSave = async () => {
    try {
      await axios.post('/api/Graph/CreateItem', {
        patientId: PatientId,
        moodId: selectedMood,
        activityId: selectedActivity,
        appetiteId: selectedAppetite,
        sleepId: selectedSleep,
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
        <label>Mood:</label>
        <select value={selectedMood} onChange={e => setSelectedMood(e.target.value)} className="block border p-2 w-full">
          <option value="">Välj mood</option>
          {moods.map(m => (
            <option key={m.moodId} value={m.moodId}>{m.name}</option>
          ))}
        </select>
      </div>

      <div className="mb-4">
        <label>Activity:</label>
        <select value={selectedActivity} onChange={e => setSelectedActivity(e.target.value)} className="block border p-2 w-full">
          <option value="">Välj aktivitet</option>
          {activities.map(a => (
            <option key={a.activityId} value={a.activityId}>{a.name}</option>
          ))}
        </select>
      </div>

      <div className="mb-4">
        <label>Appetite:</label>
        <select value={selectedAppetite} onChange={e => setSelectedAppetite(e.target.value)} className="block border p-2 w-full">
          <option value="">Välj aptit</option>
          {appetites.map(ap => (
            <option key={ap.appetiteId} value={ap.appetiteId}>{ap.name}</option>
          ))}
        </select>
      </div>

      <div className="mb-4">
        <label>Sleep:</label>
        <select value={selectedSleep} onChange={e => setSelectedSleep(e.target.value)} className="block border p-2 w-full">
          <option value="">Välj sömn</option>
          {sleeps.map(sl => (
            <option key={sl.sleepId} value={sl.sleepId}>{sl.name}</option>
          ))}
        </select>
      </div>

      <button onClick={handleSave} className="bg-blue-600 text-white px-4 py-2 rounded">
        Spara Symptom
      </button>
    </div>
  );
}

export default PatientPage;
