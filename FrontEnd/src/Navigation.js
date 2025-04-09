import React from 'react';
import { Link } from 'react-router-dom';
import styled from 'styled-components';
import { FaUserShield, FaInfoCircle, FaUserNurse, FaUserInjured, FaHome } from 'react-icons/fa';
import { useTranslation } from 'react-i18next';  // Import the translation hook

// Navigation bar at the bottom
const Nav = styled.nav`
  background-color: #125358;
  padding: 10px 25px;
  display: flex;
  justify-content: space-between;
  align-items: center;
  position: fixed; /* Fixed position at the bottom */
  bottom: 0;
  left: 0;
  width: 100%; /* Span the full width of the screen */
  z-index: 1000; /* Ensure it stays on top */
`;

const NavLinks = styled.div`
  display: flex;
  gap: 20px;
`;

const StyledLink = styled(Link)`
  color:  #125358;
  text-decoration: none;
  font-weight: bold;
  display: flex;
  align-items: center;
  gap: 5px;

  &:hover {
    color: #00d4ff;
  }
`;

const StyledHomeLink = styled(StyledLink)`
  color: white; /* Specific white color for the "Home" link */
  
  &:hover {
    color: #00d4ff; /* Hover effect stays the same */
  }
`;

const Navigation = () => {
  const { t } = useTranslation(); // Get the translation function

  return (
    <Nav>
      <StyledHomeLink to="/"><FaHome /> {t('home')}</StyledHomeLink>
      <NavLinks>
        <StyledLink to="/about"><FaInfoCircle /> {t('about Us')}</StyledLink>
        <StyledLink to="/admin"><FaUserShield /> {t('admin')}</StyledLink>
        <StyledLink to="/staff"><FaUserNurse /> {t('staff')}</StyledLink>
        <StyledLink to="/patient"><FaUserInjured /> {t('patient')}</StyledLink>
      </NavLinks>
    </Nav>
  );
};

export default Navigation;
