import React, { useEffect, useState } from 'react';
import styled, { createGlobalStyle } from 'styled-components';
import { useTranslation } from 'react-i18next';
import axiosInstance from '../src/axiosInstance';
import { useNavigate, Link } from 'react-router-dom';
import logo1 from '../src/media/logo1.png';
import DetailsModal from '../Modals/DetailsModal';
import Select from 'react-select';

const GlobalStyle = createGlobalStyle`
  * {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
    font-size: 1.3rem;
  }
  body {
    font-family: 'Helvetica Neue', Arial, sans-serif;
    background: linear-gradient(135deg, #e0f7f9, #cceae7, #b2dfdb);
    min-height: 100vh;
    overflow-y: auto;
    color: #1a1a1a;
    display: flex;
    justify-content: center;
    align-items: flex-start;
    padding: 30px 0;
    transition: background 0.6s ease-in-out;
  }
`;

const AdminContainer = styled.div`
  background-color: #ffffff;
  padding: 80px 60px;
  border-radius: 16px;
  width: 800px;
  box-shadow: 0 8px 24px rgba(0, 0, 0, 0.2);
  text-align: center;
  margin: 20px auto;

  @media (max-width: 760px) {
    padding: 30px 20px;
    width: 100%;
    margin-top: 10px;
  }
`;

const LoggedInInfo = styled.p`
  font-size: 1.5rem;
  color: rgb(12, 53, 57);
  margin-bottom: 20px;
`;

const Title = styled.h1`
  color: #1a5b61;
  font-size: 2.5rem;
  margin-bottom: 20px;
`;

const Tabs = styled.div`
  display: flex;
  justify-content: center;
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
  font-size: 1rem;
  &:hover {
    background: #00d4ff;
  }
`;

const Input = styled.input`
  margin-bottom: 10px;
  padding: 10px;
  width: 100%;
  border: 1px solid #ccc;
`;

const SubmitButton = styled.button`
  padding: 10px 20px;
  background-color: #125358;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  &:hover {
    background-color: #00d4ff;
  }
`;

const SelectWrapper = styled.div`
  margin: 20px 0;
`;

