import React, { useEffect, useState, useMemo } from 'react';
import {
  LineChart, BarChart, PieChart, ComposedChart,
  Line, Bar, Pie, XAxis, YAxis, CartesianGrid,
  Tooltip, Legend, ResponsiveContainer, Cell
} from 'recharts';
import styled, { createGlobalStyle } from 'styled-components';
import { useParams, Link, useLocation } from 'react-router-dom';
import axios from 'axios';
import logo1 from '../src/media/logo1.png';
import patient1 from '../src/media/patient1.jpg';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';

// Update the body styles in GlobalStyle
const GlobalStyle = createGlobalStyle`
  * {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
    font-size: 1rem;
  }

  body {
    font-family: 'Helvetica Neue', Arial, sans-serif;
    background: linear-gradient(135deg, #e0f7f9, #cceae7, #b2dfdb);
    min-height: 100vh;
    color: #fff;
    position: relative;
    padding: 20px 0;
    width: 100vw;  // Added to ensure full viewport width
    overflow-x: hidden;  // Prevent horizontal scrolling

    /* Mobile (320px – 480px) */
    @media (max-width: 480px) {
      padding: 10px 0;
      align-items: center;
    }
  }
`;

// Update the GraphContainer styled component
const GraphContainer = styled.div`
  background-color: #ffffffee;
  padding: 30px;
  border-radius: 16px;
  max-width: 1200px;  // Increased from 1000px
  width: 95%;  // Changed from 100%
  box-shadow: 0 8px 20px rgba(0, 0, 0, 0.2);
  margin: 20px auto;
  transition: all 0.3s ease;
  
  &:hover {
    box-shadow: 0 12px 24px rgba(0, 0, 0, 0.2);
    transform: translateY(-2px);
  }

  /* Tablet (481px – 1024px) */
  @media (min-width: 481px) and (max-width: 1024px) {
    width: 98%;
    padding: 25px;
  }

  /* Desktop (1025px and up) */
  @media (min-width: 1025px) {
    width: 90%;
    padding: 40px;
    max-width: 1300px;  // Increased from 1100px
  }

  /* Mobile (320px – 480px) */
  @media (max-width: 480px) {
    width: 98%;
    padding: 15px;
    margin: 10px auto;
    border-radius: 12px;
  }
`;

const ChartControls = styled.div`
  display: flex;
  flex-wrap: wrap;
  gap: 15px;
  margin-bottom: 20px;
  justify-content: center;
  align-items: center;
  width: 100%;

  /* Tablet (481px – 1024px) */
  @media (min-width: 481px) and (max-width: 1024px) {
    gap: 10px;
  }

  /* Mobile (320px – 480px) */
  @media (max-width: 480px) {
    gap: 8px;
    margin-bottom: 15px;
    flex-direction: column;
  }
`;

const ChartTypeButton = styled.button`
  padding: 8px 16px;
  border: none;
  border-radius: 20px;
  background-color: ${(props) => (props.active ? '#125358' : '#e0e0e0')};
  color: ${(props) => (props.active ? 'white' : '#333')};
  cursor: pointer;
  font-size: 14px;
  transition: all 0.3s ease;

  &:hover {
    background-color: ${(props) => (props.active ? '#0e4246' : '#d0d0d0')};
  }

  /* Desktop (1025px and up) */
  @media (min-width: 1025px) {
    padding: 10px 20px;
    font-size: 15px;
  }

  /* Mobile (320px – 480px) */
  @media (max-width: 480px) {
    padding: 6px 12px;
    font-size: 12px;
    border-radius: 15px;
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

  /* Mobile (320px – 480px) */
  @media (max-width: 480px) {
    padding: 4px 8px;
    font-size: 12px;
    gap: 3px;
  }
`;

const TimeRangeButton = styled.button`
  padding: 8px 16px;
  border: none;
  border-radius: 20px;
  background-color: ${props => props.active ? '#125358' : '#e0e0e0'};
  color: ${props => props.active ? 'white' : '#333'};
  cursor: pointer;
  font-size: 14px;
  transition: all 0.3s ease;
  text-align: center;
  white-space: nowrap;

  &:hover {
    background-color: ${props => props.active ? '#0e4246' : '#d0d0d0'};
  }

  /* Desktop (1025px and up) */
  @media (min-width: 1025px) {
    padding: 10px 20px;
    font-size: 15px;
  }

  /* Mobile (320px – 480px) */
  @media (max-width: 480px) {
    padding: 6px 12px;
    font-size: 12px;
    border-radius: 15px;
    width: auto;
  }
`;

