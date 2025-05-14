import React, { useState, useEffect } from 'react';
import styled from 'styled-components';
import axiosInstance from '../src/axiosInstance.js'; // Adjust path if necessary
import { useTranslation } from 'react-i18next';
import PersonalNumberUtils from '../src/PersonalNumberUtils.js';
import InputValidationUtils from '../src/InputValidationUtils.js';


// Styled Components for Modal
const ModalBackground = styled.div`
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 2000;
`;

const ModalContainer = styled.div`
  background-color: #F5ECD5;
  padding: 20px;
  border-radius: 12px;
  width: 100%;
  max-width: 400px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  font-family: 'Poppins', sans-serif;
`;

const ModalHeader = styled.h3`
  text-align: center;
  color: #1a5b61;
  font-size: 1.5rem;
  margin-bottom: 20px;
`;

const Label = styled.label`
  font-size: 1rem;
  color: #333;
  margin-bottom: 5px;
  display: block;
`;

const InputField = styled.input`
  width: 100%;
  padding: 12px;
  margin-bottom: 15px;
  font-size: 1rem;
  border: 1px solid #ccc;
  border-radius: 8px;
  background-color: #f9f9f9;

  &:focus {
    outline: none;
    border-color: #3b878c;
    background-color: #fff;
  }
`;

const ButtonGroup = styled.div`
  display: flex;
  justify-content: space-between;
  margin-top: 15px;
`;

const Button = styled.button`
  padding: 10px 20px;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  font-size: 1rem;
  background-color: ${props => props.$variant === 'primary' ? 'blue' : 'gray'};
  color: white;
`;

const ProfileMessage = styled.p`
  color: ${({ success }) => (success ? 'green' : 'red')};
  font-weight: bold;
  text-align: center;
  margin-top: 15px;
`;

