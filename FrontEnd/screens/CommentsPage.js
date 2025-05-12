import React, { useState, useEffect } from 'react';
import { useParams, Link } from 'react-router-dom';
import axios from 'axios';
import styled, { createGlobalStyle } from 'styled-components';
import logo1 from '../src/media/logo1.png';
import { FaArrowLeft, FaArrowRight } from 'react-icons/fa';
import patient1 from '../src/media/patient1.jpg';

const GlobalStyle = createGlobalStyle`
  body {
    background: linear-gradient(135deg, #e0f7f9, #cceae7, #b2dfdb);
    background-attachment: fixed;
    margin: 0;
    font-family: 'Times New Roman', cursive, sans-serif;
  }
`;

const PageContainer = styled.div`
  background-color: rgba(255, 255, 255, 0.95);
  padding: 40px;
  border-radius: 8px;
  max-width: 700px;
  margin: 80px auto;
  height: 80vh;
  overflow-y: auto;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
`;

const Title = styled.h1`
  text-align: center;
  color: #125358;
  margin-bottom: 20px;
`;

const CommentBox = styled.textarea`
  width: 100%;
  padding: 12px;
  font-size: 16px;
  margin-bottom: 12px;
  border-radius: 8px;
  border: 1px solid #ccc;
  resize: none;
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
`;

