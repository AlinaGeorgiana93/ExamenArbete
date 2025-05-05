import React, { useEffect, useState } from 'react';
import styled, { createGlobalStyle } from 'styled-components';
import { useTranslation } from 'react-i18next';
import axiosInstance from '../src/axiosInstance';
import { useNavigate, Link } from 'react-router-dom';
import logo1 from '../src/media/logo1.png';
import DetailsModal from '../Modals/DetailsModal';
import Select from 'react-select'; // Importing react-select
import customSelect from '../src/customSelect.css'; // Importing custom styles for react-select

const GlobalStyle = createGlobalStyle`
  * {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
  }
  body {
    font-family: 'Poppins', sans-serif; /* Change the font */
    background: linear-gradient(
        135deg,
        #BE5985 0%,
        #1679AB 30%,
        #8EACCD 60%,
        #2A629A 90%,
        #BE5985 110%
    );
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100vh;
    color: black;
    position: relative;
  }
`;

const Title = styled.h1`
  color: #fff;
  font-size: 36px;
  font-weight: 800;
  letter-spacing: 1px;
  text-shadow: 2px 2px 4px rgba(0,0,0,0.3);

  text-align: center;

  @media (max-width: 480px) {
    font-size: 28px;
  }
`;

const Subtitle = styled.span`
  color:rgb(58, 53, 53);
  font-size: 22px;
  font-weight: 400;
  opacity: 0.9;
  margin-bottom: 0;  // remove unnecessary bottom space
  display: block;
  text-align: center;
  font-weight: 600;

  @media (max-width: 480px) {
    font-size: 20px;
  }
`;

const Container = styled.div`
  padding: 10px;
  justify-content: center;  
  align-items: center;
  display: flex;
  flex-direction: column;
  
  h1 {
    color: white;
    margin-bottom: 30px;
  }
  
`;

const SelectContainer = styled.div`
  margin-bottom: 20px; /* Add margin below the Select component */
`;



const Tabs = styled.div`
  display: flex;
  gap: 20px;
  margin-bottom: 20px;
`;

const ButtonWrapper = styled.div`
  display: flex;
  justify-content: center;
  align-items: center; 
  gap: 20px;
  margin-top: 20px;
  margin-bottom: 10px;
  
  
`;


const TabButton = styled.button`
  padding: 10px 20px;
  background: ${(props) => (props.active ? 'rgb(40, 136, 155)' : '#eee')};
  color: ${(props) => (props.active ? '#fff' : '#000')};
  border: none;
  border-radius: 5px;
  cursor: pointer;
  font-size: 16px;

  &:hover {
    background: ${(props) => (props.active ? 'rgb(40, 136, 155)' : ' #8ACCD5')};
  }
`;

const MainWrapper = styled.div`
  width: 350px; /* Or any fixed width you prefer */
  display: flex;
  flex-direction: column;
  justify-content: center;
  gap: 15px;
  align-items: stretch;
  

`;


const Form = styled.form`
  font-size: 1rem;
  font-weight: 500;
  margin-top: 30px;
  background-color: #FAF1E6;
  padding: 30px;
  border-radius: 12px;
  width: ${(props) =>
    props.activeTab === 'staff' ? '60%' : '100%'};
  margin: 0 auto;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1); /* Optional for matching login form */
  display: flex;
  flex-direction: column;

  h2 {
    color:rgb(58, 53, 53);
    font-size: 22px;
    font-weight: 400;
    opacity: 0.9;
    text-align: center;
    padding: 10px;
    font-family: 'Poppins', sans-serif;
    font-weight: 600;
    
`;

const Input = styled.input`
  margin-bottom: 16px;
  padding: 12px;
  width: 100%;
  border: 1px solid #ccc;
  border-radius: 8px;
  font-size: 1rem;
`;

