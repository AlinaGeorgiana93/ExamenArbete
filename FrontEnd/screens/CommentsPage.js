import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import axios from 'axios';
import styled, { createGlobalStyle } from 'styled-components';
import { FaArrowLeft, FaArrowRight } from 'react-icons/fa';
import patient1 from '../src/media/patient1.jpg';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';
import { Link } from 'react-router-dom';
import { useLocation, useNavigate } from 'react-router-dom';
import '../language/i18n.js';
import { useTranslation } from 'react-i18next';


const GlobalStyle = createGlobalStyle``;

const PageContainer = styled.div`
  background-color: rgba(255, 255, 255, 0.95);
  padding: 40px;
  border-radius: 8px;
  max-width: 700px;
  margin: 80px auto;
  height: 80vh;
  overflow-y: auto;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
  position: relative;
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
  margin-top: 12px;
  margin-bottom: 12px;
  border-radius: 8px;
  border: 1px solid #ccc;
  resize: vertical;
`;

const CommentButton = styled.button`
  background-color: #125358;
  color: white;
  padding: 10px 18px;
  border: none;
  border-radius: 8px;
  font-size: 16px;
  font-weight: 500;
  cursor: pointer;
  transition: background-color 0.2s ease;

  &:hover {
    background-color: #125358;
  }
`;

const CommentList = styled.div`
  margin-top: 24px;
  background: #f9f9f9;
  padding: 16px;
  border-radius: 8px;
  display: flex;
  flex-direction: column;
  gap: 24px;
`;

const CommentItem = styled.div`
  background: #fff;
  padding: 16px;
  border-left: 4px solid #125358;
  border-radius: 6px;
  position: relative;
  box-shadow: 0 2px 5px rgba(0,0,0,0.05);
  transition: box-shadow 0.2s ease;

  &:hover {
    cursor: pointer;
    box-shadow: 0 4px 10px rgba(0,0,0,0.1);
  }

  &::after {
    content: '';
    display: block;
    height: 1px;
    background: #ddd;
    margin-top: 20px;
  }

  &:last-child::after {
    display: none;
  }
`;

const DeleteButton = styled.button`
  position: absolute;
  top: 10px;
  right: 10px;
  background-color: #ff4d4d;
  color: white;
  border: none;
  border-radius: 50%;
  width: 24px;
  height: 24px;
  display: none;
  font-size: 14px;
  font-weight: bold;
  text-align: center;
  line-height: 22px;
  cursor: pointer;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.2);
  transition: all 0.3s ease;

  &:hover {
    background-color: #ff1a1a;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.3);
    transform: scale(1.1);
  }

  ${CommentItem}:hover & {
    display: block;
  }
`;

const PageButtonContainer = styled.div`
  display: flex;
  justify-content: space-between;
  margin-top: 20px;
`;

const PageButton = styled.button`
  background-color: #125358;
  color: white;
  border: none;
  border-radius: 6px;
  padding: 8px 12px;
  cursor: pointer;
  font-size: 14px;
  display: flex;
  align-items: center;
  gap: 4px;

  &:disabled {
    background-color: #a5b4fc;
    cursor: not-allowed;
  }
`;

const PageInfo = styled.p`
  text-align: center;
  color: #125358;
  margin-top: 10px;
  font-size: 14px;
`;

const NameTitle = styled.h1`
  background-color: #e0f7f9;
  padding: 5px 12px;
  border-radius: 5px;
  font-size: 35px;
  font-weight: 700;
  text-align: center;
  display: inline-block;
  margin: 10px 0;
`;

const Signature = styled.div`
  position: absolute;
  bottom: 20px;
  right: 12px;
  font-size: 14px;
  font-weight: bold;
  color: #125358;
`;



const CommentsPage = () => {
  const { patientId } = useParams();
  const { t } = useTranslation();
  const [comments, setComments] = useState('');
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




  const isToday = (date) => {
    const today = new Date();
    return (
      date.getDate() === today.getDate() &&
      date.getMonth() === today.getMonth() &&
      date.getFullYear() === today.getFullYear()
    );
  };

  useEffect(() => {
    const savedComments = JSON.parse(localStorage.getItem(`comments_${patientId}`)) || [];
    setCommentList(savedComments);
  }, [patientId]);

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
    <>
      <GlobalStyle />

      {fromPage === 'graph' && (
        <Link
          to={`/graph/${patientId}`}
          style={{
            display: 'inline-block',
            backgroundColor: '#125358',
            color: 'white',
            padding: '10px 16px',
            borderRadius: '8px',
            textDecoration: 'none',
            marginBottom: '20px',
            marginTop: '10px',
            fontWeight: '500',
            position: 'absolute',
            top: '25px',
            left: '15px',
            zIndex: 2
          }}
        >
          {t('back To Graph')}
        </Link>
      )}

      <PageContainer>
        {fromPage === 'patient' && (
          <Link
            to={`/patient/${patientId}`}
            style={{
              position: 'absolute',
              top: '20px',
              right: '20px',
              backgroundColor: '#125358',
              color: 'white',
              padding: '10px 16px',
              borderRadius: '8px',
              textDecoration: 'none',
              fontWeight: '500',
              zIndex: 2
            }}
          >
            {t('Back To Patient')}
          </Link>
        )}
        <img
          src={patient1}
          alt="Patient Image"
          style={{ maxWidth: '200px', margin: 'auto', display: 'block', marginBottom: '20px' }}
        />
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

        {isToday(selectedDate) && (
          <>
            <CommentBox
              value={comments}
              onChange={(e) => setComments(e.target.value)}
              placeholder={t('Write Your Comment')}
            />

            <input
              type="text"
              placeholder={t('initials')}
              value={signature}
              onChange={(e) => setSignature(e.target.value)}
              style={{
                width: '200px',
                padding: '6px 10px',
                fontSize: '14px',
                borderRadius: '6px',
                border: '1px solid #ccc',
                marginTop: '10px',
                marginBottom: '10px'
              }}
            />

            {editMode ? (
              <div style={{ display: 'flex', gap: '10px', marginTop: '6px' }}>
                <CommentButton onClick={handleSaveEdit}>{t('save Changes')}</CommentButton>
                <CommentButton onClick={handleCancelEdit}>{t('cancel')}</CommentButton>
              </div>
            ) : (
              <div style={{ marginTop: '6px' }}>
                <CommentButton onClick={handleCommentSubmit}>{t('Submit Comment')}</CommentButton>
              </div>
            )}
          </>
        )}

        <CommentList>
          {paginatedComments.map((comment) => (
            <CommentItem key={comment.id}>
              <p>{comment.text}</p>
              <p>{comment.timestamp}</p>
              <DeleteButton onClick={() => handleDelete(comment.id)}>X</DeleteButton>
              <button onClick={() => handleEdit(comment.id)}>{t('Edit')}</button>
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

        <PageInfo>
          {t('Page Of', { current: currentPage, total: totalPages })}
        </PageInfo>
      </PageContainer>
    </>
  );
};

export default CommentsPage;