const AdminDashboard = () => {
  const { t } = useTranslation();
  const [activeTab, setActiveTab] = useState('patients');
  const [patients, setPatients] = useState([]);
  const [staff, setStaff] = useState([]);
  const [selectedPerson, setSelectedPerson] = useState(null);
  const [formData, setFormData] = useState({
    firstName: '',
    lastName: '',
    personalNumber: '',
    username: '',
    email: '',
    password: '',
  });
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  const [successMessage, setSuccessMessage] = useState('');
  const [staffName, setStaffName] = useState('');

  useEffect(() => {
    fetchPatients();
    fetchStaff();
    const storedName = localStorage.getItem('userName');
    if (storedName) {
      setStaffName(storedName);
    }
  }, []);

  const fetchPatients = async () => {
    try {
      const response = await axiosInstance.get('/Patient/ReadItems', {
        params: { flat: true, pageNr: 0, pageSize: 10 },
      });
      setPatients(response.data.pageItems);
    } catch (error) {
      console.error('Error fetching patients:', error);
    }
  };

  const fetchStaff = async () => {
    try {
      const response = await axiosInstance.get('/Staff/ReadItems', {
        params: { flat: true, pageNr: 0, pageSize: 10 },
      });
      setStaff(response.data.pageItems);
    } catch (error) {
      console.error('Error fetching staff:', error);
    }
  };

  const handleSelectChange = (selectedOption) => {
    const id = selectedOption?.value;
    const list = activeTab === 'staff' ? staff : patients;
    const found = list.find((item) =>
      String(activeTab === 'staff' ? item.staffId : item.patientId) === id
    );
    setSelectedPerson(found);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setIsLoading(true);
    const endpoint = activeTab === 'staff' ? 'Staff' : 'Patient';
    try {
      await axiosInstance.post(`/${endpoint}/CreateItem`, formData);
      setSuccessMessage(`${endpoint} created successfully!`);
      if (activeTab === 'staff') {
        fetchStaff();
      } else {
        fetchPatients();
      }
      setFormData({
        firstName: '',
        lastName: '',
        personalNumber: '',
        username: '',
        email: '',
        password: '',
      });
    } catch (error) {
      setSuccessMessage(`Failed to create ${endpoint}.`);
    } finally {
      setIsLoading(false);
      setTimeout(() => setSuccessMessage(''), 3000);
    }
  };

  const handleEdit = async (updatedPerson) => {
    const isStaff = activeTab === 'staff';
    const endpoint = isStaff ? 'Staff' : 'Patient';
    const idField = isStaff ? 'staffId' : 'patientId';
    const id = updatedPerson[idField];

    try {
      await axiosInstance.put(`/${endpoint}/UpdateItem/${id}`, updatedPerson);
      setSuccessMessage(`${endpoint} updated successfully.`);
      if (isStaff) fetchStaff();
      else fetchPatients();
    } catch {
      setSuccessMessage(`Failed to update ${endpoint}.`);
    } finally {
      setTimeout(() => setSuccessMessage(''), 3000);
    }
  };

  const handleDelete = async (id) => {
    const endpoint = activeTab === 'staff' ? 'Staff' : 'Patient';
    try {
      await axiosInstance.delete(`/${endpoint}/DeleteItem/${id}`);
      setSuccessMessage(`${endpoint} deleted successfully.`);
      if (activeTab === 'staff') {
        setStaff((prev) => prev.filter((item) => item.staffId !== id));
      } else {
        setPatients((prev) => prev.filter((item) => item.patientId !== id));
      }
    } catch {
      setSuccessMessage(`Failed to delete ${endpoint}.`);
    } finally {
      setTimeout(() => setSuccessMessage(''), 3000);
    }
  };

  const options =
    activeTab === 'staff'
      ? staff.map((item) => ({
        value: item.staffId,
        label: `${item.firstName} ${item.lastName}`,
      }))
      : patients.map((item) => ({
        value: item.patientId,
        label: `${item.firstName} ${item.lastName}`,
      }));

  return (
    <>
      <GlobalStyle />
      <Link to="/" style={{ position: 'fixed', top: '15px', right: '15px', zIndex: '2' }}>
        <img src={logo1} alt="Logo" style={{ width: '150px' }} />
      </Link>
      <AdminContainer>
        <LoggedInInfo>{t('logged in as')} {staffName}</LoggedInInfo>
        <Title>{t('admin_dashboard')}</Title>
        <Tabs>
          <TabButton active={activeTab === 'patients'} onClick={() => setActiveTab('patients')}>
            {t('patients')}
          </TabButton>
          <TabButton active={activeTab === 'staff'} onClick={() => setActiveTab('staff')}>
            {t('staff')}
          </TabButton>
        </Tabs>

        <SelectWrapper>
          <Select
            onChange={handleSelectChange}
            options={options}
            placeholder={t(activeTab === 'staff' ? 'select_staff' : 'select_patient')}
          />
        </SelectWrapper>

        {successMessage && <p>{successMessage}</p>}

        <DetailsModal
          staffMember={selectedPerson}
          onClose={() => setSelectedPerson(null)}
          onEdit={handleEdit}
          onDelete={() =>
            handleDelete(activeTab === 'staff' ? selectedPerson.staffId : selectedPerson.patientId)
          }
        />

        <form onSubmit={handleSubmit}>
          <h3>{t('add')} {activeTab === 'patients' ? t('patient') : t('staff')}</h3>
          <Input placeholder={t('first_name')} value={formData.firstName} onChange={(e) => setFormData({ ...formData, firstName: e.target.value })} required />
          <Input placeholder={t('last_name')} value={formData.lastName} onChange={(e) => setFormData({ ...formData, lastName: e.target.value })} required />
          <Input placeholder={t('personal_number')} value={formData.personalNumber} onChange={(e) => setFormData({ ...formData, personalNumber: e.target.value })} required />

          {activeTab === 'staff' && (
            <>
              <Input placeholder={t('username')} value={formData.username} onChange={(e) => setFormData({ ...formData, username: e.target.value })} required />
              <Input placeholder={t('email')} value={formData.email} onChange={(e) => setFormData({ ...formData, email: e.target.value })} required />
              <Input placeholder={t('password')} value={formData.password} onChange={(e) => setFormData({ ...formData, password: e.target.value })} required />
            </>
          )}

          <SubmitButton type="submit">{isLoading ? t('loading') : t('submit')}</SubmitButton>
        </form>
      </AdminContainer>
    </>
  );
};

export default AdminDashboard;
