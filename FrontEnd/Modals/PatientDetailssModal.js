import React from 'react';
import styled from 'styled-components';

const ModalOverlay = styled.div`
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
`;

const ModalContainer = styled.div`
  background: white;
  padding: 20px;
  border-radius: 8px;
  max-width: 400px;
  width: 100%;
`;

const ModalHeader = styled.h3`
  margin-bottom: 25px;
`;

const ModalButton = styled.button`
  padding: 10px;
  background: #125358;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  margin-right: 10px;
  
  &:hover {
    background: #00d4ff;
  }
`;

const CloseButton = styled(ModalButton)`
  background: #ccc;
`;

const PatientDetailsModal = ({ patientMember, onClose, onEdit, onDelete }) => {
  return (
    <ModalOverlay>
      <ModalContainer>
        <ModalHeader>Patient Details</ModalHeader>
        <p><strong>First Name:</strong> {patientMember.firstName}</p>
        <p><strong>Last Name:</strong> {patientMember.lastName}</p>
        <p><strong>Personal Number:</strong> {patientMember.personalNumber}</p>
        
        <ModalButton onClick={() => onEdit(patientMember)}>Edit</ModalButton>
        <ModalButton onClick={() => onDelete(patientMember._id)}>Delete</ModalButton>
        <CloseButton onClick={onClose}>Close</CloseButton>
      </ModalContainer>
    </ModalOverlay>
  );
};

export default PatientDetailsModal;
