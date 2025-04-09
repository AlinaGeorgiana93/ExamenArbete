import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import { Provider } from 'react-redux';
import { store } from '../language/store/store'; // Ensure this import points to your store correctly

const container = document.getElementById('root');
const root = ReactDOM.createRoot(container); // Create root using the container
root.render(
  <Provider store={store}> {/* Wrap your App with the Redux Provider */}
    <App />
  </Provider>
);
