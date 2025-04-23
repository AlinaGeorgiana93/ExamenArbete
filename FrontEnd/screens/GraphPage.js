import React, { useEffect, useState } from 'react';
import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer } from 'recharts';
import styled from 'styled-components';
import { useParams, Link } from 'react-router-dom';
import axios from 'axios';
import logo1 from '../src/media/logo1.png';
import patient1 from '../src/media/patient1.jpg';

const GraphContainer = styled.div`
  background-color: #ffffffee;
  padding: 30px;
  border-radius: 16px;
  max-width: 800px;
  width: 100%;
  box-shadow: 0 8px 20px rgba(0, 0, 0, 0.2);
  margin: 20px auto;
`;

function GraphPage() {
  const { id } = useParams();
  const [graphData, setGraphData] = useState([]);
  const [patientInfo, setPatientInfo] = useState(null);
  const [loading, setLoading] = useState(true);
  const [errorMsg, setErrorMsg] = useState("");
  const patient1 = '../src/media/patient1.jpg'; // Make sure this path is correct

  useEffect(() => {
    const fetchData = async () => {
      try {
        // Fetch patient info first
        const patientResponse = await axios.get(`https://localhost:7066/api/Patient/ReadItem?id=${id}`);
        setPatientInfo(patientResponse.data.item);

        // Then fetch graph data
        const localData = JSON.parse(localStorage.getItem('graphData') || '[]');
        const patientData = localData.filter(item => item.patientId === id);
        
        if (patientData.length > 0) {
          setGraphData(patientData);
        } else {
          const response = await axios.get(`https://localhost:7066/api/Graph/ReadItems?patientId=${id}`);
          if (response.data?.pageItems?.length > 0) {
            setGraphData(response.data.pageItems);
          } else {
            setErrorMsg("No data found for this patient.");
          }
        }
      } catch (error) {
        console.error("Error fetching data:", error);
        setErrorMsg("Could not fetch data. Please try again.");
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [id]);


 // Format data for graph
 const formattedData = graphData.map(item => ({
  date: new Date(item.date).toLocaleDateString(),
  moodRating: item.moodRating || 0,
  activityRating: item.activityRating || 0,
  appetiteRating: item.appetiteRating || 0,
  sleepRating: item.sleepRating || 0
}));

if (loading) return <div style={{ padding: "2rem" }}>Loading graphs...</div>;
if (errorMsg) return <div style={{ padding: "2rem", color: "red" }}>{errorMsg}</div>;

return (
  <>
    <Link to="/" style={{ position: 'fixed', top: '15px', right: '15px', zIndex: '2' }}>
      <img src={logo1} alt="Logo" style={{ width: '150px' }} />
    </Link>

    <GraphContainer>
      {/* Patient Info Header */}
      {patientInfo && (
        <div style={{ 
          display: 'flex', 
          alignItems: 'center', 
          marginBottom: '20px',
          padding: '15px',
          backgroundColor: '#f5f5f5',
          borderRadius: '8px'
        }}>
          <img 
            src={patient1} 
            alt="Patient" 
            style={{ 
              width: '80px', 
              height: '80px', 
              borderRadius: '50%', 
              objectFit: 'cover',
              marginRight: '20px'
            }} 
          />
          <div>
            <h2 style={{ margin: 0, color: '#125358' }}>
              {patientInfo.firstName} {patientInfo.lastName}
            </h2>
            <p style={{ margin: '5px 0 0 0', color: '#666' }}>
              Patient ID: {id}
            </p>
          </div>
        </div>
      )}

      <h2 style={{ textAlign: 'center', marginBottom: '20px', color: '#125358' }}>
        Symptom Progress Graph
      </h2>
      
      {formattedData.length > 0 ? (
        <ResponsiveContainer width="100%" height={400}>
          <LineChart data={formattedData}>
            <CartesianGrid strokeDasharray="3 3" />
            <XAxis dataKey="date" />
            <YAxis domain={[0, 10]} />
            <Tooltip />
            <Legend />
            <Line 
              type="monotone" 
              dataKey="moodRating" 
              stroke="#8884d8" 
              name="Mood" 
              activeDot={{ r: 8 }} 
            />
            <Line 
              type="monotone" 
              dataKey="activityRating" 
              stroke="#82ca9d" 
              name="Activity" 
            />
            <Line 
              type="monotone" 
              dataKey="appetiteRating" 
              stroke="#ffc658" 
              name="Appetite" 
            />
            <Line 
              type="monotone" 
              dataKey="sleepRating" 
              stroke="#ff7300" 
              name="Sleep" 
            />
          </LineChart>
        </ResponsiveContainer>
      ) : (
        <div style={{ textAlign: 'center' }}>No data available to display.</div>
      )}
    </GraphContainer>
  </>
);
}
export default GraphPage;