const CommentItem = styled.div`
  background: #fff;
  padding: 10px;
  margin-bottom: 10px;
  border-left: 4px solid #125358;
  border-radius: 4px;
  position: relative;

  &:hover {
    cursor: pointer;
  }

  &:hover .delete-btn {
    display: block;
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

const CommentsPage = () => {
  const { patientId } = useParams();
  const [comments, setComments] = useState('');
  const [commentList, setCommentList] = useState([]);
  const [patientData, setPatientData] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const COMMENTS_PER_PAGE = 16;
  const [currentPage, setCurrentPage] = useState(1);
  const [editMode, setEditMode] = useState(false);
  const [editId, setEditId] = useState(null);

  useEffect(() => {
    // Hämta kommentarer från localStorage och sätt timestamp om de saknas
    const savedComments = JSON.parse(localStorage.getItem('comments')) || [];
    const commentsWithTimestamp = savedComments.map(comment => {
      if (!comment.timestamp) {
        return {
          ...comment,
          timestamp: new Date().toLocaleString('sv-SE', {
            dateStyle: 'short',
            timeStyle: 'short'
          }),
        };
      }
      return comment;
    });
    setCommentList(commentsWithTimestamp);
  }, []);

  useEffect(() => {
    const fetchPatientData = async () => {
      try {
        const response = await axios.get(`/api/patient/${patientId}`);
        setPatientData(response.data);
        setLoading(false);
      } catch (err) {
        setError("Kunde inte hämta patientdata.");
        setLoading(false);
      }
    };
    fetchPatientData();
  }, [patientId]);

  const handleCommentSubmit = () => {
    if (comments.trim() !== '') {
      const newComment = {
        id: Date.now(),
        text: comments.trim(),
        timestamp: new Date().toLocaleString('sv-SE', {
          dateStyle: 'short',
          timeStyle: 'short'
        }),
      };

      const updatedComments = [...commentList, newComment];
      setCommentList(updatedComments);
      localStorage.setItem('comments', JSON.stringify(updatedComments));
      setComments('');
    }
  };

  const handleDelete = (id) => {
    const updatedComments = commentList.filter(comment => comment.id !== id);
    setCommentList(updatedComments);
    localStorage.setItem('comments', JSON.stringify(updatedComments));
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
        comment.id === editId ? { ...comment, text: comments.trim(), timestamp: new Date().toLocaleString('sv-SE', { dateStyle: 'short', timeStyle: 'short' }) } : comment
      );
      setCommentList(updatedComments);
      localStorage.setItem('comments', JSON.stringify(updatedComments));
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
  const currentComments = commentList.slice(startIdx, startIdx + COMMENTS_PER_PAGE);

  const handleNext = () => {
    if (currentPage < totalPages) setCurrentPage(currentPage + 1);
  };

  const handlePrev = () => {
    if (currentPage > 1) setCurrentPage(currentPage - 1);
  };

  return (
    <>
      <GlobalStyle />
      <Link to="/" style={{ position: 'fixed', top: '15px', right: '15px', zIndex: 2 }}>
        <img src={logo1} alt="Logo" style={{ width: '150px' }} />
      </Link>
      <PageContainer>
        {/* Patientbild */}
        <div style={{ textAlign: 'center', marginBottom: '20px' }}>
          <img
            src={patient1}
            alt="Patient"
            style={{
              width: '150px',
              height: '150px',
              borderRadius: '50%',
              objectFit: 'cover',
            }}
          />
        </div>

        {/* Titel */}
        <Title>Kommentarer för patient {patientId}</Title>

        {loading && <p>Laddar patientinformation...</p>}
        {error && <p>{error}</p>}

        {patientData && (
          <div>
            <p><strong>Namn:</strong> {patientData.name}</p>
            <p><strong>Ålder:</strong> {patientData.age}</p>
          </div>
        )}

        <CommentBox
          rows={3}
          value={comments}
          onChange={(e) => setComments(e.target.value)}
          placeholder="Skriv en kommentar..."
        />
        {editMode ? (
          <>
            <CommentButton onClick={handleSaveEdit}>Spara ändring</CommentButton>
            <CommentButton onClick={handleCancelEdit} style={{ backgroundColor: '#ff4d4d', marginTop: '8px' }}>Avbryt</CommentButton>
          </>
        ) : (
          <CommentButton onClick={handleCommentSubmit}>Lägg till kommentar</CommentButton>
        )}

        <h2 style={{ marginTop: '30px', color: '#125358' }}>Alla kommentarer</h2>
        <CommentList>
          {currentComments.length > 0 ? (
            currentComments.map((comment, index) => (
              <React.Fragment key={comment.id}>
                <CommentItem>
                  <div>{comment.text}</div>
                  <small
                    style={{
                      color: '#3b82f6',
                      fontSize: '12px',
                      display: 'block',
                      marginTop: '5px',
                      position: 'relative',
                      zIndex: 10,
                    }}
                  >
                    {comment.timestamp}
                  </small>
                  <DeleteButton
                    className="delete-btn"
                    onClick={() => handleDelete(comment.id)}
                  >
                    X
                  </DeleteButton>
                  <button
                    className="edit-btn"
                    onClick={() => handleEdit(comment.id)}
                    style={{
                      position: 'absolute',
                      top: '10px',
                      right: '40px',
                      background: '#3b82f6',
                      color: 'white',
                      border: 'none',
                      borderRadius: '50%',
                      width: '24px',
                      height: '24px',
                      textAlign: 'center',
                      lineHeight: '22px',
                      cursor: 'pointer',
                      fontWeight: 'bold',
                      fontSize: '14px',
                    }}
                  >
                    ✎
                  </button>
                </CommentItem>

                {(index + 1) % 4 === 0 && index !== currentComments.length - 1 && (
                  <div
                    style={{
                      borderTop: '1px solid #3b82f6',
                      margin: '16px 0',
                      opacity: 0.4,
                    }}
                  />
                )}
              </React.Fragment>
            ))
          ) : (
            <p>Inga kommentarer ännu.</p>
          )}
        </CommentList>

        {/* Sidnavigering */}
        {totalPages > 1 && (
          <div style={{ display: 'flex', justifyContent: 'space-between', marginTop: '16px' }}>
            <PageButton onClick={handlePrev} disabled={currentPage === 1}>
              <FaArrowLeft /> Föregående
            </PageButton>
            <span>Sida {currentPage} av {totalPages}</span>
            <PageButton onClick={handleNext} disabled={currentPage === totalPages}>
              Nästa <FaArrowRight />
            </PageButton>
          </div>
        )}
      </PageContainer>
    </>
  );
};

export default CommentsPage;
