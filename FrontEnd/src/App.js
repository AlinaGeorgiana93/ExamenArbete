import React from 'react';
import '../src/index.css';
import logo1 from '../src/logo1.png';
// import StartPage from '../screens/StartPage'; 
import PatientPage from '../screens/PatientPage';

function App() {
  return (
    <div className="App">
    {/* <StartPage /> */}
    < PatientPage/>
  </div>
  );
}

export default App;


// import React from 'react';
// import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
// import '../src/index.css';
// // import StartPage from '../screens/StartPage';
// //import StaffDashboard from '../screens/StaffDashboard';
// //import AdminDashboard from '../screens/AdminDashboard';
// import PatientPage from '../screens/PatientPage';
// //<Route path="/staff" element={<StaffDashboard />} />
// //<Route path="/admin" element={<AdminDashboard />} />

 
// function App() {
//   return (
//     <Router>
//       <Routes>
//         {/* <Route path="/" element={<StartPage />} /> */}
//         <Route path="/patient" element={<PatientPage />} />
//       </Routes>
//     </Router>
 
// );
// }
 
// export default App;