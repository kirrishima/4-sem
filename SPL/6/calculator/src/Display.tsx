import React from 'react';

type DisplayProps = {
  input: string;
  result: string;
};

export default function Display({ input, result }: DisplayProps) {
  return (
    <div className="display">
      <div className="display-input">{input || '0'}</div>
      <div className="display-result">{result}</div>
    </div>
  );
}
