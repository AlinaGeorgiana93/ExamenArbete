import React, { useState, useEffect } from 'react';
import EmojiPicker from 'emoji-picker-react';

function EmojiPickerWrapper({ onSelect }) {
  const [showPicker, setShowPicker] = useState(false);

  const onEmojiClick = (emojiData) => {
    onSelect?.(emojiData.emoji);
    setShowPicker(false);
  };

  useEffect(() => {
  const style = document.createElement('style');
  style.innerHTML = `
    .epr-body {
      max-height: 250px !important;
      overflow-y: auto !important;
    }

    /* Remove this block to show scrollbar */
    /* .epr-body::-webkit-scrollbar {
      display: none;
    } */

    .epr-category-nav {
      padding: 4px !important;
      justify-content: center !important;
      gap: 6px !important;
    }

    .epr-search, .epr-preview {
      display: none !important;
    }
  `;
  document.head.appendChild(style);
  return () => document.head.removeChild(style);
}, []);


  return (
    <div style={{ position: 'absolute', right: '8px', top: '50%', transform: 'translateY(-50%)' }}>
      <button
        type="button"
        onClick={() => setShowPicker(prev => !prev)}
        title="Choose your emoji"
        style={{
          background: 'transparent',
          border: 'none',
          cursor: 'pointer',
          fontSize: '20px',
          padding: 0,
        }}
        aria-label="Toggle emoji picker"
      >
        😊
      </button>

      {showPicker && (
        <div
          style={{
            position: 'absolute',
            top: '0',
            right: '40px',
            zIndex: 1000,
            borderRadius: '10px',
            boxShadow: '0 4px 10px rgba(0,0,0,0.15)',
            backgroundColor: 'white',
            padding: '8px',
          }}
        >
         <button
      type="button"
      onClick={() => setShowPicker(false)}
      aria-label="Close emoji picker"
      style={{
        position: 'absolute',
        top: '6px',
        right: '6px',
        background: '#f0f0f0',
        border: 'none',
        fontSize: '18px',
        cursor: 'pointer',
        borderRadius: '50%',
        width: '28px',
        height: '28px',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        boxShadow: '0 1px 2px rgba(0,0,0,0.15)',
        zIndex: 1100, // <- make sure it's on top
      }}
    >
      ×
    </button>


          <EmojiPicker
            onEmojiClick={onEmojiClick}
            emojiStyle="native"
            searchDisabled
            skinTonesDisabled
            previewConfig={{ showPreview: false }}
            height={240}
            width={280}
            categories={['smileys_people', 'activities']}
          />
        </div>
      )}
    </div>
  );
}

export default EmojiPickerWrapper;
