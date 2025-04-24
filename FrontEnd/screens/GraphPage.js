import React, { useEffect, useState } from 'react';
import {
  LineChart, BarChart, AreaChart, PieChart, RadarChart, ScatterChart,
  Line, Bar, Area, Pie, Radar, Scatter, XAxis, YAxis, CartesianGrid,
  Tooltip, Legend, ResponsiveContainer, PolarGrid, PolarAngleAxis, 
  PolarRadiusAxis, Cell
} from 'recharts';
import styled from 'styled-components';
import { useParams, Link } from 'react-router-dom';
import axios from 'axios';
import logo1 from '../src/media/logo1.png';
import patient1 from '../src/media/patient1.jpg';

const GraphContainer = styled.div`
  background-color: #ffffffee;
  padding: 30px;
  border-radius: 16px;
  max-width: 1000px;
  width: 100%;
  box-shadow: 0 8px 20px rgba(0, 0, 0, 0.2);
  margin: 20px auto;
`;

const ChartControls = styled.div`
  display: flex;
  flex-wrap: wrap;
  gap: 15px;
  margin-bottom: 20px;
  justify-content: center;
  align-items: center;
`;

const ChartTypeButton = styled.button`
  padding: 8px 16px;
  border: none;
  border-radius: 20px;
  background-color: ${props => props.active ? '#125358' : '#e0e0e0'};
  color: ${props => props.active ? 'white' : '#333'};
  cursor: pointer;
  font-size: 14px;
  transition: all 0.3s ease;

  &:hover {
    background-color: ${props => props.active ? '#0e4246' : '#d0d0d0'};
  }
`;

const MetricToggle = styled.label`
  display: flex;
  align-items: center;
  gap: 5px;
  cursor: pointer;
  padding: 5px 10px;
  border-radius: 15px;
  background-color: ${props => props.active ? '#12535820' : 'transparent'};
`;

const COLORS = ['#8884d8', '#82ca9d', '#ffc658', '#ff7300'];

const chartTypes = [
  { key: 'line', name: 'Line Chart' },
  { key: 'bar', name: 'Bar Chart' },
  { key: 'area', name: 'Area Chart' },
  { key: 'pie', name: 'Pie Chart' },
  { key: 'radar', name: 'Radar Chart' },
  { key: 'scatter', name: 'Scatter Chart' }
];

const metrics = [
  { key: 'moodRating', name: 'Mood', color: COLORS[0] },
  { key: 'activityRating', name: 'Activity', color: COLORS[1] },
  { key: 'appetiteRating', name: 'Appetite', color: COLORS[2] },
  { key: 'sleepRating', name: 'Sleep', color: COLORS[3] }
];

