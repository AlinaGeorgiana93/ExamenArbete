import React from 'react';
import { Link } from 'react-router-dom';
import styled from 'styled-components';
import { FaUserShield, FaInfoCircle, FaUserNurse, FaUserInjured, FaHome, FaSignOutAlt } from 'react-icons/fa';
import { useTranslation } from 'react-i18next';  // Import the translation hook
import { useNavigate } from 'react-router-dom';
import { useState } from 'react';

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
  const { t } = useTranslation();
  const navigate = useNavigate();
  const [logoutMessage, setLogoutMessage] = useState('');

  const handleLogout = () => {
    localStorage.removeItem('jwtToken');
    localStorage.removeItem('role');

    setTimeout(() => {
      setLogoutMessage('');
      window.location.replace('/'); // Force reload and reset history
    }, 1000);
  };

  return (
    <>
      <Nav>
        <StyledLink onClick={handleLogout}>
          < FaSignOutAlt size={18} /> {t('Log out')}
        </StyledLink>
        <NavLinks>
          {/* <StyledLink as={Link} to="/admin"><FaUserShield size={18} /> {t('admin')}</StyledLink> */}
          <StyledLink as={Link} to="/staff"><FaUserNurse size={18} /> {t('staff')}</StyledLink>
          <StyledLink as={Link} to="/patient"><FaUserInjured size={18} /> {t('patient')}</StyledLink>
          <StyledLink as={Link} to="/about"><FaInfoCircle size={18} /> {t('about Us')}</StyledLink>
        </NavLinks>
      </Nav>
      {logoutMessage && (
        <div style={{
          position: 'fixed',
          bottom: '60px',
          left: '50%',
          transform: 'translateX(-50%)',
          backgroundColor: '#125358',
          color: 'white',
          padding: '10px 20px',
          borderRadius: '5px',
          fontSize: '14px'
        }}>
          {logoutMessage}
        </div>
      )}
    </>
  );
};

export default Navigation;