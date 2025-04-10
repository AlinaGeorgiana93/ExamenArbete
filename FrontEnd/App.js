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
import { store } from './language/store/store';
import './language/i18n.js';
import Layout from './src/media/Layout.js'; // Layout will wrap all inner pages except StartPage


function App() {
  return (
    <Provider store={store}>
      <Router>
        <Routes>
          {/* StartPage visas utan layout */}
          <Route path="/" element={<StartPage />} />

          {/* Alla andra sidor inuti Layout */}
          <Route element={<Layout />}>
            <Route path="/admin"   element={<AdminDashboard />} />
            <Route path="/staff"   element={<StaffPage />} />
            <Route path="/patient" element={<PatientPage />} />
          </Route>
        </Routes>
      </Router>
    </Provider>
  );
}

export default App;
