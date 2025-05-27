import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import axios from 'axios';
import styled, { createGlobalStyle } from 'styled-components';
import { FaArrowLeft, FaArrowRight, FaTimes } from 'react-icons/fa';
import patient1 from '../src/media/patient1.jpg';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';
import { Link } from 'react-router-dom';
import { useLocation, useNavigate } from 'react-router-dom';
import '../language/i18n.js';
import { useTranslation } from 'react-i18next';

export const Overlay = styled.div`
  position: absolute;
  top: 0;
  left: 0;
  width: 200%;      
  height: 100%;
  z-index: 999;
  left: -105px;

  display: flex;
  justify-content: center;
  align-items: center;
  border-radius: 16px;  /* if container has rounding */
`;

// Make modal a bit bigger
export const ModalContainer = styled.div`
  background-color: #F1F0E8;
  border-radius: 10px;
  width: 90%;            // <= container width (500px * 0.9 = 450px max)
  max-width: 450px;      // max width so it won't overflow
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


// Title and Close
export const Title = styled.h1`
  text-align: center;
  color: #125358;
  margin-bottom: 24px;
  font-size: 24px;
`;

export const CloseButton = styled.button`
  position: absolute;
  top: 20px;
  right: 20px;
  background: transparent;
  border: none;
  font-size: 26px;
  color: #444;
  cursor: pointer;

  &:hover {
    color: #000;
  }
`;





// Form Fields
export const CommentBox = styled.textarea`
  width: 100%;
  height: 140px;
  padding: 16px;
  font-size: 16px;
  border-radius: 10px;
  border: 1px solid #ccc;
  margin-bottom: 16px;
  resize: none;

  &:focus {
    outline: none;
    border-color: #125358;
  }
`;

export const SignatureInput = styled.input`
  width: 100%;
  padding: 12px;
  border-radius: 10px;
  margin-bottom: 20px;
  font-size: 16px;
  border: 1px solid #ccc;

  &:focus {
    outline: none;
    border-color: #125358;
  }
`;

// Buttons
export const Button = styled.button`
  background-color: #125358;
  color: white;
  padding: 12px 20px;
  border: none;
  border-radius: 10px;
  font-size: 16px;
  cursor: pointer;
  transition: background-color 0.2s ease;

  &:hover {
    background-color: #0d3e40;
  }
`;

export const ActionButton = styled(Button).attrs({ as: 'button' })`
  background-color: ${({ $variant }) =>
    $variant === 'delete' ? '#ff4d4f' : '#125358'};

  &:hover {
    background-color: ${({ $variant }) =>
    $variant === 'delete' ? '#cc0000' : '#0d3e40'};
  }

  margin-left: 8px;
  padding: 10px 16px;
  font-size: 14px;
`;

// Comment Display
export const CommentList = styled.div`
  margin-top: 24px;
  display: flex;
  flex-direction: column;
  gap: 16px;
`;

export const CommentItem = styled.div`
  background: #f0f0f0;
  padding: 16px;
  border-left: 4px solid #125358;
  border-radius: 10px;
  position: relative;
`;

// Meta Info
export const NameTitle = styled.span`
  font-weight: 600;
  color: #125358;
`;

export const Signature = styled.div`
  font-style: italic;
  color: #777;
  margin-top: 6px;
  font-size: 14px;
`;

// Delete Button on Comment
export const DeleteButton = styled.button`
  position: absolute;
  top: 12px;
  right: 12px;
  background: #ff4d4d;
  border: none;
  border-radius: 50%;
  width: 26px;
  height: 26px;
  color: white;
  font-weight: bold;
  font-size: 16px;
  cursor: pointer;

  &:hover {
    background: #cc0000;
  }
`;

export const Pagination = styled.div`
  display: flex;
  justify-content: space-between; /* previous left, next right */
  align-items: center;
  margin-top: 24px;
`;


export const PageButtonContainer = styled.div`
  display: flex;
  gap: 12px;
