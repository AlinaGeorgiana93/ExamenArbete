import React, { useState } from 'react';
import styled from 'styled-components';
import { useTranslation } from 'react-i18next';
import PersonalNumberUtils from '../src/PersonalNumberUtils.js';
import InputValidationUtils from '../src/InputValidationUtils.js';

const ModalContainer = styled.div`
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: rgba(0, 0, 0, 0.7);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
  opacity: 0;
  animation: fadeIn 0.3s ease forwards;

  @keyframes fadeIn {
    0% {
      opacity: 0;
    }
    100% {
      opacity: 1;
    }
  }
`;

const ModalContent = styled.div`
   background-color: #FAF1E6;
  padding: 30px;
  border-radius: 10px;
  width: 450px;
  max-height: 80vh;
  overflow-y: auto;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
  position: relative;
  animation: slideUp 0.3s ease-out;

  @keyframes slideUp {
    0% {
      transform: translateY(20px);
      opacity: 0;
    }
    100% {
      transform: translateY(0);
      opacity: 1;
    }
  }
`;

const ModalHeader = styled.h3`
  font-size: 20px;
  margin-bottom: 20px;
  text-align: center;
  color: #333;
`;

const InputGroup = styled.div`
  display: flex;
  flex-direction: column;
  margin-bottom: 20px;
  gap: 8px;

  label {
    font-weight: bold;
    font-size: 14px;
    color: #444;
  }

  input {
    padding: 12px;
    font-size: 16px;
    border: 1px solid #ddd;
    border-radius: 8px;
    outline: none;
    width: 100%;
    &:focus {
      border-color: rgb(133, 200, 205);
      box-shadow: 0 0 5px rgba(0, 212, 255, 0.5);
    }
  }
`;

const Button = styled.button`
  padding: 12px 24px;
  font-size: 16px;
  border: none;
  background: rgb(40, 136, 155);
  color: white;
  border-radius: 8px;
  cursor: pointer;
  transition: all 0.3s ease;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);

  &:hover {
    background: #8ACCD5;
    transform: translateY(-2px);
    box-shadow: 0 6px 10px rgba(0, 0, 0, 0.2);
  }

  &:active {
    background: #0099cc;
    transform: translateY(1px);
  }
`;

const ButtonDelete = styled(Button)`
  background: red;
  &:hover {
    background: darkred;
  }
`;

const CloseButton = styled.button`
  position: absolute;
  top: 12px;
  right: 12px;
  background: transparent;
  border: none;
  font-size: 24px;
  cursor: pointer;
  color: #333;
`;

const ConfirmActions = styled.div`
  display: flex;
  justify-content: space-between;
  margin-top: 20px;
  gap: 10px;
  flex-wrap: wrap;
`;

// Your styled-components...
// ModalContainer, ModalContent, ModalHeader, etc. (same as before)

