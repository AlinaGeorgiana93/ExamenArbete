import styled, { createGlobalStyle } from 'styled-components';
import logo1 from '../src/media/logo1.png';
import Alina from '../src/media/Alina.jpg';
import Parisa from '../src/media/Parisa.jpg';
import Mona from '../src/media/Mona.jpg';
import Nagi from '../src/media/Nagi.jpg';
import React, { useRef } from 'react';
import '../src/index.css';
import { Link } from 'react-router-dom';
import { useTranslation } from 'react-i18next';
import videoFile from '../src/media/patient.mp4';

const GlobalStyle = createGlobalStyle`
  * {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
  }
  body {
    font-family: 'Times New Roman', cursive, sans-serif;
    background: linear-gradient(135deg, #e0f7f9, #cceae7, #b2dfdb); /* ← uppdaterad gradient */
    background-attachment: fixed;
    min-height: 100vh;
    color: #000;
    position: relative;
    overflow-x: hidden;
  }
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

const PageContainer = styled.div`
  background-color: rgba(255, 255, 255, 0.9);
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

const TeamHeading = styled.h2`
  text-align: center;
  color: #125358;
  margin-top: 40px;
  margin-bottom: 25px;
  font-size: 1.8rem;
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

const Role = styled.span`
  font-size: 0.9rem;
  color: #555;
  margin-top: 5px;
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

const Overlay = styled.div`
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.3); 
  z-index: -1;
`;

const ScrollTopButton = styled.button`
  position: fixed;
  bottom: 30px;
  right: 30px;
  background: #125358;
  color: #fff;
  border: none;
  padding: 10px 15px;
  border-radius: 8px;
  cursor: pointer;
  z-index: 10; 
`;
const CopyrightText = styled.div`
  text-align: center;
  color: #333;
  font-size: 14px;
  margin-top: 50px; 
  padding: 10px 0; 
`;


const scrollToTop = () => {
  const container = document.getElementById('page-container');
  container.scrollTo({ top: 0, behavior: 'smooth' });
};

const AboutUsPage = () => {
  const { t } = useTranslation();
  const containerRef = useRef(null);
  const currentYear = new Date().getFullYear(); // Hämtar det aktuella året

  return (
    <div>
      <GlobalStyle />
      <Link
        to="/"
        style={{ position: 'fixed', top: '15px', right: '15px', zIndex: '2' }}
      >
        <img src={logo1} alt="Logo" style={{ width: '150px' }} />
      </Link>
      <PageContainer id="page-container" ref={containerRef}>
        <Title>Om AutiGraph</Title>
        <Text>
          Vi är fyra engagerade tjejer som studerar systemutveckling och har tillsammans utvecklat den här webbplatsen som en del av vårt utbildningsprojekt. Vårt mål är att skapa en smidig och användarvänlig plattform som underlättar för vårdpersonal att spåra och rapportera patienters mående dagligen.
        </Text>
        <Text>
          Genom att kombinera teknik med omtanke vill vi bidra till en bättre kommunikation mellan vårdpersonal och patienter. Vi tror att små insatser varje dag kan göra stor skillnad för människors hälsa över tid.
        </Text>
        <Text>
          Plattformen är utformad med fokus på tillgänglighet, trygghet och enkelhet — och vi är stolta över att ha byggt något som kan göra vården mer effektiv och ge patienter en bättre upplevelse.
        </Text>

        <Section>
          <SectionTitle>Vad är AutiGraph?</SectionTitle>
          <SectionText>
            AutiGraph är en digital plattform som gör det enkelt för vårdpersonal att dagligen rapportera och spåra patienternas mående. Genom att visualisera hälsodata hjälper vi vårdpersonal att följa utvecklingen över tid och fatta bättre beslut tillsammans med patienterna. Plattformen gör det möjligt att få en tydlig översikt av varje individs välmående, vilket underlättar kommunikationen och ger en mer effektiv vård.
          </SectionText>
        </Section>

        <Section>
          <SectionTitle>Vår vision</SectionTitle>
          <SectionText>
            Vi tror att alla ska kunna kommunicera sitt välmående enkelt, tryggt och visuellt. Vår vision är att göra det möjligt för människor att uttrycka sitt inre tillstånd på ett sätt som både de själva och andra kan förstå.
          </SectionText>
        </Section>

        <Section>
          <SectionTitle>Vilka är vi?</SectionTitle>
          <SectionText>
            Vi är ett dedikerat team av blivande systemutvecklare med passion för att skapa digitala lösningar som gör skillnad. Med våra olika styrkor inom utveckling, design och empati, bygger vi produkter som sätter människan i centrum.
          </SectionText>
        </Section>

        <Section>
          <SectionTitle>Vår historia</SectionTitle>
          <SectionText>
            <ul>
              <li>Grundades 2025</li>
              <li>Självfinansierat: Projektet startades utan externa investeringar.</li>
              <li>Mål: Att skapa en plattform som underlättar för personer att kontinuerligt dokumentera sitt mående på ett enkelt och tryggt sätt.</li>
              <li>Utvecklingsfokus: Vi fokuserade på användarvänlighet, tillgänglighet och att göra det möjligt för både patienter och vårdpersonal att följa utvecklingen över tid.</li>
            </ul>
          </SectionText>
          <Text>
            Tack för att du använder vår tjänst! 💙
          </Text>
        </Section>

        <TeamHeading>Träffa AutiGraph-teamet</TeamHeading>
        <TeamGrid>
          <TeamMember>
            <ProfileImage src={Parisa} alt="Teammedlem 1" />
            <Name>Parisa A.</Name>
            <Role>Fullstack Developer</Role>
          </TeamMember>
          <TeamMember>
            <ProfileImage src={Alina} alt="Teammedlem 2" />
            <Name>Alina M.</Name>
            <Role>Fullstack Developer</Role>
          </TeamMember>
          <TeamMember>
            <ProfileImage src={Mona} alt="Teammedlem 3" />
            <Name>Mona E.</Name>
            <Role>Fullstack Developer</Role>
          </TeamMember>
          <TeamMember>
            <ProfileImage src={Nagi} alt="Teammedlem 4" />
            <Name>Nagihan C.</Name>
            <Role>Fullstack Developer</Role>
          </TeamMember>
        </TeamGrid>
        <CopyrightText>
  Copyright {currentYear} AutiGraph. All rights reserved. Proudly made in Sweden.
</CopyrightText>
      </PageContainer>
      
      <ScrollTopButton onClick={scrollToTop}>↑ Till toppen</ScrollTopButton>

      {/* <VideoContainer>
        <video
          src={videoFile}
          autoPlay={true}
          loop={true}
          muted={true}
          controls={false}
          style={{
            objectFit: 'cover',
            width: '100%',
            height: '100%',
            position: 'absolute',
            top: 0,
            left: 0,
            zIndex: -2, 
          }}
        />
        <Overlay /> 
      </VideoContainer> */}
     
    </div>
  );
};

export default AboutUsPage;