`;

export const PageButton = styled(Button)`
  padding: 8px;
  font-size: 18px;
  width: 40px;
  height: 40px;
  border-radius: 50%;
  display: flex;
  justify-content: center;
  align-items: center;

  /* Remove extra text spacing */
  svg {
    pointer-events: none;
  }
`;

export const PageInfo = styled.div`
  font-size: 14px;
  color: #668;
  text-align: center;
  margin-top: 8px;
`;

export const PageContainer = styled.div`
  margin-top: 20px;
`;

export const CommentButton = styled(Button)`
  background-color: #125358;
   align-items: center;
  
  &:hover {
    background-color: #0d3e40;
  }
`;

const ButtonsWrapper = styled.div`
  display: flex;
  justify-content: center;
  gap: 10px;
  margin-top: 6px;
`;





const CommentModal = ({ isOpen, onClose, patientName, readOnly = false }) => {
  const { patientId } = useParams();
  const { t } = useTranslation();
  const [comments, setComments] = useState('');
  const [newComment, setNewComment] = useState('');
  const [initials, setInitials] = useState('');
  const [commentList, setCommentList] = useState([]);
  const [patientData, setPatientData] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [selectedDate, setSelectedDate] = useState(new Date());
  const COMMENTS_PER_PAGE = 6;
  const [currentPage, setCurrentPage] = useState(1);
  const [editMode, setEditMode] = useState(false);
  const [editId, setEditId] = useState(null);
  const [signature, setSignature] = useState('');
  const location = useLocation();
  const navigate = useNavigate();
  const fromPage = location.state?.from || 'patient';
  const isModalOpen = isOpen && patientId;

  useEffect(() => {
    const savedComments = JSON.parse(localStorage.getItem(`comments_${patientId}`)) || [];
    setCommentList(savedComments);
  }, [patientId]);


  const isToday = (date) => {
    const today = new Date();
    return (
      date.getDate() === today.getDate() &&
      date.getMonth() === today.getMonth() &&
      date.getFullYear() === today.getFullYear()
    );
  };

  useEffect(() => {
    const fetchPatientData = async () => {
      try {
        const response = await axios.get(`https://localhost:7066/api/Patient/ReadItem?id=${patientId}`);
        setPatientData(response.data.item);
        setLoading(false);
      } catch (err) {
        setError(t("Could not fetch patient data."));
        setLoading(false);
      }
    };
    fetchPatientData();
  }, [patientId]);

  useEffect(() => {
    if (isModalOpen) {
      document.body.style.overflow = 'hidden';
      // Also add right padding to compensate for scrollbar width (~15-17px)
      const scrollbarWidth = window.innerWidth - document.documentElement.clientWidth;
      document.body.style.paddingRight = `${scrollbarWidth}px`;
    } else {
      document.body.style.overflow = '';
      document.body.style.paddingRight = '';
    }
    return () => {
      document.body.style.overflow = '';
      document.body.style.paddingRight = '';
    };
  }, [isModalOpen]);

  const handleCommentSubmit = () => {
    if (comments.trim() === '') {
      alert(t("Comment cannot be empty."));
      return;
    }

    if (signature.trim() === '') {
      alert(t("Please enter your initials."));
      return;
    }

    const newComment = {
      id: Date.now(),
      text: comments.trim(),
      signature: signature.trim(),
      timestamp: new Date().toLocaleString('sv-SE', { dateStyle: 'short', timeStyle: 'short' }),
    };

    const updatedComments = [newComment, ...commentList];
    setCommentList(updatedComments);
    localStorage.setItem(`comments_${patientId}`, JSON.stringify(updatedComments));
    setComments('');
    setSignature('');
  };

  const handleDelete = (id) => {
    const updatedComments = commentList.filter(comment => comment.id !== id);
    setCommentList(updatedComments);
    localStorage.setItem(`comments_${patientId}`, JSON.stringify(updatedComments));
  };

  const handleEdit = (id) => {
    const commentToEdit = commentList.find(comment => comment.id === id);
    setComments(commentToEdit.text);
    setEditMode(true);
    setEditId(id);
  };

  const handleSaveEdit = () => {
    if (comments.trim() !== '') {
      const updatedComments = commentList.map(comment =>
        comment.id === editId
          ? { ...comment, text: comments.trim(), timestamp: new Date().toLocaleString('sv-SE', { dateStyle: 'short', timeStyle: 'short' }) }
          : comment
      );
      setCommentList(updatedComments);
      localStorage.setItem(`comments_${patientId}`, JSON.stringify(updatedComments));
      setComments('');
      setEditMode(false);
      setEditId(null);
    }
  };

  const handleCancelEdit = () => {
    setComments('');
    setEditMode(false);
    setEditId(null);
  };


 // 1. Filter comments by selected date first
