import React, { useState, useEffect } from 'react';

const CommentsPage = () => {
  const [comments, setComments] = useState('');
  const [commentList, setCommentList] = useState([]);

  // Hämta tidigare sparade kommentarer från localStorage
  useEffect(() => {
    const savedComments = JSON.parse(localStorage.getItem('comments')) || [];
    setCommentList(savedComments);
  }, []);

  const handleCommentSubmit = () => {
    if (comments.trim() !== '') {
      const newComment = {
        id: Date.now(),
        text: comments.trim(),
      };

      const updatedComments = [...commentList, newComment];
      setCommentList(updatedComments);
      localStorage.setItem('comments', JSON.stringify(updatedComments));
      setComments(''); // Töm textfältet efter submit
    }
  };

  return (
    <div>
      <h1>Kommentarer</h1>
      <textarea
        value={comments}
        onChange={(e) => setComments(e.target.value)}
        placeholder="Skriv en kommentar..."
      />
      <button onClick={handleCommentSubmit}>Lägg till kommentar</button>

      <h2>Alla kommentarer</h2>
      <div>
        {commentList.length > 0 ? (
          commentList.map((comment) => (
            <div key={comment.id}>
              <p>{comment.text}</p>
            </div>
          ))
        ) : (
          <p>Inga kommentarer ännu.</p>
        )}
      </div>
    </div>
  );
};

export default CommentsPage;
