import React from 'react';
import styled, { createGlobalStyle } from 'styled-components';
import logo1 from '../src/media/logo1.png';
import patient1 from '../src/media/patient1.jpg'; 
import '../src/index.css';
import { Link } from 'react-router-dom';
import { useTranslation } from 'react-i18next';  // Import the translation hook

const GlobalStyle = createGlobalStyle`
  * {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
  }
  body {
    font-family: 'Times New Roman', cursive, sans-serif;
    background: linear-gradient(135deg, #3B878C, #00d4ff, #006E75, #50D9E6, #1A5B61);
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100vh;
    color: #fff;
    position: relative;
  }
`;

const PageContainer = styled.div`
  background-color: #fff;
  padding: 40px;
  border-radius: 8px;
  width: 100%;
  max-width: 700px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
  color: #000;
`;

const Title = styled.h1`
  text-align: center;
  color: #125358;
  margin-bottom: 20px;
`;

const Text = styled.p`
  font-size: 1rem;
  color: #333;
  line-height: 1.6;
  margin-bottom: 15px;
`;

const TeamGrid = styled.div`
  display: flex;
  justify-content: space-around;
  flex-wrap: wrap;
  margin-top: 30px;
`;

const TeamMember = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  margin: 15px;
`;

const ProfileImage = styled.img`
  width: 120px;
  height: 120px;
  object-fit: cover;
  border-radius: 50%;
  border: 3px solid #125358;
  margin-bottom: 10px;
`;

const Name = styled.span`
  font-weight: bold;
  color: #125358;
`;

const AboutUsPage = () => {
  const { t } = useTranslation(); // Use the translation hook

  return (
    <>
      <GlobalStyle />
      <Link to="/" style={{ position: 'fixed', top: '15px', right: '15px', zIndex: '2' }}>
        <img src={logo1} alt="Logo" style={{ width: '150px' }} />
      </Link>
      <PageContainer>
        <Title>{t('aboutUsTitle')}</Title>
        <Text>{t('description1')}</Text>
        <Text>{t('description2')}</Text>
        <Text>{t('description3')}</Text>
        <Text>{t('description4')}</Text>
        <Text>{t('description5')}</Text>

        <TeamGrid>
          <TeamMember>
            <ProfileImage src={patient1} alt="Teammedlem 1" />
            <Name>{t('team1')}</Name>
          </TeamMember>
          <TeamMember>
            <ProfileImage src={patient1} alt="Teammedlem 2" />
            <Name>{t('team2')}</Name>
          </TeamMember>
          <TeamMember>
            <ProfileImage src={patient1} alt="Teammedlem 3" />
            <Name>{t('team3')}</Name>
          </TeamMember>
          <TeamMember>
            <ProfileImage src={patient1} alt="Teammedlem 4" />
            <Name>{t('team4')}</Name>
          </TeamMember>
        </TeamGrid>
      </PageContainer>
    </>
  );
};

export default AboutUsPage;
