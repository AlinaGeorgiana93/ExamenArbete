import React, { useState, useEffect, useRef } from 'react';
import { useParams, Link } from 'react-router-dom';
import axios from 'axios';
import styled, { createGlobalStyle } from 'styled-components';
import logo1 from '../src/media/logo1.png';

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
  background-color: #3b82f6;
  color: white;
  padding: 10px 18px;
  border: none;
  border-radius: 8px;
  font-size: 16px;
  font-weight: 500;
  cursor: pointer;
  transition: background-color 0.2s ease;

  &:hover {
    background-color: #2563eb;
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
  border-left: 4px solid #3b82f6;
  border-radius: 4px;
`;

const CommentsPage = () => {
  const { patientId } = useParams();
  const [comments, setComments] = useState('');
  const [commentList, setCommentList] = useState([]);
  const [patientData, setPatientData] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  // Hämta från localStorage
  useEffect(() => {
    const savedComments = JSON.parse(localStorage.getItem('comments')) || [];
    setCommentList(savedComments);
  }, []);

  // Hämta patientdata
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
      };
      const updatedComments = [...commentList, newComment];
      setCommentList(updatedComments);
      localStorage.setItem('comments', JSON.stringify(updatedComments));
      setComments('');
    }
  };

  return (
    <>
      <GlobalStyle />
      <Link to="/" style={{ position: 'fixed', top: '15px', right: '15px', zIndex: 2 }}>
        <img src={logo1} alt="Logo" style={{ width: '150px' }} />
      </Link>
      <PageContainer>
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
        <CommentButton onClick={handleCommentSubmit}>Lägg till kommentar</CommentButton>

        <h2 style={{ marginTop: '30px', color: '#125358' }}>Alla kommentarer</h2>
        <CommentList>
          {commentList.length > 0 ? (
            commentList.map((comment) => (
              <CommentItem key={comment.id}>{comment.text}</CommentItem>
            ))
          ) : (
            <p>Inga kommentarer ännu.</p>
          )}
        </CommentList>
      </PageContainer>
    </>
  );
};

export default CommentsPage;