const filteredComments = commentList.filter(comment => {
  const commentDate = new Date(comment.timestamp).toLocaleDateString('sv-SE');
  const selectedDateString = selectedDate.toLocaleDateString('sv-SE');
  return commentDate === selectedDateString;
});

// 2. Calculate total pages based on filtered comments count
const totalPages = Math.ceil(filteredComments.length / COMMENTS_PER_PAGE);

// 3. Calculate start index for current page
const startIdx = (currentPage - 1) * COMMENTS_PER_PAGE;

// 4. Slice filtered comments for pagination
const paginatedComments = filteredComments.slice(startIdx, startIdx + COMMENTS_PER_PAGE);


  const handleDateChange = date => {
    setSelectedDate(date);
    setCurrentPage(1);
  };


  return (
    <Overlay>
      <ModalContainer>
        <CloseButton onClick={onClose}>
          <FaTimes />
        </CloseButton>
        <Title>
          {t('Comments for')}  {patientData ?
            <NameTitle>{patientData.firstName} {patientData.lastName}</NameTitle> :
            (error || t('No Patient Found'))}
        </Title>
        <DatePicker
          selected={selectedDate}
          onChange={handleDateChange}
          dateFormat="yyyy-MM-dd"
          showPopperArrow={false}
        />

        {isToday(selectedDate) && !readOnly && (
          <>
            <CommentBox
              value={comments}
              onChange={(e) => setComments(e.target.value)}
              placeholder={t('Write Your Comment')}
              readOnly={readOnly}
            />
            <SignatureInput
              type="text"
              placeholder={t('initials')}
              value={signature}
              onChange={(e) => setSignature(e.target.value)}
              readOnly={readOnly}
            />

            {!readOnly && (
              editMode ? (
                <ButtonsWrapper>
                  <CommentButton onClick={handleSaveEdit}>{t('Save')}</CommentButton>
                  <Button onClick={handleCancelEdit}>{t('Cancel')}</Button>
                </ButtonsWrapper>
              ) : (
                <ButtonsWrapper>
                  <CommentButton onClick={handleCommentSubmit}>{t('Submit Comment')}</CommentButton>
                </ButtonsWrapper>
              )
            )}

          </>
        )}


        <CommentList>
          {paginatedComments.map((comment) => (
            <CommentItem key={comment.id}>
              <p>{comment.text}</p>
              <p>{comment.timestamp}</p>
              {!readOnly && (
                <>
                  <ActionButton $variant="delete" onClick={() => handleDelete(comment.id)}>X</ActionButton>
                  <ActionButton onClick={() => handleEdit(comment.id)}>{t('Edit')}</ActionButton>
                </>
              )}

              <Signature>{comment.signature}</Signature>
            </CommentItem>
          ))}
        </CommentList>

        <Pagination>
          <PageButton
            onClick={() => setCurrentPage(prev => prev - 1)}
            disabled={currentPage === 1}
            aria-label={t('Previous')}
          >
            <FaArrowLeft />
          </PageButton>

          <PageInfo>
            {t('Page')} {currentPage} {t('of')} {totalPages}
          </PageInfo>

          <PageButton
            onClick={() => setCurrentPage(prev => prev + 1)}
            disabled={currentPage === totalPages}
            aria-label={t('Next')}
          >
            <FaArrowRight />
          </PageButton>
        </Pagination>
      </ModalContainer>
    </Overlay>
  );
};

export default CommentModal;
