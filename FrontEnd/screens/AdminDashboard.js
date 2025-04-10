import React, { useEffect, useState } from 'react';
import axios from 'axios';
import styled, { createGlobalStyle } from 'styled-components';
import { useTranslation } from 'react-i18next';
import logo1 from '../src/media/logo1.png';
import { useDispatch } from 'react-redux';
import { setLanguage } from '../language/languageSlice'; 
import { getI18n } from 'react-i18next';  // <-- Use getI18n instead of i18n
import '../language/i18n.js';
import { useNavigate, Link } from 'react-router-dom';  // Import Link for navigation

const GlobalStyle = createGlobalStyle`
  * {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
  }
  body {
    font-family: 'Times New Roman', cursive, sans-serif;
    background: linear-gradient(135deg, #3B878C, #00d4ff, #006E75, #50D9E6, #1A5B61); 
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100vh;
    color: #fff;
    position: relative;
  }
`;
const Container = styled.div`
  padding: 40px;

  h1 {
    color: white;
    margin-bottom: 30px;
  }
`;

const Tabs = styled.div`
  display: flex;
  gap: 20px;
  margin-bottom: 20px;
`;

const TabButton = styled.button`
  padding: 10px 20px;
  background: ${props => (props.active ? '#125358' : '#eee')};
  color: ${props => (props.active ? '#fff' : '#000')};
  border: none;
  border-radius: 5px;
  cursor: pointer;
`;

const Table = styled.table`
  width: 100%;
  border-collapse: collapse;
  background: white;
  margin-top: 20px;

  th, td {
    padding: 12px;
    border: 1px solid #ccc;
  }

  th {
    background-color: #125358;
    color: white;
  }
`;

const Form = styled.form`
  margin-top: 30px;
  background: #fff;
  padding: 20px;
  border-radius: 8px;
`;

const Input = styled.input`
  margin-bottom: 10px;
  padding: 10px;
  width: 100%;
  border: 1px solid #ccc;
`;

const Button = styled.button`
  padding: 10px 20px;
  background: #125358;
  color: white;
  border: none;
  margin-right: 10px;
  cursor: pointer;

  &:hover {
    background: #00d4ff;
  }
`;

const AdminDashboard = () => {
  const { t } = useTranslation();
  const [activeTab, setActiveTab] = useState('patients');
  const [patients, setPatients] = useState([]);
  const [staff, setStaff] = useState([]);
  const [formData, setFormData] = useState({ firstName: '', lastName: '', personalNumber: '', id: null });

  useEffect(() => {
    fetchData();
  }, []);

  const fetchData = () => {
    axios.get('https://localhost:7066/api/Patient').then(res => setPatients(res.data));
    axios.get('https://localhost:7066/api/Staff').then(res => setStaff(res.data));
  };

  const handleDelete = (type, id) => {
    axios.delete(`https://localhost:7066/api/${type}/${id}`).then(fetchData);
  };

  const handleEdit = (entry) => {
    setFormData({ ...entry, id: entry._id });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    const endpoint = activeTab === 'patients' ? 'patients' : 'staff';
    const dataToSend = {
      firstName: formData.firstName,
      lastName: formData.lastName,
      personalNumber: formData.personalNumber,
    };

    if (formData.id) {
      axios.put(`https://localhost:7066/api/${endpoint}/${formData.id}`, dataToSend).then(() => {
        setFormData({ firstName: '', lastName: '', personalNumber: '', id: null });
        fetchData();
      });
    } else {
      axios.post(`https://localhost:7066/api/${endpoint}`, dataToSend).then(() => {
        setFormData({ firstName: '', lastName: '', personalNumber: '', id: null });
        fetchData();
      });
    }
  };

  const currentData = activeTab === 'patients' ? patients : staff;

  return (

     <>
          <GlobalStyle />
          {/* Logo clickable to navigate to the start page */}
          <Link to="/" style={{ position: 'fixed', top: '15px', right: '15px', zIndex: '2' }}>
            <img
              src={logo1}
              alt="Logo"
              style={{
                width: '150px', // Adjust logo size as needed
              }}
            />
          </Link>
      <GlobalStyle />
      <Container>
        <h1>{t('admin_dashboard')}</h1>

        <Tabs>
          <TabButton active={activeTab === 'patients'} onClick={() => setActiveTab('patients')}>{t('patients')}</TabButton>
          <TabButton active={activeTab === 'staff'} onClick={() => setActiveTab('staff')}>{t('staff')}</TabButton>
        </Tabs>

        <Table>
          <thead>
            <tr>
              <th>{t('first_name')}</th>
              <th>{t('last_name')}</th>
              <th>{t('personal_number')}</th>
            </tr>
          </thead>
          <tbody>
            {currentData.map(item => (
              <tr key={item._id}>
                <td>{item.firstName}</td>
                <td>{item.lastName}</td>
                <td>{item.personalNumber}</td>
                <td>
                  <Button onClick={() => handleEdit(item)}>{t('edit')}</Button>
                  <Button onClick={() => handleDelete(activeTab, item._id)}>{t('delete')}</Button>
                </td>
              </tr>
            ))}
          </tbody>
        </Table>

        <Form onSubmit={handleSubmit}>
          <h2>{formData.id ? t('edit_entry') : t('add_entry')}</h2>
          <Input
            placeholder={t('first_name')}
            value={formData.firstName}
            onChange={(e) => setFormData({ ...formData, firstName: e.target.value })}
            required
          />
          <Input
            placeholder={t('last_name')}
            value={formData.lastName}
            onChange={(e) => setFormData({ ...formData, lastName: e.target.value })}
            required
          />
          <Input
            placeholder={t('personal_number')}
            value={formData.personalNumber}
            onChange={(e) => setFormData({ ...formData, personalNumber: e.target.value })}
            required
          />
          <Button type="submit">{formData.id ? t('update') : t('add')}</Button>
        </Form>
      </Container>
    </>
  );
};

export default AdminDashboard;
