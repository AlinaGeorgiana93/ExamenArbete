import React, { useEffect, useState } from 'react';
//import axios from 'axios';
import styled, { createGlobalStyle } from 'styled-components';
import { useTranslation } from 'react-i18next';
import axiosInstance from '../src/axiosInstance'; // adjust path as needed
//import { MenuItem, Select, FormControl, InputLabel } from '@mui/material';
import { useNavigate, Link } from 'react-router-dom'; 
import logo1 from '../src/media/logo1.png';
import StaffDetailsModal from '../Modals/StaffDetailsModal';  // Import modal component


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
  font-size: 16px;

  &:hover {
    background: ${props => (props.active ? '#125358' : '#00d4ff')};
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
  color: white;
  border: none;
  margin-right: 10px;
  cursor: pointer;
  background: ${props => (props.active === 'true' ? 'rgb(133, 200, 205)' : '#eee')};
  color: ${props => (props.active === 'true' ? '#fff' : '#000')};

  &:hover {
    background: #00d4ff;
  }
`;

const Select = styled.select`
  padding: 10px;
  margin-bottom: 20px;
  width: 100%;
  border: 1px solid #ccc;
`;

const AdminDashboard = () => {
  const { t } = useTranslation();
  const [activeTab, setActiveTab] = useState('patients');
  const [patients, setPatients] = useState([]);
  const [staff, setStaff] = useState([]);
  const [formData, setFormData] = useState({ firstName: '', lastName: '', personalNumber: '', id: null });
  const [selectedPerson, setSelectedPerson] = useState(null); // or an empty object {}
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isLoading, setIsLoading] = useState(false);


  useEffect(() => {
    fetchData();
  }, []);

  const fetchData = async () => {
    try {
      const response = await axiosInstance.get('Staff/ReadItems', {
        params: {
          flat: true,
          pageNr: 0,
          pageSize: 10,
        },
      });
      console.log('Fetched Staff:', response.data); // To inspect the response structure
      setStaff(response.data.pageItems); // Ensure you update state with the staff data
    } catch (error) {
      console.error('Error fetching staff:', error.response ? error.response.data : error.message);
    }
  };


  const handleSubmit = (e) => {
    e.preventDefault();
    const endpoint = activeTab === 'patients' ? 'Patient' : 'Staff';

    const dataToSend = {
      firstName: formData.firstName,
      lastName: formData.lastName,
      personalNumber: formData.personalNumber,
    };

    if (formData.id) {
      axiosInstance.put(`${endpoint}/${formData.id}`, dataToSend)
        .then(() => {
          setFormData({ firstName: '', lastName: '', personalNumber: '', id: null });
          fetchData();
        })
        .catch(error => {
          console.error("Error updating:", error);
        });
    } else {
      axiosInstance.post(`${endpoint}`, dataToSend)
        .then(() => {
          setFormData({ firstName: '', lastName: '', personalNumber: '', id: null });
          fetchData();
        })
        .catch(error => {
          console.error("Error adding:", error);
        });
    }
  };
  
  {selectedPerson && (
    <StaffDetailsModal
      staffMember={selectedPerson}
      onClose={() => setSelectedPerson(null)}  // This will close the modal
      onEdit={handleEdit}
      onDelete={handleDelete}
    />
  )}
  

  const currentData = activeTab === 'patients' ? patients : staff;

  const handleSelectChange = (e) => {
    const selectedStaffId = e.target.value;
    console.log('Selected staff ID:', selectedStaffId); // Log the selected staff ID
    const person = staff.find(item => item.staffId === selectedStaffId);
    console.log('Found person:', person); // Log the person object that matches the selected ID
    setSelectedPerson(person);
  };

  console.log('Staff:', staff); // Log the current staff data
  console.log('Selected Person:', selectedPerson); // Log the selected person


  const handleDelete = async (staffId) => {
    try {
      await axiosInstance.delete(`/api/Staff/DeleteItem/${staffId}`); // Using axiosInstance
      console.log('Staff deleted');
      setStaff(); // Refresh the staff list after deletion
      setModalVisible(false); // Close the modal after delete
    } catch (error) {
      console.error('Error deleting staff:', error);
    }
  };

  const handleEdit = async (updatedStaff) => {
    setIsLoading(true); // Set loading state to true
    try {
      console.log('Updating staff with:', updatedStaff);
  
      const response = await axiosInstance.put(`/Staff/UpdateItem/${updatedStaff.staffId}`, {
        firstName: updatedStaff.firstName,
        lastName: updatedStaff.lastName,
        personalNumber: updatedStaff.personalNumber
      });
  
      console.log('Staff updated:', response.data);  // Log the response from API
      fetchData(); // Refresh the staff list after update
      setIsModalOpen(false); // Close the modal after update
    } catch (error) {
      console.error('Error updating staff:', error.response || error.message);
    } finally {
      setIsLoading(false); // Set loading state to false once request is complete
    }
  };
  
  
  return (
    <>
      <GlobalStyle />
       <Link to="/" style={{ position: 'fixed', top: '15px', right: '15px', zIndex: '2' }}>
              <img
                src={logo1}
                alt="Logo"
                style={{
                  width: '150px', // Adjust logo size as needed
                }}
              />
            </Link>
      <Container>
        <h1>{t('admin_dashboard')}</h1>
        <Tabs>
          <TabButton active={activeTab === 'patients'} onClick={() => setActiveTab('patients')}>
            {t('patients')}
          </TabButton>
          <TabButton active={activeTab === 'staff'} onClick={() => setActiveTab('staff')}>
            {t('staff')}
          </TabButton>
        </Tabs>

        <Select onChange={handleSelectChange} value={selectedPerson ? selectedPerson.staffId : ''}>
        <option value=''>{t('select_person')}</option>
        {staff.map(item => (
          <option key={item.staffId} value={item.staffId}>
            {item.firstName} {item.lastName}
          </option>
        ))}
      </Select>

          {selectedPerson && (
      <StaffDetailsModal
        staffMember={selectedPerson}
        onClose={() => setSelectedPerson(null)} 
        onEdit={handleEdit}
        onDelete={handleDelete}
      />
    )}


        <Form onSubmit={handleSubmit}>
          <h2>{formData.id ? `${t('edit')} ${activeTab === 'patients' ? t('patient') : t('staff')}` : `${t('add')} ${activeTab === 'patients' ? t('patient') : t('staff')}`}</h2>
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
