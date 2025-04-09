import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { Provider } from 'react-redux';  // Import the Provider
import './src/index.js';
import './src/index.css'; // Import your CSS file here
import StartPage from './screens/StartPage'; 
import AdminDashboard from './screens/AdminDashboard';
import { store } from './language/store/store';  // Import your Redux store
import './language/i18n.js'; // this needs to be imported *before* anything else that uses translations


function App() {
  return (
    <Provider store={store}>  {/* Wrap the Router with the Redux Provider */}
      <Router>
        <Routes>
          <Route path="/" element={<StartPage />} />
          <Route path="/admin" element={<AdminDashboard />} /> 
        </Routes>
      </Router>
    </Provider>
  );
}

export default App;
