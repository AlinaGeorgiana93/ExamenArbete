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

  const handleCreateGraph = () => {
    // Prepare the complete data object to pass to GraphPage
    const graphData = {
      date: state.date || new Date().toISOString(),
      moodRating: state.moodKind?.rating,
      activityRating: state.activityLevel?.rating,
      appetiteRating: state.appetiteLevel?.rating,
      sleepRating: state.sleepLevel?.rating,
      // Include the full objects for reference
      moodKind: state.moodKind,
      activityLevel: state.activityLevel,
      appetiteLevel: state.appetiteLevel,
      sleepLevel: state.sleepLevel,
      patientId: patientId  // Ensure patientId is included
    };

    navigate(`/graph/${patientId}`, { state: graphData });
  };

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
          {state.moodKind?.label} (Rating: {state.moodKind?.rating || 'N/A'}, ID: {state.moodKind?.moodKindId || 'N/A'})
        </Value>
      </DataItem>
      
      <DataItem>
        <Label>Activity:</Label>
        <Value>
          {state.activityLevel?.label} (Rating: {state.activityLevel?.rating || 'N/A'}, ID: {state.activityLevel?.activityLevelId || 'N/A'})
        </Value>
      </DataItem>
      
      <DataItem>
        <Label>Appetite:</Label>
        <Value>
          {state.appetiteLevel?.label} (Rating: {state.appetiteLevel?.rating || 'N/A'}, ID: {state.appetiteLevel?.appetiteLevelId || 'N/A'})
        </Value>
      </DataItem>
      
      <DataItem>
        <Label>Sleep:</Label>
        <Value>
          {state.sleepLevel?.label} (Rating: {state.sleepLevel?.rating || 'N/A'}, ID: {state.sleepLevel?.sleepLevelId || 'N/A'})
        </Value>
      </DataItem>
      
      <DataItem>
        <Label>Date Recorded:</Label>
        <Value>{state.date ? new Date(state.date).toLocaleString() : 'Not recorded'}</Value>
      </DataItem>

      <ButtonGroup>
        <Button onClick={handleCreateGraph}>
          Save and Create Graph
        </Button>
        <Button onClick={() => navigate(`/patient/${patientId}`)}>
          Go Back
        </Button>
      </ButtonGroup>
    </ReviewContainer>
  );
}

export default PatientDataReview;