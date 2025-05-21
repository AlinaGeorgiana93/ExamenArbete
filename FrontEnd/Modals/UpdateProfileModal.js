import React, { useState, useEffect } from 'react';
import styled from 'styled-components';
import axiosInstance from '../src/axiosInstance';
import { useTranslation } from 'react-i18next';
import { FaInfoCircle } from 'react-icons/fa';
import { Tooltip } from 'react-tooltip';



const ModalBackground = styled.div`
  position: fixed;
  top: 0; left: 0;
  width: 100%; height: 100%;
  background: rgba(0, 0, 0, 0.5);
  display: flex; justify-content: center; align-items: center;
  z-index: 2000;
`;

const ModalContainer = styled.div`
  background-color:#F1F0E8;
  padding: 20px;
  border-radius: 12px;
  max-width: 400px;
  width: 100%;
  font-family: 'Poppins', sans-serif;
  position: relative;
`;

const CloseButton = styled.button`
  position: absolute;
  top: 10px;
  right: 12px;
  background: transparent;
  border: none;
  font-size: 1.5rem;
  color: #555;
  cursor: pointer;
  transition: color 0.2s;

  &:hover {
    color: #1a5b61;
  }
`;

const Tabs = styled.div`
  display: flex;
  justify-content: space-around;
  margin-bottom: 20px;
`;

const TabButton = styled.button`
  padding: 10px;
  border: none;
  border-bottom: ${({ active }) => (active ? '3px solid #1a5b61' : 'none')};
  background: transparent;
  font-weight: bold;
  cursor: pointer;
  color: ${({ active }) => (active ? '#1a5b61' : '#555')};
`;

const Label = styled.label`
  display: block;
  margin-bottom: 5px;
  color: #333;
`;

const InputField = styled.input`
  width: 100%;
  padding: 10px;
  margin-bottom: 15px;
  font-size: 1rem;
  border: 1px solid #ccc;
  border-radius: 8px;
  background: #f9f9f9;

  &:focus {
    outline: none;
    border-color: #3b878c;
    background: #fff;
  }
`;

// Tooltip container for the new password
const TooltipWrapper = styled.div`
  position: relative;
  display: inline-block;
  width: 100%;
  margin-bottom: 15px;
`;

const Button = styled.button`
    padding: 12px 24px;
    border: none;
    margin-right: 10px;
    border-radius: 6px;
    font-size: 0.9rem;
    cursor: pointer;
    background: rgb(40, 136, 155);
    color: #fff;
    align-self: center;
    width: 35%;
`;

const Message = styled.p`
  text-align: center;
  color: ${({ success }) => (success ? 'green' : 'red')};
  font-weight: bold;
  margin-top: 10px;
`;

