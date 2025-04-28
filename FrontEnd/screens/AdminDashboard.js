import React, { useEffect, useState } from 'react';
import styled, { createGlobalStyle } from 'styled-components';
import { useTranslation } from 'react-i18next';
import axiosInstance from '../src/axiosInstance';
import { useNavigate, Link } from 'react-router-dom';
import logo1 from '../src/media/logo1.png';
import DetailsModal from '../Modals/DetailsModal';
import Select from 'react-select'; // Importing react-select

const GlobalStyle = createGlobalStyle`
  * {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
  }
 body {
  font-family: 'Times New Roman', cursive, sans-serif;
  background: linear-gradient(135deg, #3B878C, #00d4ff, #006E75, #50D9E6, #1A5B61);
  min-height: 100vh;
  color:  black;
  overflow-y: auto;
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
  background: ${(props) => (props.active ? '#125358' : '#eee')};
  color: ${(props) => (props.active ? '#fff' : '#000')};
  border: none;
  border-radius: 5px;
  cursor: pointer;
  font-size: 16px;
  &:hover {
    background: ${(props) => (props.active ? '#125358' : '#00d4ff')};
  }
`;

const Form = styled.form`
  margin-top: 30px;
  background: #fff;
  padding: 20px;
  border-radius: 8px;
  width: ${(props) =>
    props.activeTab === 'staff' ? '60%' : '100%'};
  max-width: 600px;
  margin: 0 auto;
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
  background: ${(props) =>
    props.active === 'true' ? 'rgb(133, 200, 205)' : '#eee'};
  color: ${(props) => (props.active === 'true' ? '#fff' : '#000')};
  &:hover {
    background: #00d4ff;
  }
`;

