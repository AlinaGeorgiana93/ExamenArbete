import React from 'react';
import ReactDOM from 'react-dom/client';
import { Provider } from 'react-redux';
import { store } from '../language/store/store'; // Ensure this points to your correct Redux store
import App from '../App'; // Adjust the path if needed
import '../language/i18n.js'; // Import your i18n configuration here, without the `.js` extension

const container = document.getElementById('root');
const root = ReactDOM.createRoot(container); // Create root using the container

root.render(
  <Provider store={store}>
    {' '}
    {/* Wrap your App with the Redux Provider */}
    <App />
  </Provider>
);
