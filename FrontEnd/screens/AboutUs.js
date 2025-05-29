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
import AOS from 'aos';
import 'aos/dist/aos.css';
import React, { useEffect } from 'react';


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
  position: relative;
  background-color: #fff;
  padding: 40px;
  border-radius: 8px;
  width: 100%;
  max-width: 700px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
  color: #000;
  scrollbar-width: thin;
  scrollbar-color: #50D9E6 #ffffff;

  display: flex;
  flex-direction: column;
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
  justify-content: center; 
  flex-wrap: wrap;
  gap: 30px;        
  margin-top: 30px;
`;

const TeamMember = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  flex-basis: calc(50% - 15px); 
  
  @media (max-width: 600px) {
    flex-basis: 100%;
  }
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
  transition: color 0.3s ease;

  &:hover {
    color: #50D9E6;
  }
`;
const ChecklistImage = styled.img`
  width: 200px;
  height: 200px;
  object-fit: cover;   
  display: block;
  margin: 0 auto 30px auto;
  border-radius: 50%;
  box-shadow: 0 4px 10px rgba(0,0,0,0.15);
  transition: transform 0.3s ease;

  &:hover {
    transform: scale(1.05);
  }
  
  @media (max-width: 500px) {
    width: 150px;
    height: 150px;
  }
`;
const StyledList = styled.ul`
  max-width: 600px;
  margin: 0 auto 30px auto;  
  padding-left: 0;
  list-style: none;   
  text-align: center;

  li {
    position: relative;
    margin-bottom: 12px;
    font-size: 17px;
    color: #333;
    padding-left: 30px; 
    text-align: left;  
    max-width: 100%;

    
    &::before {
      content: "✨";  
      position: absolute;
      left: 0;
      top: 0;
      font-size: 18px;
      line-height: 1;
      color: #125358; 
    }
  }
`;
const Footer = styled.footer`
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 30px;  
  padding: 20px;
  background-color: #f7f7f7;
  color: #555;
  font-size: 14px;
  margin-top: 50px;
  border-top: 1px solid #ddd;
`;

const BackToTopButton = styled.button`
align-self: flex-end;
margin-top: 30px;
margin-bottom: 30px;

width: 50px;
height: 50px;
background-color:rgb(83, 189, 197);
color: white;
border: none;
border-radius: 50%;
font-size: 24px;
cursor: pointer;
opacity: 0.7;
transition: opacity 0.3s ease, transform 0.3s ease;

display: flex;
align-items: center;
justify-content: center;
position: relative;

&:hover {
  opacity: 1;
  transform: scale(1.1);
}

&::after {
  content: attr(data-tooltip);
  position: absolute;
  bottom: 120%;
  left: 50%;
  transform: translateX(-50%);
  background: rgba(255, 255, 255, 0.9);
  color: #125358;
  border: 1px solid #125358;
  padding: 5px 8px;
  border-radius: 4px;
  font-size: 12px;
  opacity: 0;
  pointer-events: none;
  transition: opacity 0.3s;
  white-space: nowrap;
}

&:hover::after {
  opacity: 1;
}
`;



// ... dina imports oförändrade

const AboutUsPage = () => {
  const { t } = useTranslation();

  useEffect(() => {
    AOS.init({ duration: 800, once: true });

    const container = document.querySelector('#page-container');
    const onScroll = () => {
      AOS.refresh();
    };
    if (container) {
      container.addEventListener('scroll', onScroll);
    }

    return () => {
      if (container) {
        container.removeEventListener('scroll', onScroll);
      }
    };
  }, []);
  
  return (
    <div>
      <GlobalStyle />
      <LogoContainer>
        <Link to="/">
          <img src={logo1} alt="Logo" style={{ width: '200px', objectFit: 'contain' }} />
        </Link>
      </LogoContainer>

      <PageContainer id="page-container">
        <ChecklistImage data-aos="fade-up" src={checklist} alt="Checklist" />
        <Title data-aos="fade-up">{t('about.title')}</Title>

        <SectionText data-aos="fade-up">{t('about.intro1')}</SectionText>
        <SectionText data-aos="fade-up">{t('about.intro2')}</SectionText>
        <SectionText data-aos="fade-up">{t('about.intro3')}</SectionText>

        <Section>
          <SectionTitle data-aos="fade-up">{t('about.whatIs.title')}</SectionTitle>
          <SectionText data-aos="fade-up">{t('about.whatIs.text1')}</SectionText>
          <SectionText data-aos="fade-up">{t('about.whatIs.text2')}</SectionText>
          <SectionText data-aos="fade-up">{t('about.whatIs.text3')}</SectionText>
        </Section>

        <Section>
          <SectionTitle data-aos="fade-up">{t('about.vision.title')}</SectionTitle>
          <SectionText data-aos="fade-up">{t('about.vision.text')}</SectionText>
        </Section>

        <Section>
          <SectionTitle data-aos="fade-up">{t('about.whoAreWe.title')}</SectionTitle>
          <SectionText data-aos="fade-up">{t('about.whoAreWe.text')}</SectionText>
        </Section>

        <Section>
          <SectionTitle data-aos="fade-up">{t('about.story.title')}</SectionTitle>
          <StyledList>
            <li data-aos="fade-up">{t('about.story.founded')}</li>
            <li data-aos="fade-up">{t('about.story.selfFunded')}</li>
            <li data-aos="fade-up">{t('about.story.goal')}</li>
            <li data-aos="fade-up">{t('about.story.focus')}</li>
          </StyledList>
          <Text data-aos="fade-up">{t('about.story.thankYou')}</Text>
        </Section>

        <TeamHeading data-aos="fade-up">{t('about.team.title')}</TeamHeading>
        <TeamGrid>
          <TeamMember data-aos="fade-up">
            <ProfileImage src={Parisa} alt="Parisa" />
            <Name>{t('team.parisa.name')}</Name>
            <Role>{t('team.parisa.role')}</Role>
          </TeamMember>
          <TeamMember data-aos="fade-up">
            <ProfileImage src={Alina} alt="Alina" />
            <Name>{t('team.alina.name')}</Name>
            <Role>{t('team.alina.role')}</Role>
          </TeamMember>
          <TeamMember data-aos="fade-up">
            <ProfileImage src={Mona} alt="Mona" />
            <Name>{t('team.mona.name')}</Name>
            <Role>{t('team.mona.role')}</Role>
          </TeamMember>
          <TeamMember data-aos="fade-up">
            <ProfileImage src={Nagi} alt="Nagihan" />
            <Name>{t('team.nagihan.name')}</Name>
            <Role>{t('team.nagihan.role')}</Role>
          </TeamMember>
        </TeamGrid>

        <Footer>
          &copy; {new Date().getFullYear()} {t('VitalsGraph. All rights reserved.')}
          <BackToTopButton 
            onClick={() => window.scrollTo({ top: 0, behavior: 'smooth' })} 
            data-tooltip={t('scrollTop')}
            style={{ marginLeft: '20px' }} 
          >
            ↑
          </BackToTopButton>
        </Footer>
      </PageContainer>
    </div>
  );
};

export default AboutUsPage;
