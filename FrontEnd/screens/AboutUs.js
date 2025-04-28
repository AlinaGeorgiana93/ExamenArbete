import React from 'react';
import styled, { createGlobalStyle } from 'styled-components';
import logo1 from '../src/media/logo1.png';
import Alina from '../src/media/Alina.jpg'; 
import Parisa from '../src/media/Parisa.jpg'; 
import Mona from '../src/media/Mona.jpg'; 
import Nagi from '../src/media/Nagi.jpg';

import '../src/index.css';
import { Link } from 'react-router-dom';
import { useTranslation } from 'react-i18next'; // Import the translation hook
import videoFile from '../src/media/patient.mp4'; // Importera videon

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
    background-attachment: fixed;
    justify-content: center;
    align-items: center;
    height: 100vh;
    color: #fff;
    position: relative;
  }
`;

const Section = styled.section`
  margin-bottom: 50px; /* Större mellanrum mellan sektionerna */
`;

const SectionTitle = styled.h2`
  color: #125358;
  font-size: 1.8rem; /* Större fontstorlek */
  margin-bottom: 15px; /* Större avstånd mellan rubrik och text */
  text-align: center;
`;

const SectionText = styled.p`
  font-size: 17px;
  color: #333;
  line-height: 1.6;
  text-align: center;
  margin-bottom: 30px; /* Mer mellanrum efter texten */
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
  margin-bottom: 30px; /* Större avstånd till rubrikens text */
`;

const Text = styled.p`
  font-size: 18px;
  color: #333;
  line-height: 1.6;
  margin-bottom: 20px; /* Större mellanrum */
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
  margin-bottom: 25px; /* Mer mellanrum före och efter */
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
  z-index: -1; /* För att hålla videon i bakgrunden */
  display: flex;
  justify-content: center;
  align-items: center;
`;

const AboutUsPage = () => {
  const { t } = useTranslation(); // Use the translation hook

  return (
    <div>
      <GlobalStyle />
      <Link
        to="/"
        style={{ position: 'fixed', top: '15px', right: '15px', zIndex: '2' }}
      >
        <img src={logo1} alt="Logo" style={{ width: '150px' }} />
      </Link>
      <PageContainer>
        <Title>Om AutiGraph</Title>
        <Text>
          Vi är fyra engagerade tjejer som studerar systemutveckling och har tillsammans utvecklat den här webbplatsen som en del av vårt utbildningsprojekt. Vårt mål är att skapa en smidig och användarvänlig plattform som underlättar för patienter att rapportera sitt mående dagligen.
        </Text>
        <Text>
          Genom att kombinera teknik med omtanke vill vi bidra till en bättre kommunikation mellan patienter och vårdpersonal. Vi tror att små insatser varje dag kan göra stor skillnad för människors hälsa över tid.
        </Text>
        <Text>
          Plattformen är utformad med fokus på tillgänglighet, trygghet och enkelhet — och vi är stolta över att ha byggt något som kan göra vardagen lite lättare för andra.
        </Text>

        <Section>
          <SectionTitle>Vad är AutiGraph?</SectionTitle>
          <SectionText>
            AutiGraph är en digital plattform som gör det enkelt för patienter att dagligen rapportera sitt mående. Genom att visualisera hälsodata hjälper vi både patienter och vårdpersonal att följa utvecklingen över tid och fatta bättre beslut tillsammans.
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
            AutiGraph föddes 2025 som ett utbildningsprojekt, helt självfinansierat, med syftet att skapa en plattform som gör vardagen enklare för personer med behov av att kontinuerligt dokumentera sitt mående.
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
      </PageContainer>

      {/* Video container with the updated video settings */}
      <VideoContainer>
        <video
          src={videoFile} // Källan för videon
          autoPlay={true}  // Starta automatiskt
          loop={true}      // Spela om när den är klar
          muted={true}     // Håll videon ljudlös (valfritt)
          controls={false} // Döljer kontrollerna
          style={{
            objectFit: 'cover', // Gör att videon täcker hela området utan att förvrängas
            width: '100%',      // Responsiv bredd
            height: '100%',     // Responsiv höjd
            position: 'absolute', // Så att den täcker hela skärmen
            top: 0,
            left: 0,
            zIndex: -1, // Håller videon bakom innehållet
          }}
        />
      </VideoContainer>
    </div>
  );
};

export default AboutUsPage;
