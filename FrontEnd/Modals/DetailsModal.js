import React, { useState } from 'react';
import styled from 'styled-components';

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

const ModalHeader = styled.h3`
  font-size: 45px;
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
    font-size: 27px;  // Ändrat från 14px till 18px (eller valfri storlek)
    color: #444;
  }

  input {
    padding: 12px;
    font-size: 25px;
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
  font-size: 25px;  // Större text i knappar
  border: none;
  background: rgb(133, 200, 205);
  color: white;
  border-radius: 8px;
  cursor: pointer;
  transition: all 0.3s ease;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);

  &:hover {
    background: #00d4ff;
    transform: translateY(-2px);
    box-shadow: 0 6px 10px rgba(0, 0, 0, 0.2);
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

const DetailsModal = ({ staffMember, onClose, onEdit, onDelete }) => {
  if (!staffMember) return null;

  const [firstName, setFirstName] = useState(staffMember.firstName);
  const [lastName, setLastName] = useState(staffMember.lastName);
  const [personalNumber, setPersonalNumber] = useState(staffMember.personalNumber);
  const [confirmDelete, setConfirmDelete] = useState(false);

  const handleUpdate = () => {
    onEdit({ ...staffMember, firstName, lastName, personalNumber });
    onClose();  // Close modal after update
  };

  const handleConfirmDelete = () => {
    onDelete(staffMember.staffId || staffMember.patientId);
    onClose();  // Close modal after delete
  };

  const handleCancel = () => {
    onClose();  // Close modal when cancel is clicked
  };

  return (
    <ModalContainer>
      <ModalContent>
        <ModalHeader>{`Edit ${staffMember.firstName} ${staffMember.lastName}`}</ModalHeader>

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

        <ConfirmActions>
          {!confirmDelete ? (
            <>
              <Button onClick={handleUpdate}>Save</Button>
              <Button onClick={handleCancel}>Cancel</Button>
              <ButtonDelete onClick={() => setConfirmDelete(true)}>
                Delete
              </ButtonDelete>
            </>
          ) : (
            <>
              <ButtonDelete onClick={handleConfirmDelete}>Yes, delete</ButtonDelete>
              <Button onClick={() => setConfirmDelete(false)}>Cancel</Button>
            </>
          )}
        </ConfirmActions>

        <CloseButton onClick={handleCancel} aria-label="Close modal">
          &times;
        </CloseButton>
      </ModalContent>
    </ModalContainer>
  );
};

export default DetailsModal;
