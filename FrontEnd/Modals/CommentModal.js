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

const Overlay = styled.div`
  position: fixed;
  top: 0;
  left: 0;
  height: 100%;
  width: 100%;
  background: rgba(0, 0, 0, 0.5);
  z-index: 999;
`;

const ModalContainer = styled.div`
  background-color: #f9fafa;
  width: 700px;
  max-height: 90vh;
  margin: 50px auto;
  padding: 32px;
  border-radius: 10px;
  position: relative;
  overflow-y: auto;
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

const Title = styled.h1`
  text-align: center;
  color: #125358;
  margin-bottom: 20px;
`;

const CommentBox = styled.textarea`
  width: 100%;
  height: 140px;
  padding: 14px;
  font-size: 16px;
  margin: 12px 0;
  border-radius: 8px;
  border: 1px solid #ccc;
`;

const SignatureInput = styled.input`
  width: 100%;
  padding: 10px;
  border-radius: 8px;
  margin-bottom: 12px;
  border: 1px solid #ccc;
`;

const Button = styled.button`
  background-color: #125358;
  color: white;
  padding: 10px 18px;
  border: none;
  border-radius: 8px;
  font-size: 16px;
  cursor: pointer;
  margin-right: 10px;

  &:hover {
    background-color: #0d3e40;
  }
`;

const CommentList = styled.div`
  margin-top: 24px;
  display: flex;
  flex-direction: column;
  gap: 16px;
`;

const CommentItem = styled.div`
  background: #f0f0f0;
  padding: 14px;
  border-left: 4px solid #125358;
  border-radius: 6px;
  position: relative;
`;

const DeleteButton = styled.button`
  position: absolute;
  top: 10px;
  right: 10px;
  background: #ff4d4d;
  border: none;
  border-radius: 50%;
  width: 24px;
  height: 24px;
  color: white;
  font-weight: bold;
  cursor: pointer;

  &:hover {
    background: #cc0000;
  }
`;

const Pagination = styled.div`
  display: flex;
  justify-content: space-between;
  margin-top: 16px;
`;
const NameTitle = styled.span`
  color: #125358;
  font-weight: bold;
`;

const Signature = styled.div`
  font-style: italic;
  color: #888;
  margin-top: 6px;
`;

const CommentButton = styled(Button)`
  background-color: #125358;
`;

const PageButtonContainer = styled.div`
  display: flex;
  justify-content: space-between;
  margin-top: 20px;
`;

const PageButton = styled(Button)`
  font-size: 14px;
`;

const PageInfo = styled.div`
  margin-top: 10px;
  text-align: center;
  font-size: 14px;
  color: #666;
`;

const PageContainer = styled.div`
  margin-top: 20px;
`;

const ActionButton = styled.button`
  background-color: ${({ $variant }) =>
    $variant === 'delete' ? '#ff4d4f' : '#1890ff'};
  color: white;
  border: none;
  border-radius: 4px;
  padding: 4px 8px;
  margin-left: 4px;
  cursor: pointer;
  font-size: 0.9rem;

  &:hover {
    opacity: 0.9;
  }
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
  const COMMENTS_PER_PAGE = 16;
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


  const totalPages = Math.ceil(commentList.length / COMMENTS_PER_PAGE);
  const startIdx = (currentPage - 1) * COMMENTS_PER_PAGE;
  const filteredComments = commentList.filter(comment => {
    const commentDate = new Date(comment.timestamp).toLocaleDateString('sv-SE');
    const selectedDateString = selectedDate.toLocaleDateString('sv-SE');
    return commentDate === selectedDateString;
  });

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
              <div style={{ display: 'flex', gap: '10px', marginTop: '6px' }}>
                <CommentButton onClick={handleSaveEdit}>{t('Save')}</CommentButton>
                <Button onClick={handleCancelEdit}>{t('Cancel')}</Button>
              </div>
            ) : (
              <CommentButton onClick={handleCommentSubmit}>{t('Submit Comment')}</CommentButton>
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

        <PageButtonContainer>
          <PageButton
            onClick={() => setCurrentPage((prev) => prev - 1)}
            disabled={currentPage === 1}
          >
            <FaArrowLeft />
            {t('Previous')}
          </PageButton>
          <PageButton
            onClick={() => setCurrentPage((prev) => prev + 1)}
            disabled={currentPage === totalPages}
          >
            {t('Next')}
            <FaArrowRight />
          </PageButton>
        </PageButtonContainer>

        <CloseButton onClick={onClose} aria-label="Close modal">
          <FaTimes />
        </CloseButton>


        <PageInfo>
          {t('Page Of', { current: currentPage, total: totalPages })}
        </PageInfo>
      </ModalContainer>
    </Overlay>
  );
};

export default CommentModal;
