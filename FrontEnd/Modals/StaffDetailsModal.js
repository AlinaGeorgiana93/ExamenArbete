import React, { useState } from 'react';
import styled from 'styled-components';

// Styled components for modal
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
  background: #fff;
  padding: 30px;
  border-radius: 10px;
  width: 450px;
  max-height: 80vh;
  overflow-y: auto;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
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


const Button = styled.button`
  padding: 12px 24px;
  font-size: 16px;
  border: none;
  background: rgb(133, 200, 205);
  color: white;
  border-radius: 8px;
  cursor: pointer;
  margin-top: 10px;
  transition: all 0.3s ease;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);

  &:hover {
    background: #00d4ff;
    transform: translateY(-2px);
    box-shadow: 0 6px 10px rgba(0, 0, 0, 0.2);
  }

  &:active {
    background: #0099cc;
    transform: translateY(1px);
  }

  &:focus {
    outline: none;
    box-shadow: 0 0 0 3px rgba(0, 0, 0, 0.1);
  }
`;

const ButtonDelete = styled(Button)`
  background: red;
  &:hover {
    background: darkred;
  }
`;

const CancelButton = styled(Button)`
  background: #e4e4e4;
  color: #333;
  width: 100%;
  margin-top: 15px;
  &:hover {
    background: #ddd;
  }
`;

const InputGroup = styled.div`
  display: flex;
  flex-direction: column;
  margin-bottom: 20px;  // Increased bottom margin for better spacing
  gap: 8px;

  label {
    font-weight: bold;
    font-size: 14px;
    color: #444;
    margin-bottom: 5px;  // Ensure label has space from the input
  }

  input {
    padding: 12px;  // Adjusted padding to give more space
    font-size: 16px;
    border: 1px solid #ddd;
    border-radius: 8px;
    outline: none;
    transition: all 0.2s ease-in-out;
    width: 100%;  // Ensures the input fields take up the full width available

    &:focus {
      border-color: rgb(133, 200, 205);
      box-shadow: 0 0 5px rgba(0, 212, 255, 0.5);
    }
  }
`;



const StaffDetailsModal = ({ staffMember, onClose, onEdit, onDelete }) => {
    if (!staffMember) return null; // Don't render if no staff member is selected
  
    const [firstName, setFirstName] = useState(staffMember.firstName);
    const [lastName, setLastName] = useState(staffMember.lastName);
    const [personalNumber, setPersonalNumber] = useState(staffMember.personalNumber);
  
    const handleUpdate = () => {
      onEdit({ ...staffMember, firstName, lastName, personalNumber });
    };
  
    const handleDelete = () => {
      onDelete(staffMember.staffId);
    };
  
    return (
      <ModalContainer>
      <ModalContent>
        <ModalHeader>{`Edit ${staffMember.firstName} ${staffMember.lastName}`}</ModalHeader>
        
        {/* Input fields for first name, last name, and personal number */}
        <InputGroup>
          <label>First Name</label>
          <input
            type="text"
            value={firstName}
            onChange={(e) => setFirstName(e.target.value)}
          />
        </InputGroup>
        
        <InputGroup>
          <label>Last Name</label>
          <input
            type="text"
            value={lastName}
            onChange={(e) => setLastName(e.target.value)}
          />
        </InputGroup>
        
        <InputGroup>
          <label>Personal Number</label>
          <input
            type="text"
            value={personalNumber}
            onChange={(e) => setPersonalNumber(e.target.value)}
          />
        </InputGroup>
    
        {/* Buttons */}
        <Button onClick={handleUpdate}>Update</Button>
        <ButtonDelete onClick={handleDelete}>Delete</ButtonDelete>
        <CancelButton onClick={onClose}>Cancel</CancelButton>
      </ModalContent>
    </ModalContainer>
    );
  };
  export default StaffDetailsModal;