const DetailsModal = ({ staffMember, onClose, onEdit, onDelete, activeTab }) => {
  if (!staffMember) return null;

  const { t } = useTranslation();
  const [firstName, setFirstName] = useState(staffMember.firstName);
  const [lastName, setLastName] = useState(staffMember.lastName);
  const [personalNumber, setPersonalNumber] = useState(staffMember.personalNumber);
  const [username, setUsername] = useState(staffMember.username || '');  // assuming staffMember has a username
  const [email, setEmail] = useState(staffMember.email || '');  // assuming staffMember has an email
  const [password, setPassword] = useState(''); // assuming password is handled separately
  const [confirmDelete, setConfirmDelete] = useState(false);
  const [isValid, setIsValid] = useState(false);
  const [fieldErrors, setFieldErrors] = useState({
    firstName: '',
    lastName: '',
    personalNumber: '',
    username: '',
    email: '',
    password: '',
  });

  const handleUpdate = () => {
    // Validate the fields
    const errors = validateAllFields({ firstName, lastName, username, email, password, personalNumber, activeTab });
    setFieldErrors(errors);

    // Log the validation errors
    console.log('Validation errors:', errors);

    if (Object.keys(errors).length > 0) {
      return; // stop form submission if there are errors
    }

    // Proceed with the update if no validation errors
    onEdit({ ...staffMember, firstName, lastName, personalNumber, username, email, password });
  };

  const handleConfirmDelete = () => {
    onDelete(staffMember.staffId || staffMember.patientId);
  };

  const validateAllFields = ({ firstName, lastName, username, email, password, personalNumber, activeTab }) => {
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

    // Only validate these fields for staff
    if (activeTab === 'staff') {
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

    if (PersonalNumberUtils.isValid(newPersonalNumber)) {
      setIsValid(true);
    } else {
      setIsValid(false);
    }
  };

  return (
    <ModalContainer>
      <ModalContent>
        <ModalHeader>{`${t('Edit')} ${staffMember.firstName} ${staffMember.lastName}`}</ModalHeader>

        <InputGroup>
          <label>{t('first_name')}</label>
          <input
            type="text"
            id="first_name"
            placeholder={t('first_name')}
            value={firstName}
            onChange={(e) => setFirstName(e.target.value)}
          />
          {fieldErrors.firstName && (
            <div style={{ color: 'red', fontSize: '0.8rem' }}>{fieldErrors.firstName}</div>
          )}
        </InputGroup>

        <InputGroup>
          <label>{t('last_name')}</label>
          <input
            type="text"
            id="last_name"
            value={lastName}
            onChange={(e) => setLastName(e.target.value)}
          />
          {fieldErrors.lastName && (
            <div style={{ color: 'red', fontSize: '0.8rem' }}>{fieldErrors.lastName}</div>
          )}
        </InputGroup>

        <InputGroup>
          <label>{t('personal_number')}</label>
          <input
            type="text"
            id="personal_number"
            placeholder="YYYYMMDDXXXX"
            value={personalNumber}
            onChange={handlePersonalNumberChange}
          />
          {fieldErrors.personalNumber && (
            <div style={{ color: 'red', fontSize: '0.8rem' }}>{fieldErrors.personalNumber}</div>
          )}
        </InputGroup>

        {activeTab === 'staff' && (
          <>
            <InputGroup>
              <label>{t('username')}</label>
              <input
                type="text"
                id="username"
                value={username}
                onChange={(e) => setUsername(e.target.value)}
              />
              {fieldErrors.username && (
                <div style={{ color: 'red', fontSize: '0.8rem' }}>{fieldErrors.username}</div>
              )}
            </InputGroup>

            <InputGroup>
              <label>{t('email')}</label>
              <input
                type="email"
                id="email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
              />
              {fieldErrors.email && (
                <div style={{ color: 'red', fontSize: '0.8rem' }}>{fieldErrors.email}</div>
              )}
            </InputGroup>

            <InputGroup>
              <label>{t('password')}</label>
              <input
                type="password"
                id="password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                
              />
              {fieldErrors.password && (
                <div style={{ color: 'red', fontSize: '0.8rem' }}>{fieldErrors.password}</div>
              )}
            </InputGroup>
          </>
        )}

        <ConfirmActions>
          {!confirmDelete ? (
            <>
              <Button onClick={handleUpdate}>{t('Update')}</Button>
              <Button onClick={onClose}>{t('Cancel')}</Button>
              <ButtonDelete onClick={() => setConfirmDelete(true)}>{t('Delete')}</ButtonDelete>
            </>
          ) : (
            <>
              <ButtonDelete onClick={handleConfirmDelete}>{t('Yes, delete')}</ButtonDelete>
              <Button onClick={() => setConfirmDelete(false)}>{t('Cancel')}</Button>
            </>
          )}
        </ConfirmActions>

        <CloseButton onClick={onClose} aria-label="Close modal">
          &times;
        </CloseButton>
      </ModalContent>
    </ModalContainer>
  );
};

export default DetailsModal;
