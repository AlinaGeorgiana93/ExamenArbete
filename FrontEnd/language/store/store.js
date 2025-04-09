// src/app/store.js
import { configureStore } from '@reduxjs/toolkit';
import languageReducer from '../languageSlice'; // Adjust the import path as necessary

export const store = configureStore({
  reducer: {
    language: languageReducer,
  },
});
