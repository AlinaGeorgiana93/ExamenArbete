import React, { useState } from 'react';
import EmojiPicker from 'emoji-picker-react';

function EmojiPickerWrapper(props) {
  const [showPicker, setShowPicker] = useState(false);

  // onEmojiClick is inside the component, so has access to props
  const onEmojiClick = (emojiObject, event) => {
    if (props.onSelect) {
      props.onSelect(emojiObject.emoji);
    }
    setShowPicker(false);
  };

  return (
    <div style={{ position: 'relative', display: 'inline-block', width: '100%' }}>
      <input
        type="text"
        name={props.name}
        value={props.value}
        onChange={props.onChange}
        autoComplete="off"
        style={{ width: '100%', paddingRight: '32px' }}
      />
      <button
        type="button"
        onClick={() => setShowPicker(!showPicker)}
        style={{
          position: 'absolute',
          right: '8px',
          top: '50%',
          transform: 'translateY(-50%)',
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
            top: '100%',
            right: 0,
            width: '280px',
            zIndex: 1000,
            boxShadow: '0 4px 8px rgba(0,0,0,0.2)',
            borderRadius: '8px',
            backgroundColor: '#fff',
          }}
        >
          <button
            type="button"
            aria-label="Close emoji picker"
            onClick={() => setShowPicker(false)}
            style={{
              position: 'absolute',
              top: '8px',
              right: '8px',
              background: 'rgba(255,255,255,0.8)',
              border: 'none',
              fontSize: '18px',
              cursor: 'pointer',
              lineHeight: 1,
              borderRadius: '50%',
              width: '24px',
              height: '24px',
              display: 'flex',
              alignItems: 'center',
              justifyContent: 'center',
              zIndex: 1100,
              boxShadow: '0 0 5px rgba(0,0,0,0.1)',
            }}
          >
            ×
          </button>
          <EmojiPicker
            onEmojiClick={onEmojiClick}
            searchDisabled={true}
            skinTonePickerEnabled={false}
            groupNames={{ smileys_people: 'Smileys & People' }}
            categories={['smileys_people']}
          />
        </div>
      )}
    </div>
  );
}

export default EmojiPickerWrapper;
