import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import StartPage from '../screens/StartPage';
import StaffPage from '../screens/StaffPage';
import '../src/index.css';


function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<StartPage />} />
        <Route path="/start" element={<StaffPage />} />
      </Routes>
    </Router>
  );
}

export default App;