const UpdateProfileModal = ({ showModal, setShowModal, userName, setUserName, email, setStaffEmail }) => {
  const { t } = useTranslation();
  const [activeTab, setActiveTab] = useState('profile');

  const [newUsername, setNewUsername] = useState(userName);
  const [newEmail, setNewEmail] = useState(email);
  const [currentPassword, setCurrentPassword] = useState('');
  const [newPassword, setNewPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [message, setMessage] = useState('');
  const [success, setSuccess] = useState(false);

  useEffect(() => {
    if (showModal) {
      fetchProfile();
    }
  }, [showModal]);

  const fetchProfile = async () => {
    const staffId = localStorage.getItem('staffId');
    const token = localStorage.getItem('jwtToken');

    try {
      const response = await axiosInstance.get(`/Staff/ReadItem`, {
        params: { id: staffId },
        headers: { Authorization: `Bearer ${token}` }
      });
      const { item } = response.data;
      if (item) {
        setNewUsername(item.userName);
        setNewEmail(item.email);
      }
    } catch {
      setMessage('Failed to fetch profile');
      setSuccess(false);
    }
  };

  const handleProfileUpdate = async () => {
    const staffId = localStorage.getItem('staffId');
    const token = localStorage.getItem('jwtToken');

    try {
      const res = await axiosInstance.put(`/Staff/UpdateItem/${staffId}`, {
        StaffId: staffId,
        userName: newUsername,
        email: newEmail
      }, {
        headers: { Authorization: `Bearer ${token}` }
      });

      const newToken = res?.data?.Item?.Token;
      if (newToken) localStorage.setItem('jwtToken', newToken);

      setUserName(newUsername);
      setStaffEmail(newEmail);
      setSuccess(true);
      setMessage(t('profile_updated'));
      setTimeout(() => setShowModal(false), 2000);
    } catch {
      setSuccess(false);
      setMessage(t('update_failed'));
    }
  };

  const handlePasswordChange = async () => {
    const staffId = localStorage.getItem('staffId');
    const token = localStorage.getItem('jwtToken');

    if (newPassword !== confirmPassword) {
      setMessage(t('passwords_do_not_match'));
      setSuccess(false);
      return;
    }

    try {
      await axiosInstance.put(`/Staff/UpdateProfile/${staffId}`, {
        staffId,
        currentPassword,
        newPassword,
        confirmPassword
      }, {
        headers: { Authorization: `Bearer ${token}` }
      });

      setSuccess(true);
      setMessage(t('password_changed'));
      setTimeout(() => setShowModal(false), 2000);
    } catch {
      setSuccess(false);
      setMessage(t('password_change_failed'));
    }
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (activeTab === 'profile') handleProfileUpdate();
    else handlePasswordChange();
  };

  if (!showModal) return null;

  return (
    <ModalBackground>
      <ModalContainer>
        <CloseButton aria-label="Close modal" onClick={() => setShowModal(false)}>
          &times;
        </CloseButton>

        <Tabs>
          <TabButton active={activeTab === 'profile'} onClick={() => setActiveTab('profile')}>
            {t('profile_info')}
          </TabButton>
          <TabButton active={activeTab === 'password'} onClick={() => setActiveTab('password')}>
            {t('change_password')}
          </TabButton>
        </Tabs>

        <form onSubmit={handleSubmit}>
          {activeTab === 'profile' ? (
            <>
              <Label>{t('username')}</Label>
              <InputField
                type="text"
                value={newUsername}
                autoComplete="username"
                onChange={(e) => setNewUsername(e.target.value)}
              />
              <Label>{t('email')}</Label>
              <InputField
                type="email"
                value={newEmail}
                autoComplete="email"
                onChange={(e) => setNewEmail(e.target.value)}
              />
            </>
          ) : (
            <>
            <Label htmlFor="username">{t('username')}</Label>
                  <InputField
                    id="username"
                    name="username"
                    type="text"
                    autoComplete="username"
                    value={userName}
                    readOnly
                      />
            <Label>{t('current_Password')}</Label>
              <InputField
                type="password"
                value={currentPassword}
                onChange={(e) => setCurrentPassword(e.target.value)}
              />
              <Label htmlFor="newPassword">{t('new_Password')}</Label>
<div style={{ display: 'flex', alignItems: 'center' }}>


  <InputField
    id="newPassword"
    type="password"
    placeholder={t('new_Password')}
    value={newPassword}
    onChange={(e) => setNewPassword(e.target.value)}
  />
  <FaInfoCircle
    data-tooltip-id="passwordTooltip"
    data-tooltip-html={t('password_strength_tooltip').replace(/\n/g, '<br />')}
    style={{ marginLeft: '8px', cursor: 'pointer' }}
    size={18}
    color="#888"
  />
</div>

<Tooltip id="passwordTooltip" place="right" />

              <Label>{t('confirm_Password')}</Label>
              <InputField
                type="password"
                value={confirmPassword}
                onChange={(e) => setConfirmPassword(e.target.value)}
              />
            </>
          )}
          <Button type="submit" $variant="primary">
            {t('save')}
          </Button>
          {message && <Message success={success}>{message}</Message>}
        </form>
      </ModalContainer>
    </ModalBackground>
  );
};

export default UpdateProfileModal;
