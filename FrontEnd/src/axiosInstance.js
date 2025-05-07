// axiosInstance.js
import axios from 'axios';

const axiosInstance = axios.create({
  baseURL: 'https://localhost:7066/api/',
});

axiosInstance.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('jwtToken');

    if (token) {
      console.log('✅ Token found and added to headers:', token);
      config.headers.Authorization = `Bearer ${token}`;
    } else {
      console.log('🔒 No token in localStorage (user/staff probably not logged in)');
    }

    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);


export default axiosInstance;
