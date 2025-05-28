import React, { useState } from 'react';
import styled from 'styled-components';
import patient1 from '../src/media/patient1.jpg';
import UpdateProfileModal from '../Modals/UpdateProfileModal';
import { useTranslation } from 'react-i18next';

const FloatingProfileContainer = styled.div`
  position: fixed;
  bottom: 50px;
  left: 20px;      /* changed from right: 20px */
 background-color: #F1F0E8;
  border-radius: 12px;
  padding: 16px;
  width: 240px;
  max-width: 90vw;
  box-shadow: 0 6px 12px rgba(0, 0, 0, 0.15);
  z-index: 1000;
  font-family: 'Poppins', sans-serif;
`;


const ProfileHeader = styled.div`
  display: flex;
  align-items: center;
  gap: 12px;
`;

const Avatar = styled.img`
  width: 44px;
  height: 44px;
  border-radius: 50%;
`;

const UsernameText = styled.span`
  font-weight: 600;
  color: #222;
  font-size: 1rem;
`;

const ProfileDetails = styled.div`
  margin-top: 12px;
  font-size: 0.92rem;
  color: #444;
  text-align: left;
`;

const ProfileItem = styled.p`
  margin: 4px 0;
`;

const ButtonWrapper = styled.div`
  display: flex;
  justify-content: center;
  margin-top: 12px;
`;

const ChangeButton = styled.button`
  background-color: #3b878c;
  color: white;
  border: none;
  border-radius: 6px;
  padding: 8px 14px;
  font-size: 0.9rem;
  cursor: pointer;
  transition: background-color 0.2s ease;

  &:hover {
    background-color: #2d6e71;
  }
`;

const FloatingProfile = ({ userName, email, role, setUserName, setEmail }) => {
  const { t } = useTranslation();
  const [showUpdateProfileModal, setShowUpdateProfileModal] = useState(false);

  return (
    <>
      <FloatingProfileContainer>
        <ProfileHeader>
          <Avatar src={patient1} alt="User Avatar" />
          <UsernameText>{userName}</UsernameText>
        </ProfileHeader>

        <ProfileDetails>
          <ProfileItem><strong>Email:</strong> {email}</ProfileItem>
          <ProfileItem><strong>Role:</strong> {role}</ProfileItem>
          <ButtonWrapper>
            <ChangeButton onClick={() => setShowUpdateProfileModal(true)}>
              {t('change_info')}
            </ChangeButton>
          </ButtonWrapper>
        </ProfileDetails>
      </FloatingProfileContainer>

      {showUpdateProfileModal && (
        <UpdateProfileModal
          showModal={showUpdateProfileModal}
          setShowModal={setShowUpdateProfileModal}
          userName={userName}
          email={email}
          setUserName={setUserName}
          setStaffEmail={setEmail}
        />
      )}
    </>
  );
};

export default FloatingProfile;
