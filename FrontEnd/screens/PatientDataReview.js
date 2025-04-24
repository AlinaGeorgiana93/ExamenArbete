import React from 'react';
import styled from 'styled-components';
import { useParams, useNavigate, useLocation } from 'react-router-dom';

const ReviewContainer = styled.div`
  background-color: #ffffffee;
  padding: 30px;
  border-radius: 16px;
  max-width: 500px;
  width: 100%;
  box-shadow: 0 8px 20px rgba(0, 0, 0, 0.2);
  margin: 20px auto;
`;

const DataItem = styled.div`
  margin-bottom: 15px;
  padding: 15px;
  background-color: #f5f5f5;
  border-radius: 8px;
`;

const Label = styled.span`
  font-weight: bold;
  color: #125358;
`;

const Value = styled.span`
  margin-left: 10px;
`;

const ButtonGroup = styled.div`
  display: flex;
  gap: 15px;
  margin-top: 30px;
`;

const Button = styled.button`
  padding: 12px 24px;
  border-radius: 8px;
  border: none;
  cursor: pointer;
  font-weight: bold;
  flex: 1;
  transition: all 0.3s ease;

  &:first-child {
    background-color: #125358;
    color: white;
    
    &:hover {
      background-color: #0e4246;
    }
  }
  
  &:last-child {
    background-color: #f5f5f5;
    color: #333;
    
    &:hover {
      background-color: #e0e0e0;
    }
  }
`;
function PatientDataReview() {
  const { patientId } = useParams();
  const { state } = useLocation();
  const navigate = useNavigate();

  if (!state) {
    return (
      <ReviewContainer>
        <h2>No Data to Review</h2>
        <p>No patient data was provided. Please go back and submit the form again.</p>
        <Button onClick={() => navigate(`/patient/${patientId}`)}>
          Go Back to Patient Page
        </Button>
      </ReviewContainer>
    );
  }

  return (
    <ReviewContainer>
      <h2>Review Patient Data</h2>
      
      <DataItem>
        <Label>Patient ID:</Label>
        <Value>{patientId}</Value>
      </DataItem>
      
      <DataItem>
        <Label>Mood:</Label>
        <Value>
          {state.moodKind?.label} (Rating: {state.moodKind?.rating}, ID: {state.moodKind?.moodKindId})
        </Value>
      </DataItem>
      
      <DataItem>
        <Label>Activity:</Label>
        <Value>
          {state.activityLevel?.label} (Rating: {state.activityLevel?.rating}, ID: {state.activityLevel?.activityLevelId})
        </Value>
      </DataItem>
      
      <DataItem>
        <Label>Appetite:</Label>
        <Value>
          {state.appetiteLevel?.label} (Rating: {state.appetiteLevel?.rating}, ID: {state.appetiteLevel?.appetiteLevelId})
        </Value>
      </DataItem>
      
      <DataItem>
        <Label>Sleep:</Label>
        <Value>
          {state.sleepLevel?.label} (Rating: {state.sleepLevel?.rating}, ID: {state.sleepLevel?.sleepLevelId})
        </Value>
      </DataItem>
      
      <DataItem>
        <Label>Date Recorded:</Label>
        <Value>{new Date(state.date).toLocaleString()}</Value>
      </DataItem>

      <ButtonGroup>
        <Button onClick={() => navigate(`/graph/${patientId}`)}>
          Save and Create a graph
        </Button>
        <Button onClick={() => navigate(`/patient/${patientId}`)}>
          Go Back
        </Button>
      </ButtonGroup>
    </ReviewContainer>
  );
}

export default PatientDataReview;