function GraphPage() {
  const { id } = useParams();
  const [graphData, setGraphData] = useState([]);
  const [patientInfo, setPatientInfo] = useState(null);
  const [loading, setLoading] = useState(true);
  const [errorMsg, setErrorMsg] = useState("");
  const [chartType, setChartType] = useState('line');
  const [activeMetrics, setActiveMetrics] = useState({
    moodRating: true,
    activityRating: true,
    appetiteRating: true,
    sleepRating: true
  });

  useEffect(() => {
    const fetchData = async () => {
      try {
        const patientResponse = await axios.get(`https://localhost:7066/api/Patient/ReadItem?id=${id}`);
        setPatientInfo(patientResponse.data.item);

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

  const formattedData = graphData.map(item => ({
    date: new Date(item.date).toLocaleDateString(),
    ...item
  }));

  const toggleMetric = (metric) => {
    setActiveMetrics(prev => ({
      ...prev,
      [metric]: !prev[metric]
    }));
  };

  const renderChart = () => {
    const activeDataKeys = metrics.filter(m => activeMetrics[m.key]).map(m => m.key);
    
    if (formattedData.length === 0) {
      return <div style={{ textAlign: 'center' }}>No data available to display.</div>;
    }

    switch (chartType) {
      case 'line':
        return (
          <LineChart data={formattedData}>
            <CartesianGrid strokeDasharray="3 3" />
            <XAxis dataKey="date" />
            <YAxis domain={[0, 10]} />
            <Tooltip />
            <Legend />
            {metrics.map(metric => (
              activeMetrics[metric.key] && (
                <Line
                  key={metric.key}
                  type="monotone"
                  dataKey={metric.key}
                  name={metric.name}
                  stroke={metric.color}
                  activeDot={{ r: 8 }}
                />
              )
            ))}
          </LineChart>
        );
      case 'bar':
        return (
          <BarChart data={formattedData}>
            <CartesianGrid strokeDasharray="3 3" />
            <XAxis dataKey="date" />
            <YAxis domain={[0, 10]} />
            <Tooltip />
            <Legend />
            {metrics.map(metric => (
              activeMetrics[metric.key] && (
                <Bar
                  key={metric.key}
                  dataKey={metric.key}
                  name={metric.name}
                  fill={metric.color}
                />
              )
            ))}
          </BarChart>
        );
      case 'area':
        return (
          <AreaChart data={formattedData}>
            <CartesianGrid strokeDasharray="3 3" />
            <XAxis dataKey="date" />
            <YAxis domain={[0, 10]} />
            <Tooltip />
            <Legend />
            {metrics.map(metric => (
              activeMetrics[metric.key] && (
                <Area
                  key={metric.key}
                  type="monotone"
                  dataKey={metric.key}
                  name={metric.name}
                  stroke={metric.color}
                  fill={metric.color}
                  fillOpacity={0.4}
                />
              )
            ))}
          </AreaChart>
        );
      case 'pie':
        // Pie chart shows average values for all metrics
        const pieData = metrics.map(metric => {
          const values = formattedData.map(d => d[metric.key]).filter(v => v !== undefined);
          const average = values.length > 0 ? 
            values.reduce((a, b) => a + b, 0) / values.length : 0;
          return {
            name: metric.name,
            value: average,
            color: metric.color
          };
        });
        
        return (
          <PieChart>
            <Pie
              data={pieData}
              cx="50%"
              cy="50%"
              labelLine={false}
              outerRadius={150}
              fill="#8884d8"
              dataKey="value"
              nameKey="name"
              label={({ name, percent }) => `${name}: ${(percent * 100).toFixed(0)}%`}
            >
              {pieData.map((entry, index) => (
                <Cell key={`cell-${index}`} fill={entry.color} />
              ))}
            </Pie>
            <Tooltip />
            <Legend />
          </PieChart>
        );
      case 'radar':
        // Radar chart shows all metrics
        const radarData = metrics.map(metric => {
          const values = formattedData.map(d => d[metric.key]).filter(v => v !== undefined);
          const average = values.length > 0 ? 
            values.reduce((a, b) => a + b, 0) / values.length : 0;
          return {
            subject: metric.name,
            A: average,
            fullMark: 10,
            color: metric.color
          };
        });
        
        return (
          <RadarChart outerRadius={150} width={600} height={400} data={radarData}>
            <PolarGrid />
            <PolarAngleAxis dataKey="subject" />
            <PolarRadiusAxis angle={30} domain={[0, 10]} />
            <Radar
              name="Average"
              dataKey="A"
              stroke="#8884d8"
              fill="#8884d8"
              fillOpacity={0.6}
            />
            <Legend />
            <Tooltip />
          </RadarChart>
        );
      case 'scatter':
        // Scatter chart shows all metrics
        const scatterData = formattedData.map(dataPoint => {
          const point = { date: dataPoint.date };
          metrics.forEach(metric => {
            point[metric.key] = dataPoint[metric.key];
          });
          return point;
        });
        
        return (
          <ScatterChart
            width={600}
            height={400}
            margin={{ top: 20, right: 20, bottom: 20, left: 20 }}
            data={scatterData}
          >
            <CartesianGrid />
            <XAxis dataKey="date" name="Date" />
            <YAxis type="number" name="Rating" domain={[0, 10]} />
            <Tooltip cursor={{ strokeDasharray: '3 3' }} />
            <Legend />
            {metrics.map(metric => (
              <Scatter
                key={metric.key}
                name={metric.name}
                dataKey={metric.key}
                fill={metric.color}
              />
            ))}
          </ScatterChart>
        );
      default:
        return null;
    }
  };

  if (loading) return <div style={{ padding: "2rem" }}>Loading graphs...</div>;
  if (errorMsg) return <div style={{ padding: "2rem", color: "red" }}>{errorMsg}</div>;

  return (
    <>
      <Link to="/" style={{ position: 'fixed', top: '15px', right: '15px', zIndex: '2' }}>
        <img src={logo1} alt="Logo" style={{ width: '150px' }} />
      </Link>

      <GraphContainer>
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
          Symptom Progress Visualization
        </h2>
        
        <ChartControls>
          <div style={{ display: 'flex', gap: '10px', flexWrap: 'wrap' }}>
            {chartTypes.map(type => (
              <ChartTypeButton
                key={type.key}
                active={chartType === type.key}
                onClick={() => setChartType(type.key)}
              >
                {type.name}
              </ChartTypeButton>
            ))}
          </div>
          
          <div style={{ display: 'flex', gap: '10px', flexWrap: 'wrap' }}>
            {metrics.map(metric => (
              <MetricToggle 
                key={metric.key}
                active={activeMetrics[metric.key]}
                onClick={() => toggleMetric(metric.key)}
              >
                <input
                  type="checkbox"
                  checked={activeMetrics[metric.key]}
                  onChange={() => {}}
                  style={{ accentColor: metric.color }}
                />
                {metric.name}
              </MetricToggle>
            ))}
          </div>
        </ChartControls>

        <div style={{ width: '100%', height: '500px' }}>
          <ResponsiveContainer width="100%" height="100%">
            {renderChart()}
          </ResponsiveContainer>
        </div>
      </GraphContainer>
    </>
  );
}

export default GraphPage;