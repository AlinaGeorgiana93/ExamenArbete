import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import styled from 'styled-components';
import {
  FaUserShield,
  FaChartLine,
  FaRegUserCircle,
  FaUsers,
  FaSignOutAlt,
} from 'react-icons/fa';
import { useTranslation } from 'react-i18next';
import FloatingProfile from '../src/FloatingProfile';
import useStoredUserInfo from '../src/useStoredUserInfo';

const Nav = styled.nav`
  background: rgb(40, 136, 155);
  padding: 10px 24px;
  display: flex;
  justify-content: space-between;
  align-items: center;
  position: fixed;
  bottom: 0;
  left: 0;
  width: 100%;
  z-index: 1000;
  box-shadow: 0 -2px 5px rgba(0, 0, 0, 0.1);
  font-family: 'Poppins', sans-serif;
`;

const NavLinks = styled.div`
  display: flex;
  gap: 22px;
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

const LeftActions = styled.div`
  display: flex;
  align-items: center;
  gap: 20px;
`;

const LogoutButton = styled.button`
  background: none;
  border: none;
  color: white;
  font-size: 16px;
  cursor: pointer;
  display: flex;
  align-items: center;
  gap: 6px;

  &:hover {
    color: #ff5252;
  }
`;

const UserNameText = styled.span`
  color: white;
  font-weight: 500;
  font-size: 15px;

&:hover {
  color: #00d4ff;
}
`;

const ProfileSection = styled.div`
  display: flex;
  align-items: center;
  gap: 8px;
  cursor: pointer;
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
  const [logoutMessage, setLogoutMessage] = useState('');
  const { userName, setUserName, email, setEmail, role } = useStoredUserInfo();
  const [showProfile, setShowProfile] = useState(false);

  const handleLogout = () => {
    localStorage.removeItem('jwtToken');
    localStorage.removeItem(t('role'));
    setLogoutMessage(t('logout_success'));

    setTimeout(() => {
      setLogoutMessage('');
      window.location.replace('/');
    }, 1500);
  };

  return (
    <>
      <Nav>
        {/* Left: Logout + Profile */}
        <LeftActions>
          <LogoutButton onClick={handleLogout} title={t('Log out')}>
            <FaSignOutAlt size={18} />
            {t('Log out')}
          </LogoutButton>

          <ProfileSection onClick={() => setShowProfile(!showProfile)}>
            <FaRegUserCircle size={18} color="#fff" />
            <UserNameText>{userName}</UserNameText>
          </ProfileSection>
        </LeftActions>

        {/* Right: Navigation Links */}
        <NavLinks>
          <StyledLink to="/staff">
            <FaUsers size={18} /> {t('Patients')}
          </StyledLink>

          {role === 'sysadmin' && (
            <StyledLink to="/admin">
              <FaUserShield size={18} /> {t('Admin')}
            </StyledLink>
          )}

          {showGraphLink && patientId && (
            <StyledLink to={`/graph/${patientId}`}>
              <FaChartLine size={18} /> {t('Graph')}
            </StyledLink>
          )}
        </NavLinks>
      </Nav>


      {logoutMessage && <LogoutMessage>{logoutMessage}</LogoutMessage>}

      {showProfile && (
        <FloatingProfile
          userName={userName}
          setUserName={setUserName}
          email={email}
          setEmail={setEmail}
          role={role}
          onClose={() => setShowProfile(false)}
        />
      )}
    </>
  );
};

export default Navigation;
