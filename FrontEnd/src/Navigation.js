import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import styled from 'styled-components';
import {
  FaUserShield,
  FaChartLine,
  FaInfoCircle,
  FaUserInjured,
  FaHome,
  FaComments,
  FaSignOutAlt,
  FaUsers,
} from 'react-icons/fa';
import { useTranslation } from 'react-i18next';

const Nav = styled.nav`
  background-color: #125358;
  padding: 8px 20px;
  display: flex;
  justify-content: space-between;
  align-items: center;
  position: fixed;
  bottom: 0;
  left: 0;
  width: 100%;
  z-index: 1000;
  box-shadow: 0 -2px 5px rgba(0, 0, 0, 0.1);
`;

const NavLinks = styled.div`
  display: flex;
  gap: 20px;
  align-items: center;
`;

const StyledLink = styled(Link)`
  color: white;
  text-decoration: none;
  font-weight: 500;
  display: flex;
  align-items: center;
  gap: 6px;
  font-size: 15px;

  &:hover {
    color: #00d4ff;
  }
`;

const LogoutMessage = styled.div`
  position: fixed;
  bottom: 60px;
  left: 50%;
  transform: translateX(-50%);
  background-color: #125358;
  color: white;
  padding: 10px 20px;
  border-radius: 6px;
  font-size: 14px;
  box-shadow: 0px 2px 6px rgba(0, 0, 0, 0.15);
`;

const Navigation = ({ showGraphLink = false, patientId = null }) => {
  const { t } = useTranslation();
  const navigate = useNavigate();
  const [logoutMessage, setLogoutMessage] = useState('');
  const role = localStorage.getItem('role');

  const handleLogout = () => {
    localStorage.removeItem('jwtToken');
    localStorage.removeItem('role');
    setLogoutMessage(t('logout_success'));

    setTimeout(() => {
      setLogoutMessage('');
      window.location.replace('/');
    }, 1500);
  };

  return (
    <>
      <Nav>


        <NavLinks>
          <StyledLink onClick={handleLogout}>
            <FaHome size={18} /> {t('Log out')}
          </StyledLink>

          {showGraphLink && patientId && (
            <StyledLink to={`/graph/${patientId}`}>
              <FaChartLine size={18} /> {t('Graph')}
            </StyledLink>
          )}

          <StyledLink to="/staff">
            <FaUsers size={18} /> {t('Patients')}
          </StyledLink>

          {role === 'sysadmin' && (
            <StyledLink to="/admin">
              <FaUserShield size={18} /> {t('Admin')}
            </StyledLink>
          )}

          <StyledLink to="/about">
            <FaInfoCircle size={18} /> {t('About Us')}
          </StyledLink>
        </NavLinks>
      </Nav>

      {logoutMessage && <LogoutMessage>{logoutMessage}</LogoutMessage>}
    </>
  );
};

export default Navigation;
