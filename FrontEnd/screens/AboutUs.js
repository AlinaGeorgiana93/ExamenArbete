import React from 'react';
import styled, { createGlobalStyle } from 'styled-components';
import Alina from '../src/media/Alina.jpg';
import Parisa from '../src/media/Parisa.jpg';
import Mona from '../src/media/Mona.jpg';
import Nagi from '../src/media/Nagi.jpg';
import checklist from '../src/media/checklist.jpg';
import '../src/index.css';
import { Link } from 'react-router-dom';
import { useTranslation } from 'react-i18next';
import logo1 from '../src/media/logo1.png';

const GlobalStyle = createGlobalStyle`
  * {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
  }
`;


const LogoContainer = styled.div`
  position: absolute;
  top: 15px;
  left: 15px;
  z-index: 2;
  margin-left: -50px;
  margin-top: -110px;
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

const AboutUsPage = () => {
  const { t } = useTranslation();

  return (
    <div>
      <GlobalStyle />
      <LogoContainer>
        <Link to="/">
          <img src={logo1} alt="Logo" style={{ width: '200px', objectFit: 'contain' }} />
        </Link>
      </LogoContainer>

      <PageContainer>
        {/* Lägg till bilden här */}
        <img src={checklist} alt="Checklist" style={{ width: '75%', height: 'auto', display: 'block', margin: '0 auto' }} />

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
      </PageContainer>

    </div>
  );
};

export default AboutUsPage;

