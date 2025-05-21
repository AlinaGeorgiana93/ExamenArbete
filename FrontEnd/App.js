import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { Provider } from 'react-redux';
import './src/index.js';
import './src/index.css';
import LoginPage from './screens/LoginPage';
import AdminDashboard from './screens/AdminDashboard';
import PatientPage from './screens/PatientPage';
import StaffPage from './screens/StaffPage';
import GraphPage from './screens/GraphPage';
import CommentsPage from './screens/CommentsPage.js';
import PatientDataReview from './screens/PatientDataReview';
import { store } from './language/store/store';
import './language/i18n.js';
import Layout from './src/media/Layout.js';
import AboutUs from './screens/AboutUs';

function App() {
  return (
    <Provider store={store}>
      <Router>
        <Routes>
          {/* Standalone StartPage */}
          <Route path="/" element={<LoginPage />} />
          <Route path="/about" element={<AboutUs />} />


          {/* All other routes with shared layout */}
          <Route element={<Layout />}>

            <Route path="/admin" element={<AdminDashboard />} />
            <Route path="/patient/:patientId" element={<PatientPage />} />

            <Route path="/staff" element={<StaffPage />} />

            <Route path="/review/:patientId" element={<PatientDataReview />} />



            <Route path="/graph/:patientId" element={<GraphPage />} />

            <Route path="/comments/:patientId" element={<CommentsPage />} />

          </Route>
        </Routes>
      </Router>
    </Provider>
  );
}

export default App;