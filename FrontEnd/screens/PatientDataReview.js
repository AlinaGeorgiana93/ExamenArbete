import React from 'react';
import styled, { createGlobalStyle } from 'styled-components';
import { useParams, useNavigate, useLocation } from 'react-router-dom';
import { useTranslation } from 'react-i18next';
import axios from 'axios';

const GlobalStyle = createGlobalStyle`
  * {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
  }

  body {
    font-family: 'Poppins', sans-serif;
    background-color: #f4f6f8;
    color: #333;
  }
`;

const PageContainer = styled.div`
  display: flex;
  justify-content: center;
  padding: 40px 20px;
`;

const ReviewCard = styled.div`
  background-color: #fff;
  padding: 40px;
  border-radius: 20px;
  width: 100%;
  max-width: 800px;
  box-shadow: 0 8px 25px rgba(0, 0, 0, 0.1);
  animation: fadeIn 0.5s ease-in-out;

  @keyframes fadeIn {
    from {
      opacity: 0;
      transform: translateY(10px);
    }
    to {
      opacity: 1;
      transform: translateY(0);
    }
  }

  @media (max-width: 600px) {
    padding: 20px;
    border-radius: 10px;
  }
`;

const Title = styled.h2`
  font-size: 2.5rem;
  color: #125358;
  margin-bottom: 30px;
  border-bottom: 2px solid #e0e0e0;
  padding-bottom: 15px;
`;

const DataItem = styled.div`
  display: flex;
  flex-direction: column;
  padding: 20px;
  margin-bottom: 16px;
  background-color: #f9fafa;
  border: 1px solid #e2e8f0;
  border-radius: 12px;
`;

const Label = styled.span`
  font-weight: 600;
  color: #0f3d3e;
  margin-bottom: 5px;
  font-size: 1.1rem;
`;

const Value = styled.span`
  font-size: 1rem;
  color: #333;
`;

const Skeleton = styled.div`
  height: 1.2em;
  width: 150px;
  background-color: #ddd;
  border-radius: 6px;
  display: inline-block;
`;

const ButtonGroup = styled.div`
  display: flex;
  flex-wrap: wrap;
  gap: 16px;
  margin-top: 30px;
`;

const PrimaryButton = styled.button`
  flex: 1;
  min-width: 150px;
  padding: 12px 20px;
  font-size: 1rem;
  border-radius: 10px;
  font-weight: 600;
  border: none;
  cursor: pointer;
  background-color: #28889b;
  color: #fff;
  transition: background-color 0.3s ease;

  &:hover {
    &:hover {
    background-color: #e0e0e0;
  }
`;

const SecondaryButton = styled.button`
  flex: 1;
  min-width: 150px;
  padding: 12px 20px;
  font-size: 1rem;
  border-radius: 10px;
  font-weight: 600;
  border: none;
  cursor: pointer;
  background-color: #28889b;
  color:  #f0f0f0;
  transition: background-color 0.3s ease;

  &:hover {
    background-color: #e0e0e0;
  }
`;


function PatientDataReview() {
  const { t } = useTranslation();
  const { patientId } = useParams();
  const { state } = useLocation();
  const navigate = useNavigate();
  const [patientInfo, setPatientInfo] = React.useState(null);
  const [loading, setLoading] = React.useState(true);

  React.useEffect(() => {
    const fetchPatientInfo = async () => {
      try {
        const response = await axios.get(`https://localhost:7066/api/Patient/ReadItem?id=${patientId}`);
        setPatientInfo(response.data.item);
      } catch (error) {
        console.error('Error fetching patient info:', error);
      } finally {
        setLoading(false);
      }
    };

    fetchPatientInfo();
  }, [patientId]);

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

  const getRatingDisplay = (label, rating) =>
    `${label || t('not_specified')} (Rating: ${rating ?? t('not_available')})`;

  if (!state || !patientId) {
    return (
      <>
        <GlobalStyle />
        <PageContainer>
          <ReviewCard>
            <Title>{t('no_data_to_review')}</Title>
            <p>{t('no_patient_data')}</p>
            <SecondaryButton onClick={() => navigate('/')}>
              {t('go_back_home')}
            </SecondaryButton>
          </ReviewCard>
        </PageContainer>
      </>
    );
  }

  return (
    <>
      <GlobalStyle />
      <PageContainer>
        <ReviewCard>
          <Title>{t('review_patient_data')}</Title>

          <DataItem>
            <Label>{t('patient_name')}</Label>
            <Value>
              {loading ? <Skeleton /> : patientInfo ? `${patientInfo.firstName} ${patientInfo.lastName}` : t('not_found')}
            </Value>
          </DataItem>

          <DataItem>
            <Label>{t('mood')}</Label>
            <Value>{getRatingDisplay(state.moodKind?.label, state.moodKind?.rating)}</Value>
          </DataItem>

          <DataItem>
            <Label>{t('activity')}</Label>
            <Value>{getRatingDisplay(state.activityLevel?.label, state.activityLevel?.rating)}</Value>
          </DataItem>

          <DataItem>
            <Label>{t('appetite')}</Label>
            <Value>{getRatingDisplay(state.appetiteLevel?.label, state.appetiteLevel?.rating)}</Value>
          </DataItem>

          <DataItem>
            <Label>{t('sleep')}</Label>
            <Value>{getRatingDisplay(state.sleepLevel?.label, state.sleepLevel?.rating)}</Value>
          </DataItem>

          <DataItem>
            <Label>{t('date_recorded')}</Label>
            <Value>{state.date ? new Date(state.date).toLocaleString() : t('not_recorded')}</Value>
          </DataItem>

          <ButtonGroup>
            <PrimaryButton onClick={handleCreateGraph}>{t('save_and_create_graph')}</PrimaryButton>
            <SecondaryButton onClick={() => navigate(`/patient/${patientId}`)}>{t('go_back')}</SecondaryButton>
          </ButtonGroup>
        </ReviewCard>
      </PageContainer>
    </>
  );
}

export default PatientDataReview;