const AdminDashboard = () => {
  const { t } = useTranslation();
  const [activeTab, setActiveTab] = useState('patients');
  const [patients, setPatients] = useState([]);
  const [staff, setStaff] = useState([]);
  const [formData, setFormData] = useState({
    firstName: '',
    lastName: '',
    personalNumber: '',
    username: '',    
    email: '',      
    password: '',     
    id: null,
  });
  const [selectedPerson, setSelectedPerson] = useState(null);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  const [successMessage, setSuccessMessage] = useState('');
  const [showSuccessMessage, setShowSuccessMessage] = useState(false);

  useEffect(() => {
    fetchData();
    fetchPatientData();
  }, []);

  const fetchData = async () => {
    try {
      const response = await axiosInstance.get('Staff/ReadItems', {
        params: { flat: true, pageNr: 0, pageSize: 10 },
      });
      setStaff(response.data.pageItems);
    } catch (error) {
      console.error('Error fetching staff:', error.response || error.message);
    }
  };

  const fetchPatientData = async () => {
    try {
      const response = await axiosInstance.get('Patient/ReadItems', {
        params: { flat: true, pageNr: 0, pageSize: 10 },
      });
      setPatients(response.data.pageItems);
    } catch (error) {
      console.error('Error fetching patient:', error.response || error.message);
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setIsLoading(true);
    try {
      const isStaff = activeTab === 'staff';
      const endpoint = isStaff ? 'Staff' : 'Patient';
      const response = await axiosInstance.post(`/${endpoint}/CreateItem`, {
        ...formData,
      });
      setSuccessMessage(`${endpoint} created successfully!`);
      setShowSuccessMessage(true);
      setTimeout(() => {
        setShowSuccessMessage(false);
        setSuccessMessage('');
      }, 2000);

      setFormData({
        firstName: '',
        lastName: '',
        personalNumber: '',
        username: '',  
        email: '',     
        password: '',  
        id: null,
      });
      if (isStaff) {
        fetchData();
      } else {
        fetchPatientData();
      }
    } catch (error) {
      setSuccessMessage(`Error creating ${activeTab}. Please try again.`);
      setShowSuccessMessage(true);
    } finally {
      setIsLoading(false);
    }
  };

  const handleDelete = async (personId) => {
    try {
      const isStaff = activeTab === 'staff';
      const endpoint = isStaff ? 'Staff' : 'Patient';
      await axiosInstance.delete(`/${endpoint}/DeleteItem/${personId}`);
      setSuccessMessage(`${endpoint} deleted successfully!`);
      setShowSuccessMessage(true);
      setTimeout(() => {
        setShowSuccessMessage(false);
        setSuccessMessage('');
      }, 2000);
      if (isStaff) {
        await fetchData();
      } else {
        await fetchPatientData();
      }
      setSelectedPerson(null);
    } catch (error) {
      console.error(`Error deleting ${activeTab}:`, error.response || error.message);
    }
  };

  const handleEdit = async (updatedPerson) => {
    setIsLoading(true);
    try {
      const isStaff = activeTab === 'staff';
      const endpoint = isStaff ? 'Staff' : 'Patient';
      const idField = isStaff ? 'staffId' : 'patientId';
      const personId = updatedPerson[idField];

      await axiosInstance.put(`/${endpoint}/UpdateItem/${personId}`, {
        [idField]: personId,
        firstName: updatedPerson.firstName,
        lastName: updatedPerson.lastName,
        personalNumber: updatedPerson.personalNumber,
      });

      setSuccessMessage(`${isStaff ? 'Staff' : 'Patient'} updated successfully.`);
      setShowSuccessMessage(true);
      setTimeout(() => {
        setShowSuccessMessage(false);
        setSuccessMessage('');
      }, 2000);

      setSelectedPerson(null);
      if (isStaff) {
        fetchData();
      } else {
        fetchPatientData();
      }
    } catch (error) {
      setSuccessMessage(`Error updating ${activeTab}.`);
      setShowSuccessMessage(true);
    } finally {
      setIsLoading(false);
    }
  };

  const currentData = activeTab === 'patients' ? patients : staff;

  const handleSelectChange = (selectedOption) => {
    const id = selectedOption?.value;
    let person;
    if (activeTab === 'staff') {
      person = staff.find((item) => String(item.staffId) === id);
    } else {
      person = patients.find((item) => String(item.patientId) === id);
    }
    setSelectedPerson(person);
  };

  return (
    <>
      <GlobalStyle />
      <Link to="/" style={{ position: 'fixed', top: '15px', right: '15px', zIndex: '2' }}>
        <img src={logo1} alt="Logo" style={{ width: '150px' }} />
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

        <Select
          onChange={handleSelectChange}
          options={activeTab === 'staff'
            ? staff.map(item => ({
                value: item.staffId,
                label: `${item.firstName} ${item.lastName}`,
              }))
            : patients.map(item => ({
                value: item.patientId,
                label: `${item.firstName} ${item.lastName}`,
              }))
          }
          value={selectedPerson ? { value: selectedPerson.id, label: `${selectedPerson.firstName} ${selectedPerson.lastName}` } : null}
          placeholder={t(activeTab === 'staff' ? 'select_staff' : 'select_patient')}
        />

        <DetailsModal
          staffMember={selectedPerson}
          onClose={() => setSelectedPerson(null)}
          onEdit={handleEdit}
          onDelete={() =>
            handleDelete(activeTab === 'staff' ? selectedPerson.staffId : selectedPerson.patientId)
          }
        />

        {showSuccessMessage && (
          <div
            style={{
              backgroundColor: successMessage.toLowerCase().includes('successfully')
                ? '#d4edda'
                : '#f8d7da',
              color: successMessage.toLowerCase().includes('successfully')
                ? '#155724'
                : '#721c24',
              padding: '10px 15px',
              borderRadius: '5px',
              marginBottom: '20px',
              border: '1px solid',
              borderColor: successMessage.toLowerCase().includes('successfully')
                ? '#c3e6cb'
                : '#f5c6cb',
            }}
          >
            {successMessage}
          </div>
        )}

        <Form onSubmit={handleSubmit}>
          <h2>{`${t('add')} ${activeTab === 'patients' ? t('patient') : t('staff')}`}</h2>

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

          {activeTab === 'staff' && (
            <>
              <Input
                placeholder={t('username')}
                value={formData.username}
                onChange={(e) => setFormData({ ...formData, username: e.target.value })}
                required
              />

              <Input
                placeholder={t('email')}
                value={formData.email}
                onChange={(e) => setFormData({ ...formData, email: e.target.value })}
                required
              />

              <Input
                placeholder={t('password')}
                value={formData.password}
                onChange={(e) => setFormData({ ...formData, password: e.target.value })}
                required
              />
            </>
          )}

          <Button type="submit" active="true">
            {isLoading ? t('loading') : t('submit')}
          </Button>
        </Form>
      </Container>
    </>
  );
};

export default AdminDashboard;
