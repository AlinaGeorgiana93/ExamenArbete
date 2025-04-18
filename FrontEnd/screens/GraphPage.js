import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Link, useParams } from 'react-router-dom';
import {
  LineChart,
  Line,
  XAxis,
  YAxis,
  CartesianGrid,
  Tooltip,
  Legend,
  ResponsiveContainer
} from 'recharts';
import styled, { createGlobalStyle } from 'styled-components';
import logo1 from '../src/media/logo1.png';
import '../src/index.css';

const GlobalStyle = createGlobalStyle`
  body {
    margin: 0;
    font-family: 'Arial', sans-serif;
  }
`;

const GraphContainer = styled.div`
  padding: 2rem;
  max-width: 900px;
  margin: 0 auto;
`;

function GraphPage() {
  const { id } = useParams(); // Fetching the dynamic 'id' from the URL
  const [graphData, setGraphData] = useState([]);
  const [loading, setLoading] = useState(true);
  const [errorMsg, setErrorMsg] = useState("");

  useEffect(() => {
    const token = localStorage.getItem("token");

    if (!token) {
      setErrorMsg("No valid token found. Please log in.");
      setLoading(false);
      return;
    }

    // Correcting the axios call to use the patientId (id) from the URL
    axios.get(`https://localhost:7066/api/Graph/ReadItem?id=${id}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      }
    })
    .then(response => {
      if (response.data && response.data.pageItems) {
        const formatted = response.data.pageItems.map(item => ({
          date: item.date.split('T')[0],
          moodLevel: item.moodLevel,
          activityLevel: item.activityLevel,
          appetiteLevel: item.appetiteLevel,
          sleepLevel: item.sleepLevel,
        }));
        setGraphData(formatted);
      } else {
        setErrorMsg("Inga data hittades för den valda perioden.");
      }
      setLoading(false);
    })
    .catch(error => {
      console.error("Error fetching graph data:", error);
      setErrorMsg("Kunde inte hämta grafdata. Försök igen senare.");
      setLoading(false);
    });
  }, [id]); // Refetch data whenever 'id' changes

  if (loading) return <div style={{ padding: "2rem" }}>Laddar grafer...</div>;
  if (errorMsg) return <div style={{ padding: "2rem", color: "red" }}>{errorMsg}</div>;

  return (
    <>
      <GlobalStyle />
      <Link to="/" style={{ position: 'fixed', top: '15px', right: '15px', zIndex: '2' }}>
        <img src={logo1} alt="Logo" style={{ width: '150px' }} />
      </Link>

      <GraphContainer>
        <h2 className="text-2xl font-bold mb-6">Symptomgraf</h2>
        {graphData.length > 0 ? (
          <ResponsiveContainer width="100%" height={400}>
            <LineChart data={graphData}>
              <CartesianGrid strokeDasharray="3 3" />
              <XAxis dataKey="date" />
              <YAxis domain={[0, 10]} />
              <Tooltip />
              <Legend />
              <Line type="monotone" dataKey="moodLevel" stroke="#8884d8" name="Humör" />
              <Line type="monotone" dataKey="activityLevel" stroke="#82ca9d" name="Aktivitet" />
              <Line type="monotone" dataKey="appetiteLevel" stroke="#ffc658" name="Aptit" />
              <Line type="monotone" dataKey="sleepLevel" stroke="#ff7300" name="Sömn" />
            </LineChart>
          </ResponsiveContainer>
        ) : (
          <div>Ingen data att visa.</div>
        )}
      </GraphContainer>
    </>
  );
}

export default GraphPage;
