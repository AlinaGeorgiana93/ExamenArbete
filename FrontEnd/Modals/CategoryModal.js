import React, { useEffect, useState } from 'react';
import styled from 'styled-components';
import axios from 'axios';
import { useTranslation } from 'react-i18next';
import EmojiPickerWrapper from '../src/emojiPickerWrapper.js';  // import emoji picker
import { useNavigate } from 'react-router-dom';


// Styled components (similar style to your Staff modal)
const ModalContainer = styled.div`
  position: fixed;
  inset: 0;
  background: rgba(0, 0, 0, 0.7);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
  animation: fadeIn 0.3s ease forwards;
  @keyframes fadeIn {
    from {opacity: 0;}
    to {opacity: 1;}
  }
`;

const ModalContent = styled.div`
  background: #F1F0E8;
  border-radius: 10px;
  width: 450px;
  max-height: 80vh;
  overflow-y: auto;
  padding: 30px;
  box-shadow: 0 4px 10px rgba(0,0,0,0.1);
  position: relative;
  animation: slideUp 0.3s ease-out;
  @keyframes slideUp {
    from {transform: translateY(20px); opacity: 0;}
    to {transform: translateY(0); opacity: 1;}
  }
`;

const ModalHeader = styled.h3`
  font-size: 20px;
  margin-bottom: 20px;
  text-align: center;
  color: #333;
`;

const CloseButton = styled.button`
  position: absolute;
  right: 15px;
  top: 15px;
  background: transparent;
  border: none;
  font-size: 24px;
  cursor: pointer;
  color: #333;
`;

const InputGroup = styled.div`
  display: flex;
  flex-direction: column;
  margin-bottom: 15px;
  gap: 6px;

  label {
    font-weight: 600;
    color: #444;
    font-size: 14px;
  }

  input {
    padding: 10px;
    border: 1px solid #ddd;
    border-radius: 8px;
    font-size: 16px;
    outline: none;

    &:focus {
      border-color: rgb(133, 200, 205);
      box-shadow: 0 0 5px rgba(0, 212, 255, 0.5);
    }
  }
`;

const ButtonGroup = styled.div`
  display: flex;
  justify-content: space-between;
  margin-top: 20px;
  gap: 10px;
  flex-wrap: wrap;
`;

const Button = styled.button`
  flex: 1;
  padding: 12px 24px;
  font-size: 16px;
  border: none;
  background: rgb(40, 136, 155);
  color: white;
  border-radius: 8px;
  cursor: pointer;
  box-shadow: 0 4px 6px rgba(0,0,0,0.1);
  transition: all 0.3s ease;

  &:hover {
    background: #8ACCD5;
    transform: translateY(-2px);
    box-shadow: 0 6px 10px rgba(0,0,0,0.2);
  }

  &:active {
    background: #0099cc;
    transform: translateY(1px);
  }
`;
const ToastMessage = styled.div`
  position: fixed;
  bottom: 30px;
  left: 50%;
  transform: translateX(-50%);
  background-color: #4caf50;
  color: white;
  padding: 12px 24px;
  border-radius: 6px;
  box-shadow: 0 4px 10px rgba(0,0,0,0.15);
  animation: fadeInOut 3s ease;
  z-index: 9999;
  @keyframes fadeInOut {
    0% { opacity: 0; transform: translateX(-50%) translateY(20px); }
    10%, 90% { opacity: 1; transform: translateX(-50%) translateY(0); }
    100% { opacity: 0; transform: translateX(-50%) translateY(-20px); }
  }
`;


const ButtonDelete = styled(Button)`
  background: red;
  &:hover {
    background: darkred;
  }
`;

// Your category types mapping
const types = {
  moodKind: 'MoodKind',
  activityLevel: 'ActivityLevel',
  appetiteLevel: 'AppetiteLevel',
  sleepLevel: 'SleepLevel',
};


