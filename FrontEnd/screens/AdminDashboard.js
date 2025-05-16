import React, { useEffect, useState } from 'react';
import styled, { createGlobalStyle } from 'styled-components';
import { useTranslation } from 'react-i18next';
import axiosInstance from '../src/axiosInstance';
import { useNavigate, Link } from 'react-router-dom';
import logo1 from '../src/media/logo1.png';
import DetailsModal from '../Modals/DetailsModal';
import Select from 'react-select'; // Importing react-select
import patient1 from '../src/media/patient1.jpg';
import PersonalNumberUtils from '../src/PersonalNumberUtils.js';
import InputValidationUtils from '../src/InputValidationUtils.js';
import { FaInfoCircle } from 'react-icons/fa';
import { Tooltip } from 'react-tooltip';


const GlobalStyle = createGlobalStyle`
  * {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
  }
  body {
    font-family: 'Poppins', sans-serif; /* Change the font */
    background: linear-gradient(135deg,rgb(139, 229, 238),rgb(51, 225, 207), #b2dfdb);
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
  background-color: #F5ECD5;
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
const FloatingProfile = styled.div`
  position: fixed;
  bottom: 32px;
  right: 20px;
  background-color: #ffffff;
  border-radius: 8px;
  padding: 10px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  cursor: pointer;
  z-index: 1000;
`;

const ProfileHeader = styled.div`
  display: flex;
  align-items: center;
  gap: 10px;
`;

const ProfileDetails = styled.div`
  margin-top: 10px;
  font-size: 0.9rem;
  color: #333;
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
    role: 'usr'
  });
  const [selectedPerson, setSelectedPerson] = useState(null);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  const [successMessage, setSuccessMessage] = useState('');
  const [showSuccessMessage, setShowSuccessMessage] = useState(false);
  const [formVisible, setFormVisible] = useState(true);  // initially visible
  const [isDropdownChanged, setIsDropdownChanged] = useState(false);
  const [nameOf, setNameOf] = useState('');
  const [showDetails, setShowDetails] = useState(false);
  const [userEmail, setUserEmail] = useState('');
  const [personalNumber, setPersonalNumber] = useState('');
  const [isValid, setIsValid] = useState(false);
  const [fieldErrors, setFieldErrors] = useState({
    firstName: '',
    lastName: '',
    personalNumber: '',
    username: '',
    email: '',
    password: '',
    id: null,
    role: 'usr'
  });
  
// Dummy data for now
const userData = {
  name: localStorage.getItem('userName'),
  email: localStorage.getItem('email'),
  role: localStorage.getItem('role'),
};


const validateAllFields = ({ firstName, lastName, username, email, password, personalNumber, id, role }) => {
  const errors = {};

  if (!InputValidationUtils.isValidName(firstName)) {
    errors.firstName = 'First name is invalid.';
  }

  if (!InputValidationUtils.isValidName(lastName)) {
    errors.lastName = 'Last name is invalid.';

  }

   if (!PersonalNumberUtils.isValid(personalNumber)) {
    errors.personalNumber = 'Personal number is invalid.';
  }
  
 if (activeTab === 'staff' && !id && (role === 'usr' || role === 'sysadmin')) {
   
  if (!InputValidationUtils.isValidUsername(username)) {
    errors.username = 'Username should be at least 4 characters long and contain only alphanumeric characters.';
  }

  if (!InputValidationUtils.isValidEmail(email)) {
    errors.email = 'Please provide a valid email address.';
  }

  if (!InputValidationUtils.isStrongPassword(password)) {
    errors.password = 'Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.';
  }
}

  return errors;
};



