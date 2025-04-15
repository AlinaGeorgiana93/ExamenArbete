import React, { useEffect, useState } from 'react';
import styled, { createGlobalStyle } from 'styled-components';
import { useTranslation } from 'react-i18next';
import axiosInstance from '../src/axiosInstance';
import { useNavigate, Link } from 'react-router-dom';
import logo1 from '../src/media/logo1.png';
import DetailsModal from '../Modals/DetailsModal';

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
  max-height: 200px; /* Maximal höjd på dropdown */
  overflow-y: auto; /* Aktivera vertikal scrollbar när innehållet är för stort */
  display: block;
`;



const AdminDashboard = () => {
  const { t } = useTranslation();

  const [activeTab, setActiveTab] = useState('patients');
  const [patients, setPatients] = useState([]);
  const [staff, setStaff] = useState([]);
  const [formData, setFormData] = useState({ firstName: '', lastName: '', personalNumber: '', id: null });
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
        params: {
          flat: true,
          pageNr: 0,
          pageSize: 10,
        },
      });
      setStaff(response.data.pageItems);
    } catch (error) {
      console.error('Error fetching staff:', error.response ? error.response.data : error.message);
    }
  };

  const fetchPatientData = async () => {
    try {
      const response = await axiosInstance.get('Patient/ReadItems', {
        params: {
          flat: true,
          pageNr: 0,
          pageSize: 10,
        },
      });
      setPatients(response.data.pageItems);
    } catch (error) {
      console.error('Error fetching patient:', error.response ? error.response.data : error.message);
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setIsLoading(true);
    try {
      const isStaff = activeTab === 'staff';
      const endpoint = isStaff ? 'Staff' : 'Patient';
      const response = await axiosInstance.post(`/${endpoint}/CreateItem`, { ...formData });
      setSuccessMessage(`${endpoint} created successfully!`);
      setShowSuccessMessage(true);
      setTimeout(() => {
        setShowSuccessMessage(false);
        setSuccessMessage('');
      }, 2000);

      setFormData({ firstName: '', lastName: '', personalNumber: '', id: null });
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

      const response = await axiosInstance.put(
        `/${endpoint}/UpdateItem/${personId}`,
        {
          [idField]: personId,
          firstName: updatedPerson.firstName,
          lastName: updatedPerson.lastName,
          personalNumber: updatedPerson.personalNumber,
        }
      );

      alert(`${isStaff ? 'Staff' : 'Patient'} updated successfully.`);
      setSelectedPerson(null);

      if (endpoint === 'Staff') {
        fetchData();
      } else {
        fetchPatientData();
      }
    } catch (error) {
      alert(`Error updating ${isStaff ? 'staff' : 'patient'}.`);
    } finally {
      setIsLoading(false);
    }
  };

  const currentData = activeTab === 'patients' ? patients : staff;

  const handleSelectChange = (e) => {
    const id = e.target.value;
    let person;
    if (activeTab === 'staff') {
      person = staff.find(item => String(item.staffId) === id);
    } else {
      person = patients.find(item => String(item.patientId) === id);
    }
    setSelectedPerson(person);
  };

  return (
    <>
      <GlobalStyle />
      <Container>
        <h1>{t('admin_dashboard')}</h1>
        <Tabs>
          <TabButton active={activeTab === 'patients'} onClick={() => setActiveTab('patients')}>{t('patients')}</TabButton>
          <TabButton active={activeTab === 'staff'} onClick={() => setActiveTab('staff')}>{t('staff')}</TabButton>
        </Tabs>

        <Select onChange={handleSelectChange} value={selectedPerson?.staffId || ''} style={{ display: activeTab === 'staff' ? 'block' : 'none' }}>
          <option value="">{t('select_staff')}</option>
          {staff.map(item => (
            <option key={item.staffId} value={item.staffId}>
              {item.firstName} {item.lastName}
            </option>
          ))}
        </Select>

        <Select onChange={handleSelectChange} value={selectedPerson?.patientId || ''} style={{ display: activeTab === 'patients' ? 'block' : 'none' }}>
          <option value="">{t('select_patient')}</option>
          {patients.map(item => (
            <option key={item.patientId} value={item.patientId}>
              {item.firstName} {item.lastName}
            </option>
          ))}
        </Select>

        <DetailsModal
          staffMember={selectedPerson}
          onClose={() => setSelectedPerson(null)}
          onEdit={handleEdit}
          onDelete={() => handleDelete(activeTab === 'staff' ? selectedPerson.staffId : selectedPerson.patientId)}
        />

        {showSuccessMessage && (
          <div style={{
            backgroundColor: successMessage.toLowerCase().includes('successfully') ? '#d4edda' : '#f8d7da',
            color: successMessage.toLowerCase().includes('successfully') ? '#155724' : '#721c24',
            padding: '10px 15px',
            borderRadius: '5px',
            marginBottom: '20px',
            border: '1px solid',
            borderColor: successMessage.toLowerCase().includes('successfully') ? '#c3e6cb' : '#f5c6cb',
          }}>
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
          <Button type="submit">{t('add')}</Button>
        </Form>
      </Container>
    </>
  );
};

export default AdminDashboard;