const CategoryModal = ({ onClose, initialType = 'moodKind' }) => {
  const { t } = useTranslation();
  const [activeType, setActiveType] = useState(initialType);
  const [items, setItems] = useState([]);
  const [formValues, setFormValues] = useState({ name: '', rating: '', label: '' });
  const [editingId, setEditingId] = useState(null);
  const [fieldErrors, setFieldErrors] = useState({});
  const token = localStorage.getItem('jwtToken');
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [successMessage, setSuccessMessage] = useState('');
  const [showSuccessMessage, setShowSuccessMessage] = useState(false);
  const [showEmojiPicker, setShowEmojiPicker] = useState(false);
  const navigate = useNavigate();


  const handleEmojiSelect = (emoji) => {
    setFormValues((prev) => ({ ...prev, label: prev.label + emoji }));
  };


  const openEditModal = (item) => {
    setEditingId(item.id);
    setFormValues(item);
    setIsModalOpen(true);
  };



  const getItemId = (item) => {
    const idFields = {
      moodKind: 'moodKindId',
      activityLevel: 'activityLevelId',
      appetiteLevel: 'appetiteLevelId',
      sleepLevel: 'sleepLevelId',
    };
    return item[idFields[activeType]];
  };


  useEffect(() => {
    fetchItems();
    resetForm();
  }, [activeType]);

  const resetForm = () => {
    setFormValues({ name: '', rating: '', label: '' });
    setEditingId(null);
    setFieldErrors({});
  };

  const fetchItems = async () => {
    console.log(`🔄 Fetching items for type: ${activeType} with token:`, token);

    try {
      const response = await axios.get(
        `https://localhost:7066/api/${types[activeType]}/ReadItems`,
        { headers: { Authorization: `Bearer ${token}` } }
      );

      console.log(`✅ Fetched items response for ${activeType}:`, response.data);

      setItems(response.data.pageItems || []);
      console.log(`📦 Set items state with ${(response.data.pageItems || []).length} items`);
    } catch (err) {
      console.error(`❌ Failed to fetch ${activeType}:`, err.response?.data || err.message || err);
    }
  };


  // Simple validation for name, rating, label
  const validate = () => {
    const errors = {};
    if (!formValues.name.trim()) errors.name = t('Name is required');
    const ratingNum = Number(formValues.rating);
    if (!formValues.rating || isNaN(ratingNum) || ratingNum < 1 || ratingNum > 10) {
      errors.rating = 'Rating must be a number between 1 and 10';  // no translation here per your request
    }
    if (!formValues.label.trim()) errors.label = t('Label is required');
    setFieldErrors(errors);
    return Object.keys(errors).length === 0;
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormValues((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    if (editingId) {
      await handleUpdate(event);
    } else {
      await handleCreate();
    }
  };


  const handleCreate = async () => {
    if (!validate()) {
      console.log('❌ Validation failed');
      return;
    }
    try {
      const response = await axios.post(
        `https://localhost:7066/api/${types[activeType]}/CreateItem`,
        formValues,
        { headers: { Authorization: `Bearer ${token}` } }
      );
      console.log('✅ Create response:', response.data);
      await fetchItems(); // wait to refresh list

      setSuccessMessage(t(`${types[activeType]}CreatedSuccessfully`));
      setShowSuccessMessage(true);

      setTimeout(() => {
        setShowSuccessMessage(false);
        setSuccessMessage('');
        closeModal();  // <-- Close the modal here
      }, 2000);

      resetForm();
    } catch (err) {
      console.error('❌ Error creating item:', err.response?.data || err.message);
    }
  };


  const startEditing = (item) => {
    console.log('✏️ Edit clicked for item:', item);
    const id = getItemId(item);
    console.log('Extracted ID for editing:', id);
    console.log('Starting to edit item:', item); // Debug log
    setFormValues({
      name: item.name || '',
      rating: item.rating || '',
      label: item.label || '',
    });
    setEditingId(getItemId(item));
  };

  const handleUpdate = async () => {
    console.log('Updating:', editingId, formValues);

    if (!validate()) {
      console.warn('Validation failed');
      return;
    }
    console.log("Validation result:", validate());


    try {
      const response = await axios.put(
        `https://localhost:7066/api/${types[activeType]}/UpdateItem/${editingId}`,
        {
          ...formValues,
          [`${types[activeType].toLowerCase()}Id`]: editingId, // e.g., staffId or patientId
        },
        { headers: { Authorization: `Bearer ${token}` } }
      );

      console.log('Update success:', response.data);

      // ✅ Save new token if provided
      const newToken = response?.data?.Item?.Token;
      if (newToken) {
        localStorage.setItem('jwtToken', newToken);
      }
      await fetchItems();

      setSuccessMessage(t(`${types[activeType]}UpdatedSuccessfully`));

      setShowSuccessMessage(true);

      setTimeout(() => {
        setShowSuccessMessage(false);
        setSuccessMessage('');
        closeModal();  // <-- Close the modal here
      }, 2000);

      resetForm();
    } catch (err) {
      console.error('❌ Error creating item:', err.response?.data || err.message);
    }
  };

  const handleDelete = async (id) => {
    try {
      await axios.delete(
        `https://localhost:7066/api/${types[activeType]}/DeleteItem/${id}`,
        { headers: { Authorization: `Bearer ${token}` } }
      );

      if (editingId === id) resetForm();
      fetchItems();

      setSuccessMessage(`${t(activeType)} ${t('deletedSuccessfully')}`);
      setShowSuccessMessage(true);
      setTimeout(() => setShowSuccessMessage(false), 3000);
    } catch (err) {
      console.error('Error deleting item:', err);
    }
  };

  const closeModal = () => {
    resetForm();
    setEditingId(null);
    onClose();  // Tell parent to close modal

  };

  return (
    <ModalContainer>
      <ModalContent>
        <CloseButton onClick={onClose}>×</CloseButton>
        <ModalHeader>{t('manageCategories')}</ModalHeader>


        {/* Tabs */}
        <div style={{ display: 'flex', marginBottom: '20px', gap: '10px' }}>
          {Object.keys(types).map((key) => (
            <button
              key={key}
              style={{
                padding: '8px 16px',
                borderRadius: 8,
                border: 'none',
                cursor: 'pointer',
                backgroundColor: key === activeType ? '#28889b' : '#eee',
                color: key === activeType ? '#fff' : '#000',
                flex: 1,
              }}
              onClick={() => {
                setActiveType(key);
                resetForm();
              }}
            >
              {t(key)}
            </button>
          ))}
        </div>


        {/* Item list */}
        <ul style={{ listStyle: 'none', padding: 0, maxHeight: '200px', overflowY: 'auto', marginBottom: '20px' }}>
          {items.map((item) => (
            <li key={getItemId(item)}
              style={{
                backgroundColor: '#fff',
                padding: '10px 15px',
                marginBottom: 8,
                borderRadius: 8,
                display: 'flex',
                justifyContent: 'space-between',
                alignItems: 'center',
                boxShadow: '0 1px 3px rgba(0,0,0,0.1)',
              }}
            >
              <div>
                    <strong>{t(item.name)}</strong> <br />
            <small>
              {t('rating')}: {item.rating} | {t('label')}: {t(item.label)}
            </small>
              </div>
              <div style={{ display: 'flex', gap: '8px' }}>
                <button
                  onClick={() => startEditing(item)}
                  style={{
                    padding: '6px 12px',
                    backgroundColor: '#28889b',
                    color: '#fff',
                    border: 'none',
                    borderRadius: 6,
                    fontSize: 14,
                    cursor: 'pointer'
                  }}
                >
                  {t('edit')}
                </button>

                <button
                  onClick={() => handleDelete(getItemId(item))}
                  style={{
                    padding: '6px 12px',
                    backgroundColor: 'red',
                    color: 'white',
                    border: 'none',
                    borderRadius: 6,
                    fontSize: 14,
                    cursor: 'pointer'
                  }}
                >
                  {t('delete')}
                </button>
              </div>

            </li>
          ))}
          {items.length === 0 && <li>{t('noItemsFound')}</li>}
        </ul>
        <ModalHeader>
          {editingId ? `${t('edit')} ${t(activeType)}` : `${t('create')} ${t(activeType)}`}
        </ModalHeader>
        {/* Form */}
        <form onSubmit={handleSubmit} noValidate>
          <InputGroup>
            <label htmlFor="name">{t('name')}</label>
            <input
              id="name"
              name={t('name')}
              value={formValues.name}
              onChange={handleInputChange}
              placeholder={t('enter name')}
            />
            {fieldErrors.name && <span style={{ color: 'red', fontSize: '13px' }}>{fieldErrors.name}</span>}
          </InputGroup>

          <InputGroup>
            <label htmlFor="rating">{t('rating')}</label>
            <input
              id="rating"
              name={t('rating')}
              type="number"
              min="1"
              max="10"
              value={formValues.rating}
              onChange={handleInputChange}
              placeholder={t('enter rating (1-10)')}
            />
            {fieldErrors.rating && <span style={{ color: 'red', fontSize: '13px' }}>{fieldErrors.rating}</span>}
          </InputGroup>
          <InputGroup>
            <label htmlFor="label">{t('label')}</label>

            <div style={{ position: 'relative', width: '100%' }}>
              <input
                id="label"
                name={t('label')}
                type="text"
                value={formValues.label}
                onChange={handleInputChange}
                placeholder={t('enter label')}
                style={{ width: '100%', paddingRight: '32px' }}
              />

              <EmojiPickerWrapper
                onSelect={handleEmojiSelect}
              />
            </div>

            {fieldErrors.label && <small style={{ color: 'red' }}>{fieldErrors.label}</small>}
          </InputGroup>

          <ButtonGroup>
            {!editingId && (
              <Button type="button" onClick={handleCreate}>{t('create')}</Button>

            )}
            {editingId && (
              <>
                <Button type="button" onClick={handleUpdate}>{t('update')}</Button>
                <ButtonDelete
                  type="button"
                  onClick={resetForm}
                >
                  {t('cancel')}
                </ButtonDelete>
              </>
            )}
          </ButtonGroup>
        </form>
      </ModalContent>
      {showSuccessMessage && <ToastMessage>{successMessage}</ToastMessage>}
    </ModalContainer>
  );
};

export default CategoryModal;