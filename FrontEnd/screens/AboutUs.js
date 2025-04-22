import React from 'react';
import styled, { createGlobalStyle } from 'styled-components';
import logo1 from '../src/media/logo1.png';
import patient1 from '../src/media/patient1.jpg'; // Bild på "er"
import '../src/index.css';
import { Link } from 'react-router-dom';

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
  return (
    <>
      <GlobalStyle />
      <Link to="/" style={{ position: 'fixed', top: '15px', right: '15px', zIndex: '2' }}>
        <img src={logo1} alt="Logo" style={{ width: '150px' }} />
      </Link>
      <PageContainer>
        <Title>Om oss</Title>
        <Text>
          Vi är fyra engagerade tjejer som studerar systemutveckling och har tillsammans utvecklat den här webbplatsen som en del av vårt utbildningsprojekt. Vårt mål är att skapa en smidig och användarvänlig plattform som underlättar för patienter att rapportera sitt mående dagligen.
        </Text>
        <Text>
          Genom att kombinera teknik med omtanke vill vi bidra till en bättre kommunikation mellan patienter och vårdpersonal. Vi tror att små insatser varje dag kan göra stor skillnad för människors hälsa över tid.
        </Text>
        <Text>
          Plattformen är utformad med fokus på tillgänglighet, trygghet och enkelhet — och vi är stolta över att ha byggt något som kan göra vardagen lite lättare för andra.
        </Text>
        <Text>
          Har du frågor, tankar eller feedback får du gärna höra av dig.
        </Text>
        <Text>
          Tack för att du använder vår tjänst! 💙 
        </Text>

        <TeamGrid>
          <TeamMember>
            <ProfileImage src={patient1} alt="Teammedlem 1" />
            <Name>Parisa</Name>
          </TeamMember>
          <TeamMember>
            <ProfileImage src={patient1} alt="Teammedlem 2" />
            <Name>Alina</Name>
          </TeamMember>
          <TeamMember>
            <ProfileImage src={patient1} alt="Teammedlem 3" />
            <Name>Mona</Name>
          </TeamMember>
          <TeamMember>
            <ProfileImage src={patient1} alt="Teammedlem 4" />
            <Name>Nagihan</Name>
          </TeamMember>
        </TeamGrid>
      </PageContainer>
    </>
  );
};

export default AboutUsPage;
