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
      <ChecklistImage src={checklist} alt="Checklist" />
      <Title>About VitalsGraph</Title>
<SectionText>
  We are four dedicated women studying system development, and together we have created this website as part of our educational project. Our goal is to build a smooth and user-friendly platform that helps healthcare professionals track and report patients well-being on a daily basis.
</SectionText>
<SectionText>
  By combining technology with compassion, we aim to improve communication between healthcare staff and patients. We believe that small actions every day can make a big difference in peoples health over time.
</SectionText>
<SectionText>
  The platform is designed with a focus on accessibility, safety and simplicity – and we are proud to have built something that can make healthcare more efficient and provide patients with a better experience.
</SectionText>


<Section>
  <SectionTitle>What is VitalsGraph?</SectionTitle>

  <SectionText>
    VitalsGraph is a digital platform that makes it easy for healthcare professionals to report and track patients' well-being on a daily basis.
  </SectionText>

  <SectionText>
    By visualizing health data, we help healthcare staff monitor progress over time and make better decisions together with the patients.
  </SectionText>

  <SectionText>
    The platform provides a clear overview of each individual's well-being, which improves communication and enables more efficient care.
  </SectionText>
</Section>


        <Section>
          <SectionTitle>Our Vision</SectionTitle>
          <SectionText>
          We believe that everyone should be able to communicate their well-being easily, safely and visually. Our vision is to enable people to express their inner state in a way that both they and others can understand.
          </SectionText>
        </Section>

        <Section>
          <SectionTitle>Who Are We?</SectionTitle>
          <SectionText>
          We are a dedicated team of future software developers with a passion for creating digital solutions that make a difference. With our diverse strengths in development, design, and empathy, we build products that put people at the center.
          </SectionText>
        </Section>

        <Section>
  <SectionTitle>Our Story</SectionTitle>
  <StyledList>
    <li>Founded in 2025</li>
    <li>Self-funded: The project was started without external investments.</li>
    <li>Goal: To create a platform that makes it easier for individuals to continuously document their well-being in a simple and safe way.</li>
    <li>Development focus: We focused on user-friendliness, accessibility and enabling both patients and healthcare professionals to track progress over time.</li>
  </StyledList>
  <Text>
    Thank you for using our service! 💙
  </Text>
</Section>


        <TeamHeading>Meet the VitalsGraph Team</TeamHeading>
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