// Update the ChartWrapper styled component
const ChartWrapper = styled.div`
  display: flex;
  width: 100%;
  height: 500px;
  transition: all 0.3s ease;
  
  /* Desktop */
  @media (min-width: 1025px) {
    height: 600px;  // Increased from 550px
  }
  
  /* Tablet */
  @media (max-width: 1024px) {
    height: 500px;  // Increased from 450px
  }
  
  /* Mobile */
  @media (max-width: 480px) {
    height: 400px;  // Increased from 350px
    flex-direction: column;
  }
  
  .recharts-wrapper {
    transition: all 0.5s ease;
    flex: 1;
  }
  
  &:hover .recharts-wrapper {
    filter: drop-shadow(0 4px 8px rgba(0,0,0,0.1));
  }
`;


const TimePeriodContainer = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: flex-start;
  padding: 0 15px;
  gap: 10px;
  width: 120px;
  
  @media (max-width: 480px) {
    flex-direction: row;
    width: 100%;
    padding: 15px 0 0 0;
    justify-content: center;
  }
`;

const COLORS = ['#8884d8', '#82ca9d', '#ffc658', '#ff7300'];

const chartTypes = [
  { key: 'bar', name: 'Bar Chart' },
  { key: 'line', name: 'Line Chart' },
  { key: 'pie', name: 'Pie Chart' }
];

const timeRanges = [
  { key: 'day', name: 'Daily' },
  { key: 'month', name: 'Monthly' },
  { key: 'year', name: 'Annual' }
];

const metrics = [
  { key: 'moodRating', name: 'Mood', color: COLORS[0] },
  { key: 'activityRating', name: 'Activity', color: COLORS[1] },
  { key: 'appetiteRating', name: 'Appetite', color: COLORS[2] },
  { key: 'sleepRating', name: 'Sleep', color: COLORS[3] },
];

const calculateDailyAverages = (data) => {
  const dateMap = {};

  data.forEach(item => {
    const dateKey = new Date(item.date).toLocaleDateString();
    if (!dateMap[dateKey]) {
      dateMap[dateKey] = {
        date: dateKey,
        count: 1,
        moodRating: item.moodRating || 0,
        activityRating: item.activityRating || 0,
        appetiteRating: item.appetiteRating || 0,
        sleepRating: item.sleepRating || 0
      };
    } else {
      dateMap[dateKey].count += 1;
      dateMap[dateKey].moodRating += item.moodRating || 0;
      dateMap[dateKey].activityRating += item.activityRating || 0;
      dateMap[dateKey].appetiteRating += item.appetiteRating || 0;
      dateMap[dateKey].sleepRating += item.sleepRating || 0;
    }
  });

  return Object.values(dateMap).map(entry => ({
    date: entry.date,
    moodRating: Math.round((entry.moodRating / entry.count) * 10) / 10,
    activityRating: Math.round((entry.activityRating / entry.count) * 10) / 10,
    appetiteRating: Math.round((entry.appetiteRating / entry.count) * 10) / 10,
    sleepRating: Math.round((entry.sleepRating / entry.count) * 10) / 10
  }));
};

const groupDataByTimePeriod = (data, period) => {
  const groupedData = {};

  data.forEach(item => {
    const date = new Date(item.date);
    let key;

    if (period === 'month') {
      key = `${date.getFullYear()}-${(date.getMonth() + 1).toString().padStart(2, '0')}`;
    } else if (period === 'year') {
      key = date.getFullYear();
    } else if (period === 'week') {
      const weekNumber = getWeekNumber(date);
      key = `${date.getFullYear()}-W${weekNumber.toString().padStart(2, '0')}`;
    } else {
      key = new Date(item.date).toLocaleDateString();
    }

    if (!groupedData[key]) {
      groupedData[key] = {
        date: key,
        count: 1,
        moodRating: item.moodRating || 0,
        activityRating: item.activityRating || 0,
        appetiteRating: item.appetiteRating || 0,
        sleepRating: item.sleepRating || 0
      };
    } else {
      groupedData[key].count += 1;
      groupedData[key].moodRating += item.moodRating || 0;
      groupedData[key].activityRating += item.activityRating || 0;
      groupedData[key].appetiteRating += item.appetiteRating || 0;
      groupedData[key].sleepRating += item.sleepRating || 0;
    }
  });

  return Object.values(groupedData).map(entry => ({
    date: entry.date,
    moodRating: Math.round((entry.moodRating / entry.count) * 10) / 10,
    activityRating: Math.round((entry.activityRating / entry.count) * 10) / 10,
    appetiteRating: Math.round((entry.appetiteRating / entry.count) * 10) / 10,
    sleepRating: Math.round((entry.sleepRating / entry.count) * 10) / 10
  }));
};

function getWeekNumber(date) {
  const d = new Date(date);
  d.setHours(0, 0, 0, 0);
  d.setDate(d.getDate() + 3 - (d.getDay() + 6) % 7);
  const week1 = new Date(d.getFullYear(), 0, 4);
  return 1 + Math.round(((d - week1) / 86400000 - 3 + (week1.getDay() + 6) % 7) / 7);
}

function GraphPage() {
  const { patientId } = useParams();
  const location = useLocation();
  const [rawData, setRawData] = useState([]);
  const [patientInfo, setPatientInfo] = useState(null);
  const [loading, setLoading] = useState(true);
  const [errorMsg, setErrorMsg] = useState("");
  const [chartType, setChartType] = useState('bar');
  const [timeRange, setTimeRange] = useState('day');
  const [startDate, setStartDate] = useState(new Date(Date.now() - 30 * 24 * 60 * 60 * 1000));
  const [endDate, setEndDate] = useState(new Date());
  const [activeMetrics, setActiveMetrics] = useState({
    moodRating: true,
    activityRating: true,
    appetiteRating: true,
    sleepRating: true,
  });

  const processedData = useMemo(() => {
    if (!rawData.length) return [];

    const filteredData = rawData.filter(item => {
      try {
        const itemDate = new Date(item.date);
        return itemDate >= startDate && itemDate <= endDate;
      } catch (e) {
        return false;
      }
    });

    if (!filteredData.length) return [];

    const dailyAverages = calculateDailyAverages(filteredData);
    return groupDataByTimePeriod(dailyAverages, timeRange);
  }, [rawData, timeRange, startDate, endDate]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        setLoading(true);
        
        const patientResponse = await axios.get(`https://localhost:7066/api/Patient/ReadItem?id=${patientId}`);
        setPatientInfo(patientResponse.data.item);

        const apiResponse = await axios.get(`https://localhost:7066/api/Graph/ReadItems?patientId=${patientId}`);
        let apiData = apiResponse.data?.pageItems || [];

        const localData = JSON.parse(localStorage.getItem(`patientData_${patientId}`) || '[]');

        let allData = [...apiData, ...localData];

        if (location.state?.date) {
          const newDataPoint = {
            date: new Date(location.state.date).toISOString(),
            moodRating: location.state.moodRating,
            activityRating: location.state.activityRating,
            appetiteRating: location.state.appetiteRating,
            sleepRating: location.state.sleepRating,
            patientId: patientId
          };
          allData.push(newDataPoint);
          localStorage.setItem(`patientData_${patientId}`, JSON.stringify([...localData, newDataPoint]));
        }

        const validData = allData.filter(item => 
          item.date && 
          (item.moodRating !== undefined || 
           item.activityRating !== undefined || 
           item.appetiteRating !== undefined || 
           item.sleepRating !== undefined)
        ).map(item => ({
          ...item,
          date: new Date(item.date).toISOString()
        }));

        setRawData(validData);

      } catch (error) {
        console.error("Error fetching data:", error);
        setErrorMsg("Could not load patient data. Please try again later.");
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [patientId, location.state]);

  const toggleMetric = (metric) => {
    setActiveMetrics(prev => ({
      ...prev,
      [metric]: !prev[metric]
    }));
  };

  const renderChart = () => {
    if (loading) {
      return (
        <div style={{ 
          display: 'flex', 
          justifyContent: 'center', 
          alignItems: 'center', 
          height: '100%'
        }}>
          <div className="spinner"></div>
          <p>Loading chart data...</p>
        </div>
      );
    }

    if (!processedData.length) {
      return (
        <div style={{
          textAlign: 'center',
          padding: '40px',
          color: '#666',
          display: 'flex',
          flexDirection: 'column',
          alignItems: 'center',
          justifyContent: 'center',
          height: '100%'
        }}>
          <h3>No data available</h3>
          <p>Please check your date range or submit patient data first.</p>
          <button
            onClick={() => {
              setStartDate(new Date(Date.now() - 365 * 24 * 60 * 60 * 1000));
              setEndDate(new Date());
            }}
            style={{
              padding: '8px 16px',
              backgroundColor: '#125358',
              color: 'white',
              border: 'none',
              borderRadius: '4px',
              marginTop: '10px',
              cursor: 'pointer'
            }}
          >
            Show Full History
          </button>
        </div>
      );
    }

    const commonProps = {
      data: processedData,
      margin: { top: 20, right: 30, left: 20, bottom: 60 }
    };

    const tooltipStyle = {
      background: 'rgba(255, 255, 255, 0.96)',
      borderRadius: '8px',
      border: 'none',
      boxShadow: '0 4px 12px rgba(0,0,0,0.15)',
      padding: '12px',
      color: '#333'
    };

    switch (chartType) {
      case 'bar':
        return (
          <BarChart {...commonProps}>
            <CartesianGrid strokeDasharray="3 3" opacity={0.5} />
            <XAxis 
              dataKey="date" 
              tick={{ fill: '#555' }}
              tickFormatter={(value) => formatDateLabel(value, timeRange)}
            />
            <YAxis domain={[0, 10]} tick={{ fill: '#555' }} />
            <Tooltip 
              contentStyle={tooltipStyle}
              labelFormatter={(value) => formatDateLabel(value, timeRange, true)}
            />
            <Legend wrapperStyle={{ paddingTop: '20px' }} />
            {metrics.map(metric => 
              activeMetrics[metric.key] && (
                <Bar
                  key={metric.key}
                  dataKey={metric.key}
                  name={metric.name}
                  fill={metric.color}
                  radius={[4, 4, 0, 0]}
                  animationDuration={1500}
                />
              )
            )}
          </BarChart>
        );

      case 'line':
        return (
          <LineChart {...commonProps}>
            <CartesianGrid strokeDasharray="3 3" opacity={0.3} />
            <XAxis 
              dataKey="date" 
              tick={{ fill: '#555' }}
              tickFormatter={(value) => formatDateLabel(value, timeRange)}
            />
            <YAxis domain={[0, 10]} tick={{ fill: '#555' }} />
            <Tooltip 
              contentStyle={tooltipStyle}
              labelFormatter={(value) => formatDateLabel(value, timeRange, true)}
            />
            <Legend wrapperStyle={{ paddingTop: '20px' }} />
            {metrics.map(metric => (
              activeMetrics[metric.key] && (
                <Line
                  key={metric.key}
                  type="monotone"
                  dataKey={metric.key}
                  name={metric.name}
                  stroke={metric.color}
                  strokeWidth={3}
                  activeDot={{ r: 8 }}
                  dot={{ r: 4 }}
                  animationDuration={1500}
                />
              )
            ))}
          </LineChart>
        );

        case 'pie':
          // Calculate average values for each metric
          const pieData = metrics.map(metric => {
            const validEntries = processedData.filter(item => item[metric.key] !== undefined);
            const total = validEntries.reduce((sum, item) => sum + item[metric.key], 0);
            const average = validEntries.length > 0 ? total / validEntries.length : 0;
            
            return {
              name: metric.name,
              value: parseFloat(average.toFixed(2)),
              color: metric.color
            };
          }).filter(item => !isNaN(item.value));
        
          // Calculate total for percentage calculation
          const totalValue = pieData.reduce((sum, item) => sum + item.value, 0);
        
          return (
            <PieChart {...commonProps}>
              <Pie
                data={pieData}
                cx="50%"
                cy="50%"
                outerRadius={150}
                fill="#8884d8"
                dataKey="value"
                label={({ name, percent }) => `${name}: ${(percent * 100).toFixed(0)}%`}
                labelLine={false}
              >
                {pieData.map((entry, index) => (
                  <Cell key={`cell-${index}`} fill={entry.color} />
                ))}
              </Pie>
              <Tooltip 
                contentStyle={tooltipStyle}
                formatter={(value, name, props) => {
                  const percentage = totalValue > 0 ? (value / totalValue * 100).toFixed(2) : 0;
                  return [
                    `${name}: ${value}`,
                    `Percentage: ${percentage}%`
                  ];
                }}
              />
              <Legend />
            </PieChart>
          );

      default:
        return (
          <ComposedChart {...commonProps}>
            <CartesianGrid strokeDasharray="3 3" />
            <XAxis 
              dataKey="date" 
              tick={{ fill: '#555' }}
              tickFormatter={(value) => formatDateLabel(value, timeRange)}
            />
            <YAxis yAxisId="left" orientation="left" domain={[0, 10]} />
            <YAxis yAxisId="right" orientation="right" domain={[0, 10]} />
            <Tooltip 
              contentStyle={tooltipStyle}
              labelFormatter={(value) => formatDateLabel(value, timeRange, true)}
            />
            <Legend />
            {activeMetrics.moodRating && (
              <Bar yAxisId="left" dataKey="moodRating" name="Mood" fill="#8884d8" />
            )}
            {activeMetrics.activityRating && (
              <Line yAxisId="right" type="monotone" dataKey="activityRating" name="Activity" stroke="#82ca9d" />
            )}
          </ComposedChart>
        );
    }
  };

  const formatDateLabel = (value, range, detailed = false) => {
    if (!value) return '';
    
    try {
      if (range === 'week') {
        const match = String(value).match(/(\d{4})-W(\d{2})/);
        if (match) return detailed ? `Week ${match[2]}, ${match[1]}` : `W${match[2]}`;
      }
      if (range === 'month') {
        const date = new Date(value);
        return detailed ? 
          date.toLocaleDateString('default', { month: 'long', year: 'numeric' }) :
          date.toLocaleDateString('default', { month: 'short' });
      }
      if (range === 'year') {
        return value;
      }
      const date = new Date(value);
      return detailed ? 
        date.toLocaleDateString() : 
        date.toLocaleDateString('default', { day: 'numeric', month: 'short' });
    } catch {
      return value;
    }
  };

  if (errorMsg) {
    return (
      <div style={{ 
        padding: "2rem", 
        textAlign: "center",
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        justifyContent: 'center',
        height: '100vh'
      }}>
        <h2 style={{ color: "#d32f2f", marginBottom: '1rem' }}>{errorMsg}</h2>
        <button 
          onClick={() => window.location.reload()}
          style={{
            padding: '10px 20px',
            backgroundColor: '#125358',
            color: 'white',
            border: 'none',
            borderRadius: '4px',
            cursor: 'pointer'
          }}
        >
          Retry
        </button>
      </div>
    );
  }

  return (
    <>
      <GlobalStyle />
      
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
            borderRadius: '8px',
          }}>
            <img
              src={patient1}
              alt="Patient"
              style={{
                width: '80px',
                height: '80px',
                borderRadius: '50%',
                objectFit: 'cover',
                marginRight: '20px',
              }}
            />
            <div>
              <h2 style={{ margin: 0, color: '#125358' }}>
                {patientInfo.firstName} {patientInfo.lastName}
              </h2>
              <p style={{ margin: '5px 0 0 0', color: '#666' }}>
                Patient ID: {patientId}
              </p>
            </div>
          </div>
        )}

        <div style={{ 
          marginBottom: '25px',
          padding: '15px',
          backgroundColor: '#f9f9f9',
          borderRadius: '8px',
          border: '1px solid #eee'
        }}>
          <h3 style={{ marginBottom: '15px', color: '#125358' }}>Select Metrics to Display</h3>
          <div style={{
            display: 'grid',
            gridTemplateColumns: 'repeat(auto-fit, minmax(120px, 1fr))',
            gap: '10px'
          }}>
            {metrics.map(metric => (
              <MetricToggle
                key={metric.key}
                active={activeMetrics[metric.key]}
                onClick={() => toggleMetric(metric.key)}
              >
                <input
                  type="checkbox"
                  checked={activeMetrics[metric.key]}
                  readOnly
                  style={{ accentColor: metric.color }}
                />
                <span style={{ color: metric.color, fontWeight: '500' }}>
                  {metric.name}
                </span>
              </MetricToggle>
            ))}
          </div>
        </div>
        
        // In your JSX, update the date picker container
