import { useState } from 'react';

function CustomDropdown({ options, onSelect, placeholder }) {
  const [isOpen, setIsOpen] = useState(false);
  const [selectedOption, setSelectedOption] = useState(null);

  const handleSelect = (option) => {
    setSelectedOption(option);
    onSelect(option);
    setIsOpen(false);
  };

  return (
    <div style={{ position: 'relative', width: '300px' }}>
      <div
        style={{
          border: '1px solid #ccc',
          padding: '10px',
          borderRadius: '5px',
          cursor: 'pointer',
          backgroundColor: '#fff',
        }}
        onClick={() => setIsOpen(!isOpen)}
      >
        {selectedOption ? `${selectedOption.firstName} ${selectedOption.lastName}` : placeholder}
      </div>

      {isOpen && (
        <div
          style={{
            position: 'absolute',
            top: '100%',
            left: 0,
            right: 0,
            maxHeight: '200px',
            overflowY: 'auto',
            border: '1px solid #ccc',
            borderTop: 'none',
            backgroundColor: '#fff',
            zIndex: 1000,
          }}
        >
          {options.map((option) => (
            <div
              key={option.staffId || option.patientId}
              style={{
                padding: '10px',
                cursor: 'pointer',
                borderBottom: '1px solid #eee',
              }}
              onClick={() => handleSelect(option)}
            >
              {option.firstName} {option.lastName}
            </div>
          ))}
        </div>
      )}
    </div>
  );
}
