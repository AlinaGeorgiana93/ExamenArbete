import React from 'react';
import { Link } from 'react-router-dom';
import styled from 'styled-components';
import { FaUserShield, FaInfoCircle, FaUserNurse, FaUserInjured, FaHome } from 'react-icons/fa';
import { useTranslation } from 'react-i18next';  // Import the translation hook

// Navigation bar at the bottom
const Nav = styled.nav`
  background-color: #125358;
  padding: 5px 15px; /* Reduced padding for a slimmer nav */
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
  gap: 15px; /* Reduced gap between links */
`;

const StyledLink = styled(Link)`
   color: white;
  text-decoration: none;
  font-weight: bold;
  display: flex;
  align-items: center;
  gap: 5px;
  font-size: 14px; /* Reduced font size for a more compact look */

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
      <StyledHomeLink to="/"><FaHome size={18} /> {t('home')}</StyledHomeLink> {/* Adjusted icon size */}
      <NavLinks>
       {/*  <StyledLink to="/admin"><FaUserShield size={18} /> {t('admin')}</StyledLink> {/* Adjusted icon size */}
        <StyledLink to="/staff"><FaUserNurse size={18} /> {t('staff')}</StyledLink> {/* Adjusted icon size */}
        <StyledLink to="/patient"><FaUserInjured size={18} /> {t('patient')}</StyledLink> {/* Adjusted icon size */}
        <StyledLink to="/about"><FaInfoCircle size={18} /> {t('about Us')}</StyledLink> {/* Adjusted icon size */}
      </NavLinks>
    </Nav>
  );
};

export default Navigation;
