import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { Provider } from 'react-redux';  // Import the Provider
import '../src/index.css';
import StartPage from '../screens/StartPage'; 
import AdminDashboard from '../screens/AdminDashboard';
import { store } from '../language/store/store';  // Import your Redux store

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
