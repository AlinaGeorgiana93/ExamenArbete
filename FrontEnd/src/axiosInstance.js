// axiosInstance.js
import axios from 'axios';

const axiosInstance = axios.create({
  baseURL: 'https://localhost:7066/api/',
});

axiosInstance.interceptors.request.use((config) => {
  const token = localStorage.getItem('jwtToken'); // ✅ match the login key name
  console.log("Retrieved token from localStorage:", token);
  

  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }

  return config;
}, (error) => {
  return Promise.reject(error);
});

export default axiosInstance;
