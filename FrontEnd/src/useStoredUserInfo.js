import { useState, useEffect } from 'react';

const useStoredUserInfo = () => {
  const [userName, setUserName] = useState('');
  const [email, setEmail] = useState('');
  const [role, setRole] = useState('');

  useEffect(() => {
    const storedName = localStorage.getItem('userName');
    const storedEmail = localStorage.getItem('email');
    const storedRole = localStorage.getItem('role');

    if (storedName) setUserName(storedName);
    if (storedEmail) setEmail(storedEmail);
    if (storedRole) setRole(storedRole);
  }, []);

  return { userName, setUserName, email, setEmail, role };
};

export default useStoredUserInfo;