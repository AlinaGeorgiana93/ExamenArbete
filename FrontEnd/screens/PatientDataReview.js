import React from 'react';
import styled from 'styled-components';
import { useParams, useNavigate, useLocation } from 'react-router-dom';
import { useTranslation } from 'react-i18next';

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
  const { t } = useTranslation();  // Access i18n translation function
  const { patientId } = useParams();
  const { state } = useLocation();
  const navigate = useNavigate();

  if (!state) {
    return (
      <ReviewContainer>
        <h2>{t('no_data_to_review')}</h2>
        <p>{t('no_patient_data')}</p>
        <Button onClick={() => navigate(`/patient/${patientId}`)}>
          {t('go_back_to_patient_page')}
        </Button>
      </ReviewContainer>
    );
  }

  return (
    <ReviewContainer>
      <h2>{t('review_patient_data')}</h2>
      
      <DataItem>
        <Label>{t('patient_id')}</Label>
        <Value>{patientId}</Value>
      </DataItem>
      
      <DataItem>
        <Label>{t('mood')}</Label>
        <Value>
          {state.moodKind?.label} ({t('rating')}: {state.moodKind?.rating}, {t('id')}: {state.moodKind?.moodKindId})
        </Value>
      </DataItem>
      
      <DataItem>
        <Label>{t('activity')}</Label>
        <Value>
          {state.activityLevel?.label} ({t('rating')}: {state.activityLevel?.rating}, {t('id')}: {state.activityLevel?.activityLevelId})
        </Value>
      </DataItem>
      
      <DataItem>
        <Label>{t('appetite')}</Label>
        <Value>
          {state.appetiteLevel?.label} ({t('rating')}: {state.appetiteLevel?.rating}, {t('id')}: {state.appetiteLevel?.appetiteLevelId})
        </Value>
      </DataItem>
      
      <DataItem>
        <Label>{t('sleep')}</Label>
        <Value>
          {state.sleepLevel?.label} ({t('rating')}: {state.sleepLevel?.rating}, {t('id')}: {state.sleepLevel?.sleepLevelId})
        </Value>
      </DataItem>
      
      <DataItem>
        <Label>{t('date_recorded')}</Label>
        <Value>{new Date(state.date).toLocaleString()}</Value>
      </DataItem>

      <ButtonGroup>
        <Button onClick={() => navigate(`/graph/${patientId}`)}>
          {t('save_and_create_graph')}
        </Button>
        <Button onClick={() => navigate(`/patient/${patientId}`)}>
          {t('go_back')}
        </Button>
      </ButtonGroup>
    </ReviewContainer>
  );
}

export default PatientDataReview;
