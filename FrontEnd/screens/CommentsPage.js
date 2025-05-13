import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import axios from 'axios';
import styled, { createGlobalStyle } from 'styled-components';
import logo1 from '../src/media/logo1.png';
import { FaArrowLeft, FaArrowRight } from 'react-icons/fa';
import patient1 from '../src/media/patient1.jpg'; 
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';
import { Link } from 'react-router-dom';



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
  height: 140px; /* ⬅️ Lägg till eller ändra höjden här */
  padding: 14px;
  font-size: 16px;
  margin-top: 12px;
  margin-bottom: 12px;
  border-radius: 8px;
  border: 1px solid #ccc;
  resize: vertical; /* ⬅️ Tillåt att dra rutan manuellt om du vill */
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
  
  const isToday = (date) => {
    const today = new Date();
    return (
      date.getDate() === today.getDate() &&
      date.getMonth() === today.getMonth() &&
      date.getFullYear() === today.getFullYear()
    );
  };

  useEffect(() => {
    const savedComments = JSON.parse(localStorage.getItem('comments')) || [];
    setCommentList(savedComments);
  }, []);

  useEffect(() => {
    const fetchPatientData = async () => {
      try {
        const response = await axios.get(`https://localhost:7066/api/Patient/ReadItem?id=${patientId}`);
        setPatientData(response.data.item);
        setLoading(false);
      } catch (err) {
        setError("Kunde inte hämta patientdata.");
        setLoading(false);
      }
    };
    fetchPatientData();
  }, [patientId]);

  const handleCommentSubmit = () => {
    if (comments.trim() === '') {
      alert("Kommentar kan inte vara tom.");
      return;
    }
  
    if (signature.trim() === '') {
      alert("Vänligen fyll i dina initialer.");
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
    localStorage.setItem('comments', JSON.stringify(updatedComments));
    setComments('');
    setSignature(''); 
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
      <Link
  to="/"
  style={{ position: 'fixed', top: '15px', right: '15px', zIndex: '2' }}
>
  <img src={logo1} alt="Logo" style={{ width: '140px' }} />
</Link>

      <PageContainer>
        <img 
          src={patient1} 
          alt="Patient Bild" 
          style={{ maxWidth: '200px', margin: 'auto', display: 'block', marginBottom: '20px' }} 
        />
        <Title>
          Kommentarer för {patientData ? 
            <NameTitle>{patientData.firstName} {patientData.lastName}</NameTitle> : 
            (error || 'Ingen patient hittades')}
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
      placeholder="Skriv din kommentar här..."
    />

    <input 
      type="text" 
      placeholder="Signatur" 
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
        <CommentButton onClick={handleSaveEdit}>Spara ändring</CommentButton>
        <CommentButton onClick={handleCancelEdit}>Avbryt</CommentButton>
      </div>
    ) : (
      <div style={{ marginTop: '6px' }}>
        <CommentButton onClick={handleCommentSubmit}>Skicka kommentar</CommentButton>
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
              <button onClick={() => handleEdit(comment.id)}>Redigera</button>
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
            Föregående
          </PageButton>
          <PageButton
            onClick={() => setCurrentPage((prev) => prev + 1)}
            disabled={currentPage === totalPages}
          >
            Nästa
            <FaArrowRight />
          </PageButton>
        </PageButtonContainer>

        <PageInfo>
          Sida {currentPage} av {totalPages}
        </PageInfo>
      </PageContainer>
    </>
  );
};

export default CommentsPage;