const handlePersonalNumberChange = (e) => {
  const newPersonalNumber = e.target.value;
  setPersonalNumber(newPersonalNumber);
  setFormData((prev) => ({ ...prev, personalNumber: newPersonalNumber }));

  if (PersonalNumberUtils.isValid(newPersonalNumber)) {
    setIsValid(true);
  } else {
    setIsValid(false);
  }
};
  

  useEffect(() => {
    fetchPatients();
    fetchStaff();
    const storedName = localStorage.getItem('userName');
    if (storedName) {
      setNameOf(storedName);
    }
  }, []);

  const fetchStaff = async () => {
    const token = localStorage.getItem('jwtToken'); // or 'user_token', depending on where you store it
    try {
      const response = await axiosInstance.get('Staff/ReadItems', {
        params: { flat: true, pageNr: 0, pageSize: 10 },
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      setStaff(response.data.pageItems);
    } catch (error) {
      console.error('Error fetching staff:', error.response || error.message);
    }
  };
  
  const fetchPatients = async () => {
    const token = localStorage.getItem('jwtToken');
    try {
      const response = await axiosInstance.get('Patient/ReadItems', {
        params: { flat: true, pageNr: 0, pageSize: 10 },
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      setPatients(response.data.pageItems);
    } catch (error) {
      console.error('Error fetching patient:', error.response || error.message);
    }
  };
  
const handleSubmit = async (e) => {
    e.preventDefault();
    
    console.log('Form submitted with data:', formData); // Add this line for debugging

    // Validate fields
    const errors = validateAllFields(formData);
    setFieldErrors(errors);

    // Log the validation errors
    console.log('Validation errors:', errors);

    if (Object.keys(errors).length > 0) {
      setIsLoading(false);  // stop loading
      return; // stop form submission
    }

    const isStaff = activeTab === 'staff';
    const endpoint = isStaff ? 'Staff' : 'Patient';

    try {
      const response = await axiosInstance.post(`/${endpoint}/CreateItem`, formData);
      console.log('API Response:', response);  // Check if the API call is successful

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
        role:''
      });

      // Fetch updated data for the correct tab (Staff or Patient)
      if (isStaff) {
        fetchStaff();
      } else {
        fetchPatients();
      }
    } catch (error) {
      console.error('Error during submission:', error.response || error.message); // Log the error for debugging
      setSuccessMessage(`Error creating ${activeTab}. Please try again.`);
      setShowSuccessMessage(true);
    } finally {
      setIsLoading(false);  // stop loading after submission
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
        await fetchStaff();
      } else {
        await fetchPatients();
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

    // Perform the update operation and get response
    const response = await axiosInstance.put(`/${endpoint}/UpdateItem/${personId}`, {
      [idField]: personId,
      firstName: updatedPerson.firstName,
      lastName: updatedPerson.lastName,
      personalNumber: updatedPerson.personalNumber,
      // You may want to include other fields too, like username/email/password if staff
      ...(isStaff && {
        username: updatedPerson.username,
        email: updatedPerson.email,
        password: updatedPerson.password,
        role: updatedPerson.role
      }),
    });

    // Check if the backend returned a new token
    const newToken = response?.data?.Item?.Token;
    if (newToken) {
      localStorage.setItem('jwtToken', newToken);
    }

    setSuccessMessage(`${isStaff ? 'Staff' : 'Patient'} updated successfully.`);
    setShowSuccessMessage(true);

    setTimeout(() => {
      setShowSuccessMessage(false);
      setSuccessMessage('');
    }, 2000);

    // Reset selection and refresh list
    setSelectedPerson(null);
    if (isStaff) {
      await fetchStaff();
    } else {
      await fetchPatients();
    }

    setFormVisible(true); // Ensure form appears after update

  } catch (error) {
    setSuccessMessage(`Error updating ${activeTab}.`);
    setShowSuccessMessage(true);
    setFormVisible(true);
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
        
      <FloatingProfile onClick={() => setShowDetails(prev => !prev)}>
  <ProfileHeader>
  <img
  src={patient1}
  alt="User Avatar"
  style={{ width: '40px', height: '40px', borderRadius: '50%' }}
/>
    <span>{userData.name}</span>
  </ProfileHeader>

  {showDetails && (
    <ProfileDetails>
      <div><strong>Email:</strong> {userData.email}</div>
      <div><strong>Role:</strong> {userData.role}</div>
    </ProfileDetails>
  )}
</FloatingProfile>


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
          
          <label htmlFor="first_name">{t('first_name')}</label>
          <Input
            type="text"
            id="first_name"
            placeholder={t('first_name')}
            value={formData.firstName}
            onChange={(e) => setFormData({ ...formData, firstName: e.target.value })}
            required
          />
                {fieldErrors.firstName && (
        <div style={{ color: 'red', fontSize: '0.8rem' }}>{fieldErrors.firstName}</div>
      )}

          <label htmlFor="last_name">{t('last_name')}</label>
          <Input
            type="text"
            id="last_name"
            placeholder={t('last_name')}
            value={formData.lastName}
            onChange={(e) => setFormData({ ...formData, lastName: e.target.value })}
            required
          />
          {fieldErrors.lastName && (
      <div style={{ color: 'red', fontSize: '0.8rem' }}>{fieldErrors.lastName}</div>
    )}


          <label htmlFor="personal_number">{t('personal_number')}</label>
          <Input
            type="text"
            id="personal_number"
            placeholder="YYYYMMDDXXX"
            value={formData.personalNumber}
            onChange={handlePersonalNumberChange}
            required
          />
          {fieldErrors.personalNumber && (
         <div style={{ color: 'red', fontSize: '0.8rem' }}>{fieldErrors.personalNumber}</div>
          )}

          {activeTab === 'staff' && !formData.id && (
  <>
    {/* Role Dropdown */}
    <Select
      value={{ label: formData.role, value: formData.role }}
      onChange={(selectedOption) => {
        setFormData({ ...formData, role: selectedOption.value });
      }}
      options={[
        { label: 'usr', value: 'usr' },
        { label: 'sysadmin', value: 'sysadmin' },
      ]}
      placeholder={t('selectRole')}
      styles={{
        container: (provided) => ({ ...provided, marginBottom: '10px' }),
      }}
    />

    {/* Username Field */}
    <Input
      type="text"
      placeholder={t('username')}
      value={formData.username}
      onChange={(e) => setFormData({ ...formData, username: e.target.value })}
    />
    {fieldErrors.username && <div style={{ color: 'red' }}>{fieldErrors.username}</div>}

    {/* Email Field */}
    <Input
      type="email"
      placeholder={t('email')}
      value={formData.email}
      onChange={(e) => setFormData({ ...formData, email: e.target.value })}
    />
    {fieldErrors.email && <div style={{ color: 'red' }}>{fieldErrors.email}</div>}

    {/* Password Field */}
  <>
  {/* Password Field */}
  <div style={{ display: 'flex', alignItems: 'center', gap: '8px' }}>
    <Input
      type="password"
      placeholder={t('password')}
      value={formData.password}
      onChange={(e) => setFormData({ ...formData, password: e.target.value })}
    />
    <FaInfoCircle
      data-tooltip-id="passwordTooltip"
      data-tooltip-html={t('password_strength_tooltip').replace(/\n/g, '<br />')}
      style={{ cursor: 'pointer' }}
      size={18}
      color="#888"
    />
    <Tooltip id="passwordTooltip" place="right" />
  </div>

  {fieldErrors.password && (
    <div style={{ color: 'red', marginTop: '4px' }}>
      {fieldErrors.password}
    </div>
  )}
</>

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
