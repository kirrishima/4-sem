import React from 'react';

type HistoryProps = {
  history: string[];
};

export default function History({ history }: HistoryProps) {
  return (
    <div className="history">
      <h3>История вычислений</h3>
      <ul>
        {history.map((item, index) => (
          <li key={index}>{item}</li>
        ))}
      </ul>
    </div>
  );
}
