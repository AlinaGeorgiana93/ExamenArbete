import React from 'react';
import styled, { createGlobalStyle } from 'styled-components';
import { useParams, useNavigate, useLocation } from 'react-router-dom';
import { useTranslation } from 'react-i18next';

// GlobalStyle will be applied to the whole page to adjust the background
const GlobalStyle = createGlobalStyle`
  body {
    background: linear-gradient(135deg, #e0f7f9, #cceae7, #b2dfdb); /* Same gradient as StaffPage */
    min-height: 100vh; /* Ensure the background covers the full height */
    margin: 0;
    font-family: 'Times New Roman', cursive, sans-serif;
    color: rgb(29, 29, 29);
    font-size: 20px; /* Increased global font size */
  }
`;

const ReviewContainer = styled.div`
  background-color: #ffffffee; /* Transparent white background for the content area */
  padding: 80px; /* Larger padding for more space inside */
  border-radius: 16px;
  width: 100%; /* Full width container */
  box-shadow: 0 8px 20px rgba(0, 0, 0, 0.2);
  margin: 40px auto; /* Increased margin for more space around the container */

  h2 {
    font-size: 2.9rem; /* Larger font size for the header */
  }
`;

const DataItem = styled.div`
  margin-bottom: 15px;
  padding: 15px;
  background-color: #f5f5f5;
  border-radius: 8px;
  font-size: 1.8rem; /* Larger text for DataItem */
`;

const Label = styled.span`
  font-weight: bold;
  color: #125358;
  font-size: 1.9rem; /* Larger font size for label */
`;

const Value = styled.span`
  margin-left: 10px;
  font-size: 1.8rem; /* Larger font size for value */
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
  font-size: 1.2rem; /* Larger font size for buttons */
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
  const { t } = useTranslation();
  const { patientId } = useParams();
  const { state } = useLocation();
  const navigate = useNavigate();

  const handleCreateGraph = () => {
    const graphData = {
      date: state.date || new Date().toISOString(),
      moodRating: state.moodKind?.rating,
      activityRating: state.activityLevel?.rating,
      appetiteRating: state.appetiteLevel?.rating,
      sleepRating: state.sleepLevel?.rating,
      moodKind: state.moodKind,
      activityLevel: state.activityLevel,
      appetiteLevel: state.appetiteLevel,
      sleepLevel: state.sleepLevel,
      patientId: patientId
    };

    navigate(`/graph/${patientId}`, { state: graphData });
  };

  if (!state) {
    return (
      <>
        <GlobalStyle /> {/* GlobalStyle applied to the page */}
        <ReviewContainer>
          <h2>{t('no_data_to_review')}</h2>
          <p>{t('no_patient_data')}</p>
          <Button onClick={() => navigate(`/patient/${patientId}`)}>
            {t('go_back_to_patient_page')}
          </Button>
        </ReviewContainer>
      </>
    );
  }

  return (
    <>
      <GlobalStyle /> {/* GlobalStyle applied to the page */}
      <ReviewContainer>
        <h2>{t('review_patient_data')}</h2>

        <DataItem>
          <Label>{t('patient_id')}</Label>
          <Value>{patientId}</Value>
        </DataItem>

        <DataItem>
          <Label>{t('mood')}</Label>
          <Value>
            {state.moodKind?.label} (Rating: {state.moodKind?.rating || 'N/A'}, ID: {state.moodKind?.moodKindId || 'N/A'})
          </Value>
        </DataItem>

        <DataItem>
          <Label>{t('activity')}</Label>
          <Value>
            {state.activityLevel?.label} (Rating: {state.activityLevel?.rating || 'N/A'}, ID: {state.activityLevel?.activityLevelId || 'N/A'})
          </Value>
        </DataItem>

        <DataItem>
          <Label>{t('appetite')}</Label>
          <Value>
            {state.appetiteLevel?.label} (Rating: {state.appetiteLevel?.rating || 'N/A'}, ID: {state.appetiteLevel?.appetiteLevelId || 'N/A'})
          </Value>
        </DataItem>

        <DataItem>
          <Label>{t('sleep')}</Label>
          <Value>
            {state.sleepLevel?.label} (Rating: {state.sleepLevel?.rating || 'N/A'}, ID: {state.sleepLevel?.sleepLevelId || 'N/A'})
          </Value>
        </DataItem>

        <DataItem>
          <Label>Date Recorded:</Label>
          <Value>{state.date ? new Date(state.date).toLocaleString() : 'Not recorded'}</Value>
        </DataItem>

        <ButtonGroup>
          <Button onClick={handleCreateGraph}>Save and Create Graph</Button>
          <Button onClick={() => navigate(`/patient/${patientId}`)}>{t('go_back')}</Button>
        </ButtonGroup>
      </ReviewContainer>
    </>
  );
}

export default PatientDataReview;
