import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import axios from 'axios';

function PatientPage() {
  const { PatientId } = useParams();
  const [patient, setPatient] = useState(null);
  const [loading, setLoading] = useState(true);

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

  const handleChange = (e) => {
    setPatient({ ...patient, [e.target.name]: e.target.value });
  };

  const handleSave = async () => {
    try {
      await axios.put(`/api/Patient/UpdateItem/${patient.PatientId}`, patient);
      alert("Patient uppdaterad!");
    } catch (err) {
      console.error(err);
      alert("Kunde inte spara");
    }
  };

  if (loading) return <div>Laddar patient...</div>;
  if (!patient) return <div>Ingen patient hittad.</div>;

  return (
    <div className="p-4">
      <h1 className="text-2xl font-bold mb-4">Patientprofil</h1>
      <input
        className="border p-2 mb-2 block w-full"
        name="firstName"
        value={patient.firstName || ''}
        onChange={handleChange}
        placeholder="Förnamn"
      />
      <input
        className="border p-2 mb-2 block w-full"
        name="lastName"
        value={patient.lastName || ''}
        onChange={handleChange}
        placeholder="Efternamn"
      />
      {/* Lägg till fler fält vid behov */}
      <button
        className="bg-blue-600 text-white px-4 py-2 rounded"
        onClick={handleSave}
      >
        Spara
      </button>
    </div>
  );
}

export default PatientPage;
