import React from 'react';
import styled, { createGlobalStyle } from 'styled-components';
import { Link } from 'react-router-dom';
import { useTranslation } from 'react-i18next';
import logo1 from '../src/media/logo1.png';
import Alina from '../src/media/Alina.jpg'; 
import Parisa from '../src/media/Parisa.jpg'; 
import Mona from '../src/media/Mona.jpg'; 
import Nagi from '../src/media/Nagi.jpg';
import checklist from '../src/media/checklist.jpg';
import videoFile from '../src/media/patient.mp4';

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
  height: 90vh;
  overflow-y: auto;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
  color: #000;
  scrollbar-width: thin;
  scrollbar-color: #50D9E6 #ffffff;
`;

const Title = styled.h1`
  text-align: center;
  color: #125358;
  margin-bottom: 30px;
`;

const Text = styled.p`
  font-size: 18px;
  color: #333;
  line-height: 1.6;
  margin-bottom: 20px;
  text-align: center;
`;

const Section = styled.section`
  margin-bottom: 50px;
`;

const SectionTitle = styled.h2`
  color: #125358;
  font-size: 1.8rem;
  margin-bottom: 15px;
  text-align: center;
`;

const SectionText = styled.p`
  font-size: 17px;
  color: #333;
  line-height: 1.6;
  text-align: center;
  margin-bottom: 30px;
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

const Name = styled.span`
  font-weight: bold;
  color: #125358;
`;

const Role = styled.span`
  font-size: 0.9rem;
  color: #555;
  margin-top: 5px;
`;

const ProfileImage = styled.img`
  width: 120px;
  height: 120px;
  object-fit: cover;
  border-radius: 50%;
  border: 3px solid #125358;
  margin-bottom: 10px;
  transition: transform 0.3s ease;

  &:hover {
    transform: scale(1.05);
  }
`;

const VideoContainer = styled.div`
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  z-index: -1;
  display: flex;
  justify-content: center;
  align-items: center;
`;

const AboutUsPage = () => {
  const { t } = useTranslation();

  return (
    <div>
      <GlobalStyle />
      <Link to="/" style={{ position: 'fixed', top: '15px', right: '15px', zIndex: '2' }}>
        <img src={logo1} alt="Logo" style={{ width: '150px' }} />
      </Link>
      <PageContainer>
        <img src={checklist} alt="Checklist" style={{ width: '75%', height: 'auto', display: 'block', margin: '0 auto' }} />

        <Title>{t('aboutUsTitle')}</Title>
        <Text>{t('aboutUsText1')}</Text>
        <Text>{t('aboutUsText2')}</Text>
        <Text>{t('aboutUsText3')}</Text>

        <Section>
          <SectionTitle>{t('whatIsAutiGraphTitle')}</SectionTitle>
          <SectionText>{t('whatIsAutiGraphText')}</SectionText>
        </Section>

        <Section>
          <SectionTitle>{t('ourVisionTitle')}</SectionTitle>
          <SectionText>{t('ourVisionText')}</SectionText>
        </Section>

        <Section>
          <SectionTitle>{t('meetTeamTitle')}</SectionTitle>
        </Section>

        <TeamGrid>
          <TeamMember>
            <ProfileImage src={Parisa} alt="Parisa" />
            <Name>Parisa A.</Name>
            <Role>Fullstack Developer</Role>
          </TeamMember>
          <TeamMember>
            <ProfileImage src={Alina} alt="Alina" />
            <Name>Alina M.</Name>
            <Role>Fullstack Developer</Role>
          </TeamMember>
          <TeamMember>
            <ProfileImage src={Mona} alt="Mona" />
            <Name>Mona E.</Name>
            <Role>Fullstack Developer</Role>
          </TeamMember>
          <TeamMember>
            <ProfileImage src={Nagi} alt="Nagi" />
            <Name>Nagihan C.</Name>
            <Role>Fullstack Developer</Role>
          </TeamMember>
        </TeamGrid>
      </PageContainer>

      <VideoContainer>
        <video
          src={videoFile}
          autoPlay
          loop
          muted
          controls={false}
          style={{
            objectFit: 'cover',
            width: '100%',
            height: '100%',
            position: 'absolute',
            top: 0,
            left: 0,
            zIndex: -1,
          }}
        />
      </VideoContainer>
    </div>
  );
};

export default AboutUsPage;
