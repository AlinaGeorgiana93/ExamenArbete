import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { Provider } from 'react-redux';
import './src/index.js';
import './src/index.css';
import StartPage from './screens/StartPage';
import AdminDashboard from './screens/AdminDashboard';
// import AboutPage from './screens/AboutPage';
import PatientPage from './screens/PatientPage';
import StaffPage from './screens/StaffPage';
import GraphPage from './screens/GraphPage';
import { store } from './language/store/store';
import './language/i18n.js';
import Layout from './src/media/Layout.js'; // Layout will wrap all inner pages except StartPage

function App() {
  return (
    <Provider store={store}>
      <Router>
        <Routes>
          {/* Standalone StartPage */}
          <Route path="/" element={<StartPage />} />

          {/* All other routes with shared layout */}
          <Route element={<Layout />}>
            <Route path="/admin" element={<AdminDashboard />} />
            <Route path="/patient/:patientId" element={<PatientPage />} />

            <Route path="/staff" element={<StaffPage />} />
            <Route path="/patient" element={<PatientPage />} />
            <Route path="/graph/:id" element={<GraphPage />} />

            {/* <Route path="/about" element={<AboutPage />} /> */}

          </Route>
        </Routes>
      </Router>
    </Provider>
  );
}

export default App;