const Button = styled.button`
  padding: 12px 24px;
  border: none;
  margin-right: 10px;
  border-radius: 6px;
  font-size: 0.9rem;
  cursor: pointer;
  background: ${(props) =>
    props.active === 'true' ? 'rgb(40, 136, 155)' : '#eee'};
  color: ${(props) => (props.active === 'true' ? '#fff' : '#000')};
   align-self: center; 
   width: 35%;
  

  &:hover {
    background: #8ACCD5;
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
  const [formVisible, setFormVisible] = useState(true);  // initially visible
  const [isDropdownChanged, setIsDropdownChanged] = useState(false);
  const [nameOf, setNameOf] = useState('');

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
    
    // Ensure activeTab is set correctly when submitting
    const isStaff = activeTab === 'staff';
    const endpoint = isStaff ? 'Staff' : 'Patient';
    
    try {
      const response = await axiosInstance.post(`/${endpoint}/CreateItem`, {
        ...formData,
      });
      
      setSuccessMessage(`${endpoint} created successfully!`);
      setShowSuccessMessage(true);
      setTimeout(() => {
        setShowSuccessMessage(false);
        setSuccessMessage('');
      }, 2000);
  
      // Reset the form after successful submission
      setFormData({
        firstName: '',
        lastName: '',
        personalNumber: '',
        username: '',  
        email: '',     
        password: '',  
        id: null,
      });
  
      // Fetch updated data for the correct tab (Staff or Patient)
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
      
      // Perform the delete operation
      await axiosInstance.delete(`/${endpoint}/DeleteItem/${personId}`);
      
      // Set success message
      setSuccessMessage(`${endpoint} deleted successfully!`);
      setShowSuccessMessage(true);
      
      setTimeout(() => {
        setShowSuccessMessage(false);
        setSuccessMessage('');
      }, 2000);
      
      // Fetch the updated data
      if (isStaff) {
        await fetchData();
      } else {
        await fetchPatientData();
      }
  
      // Reset the selected person and show the form again
      setSelectedPerson(null);
      setFormVisible(true); // Ensure the form appears after the delete
      
    } catch (error) {
      console.error(`Error deleting ${activeTab}:`, error.response || error.message);
      setFormVisible(true); // Show form again if there's an error
    }
  };
  
  const handleEdit = async (updatedPerson) => {
    setIsLoading(true);
    try {
      const isStaff = activeTab === 'staff';
      const endpoint = isStaff ? 'Staff' : 'Patient';
      const idField = isStaff ? 'staffId' : 'patientId';
      const personId = updatedPerson[idField];
  
      // Perform the update operation
      await axiosInstance.put(`/${endpoint}/UpdateItem/${personId}`, {
        [idField]: personId,
        firstName: updatedPerson.firstName,
        lastName: updatedPerson.lastName,
        personalNumber: updatedPerson.personalNumber,
      });
  
      // Set success message
      setSuccessMessage(`${isStaff ? 'Staff' : 'Patient'} updated successfully.`);
      setShowSuccessMessage(true);
      
      setTimeout(() => {
        setShowSuccessMessage(false);
        setSuccessMessage('');
      }, 2000);
  
      // Reset the selected person and fetch the updated data
      setSelectedPerson(null);
      if (isStaff) {
        await fetchData();
      } else {
        await fetchPatientData();
      }
      
      // Show the form after the update
      setFormVisible(true); // Ensure the form appears after the update
  
    } catch (error) {
      setSuccessMessage(`Error updating ${activeTab}.`);
      setShowSuccessMessage(true);
      setFormVisible(true); // Show form in case of failure
    } finally {
      setIsLoading(false);
    }
  };
  

  const currentData = activeTab === 'patients' ? patients : staff;

  const handleSelectChange = (selectedOption) => {
    if (selectedOption) {
      const personList = activeTab === 'staff' ? staff : patients;
      const person = personList.find(p =>
        activeTab === 'staff'
          ? p.staffId === selectedOption.value
          : p.patientId === selectedOption.value
      );
  
      setSelectedPerson(person);
      setIsDropdownChanged(true);  // Mark that a change occurred
      setFormVisible(false);       // Hide the form after selection
    }
  };
  
  const handleDropdownOpen = () => {
    setFormVisible(false);  // Hide the form when the dropdown is opened
  };
  
  const handleDropdownClose = () => {
    if (!isDropdownChanged) {
      setFormVisible(true);  // Show the form again only if no change occurred
    }
  };
  

  return (
    <>
      <GlobalStyle />
      <Link to="/" style={{ position: 'fixed', top: '15px', right: '15px', zIndex: '2' }}>
        <img src={logo1} alt="Logo" style={{ width: '150px' }} />
      </Link>

      
      <Container>
      <Title>{t('app_title')}</Title>
      <Subtitle>{t('admin_dashboard')}</Subtitle>
        <Tabs>
        <ButtonWrapper>
          <TabButton active={activeTab === 'patients'} onClick={() => setActiveTab('patients')}>
            {t('patients')}
          </TabButton>
          <TabButton active={activeTab === 'staff'} onClick={() => setActiveTab('staff')}>
            {t('staff')}
          </TabButton>
        </ButtonWrapper>
        </Tabs>
        <MainWrapper>
        <SelectContainer>
      
  <Select
    onMenuOpen={() => setFormVisible(false)} // Hide form on dropdown open
    onMenuClose={() => {
      if (!isDropdownChanged) {
        setFormVisible(true); // Show form again if no selection was made
      }
      setIsDropdownChanged(false); // Reset the flag when the dropdown is closed
    }}
    onChange={(selectedOption) => {
      handleSelectChange(selectedOption);
      setIsDropdownChanged(true); // Set flag when a selection is made
    }} 
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
    value={
      selectedPerson
        ? {
            value: activeTab === 'staff' ? selectedPerson.staffId : selectedPerson.patientId,
            label: `${selectedPerson.firstName} ${selectedPerson.lastName}`,
          }
        : null
    }
    placeholder={t(activeTab === 'staff' ? 'select_staff' : 'select_patient')}
    isSearchable={true}
    styles={{
      control: (provided) => ({
        ...provided,
        padding: '5px',
      }),
    }}
  /> 
</SelectContainer>

<DetailsModal
  staffMember={selectedPerson}
  onClose={() => {
    setSelectedPerson(null);
    setFormVisible(true);  // show the form again
  }}
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

  {formVisible && (
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
            {isLoading ? t('loading') : t('Add')}
          </Button>
        </Form>    
      )}
        </MainWrapper>
      </Container>
    </>
  );
};

export default AdminDashboard;
