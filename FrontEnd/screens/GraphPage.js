import React, { useEffect, useState } from 'react';
import axios from 'axios';
import {Link, useParams} from 'react-router-dom';
import {LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer
} from 'recharts';
import styled, { createGlobalStyle } from 'styled-components'; // Correct import
import logo1 from '../src/media/logo1.png';
import '../src/index.css'

function GraphPage() {
  const { moodKindId } = useParams(); // From route /graph/:moodKindId
  const [graphData, setGraphData] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const token = localStorage.getItem("token");

    axios.get(`https://localhost:7066/api/Graph/ReadItems?moodKindId=${moodKindId}`, {
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
      }
      setLoading(false);
    })
    .catch(error => {
      console.error("Error fetching graph data:", error);
      setLoading(false);
    });
  }, [moodKindId]);

  if (loading) return <div>Laddar grafer...</div>;

  return (
    <>
      <GlobalStyle />
      <Link to="/" style={{ position: 'fixed', top: '15px', right: '15px', zIndex: '2' }}>
        <img src={logo1} alt="Logo" style={{ width: '150px' }} />
      </Link>
    
      <GraphContainer>
        <h2 className="text-2xl font-bold mb-6">Symptomgraf</h2>
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
      </GraphContainer>
    </>
  );
}
export default GraphPage;