const UpdateProfileModal = ({
  showModal,
  setShowModal,
  userName, 
  setUserName,
  email,
  setStaffEmail,
}) => {
  const [newUsername, setNewUsername] = useState(userName);
  const [newEmail, setNewEmail] = useState(email);
  const [isPasswordChange, setIsPasswordChange] = useState(false);
  const [currentPassword, setCurrentPassword] = useState('');
  const [newPassword, setNewPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [passwordMessage, setPasswordMessage] = useState('');
  const [isLoading, setIsLoading] = useState(false);
  const [errorMessage, setErrorMessage] = useState('');
  const [successMessage, setSuccessMessage] = useState('');
  const [showErrorMessage, setShowErrorMessage] = useState(false);
  const [showSuccessMessage, setShowSuccessMessage] = useState(false);
  const [profile, setProfile] = useState({ userName: '', email: '' });
  const [passwordStrength, setPasswordStrength] = useState('');
  const [isSubmitting, setIsSubmitting] = useState(false);
  const { t } = useTranslation();


  useEffect(() => {
  if (showModal) {
    fetchProfile(); // Fetch profile when modal is opened
  }
}, [showModal]); // Trigger fetch only when the modal is shown


  const handleProfileUpdate = async (updatedPerson) => {
  setIsLoading(true);
   if (isSubmitting) return;
  setIsSubmitting(true);
  try {
    const staffId = localStorage.getItem('staffId');
    const token = localStorage.getItem('jwtToken');

    if (!staffId || !token) {
      throw new Error('Staff ID or Token not found. User must be logged in.');
    }

    const currentProfile = await fetchProfile();
    if (!currentProfile) {
      setErrorMessage('Failed to retrieve current profile.');
      setShowErrorMessage(true);
      return;
    }

    const { userName, email } = updatedPerson;
   

    const updatedData = {
      staffId,
      userName: newUsername || currentProfile.userName,  // Use userName here for backend
      email: newEmail || currentProfile.email
    };

     console.log('UserName prop:', userName);
    console.log('Email prop:', email);


    if (!userName && !email) {
      setErrorMessage('Please provide either a username or email to update.');
      setShowErrorMessage(true);
      return;
    }

    // Send the update request to the backend
    const response = await axiosInstance.put(`/Staff/UpdateItem/${staffId}`, updatedData);

    console.log('Response after update:', response);

    setSuccessMessage('Profile updated successfully.');
    setShowSuccessMessage(true);

    // After receiving updated data
console.log("Updated Profile:", response.data);

      // ✅ ADD THIS:
      setUserName(newUsername);
      setStaffEmail(newEmail);
      localStorage.setItem('userName', newUsername);
      localStorage.setItem('email', newEmail);



    setTimeout(() => {
      setShowSuccessMessage(false);
      setSuccessMessage('');
      setShowModal(false);  // Close modal after success
    }, 2000);

  } catch (error) {
    console.error('Error updating profile:', error);
    setErrorMessage('Failed to update profile. Please try again.');
    setShowErrorMessage(true);
    setTimeout(() => {
      setShowErrorMessage(false);
      setErrorMessage('');
    }, 2000);
  } finally {
    setIsLoading(false);
    setIsSubmitting(false);
  
  }
};


const fetchProfile = async () => {
  const staffId = localStorage.getItem('staffId');
  const token = localStorage.getItem('jwtToken');

  if (!staffId || !token) {
    console.error('Missing staffId or token');
    return null;
  }

  try {
    const response = await axiosInstance.get(`/Staff/ReadItem`, {
      params: { id: staffId, flat: false },
      headers: { Authorization: `Bearer ${token}` },
    });

    const { item } = response.data;

    if (item) {
      setProfile(item);
      setNewUsername(item.userName);
      setNewEmail(item.email);
    }

    return item; // <-- ADD THIS
  } catch (error) {
    console.error('Failed to fetch profile:', error);
    return null;
  }
};




  const evaluatePasswordStrength = (password) => {
    if (password.length < 6) return 'Weak';

    const hasUppercase = /[A-Z]/.test(password);
    const hasNumber = /\d/.test(password);
    const hasSpecialChar = /[^A-Za-z0-9]/.test(password);

    if (hasUppercase && hasNumber && hasSpecialChar) return 'Strong';
    if (hasUppercase || hasNumber) return 'Medium';

    return 'Weak';
  };


useEffect(() => {
  console.log('newUsername:', newUsername);
  console.log('newEmail:', newEmail);
}, [newUsername, newEmail]);

useEffect(() => {
  console.log('Updated Profile:', profile);
}, [profile]);

  useEffect(() => {
    setPasswordStrength(evaluatePasswordStrength(newPassword));
  }, [newPassword]);

  useEffect(() => {
  if (showModal) {
    fetchProfile(); // Fetch profile when modal is opened
  }
}, [showModal]); // Trigger fetch only when the modal is shown


 const handleChangePassword = async () => {
  setPasswordMessage('');

  if (!currentPassword || !newPassword || !confirmPassword) {
    setPasswordMessage('Please fill in all fields.');
    return;
  }

  if (newPassword !== confirmPassword) {
    setPasswordMessage('New passwords do not match.');
    return;
  }

  try {
    const token = localStorage.getItem('jwtToken');

    const response = await axiosInstance.post(
      '/PasswordResetToken/ChangePassword/ChangePassword',
      {
        currentPassword,
        newPassword,
        confirmPassword
      },
      {
        headers: { Authorization: `Bearer ${token}` }
      }
    );

    if (response.status === 200 || response.status === 204) {
      setPasswordMessage('Your password was successfully changed!');
     setPasswordMessage('Your password was successfully changed!');
      fetchProfile();
      setTimeout(() => {
        setShowModal(false);
        setPasswordMessage('');
      }, 2000);

    } else {
      setPasswordMessage('Failed to change password. Try again.');
    }
  } catch (error) {
    console.error('Password change error:', error);
    setPasswordMessage('Server error while changing password.');
  }
};


  const handleSubmit = async (e) => {
  e.preventDefault();
  if (isPasswordChange) {
    handleChangePassword();
  } else {
    handleProfileUpdate({ userName: newUsername, email: newEmail }); // ✅ correct key
  }
};


  const handlePasswordToggle = () => {
    setIsPasswordChange(!isPasswordChange);
  };

  return (
    showModal && (
      <ModalBackground>
        <ModalContainer>
          <ModalHeader>{t('update_profile')}</ModalHeader>

          <form onSubmit={handleSubmit}>
            <div>
              <Label htmlFor="username">{t('username')}</Label>
              <InputField
                id="username"
                type="text"
                placeholder="New Username"
                value={newUsername}
                onChange={(e) => setNewUsername(e.target.value)}
              />
            </div>

            <div>
              <Label htmlFor="email">{t('email')}</Label>
              <InputField
                id="email"
                type="email"
                placeholder="New Email"
                value={newEmail}
                onChange={(e) => setNewEmail(e.target.value)}
              />
            </div>

            {isPasswordChange && (
  <>
    <div>
      <Label htmlFor={t('current_Password')}>{t('current_Password')}</Label>
      <InputField
        id={t('current_Password')}
        type="password"
        placeholder={t('current_Password')}
        value={currentPassword}
        onChange={(e) => setCurrentPassword(e.target.value)}
      />
    </div>

    <div>
      <Label htmlFor={t('new_Password')}>{t('new_Password')}</Label>
      <InputField
        id={t('new_Password')}
        type="password"
        placeholder={t('new_Password')}
        value={newPassword}
        onChange={(e) => setNewPassword(e.target.value)}
      />
    </div>

    <div>
      <Label htmlFor={t('confirm_Password')}>{t('confirm_Password')}</Label>
      <InputField
        id={t('confirm_Password')}
        type="password"
        placeholder={t('confirm_Password')}
        value={confirmPassword}
        onChange={(e) => setConfirmPassword(e.target.value)}
      />
    </div>
  </>
)}


            <ButtonGroup>
              <Button type="button" $variant="cancel" onClick={() => setShowModal(false)}>
              {t('cancel')}
              </Button>
              <Button $variant="primary">{t('save')}</Button>
            </ButtonGroup>
          </form>

          {passwordMessage && (
            <ProfileMessage success={passwordMessage.includes('successfully')}>
              {passwordMessage}
            </ProfileMessage>
          )}

          <div style={{ textAlign: 'center', marginTop: '10px' }}>
            <button type="button" onClick={handlePasswordToggle} style={{ fontSize: '0.9rem' }}>
            {isPasswordChange ? t('cancel_password_change') : t('change_password')}

            </button>
          </div>
        </ModalContainer>
      </ModalBackground>
    )
  );
};

export default UpdateProfileModal;
