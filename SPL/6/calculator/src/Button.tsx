import React from 'react';

type ButtonProps = {
  value: string;
  onClick: (value: string) => void;
};

export default function Button({ value, onClick }: ButtonProps) {
  return (
    <button
      className="calc-button"
      onMouseDown={(e) => e.preventDefault()}  // Предотвращаем получение фокуса при клике
      onClick={() => onClick(value)}
    >
      {value}
    </button>
  );
}
