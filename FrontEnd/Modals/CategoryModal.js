import React, { useEffect, useState } from 'react';
import styled from 'styled-components';
import axios from 'axios';
import { useTranslation } from 'react-i18next';

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
    try {
      const response = await axios.get(
        `https://localhost:7066/api/${types[activeType]}/ReadItems`,
        { headers: { Authorization: `Bearer ${token}` } }
      );
      setItems(response.data.pageItems || []);
    } catch (err) {
      console.error(`Failed to fetch ${activeType}:`, err);
    }
  };

  // Simple validation for name, rating, label
  const validate = () => {
    const errors = {};
    if (!formValues.name.trim()) errors.name = t('Name is required');
    if (!formValues.rating || isNaN(formValues.rating) || formValues.rating < 1 || formValues.rating > 10)
      errors.rating = t('Rating must be a number between 1 and 10');
    if (!formValues.label.trim()) errors.label = t('Label is required');
    setFieldErrors(errors);
    return Object.keys(errors).length === 0;
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormValues((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!validate()) return;

    const endpoint = editingId ? `UpdateItem/${editingId}` : 'CreateItem';

    try {
      await axios[editingId ? 'put' : 'post'](
  `https://localhost:7066/api/${types[activeType]}/${editingId ? `UpdateItem/${editingId}` : 'CreateItem'}`,
  {
    ...(editingId ? { [`${types[activeType][0].toLowerCase()}${types[activeType].slice(1)}Id`]: editingId } : {}),
    name: formValues.name,
    rating: parseInt(formValues.rating),
    label: formValues.label,
  },
  { headers: { Authorization: `Bearer ${token}` } }
);
      resetForm();
      fetchItems();
    } catch (err) {
      console.error('Error submitting form:', err);
    }
  };

 const handleEdit = (item) => {
  setFormValues({
    name: item.name,
    rating: item.rating.toString(),
    label: item.label,
  });
  setEditingId(getItemId(item)); // 👈 use normalized ID
  setFieldErrors({});
};


  const handleDelete = async (id) => {
    try {
      await axios.delete(
        `https://localhost:7066/api/${types[activeType]}/DeleteItem/${id}`,
        { headers: { Authorization: `Bearer ${token}` } }
      );
      if (editingId === id) resetForm();
      fetchItems();
    } catch (err) {
      console.error('Error deleting item:', err);
    }
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
                <strong>{item.name}</strong> <br />
                <small>{t('Rating')}: {item.rating} | {t('Label')}: {item.label}</small>
              </div>
              <div>
                <button onClick={() => handleEdit(item)} style={{ marginRight: 8 }}>
                  {t('edit')}
                </button>
                <button onClick={() => handleDelete(getItemId(item))} style={{ color: 'red' }}>
                  {t('delete')}
                </button>
              </div>
            </li>
          ))}
          {items.length === 0 && <li>{t('noItemsFound')}</li>}
        </ul>

        {/* Form */}
        <form onSubmit={handleSubmit} noValidate>
          <InputGroup>
            <label htmlFor="name">{t('name')}</label>
            <input
              id="name"
              name="name"
              type="text"
              value={formValues.name}
              onChange={handleInputChange}
              autoComplete="off"
            />
            {fieldErrors.name && <small style={{ color: 'red' }}>{fieldErrors.name}</small>}
          </InputGroup>

          <InputGroup>
            <label htmlFor="rating">{t('rating')}</label>
            <input
              id="rating"
              name="rating"
              type="number"
              min="1"
              max="10"
              value={formValues.rating}
              onChange={handleInputChange}
              autoComplete="off"
            />
            {fieldErrors.rating && <small style={{ color: 'red' }}>{fieldErrors.rating}</small>}
          </InputGroup>

          <InputGroup>
            <label htmlFor="label">{t('label')}</label>
            <input
              id="label"
              name="label"
              type="text"
              value={formValues.label}
              onChange={handleInputChange}
              autoComplete="off"
            />
            {fieldErrors.label && <small style={{ color: 'red' }}>{fieldErrors.label}</small>}
          </InputGroup>

          <ButtonGroup>
            <Button type="submit">{editingId ? t('update') : t('add')}</Button>
            {editingId && (
              <ButtonDelete
                type="button"
                onClick={() => {
                  resetForm();
                }}
              >
                {t('cancel')}
              </ButtonDelete>
            )}
          </ButtonGroup>
        </form>
      </ModalContent>
    </ModalContainer>
  );
};

export default CategoryModal;