<div style={{ 
  display: 'flex', 
  gap: '10px', 
  alignItems: 'center', 
  marginBottom: '20px',
  flexWrap: 'wrap',
  justifyContent: 'center'
}}>
  <div style={{ display: 'flex', gap: '10px', alignItems: 'center' }}>
    <span style={{ fontWeight: '500' }}>From:</span>
    <DatePicker
      selected={startDate}
      onChange={date => setStartDate(date)}
      selectsStart
      startDate={startDate}
      endDate={endDate}
      maxDate={endDate}
    />
  </div>
  <div style={{ display: 'flex', gap: '10px', alignItems: 'center' }}>
    <span style={{ fontWeight: '500' }}>To:</span>
    <DatePicker
      selected={endDate}
      onChange={date => setEndDate(date)}
      selectsEnd
      startDate={startDate}
      endDate={endDate}
      minDate={startDate}
      maxDate={new Date()}
    />
  </div>
</div>
        
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
        </ChartControls>

        <ChartWrapper>
          <ResponsiveContainer width="100%" height="100%">
            {renderChart()}
          </ResponsiveContainer>
          
          <TimePeriodContainer>
            {timeRanges.map(range => (
              <TimeRangeButton
                key={range.key}
                active={timeRange === range.key}
                onClick={() => setTimeRange(range.key)}
                style={{ width: '100%' }}
              >
                {range.name}
              </TimeRangeButton>
            ))}
          </TimePeriodContainer>
        </ChartWrapper>
      </GraphContainer>
    </>
  );
}

export default GraphPage;